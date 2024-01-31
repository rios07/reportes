using Infraestructure.track3.Models.hojaruta;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using track3_api_reportes_core.Aplicacion.Logs;
using track3_api_reportes_core.Aplicacion.Requests;
using track3_api_reportes_core.Controllers.Result;
using track3_api_reportes_core.Infraestructura.Dao;
using track3_api_reportes_core.Infraestructura.Interfaces;
using track3_api_reportes_core.Infraestructura.SecureWay.Models;
using track3_api_reportes_core.Infraestructura.track3.Models.Servicios.Posicion;
using track3_api_reportes_core.Middleware.Dto;
using track3_api_reportes_core.Middleware.Interfaces;
using track3_api_reportes_core.Middleware.Modelos.Track;
using Exception = System.Exception;


namespace track3_api_reportes_core.Middleware.secureway
{
    public class SecureWayMiddleware:ISecureWayMiddleware
    {
        private readonly ISecurewayRepository _secureway;
        private readonly IMappingFormularios _mapping;
        private readonly IHojaRutaRepository _hojaRutaRepository;
        public static IConfiguration _configuration;
        private string conex;
        private string conexHist;
        private Logs sb;
        public SecureWayMiddleware(IWebHostEnvironment environment,ISecurewayRepository secureway, IMappingFormularios mapping,  IConfiguration configuration, IHojaRutaRepository hojaRutaRepository)
        {
            _secureway = secureway;
            _configuration = configuration;
            _hojaRutaRepository = hojaRutaRepository;
            conex = Infraestructure.Infraestructura.GetConnectionStringByName("Secureway");
            conexHist = Infraestructure.Infraestructura.GetConnectionStringByName("SecurewayHist");
            _mapping = mapping;
            sb = new Logs(environment);
            sb.lineas = new List<string>();
            sb.Proceso = "CalculoKM";
        }


        public async Task<List<Target>> getTargets(string? movilPatente, List<GpsHojaRutaElemento> elementosviaje, List<Elemento> elementos)
        {
            List<Target> targets = new List<Target>();
            foreach (var x in elementosviaje)
            {
                DataTable dt = _secureway.getTargets("",x.Propiedad ,conex);
                targets.Add(_mapping.mapTargets(dt));
            }

            return targets;
        }

        public Target getTarget(string? movilPatente, string Propiedad)
        {
            Target target = new Target();
            DataTable dt = _secureway.getTargets("", Propiedad, conex);

            if(dt.Rows.Count > 0)
               return _mapping.mapTarget(dt.Rows[0]);
     
            return target;
        }

        public List<Elemento> getPosiciones(string? movilPatente, List<GpsHojaRutaElemento> hojaRutaListaElementosViaje, DateTime desde, DateTime hasta,  List<Elemento> elementos)
        {
            GpsPosicion pos;

            foreach (var ele in elementos)
            {
                DataTable dt;
                List<GpsPosicion> lista = new List<GpsPosicion>();
                if (ele.nombre == movilPatente)
                {
                     dt = _secureway.getTargets(movilPatente, "", conex);
                }
                else
                {
                     dt = _secureway.getTargets("", ele.nombre, conex);
                }
                
                Target target = null;
                target =_mapping.mapTargets(dt);
                DateTime actual = desde;
                if (target != null)
                {
                    //OBTENER LAS POSICIONES
                    dt = _secureway.getPosiciones(desde,hasta ,target.TargetId ,conex);
                    List<GpsPosicion> posiciones = _mapping.mapPosiciones(dt);      
                    //POSICIONES
                     GeoCoordinate.NetStandard2.GeoCoordinate origen = new GeoCoordinate.NetStandard2.GeoCoordinate(1, 1);
                     GeoCoordinate.NetStandard2.GeoCoordinate destino;
                    foreach (GpsPosicion itm in posiciones)
                    {
                        destino = new GeoCoordinate.NetStandard2.GeoCoordinate((double)itm.GpsLatitude, (double)itm.GpsLongitude);

                        if (destino.GetDistanceTo(origen) > 1 && itm.FechaPosicion >= actual)
                        {
                            actual = itm.FechaPosicion.AddSeconds(1);
                            lista.Add(itm);
                            origen = destino;
                        }
                    }
                }

                if (target != null && desde < DateTime.Now.AddDays(-7))
                {
                     
                    dt = _secureway.getPosicionesHistoricas(desde, hasta, target.TargetId, conexHist);
                    List<GpsPosicion> posiciones = _mapping.mapPosiciones(dt);
                    //OBTENER LAS POSICIONES
                    //POSICIONES
                    GeoCoordinate.NetStandard2.GeoCoordinate origen = new GeoCoordinate.NetStandard2.GeoCoordinate(1, 1);
                    GeoCoordinate.NetStandard2.GeoCoordinate destino;
                    foreach (GpsPosicion itm in posiciones)
                    {
                        destino = new GeoCoordinate.NetStandard2.GeoCoordinate((double)itm.GpsLatitude, (double)itm.GpsLongitude);

                        if (destino.GetDistanceTo(origen) > 1 && itm.FechaPosicion >= actual)
                        {
                            actual = itm.FechaPosicion.AddSeconds(1);
                            lista.Add(new GpsPosicion()
                            { 
                                FechaPosicion = itm.FechaPosicion,
                               
                                GpsLatitude = itm.GpsLatitude,
                                GpsLongitude = itm.GpsLongitude,
                                Id = itm.Id
                            });
                            origen = destino;
                        }
                    }
                }

                ele.listPosition = new List<Location>();
                if (lista.Count > 0)
                {
                    ele.listPosition = mapperPosition(lista);
                }
                 
            }
            return elementos;
        }

        public async Task<ErrorAplication> calcularKmHojaRuta(List<Elemento> elementos, GpsHojaRuta hojaRuta)
        {

            List<GpsHojaRutaElemento> lista = hojaRuta.listaElementosViaje.OrderBy(x => x.Stamp).ToList();
            GpsHojaRutaElemento elemento = lista.Where(x => x.Elemento.Tipo.Id == 1).FirstOrDefault();

            ////se realiza la resta entre la fech de cierre con la fecha que se tomo la hdr
            var timeSpan = hojaRuta.FechaCierre.Value - elemento.Stamp;
            long tiempoReal = Convert.ToInt64(Math.Floor(timeSpan.TotalSeconds));

            List<Location> p = new List<Location>();

            string e = "";
            //se arma el listado de posiciones de todos los dipositivos asociados a la hoja de ruta
            foreach (Elemento item in elementos)
            {
                e = e + item.nombre + ", ";
                p.AddRange(item.listPosition);
            }

            sb.lineas.Add($"listado de elementos= " + e);
            sb.GrabarLogs();

            //se ordena el listado de posiciones
            List<Location> listaOrdenada = p.OrderBy(x => x.Stamp).ToList();

            List<GpsPosicion> posiciones = new List<GpsPosicion>();

            //se mapea el listado
            foreach (Location item in listaOrdenada)
            {
                GpsPosicion pos = new GpsPosicion() { GpsLatitude = (decimal)item.Lat, GpsLongitude =(decimal) item.Lng };
                posiciones.Add(pos);
            }

            sb.lineas.Add($"cantidad de posiciones= " + posiciones.Count);
            sb.GrabarLogs();

            //Se realiza calculo de distancia
            long distanciaTotal = CalculoDistanciaEnMetros(posiciones);

            using OracleDao dao = new OracleDao(_configuration.GetConnectionString("Track3"));
            string sentencia = await _hojaRutaRepository.updateDistanciaYTiempoTotal(hojaRuta.Id, distanciaTotal,  tiempoReal,dao);
            if (sentencia != "Ok")
            {
                sb.lineas.Add("Se recibe error al intentar actualizar: "+sentencia);
                sb.GrabarLogs();
                dao.RollBack();
                dao.Dispose();
                return new ErrorAplication("No se realizo la actulizacion de la hojaruta") { Codigo = 1 };
            }
            dao.Commit();
            dao.CerrarConexion();
            return new ErrorAplication("");

        }

        private long CalculoDistanciaEnMetros(List<GpsPosicion> posiciones)
        {
            double dist = 0;
            if (posiciones.Count < 1)
                return 0;

            //var anterior = posiciones[0];
            var anterior = new GeoCoordinate.NetStandard2.GeoCoordinate((double)posiciones[0].GpsLatitude, (double)posiciones[0].GpsLongitude);
            foreach (var item in posiciones)
            {
                //dist += anterior.GPSPosition.Distance(item.GPSPosition).Value;
                dist += anterior.GetDistanceTo(new GeoCoordinate.NetStandard2.GeoCoordinate((double)item.GpsLatitude, (double)item.GpsLongitude));
                anterior = new GeoCoordinate.NetStandard2.GeoCoordinate((double)item.GpsLatitude, (double)item.GpsLongitude);
            }

            return Convert.ToInt32(dist);
        }

        private List<Location> mapperPosition(List<GpsPosicion> lista)
        {
            List<Location> list = new List<Location>();

            lista.ForEach(p => list.Add(new Location()
            {
                Lat = double.Parse(p.GpsLatitude.ToString()),
                Lng = double.Parse(p.GpsLongitude.ToString()),
                Stamp = p.FechaPosicion
            }));

            return list;
        }
    }
}
