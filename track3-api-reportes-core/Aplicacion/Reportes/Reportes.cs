using Infraestructure.track3.Models.hojaruta;
using Infraestructure.track3.Models.planificacion;
using track3_api_reportes_core.Aplicacion.Filtros;
using track3_api_reportes_core.Aplicacion.Interfaces;
using track3_api_reportes_core.Infraestructura.SecureWay.Models;
using track3_api_reportes_core.Middleware.Dto;
using track3_api_reportes_core.Middleware.Interfaces;
using track3_api_reportes_core.Middleware.Modelos.movil;
using track3_api_reportes_core.Middleware.Modelos.Track;

namespace track3_api_reportes_core.Aplicacion.Reportes
{
    public class Reportes : IReportes
    {
        public readonly IReportesMiddleware _IReportesMiddleware;
        public readonly ISecureWayMiddleware _SecureWay;
        public readonly IServiciosMiddleware _Servicios;
        public static IConfiguration _configuration;

        public Reportes (IReportesMiddleware IReportesMiddleware, IServiciosMiddleware servicios, IConfiguration config, ISecureWayMiddleware secureWay)
        {
            _IReportesMiddleware = IReportesMiddleware;
            _Servicios = servicios;
            _configuration = config;
            _SecureWay = secureWay;         
        }

        public List<SpCdProdViaje> ReporteMovilesProveedor(FiltroReportes filtro)
        {
            return _IReportesMiddleware.ReporteMovilesProveedor(filtro.strFechaDesde, filtro.strFechaHasta);
        }

        public List<SpDigViajMovPed> ReporteViajesMovilesPedidos(Mobiliario filtro)
        {
          return _IReportesMiddleware.ReporteViajesMovilesPedidos(filtro.strFechaDesde,filtro.strFechaHasta, filtro.p_canal);
        }

        public List<SpObtenerPosiciones>ObtenerPosiciones(Dispositivo filtro)
        {
            return _IReportesMiddleware.ObtenerPosiciones(filtro.strFechaDesde, filtro.strFechaHasta, filtro.p_Id_Dispositivo, filtro.p_Identificador);
        }

        public List<SpObtenerPosiciones> ObtenerPosicionesSW(Dispositivo filtro)
        {
            return _IReportesMiddleware.ObtenerPosicionesSW(filtro.strFechaDesde, filtro.strFechaHasta, filtro.p_Id_Dispositivo, filtro.p_Identificador);
        }

        public List<SPGetKilometrosHojaRuta>GetKilometrosHojaRuta(Filtros.Canal Filtro)
        {
              return _IReportesMiddleware.GetKilometrosHojaRuta(Filtro.strFechaDesde, Filtro.strFechaHasta, Filtro.Sucursal);

        }
        
        public List<spGetMovilesEnViaje> getMovilesEnViaje(Mobiliario Filtro)
        {
            return _IReportesMiddleware.getMovilesEnViaje(Filtro.p_canal);
        }

        public List<CumplimientoEntrega> getCumplimientoEntrega(FiltroReportes filtro)
        {
            return _IReportesMiddleware.getCumplimientoEntrega(filtro.strFechaDesde, filtro.strFechaHasta);
        }

        public List<CumplimientoEntregaDetalle> getCumplimientoEntregaDet(FiltroReportes filtro)
        {
            return _IReportesMiddleware.getCumplimientoEntregaDet(filtro.strFechaDesde, filtro.strFechaHasta,filtro.nid);
        }

        public List<ReiteracionPedidos> getReiteracionPedidos(FiltroReportes filtro)
        {
            return _IReportesMiddleware.getReiteracionPedidos(filtro.strFechaDesde, filtro.strFechaHasta);
        }

        public List<HojaRutaEstados>getReporteEstados(FiltroEstadosHR filtro)
        {
            return _IReportesMiddleware.getReporteEstados(filtro.strFecha, filtro.nid);
        }

        public List<track3_api_reportes_core.Middleware.Dto.Canal>getAllCanal()
        {
            return _IReportesMiddleware.getAllCanal();
        }

        public List<ReiteracionPedidosSucDet> getReiteracionPedidosSucDet(FiltroReportes filtro)
        {
            return _IReportesMiddleware.getReiteracionPedidosSucDet(filtro.strFechaDesde, filtro.strFechaHasta);
        }

        public List<ReiteracionPedidosSucDet> getReiteracionPedidosSucDetx(FiltroReportes filtro)
        {
            return _IReportesMiddleware.getReiteracionPedidosSucDetx(filtro.nid,filtro.strFechaDesde, filtro.strFechaHasta);
        }

        public List<diarioPDT> get_DiarioPDT(FiltroPDT filtro)
        {
            // Crear una lista vacía para almacenar los objetos diarioPDT que se devolverán al final.
            List<diarioPDT> retorno = new List<Middleware.Dto.diarioPDT>();

            retorno = _IReportesMiddleware.get_DiarioPDT(filtro.strFecha, filtro.idCanal);


            // Devolver la lista de objetos diarioPDT con los registros diarios de trabajo y sus Hojas de Ruta asociadas.
            return retorno;
        }

        public async Task<List<Elemento>> getRecorrido(long IdHojaRuta)
        {
            var HojaRuta = await _Servicios.GetHojaRuta(IdHojaRuta, false);
            var elementos = await GetListElementosToGetRecorrido(IdHojaRuta, HojaRuta);
            return elementos;
        }

        public async Task<List<Elemento>> GetListElementosToGetRecorrido(long IdHojaRuta, GpsHojaRuta HojaRuta)
        {
            List<Elemento> elementos = new List<Elemento>();
            elementos = _IReportesMiddleware.getRecorrido(IdHojaRuta);

            elementos.Add(new Elemento
            {
                nombre = HojaRuta.HojaRutaMovil.Patente, id_elementoTipo = 100, listPosition = new List<Requests.Location>()
            });
            var camino = bool.Parse(Infraestructure.Infraestructura.getStringParametro("CAMINO_SECUREWAY"));
            if (camino)
            {
                if (HojaRuta.listaElementosViaje.Count > 0)
                {
                    List<Target> Targets = await _SecureWay.getTargets(HojaRuta.HojaRutaMovil.Patente,
                        HojaRuta.listaElementosViaje, elementos);
                    HojaRuta.listaElementosViaje.Where(x => x.Elemento.Nombre != "CANASTOS").ToList().ForEach(e =>
                    {
                        if (e.Dispositivo == null)
                        {
                            Targets.Where(x => x != null).ToList().ForEach(v =>
                            {
                                string tracker = v.TrackerId.ToUpper().Trim();
                                if (!elementos.Exists(x => x.nombre.ToUpper() == tracker))
                                {
                                    elementos.Add(new Elemento
                                        { nombre = v.TrackerId.ToUpper().Trim(), id_elementoTipo = e.Elemento.Id });
                                }
                            });
                        }
                        else
                        {
                            if (elementos.Exists(x => x.nombre.ToUpper() != e.Propiedad.ToUpper().Trim()))
                            {
                                elementos.Add(new Elemento
                                {
                                    id_dispositivo = e.Dispositivo.Id, nombre = e.Propiedad.ToUpper().Trim(),
                                    id_elementoTipo = e.Elemento.Id
                                });
                            }
                        }
                    });
                }

                var hasta = HojaRuta.FechaCierre != null
                    ? HojaRuta.FechaCierre.Value
                    : HojaRuta.FechaSalida.Value.AddHours(23.9);
                DateTime desde = (DateTime)HojaRuta.FechaSalida;

                var eleora = elementos.Where(x => x.listPosition != null && x.listPosition.Count > 0).ToList();
                elementos = _SecureWay.getPosiciones(HojaRuta.HojaRutaMovil.Patente, HojaRuta.listaElementosViaje, desde, hasta,
                    elementos.Where(x => x.listPosition == null).ToList());


                if (elementos.Exists(x => x.nombre == "ND"))
                {
                    elementos.Remove(elementos.FirstOrDefault(x => x.nombre == "ND"));
                }

                foreach (var ele in eleora)
                {
                    if (elementos.Exists(x => x.nombre == ele.nombre))
                    {
                        var x = elementos.FirstOrDefault(x => x.nombre == ele.nombre);
                        elementos.Remove(x);
                        elementos.Add(ele);
                    }
                }

                IdentificarElementos(elementos);
            }

            if (elementos.Exists(x => x.listPosition.Count == 0))
            {
                foreach (var x in elementos.Where(x => x.listPosition.Count == 0).ToList())
                {
                    elementos.Remove(x);
                }
            }

            return elementos;
        }

        private static void IdentificarElementos(List<Elemento> elementos)
        {
            elementos.ToList().ForEach(v =>
            {
                if (v.nombre.Trim().ToUpper().Substring(0, 3) == "EAD" || v.nombre == "CELULAR")
                    v.nombre = v.nombre.Trim();
                else
                {
                    if (v.nombre.Trim().ToUpper().Substring(0, 3) == "TEL")
                        v.nombre = v.nombre.Trim();
                    else
                    {
                        v.nombre = v.nombre.Trim();
                    }
                }
            });
        }

        public ActividadPorPatente ActividadPorPatente(FiltroEvento filtro)
        {
            return _IReportesMiddleware.ActividadPorPatente(filtro.strFechaDesde, filtro.strFechaHasta, filtro.patente);
        }

        public List<ReporteVersionAPP> getReporteVersionAPP(Mobiliario Filtro)
        {
            return _IReportesMiddleware.getReporteVersionAPP(Filtro.strFechaDesde, Filtro.strFechaHasta, Filtro.p_canal);
        }

        public List<ReporteReservasCD> getReservasCD(FiltroReservasCD filtro)
        {
           //Lsta vacia para recibir los datos de la consulta
            List<ReporteReservasCD> retorno = new List<ReporteReservasCD>();

            return _IReportesMiddleware.getReservasCD(filtro.strFechaDesde, filtro.strFechaHasta,filtro.waybillid);
        }

        public List<ReportePDT> ReportesPDT(FiltroPDT filtro)
        {
            return _IReportesMiddleware.ReportesPDT(filtro.strFecha, filtro.Canales);
        }

        public async Task<List<Flota>> FlotaPorCanal(string idCanal)
        {
            List<Vehiculo> lista = new List<Vehiculo>();

            if (string.IsNullOrEmpty(idCanal))
            {
                throw new Exception($"Error | API flotaPorCanal | No posee idCanal disponible para visualizar este reporte.");
            }

            List<GpsHojaRuta> hojasRuta = new List<GpsHojaRuta>();

            if (int.Parse(idCanal) == int.Parse(_configuration.GetSection("WF").Value))
            {
                hojasRuta = await _Servicios.getPanelHojarutasCD(int.Parse(idCanal), DateTime.Today, DateTime.Today);
            }
            else
            {
                hojasRuta = await _Servicios.getPanelHojarutasDigital(int.Parse(idCanal), DateTime.Today, DateTime.Today);
            }

            GpsCanal canal = hojasRuta.FirstOrDefault().Canal;
            var waybills = hojasRuta.Select(h => h.WayBillId).Distinct();

            foreach (GpsHojaRuta hojaRuta in hojasRuta) 
            { 
                if(hojaRuta.Estado.Id == int.Parse(_configuration.GetSection("HR_ENVIAJE").Value))
                {
                    List<GpsHojaRutaElemento> listaElementos = _Servicios.getHojaRutaElementoViaje(hojaRuta.Id);

                    GpsHojaRutaElemento elemento = listaElementos.Where(e => e.Elemento.Nombre == "CELULAR").FirstOrDefault();

                    Target target = _SecureWay.getTarget(hojaRuta.HojaRutaMovil.Patente, elemento.Propiedad);

                    Vehiculo vehiculo = new Vehiculo
                    {
                        TargetId = int.Parse(target.TargetId.ToString()),
                        Plate = hojaRuta.HojaRutaMovil.Patente,
                        MovilTag = target.MovilTag,
                        lat = target.GPSLatitude,
                        lng = target.GPSLongitude,
                        MovilId = target.MovilId,
                        Fecha = DateTime.Parse(target.GPSLastPositionAT.ToString()),
                        Speed = double.Parse(target.GPSSpeed.ToString()),
                        DispatchId = 0,
                        WayBillId = hojaRuta.WayBillId,
                        HojaRutaId = hojaRuta.Id,
                        lastPositionAt = target.GPSLastPositionAT,
                    };

                    vehiculo.lastPositionAtStr = target.GPSLastPositionAT.Value.ToString("dd/MM/yyyy HH:mm");

                    if (vehiculo.lastPositionAt > DateTime.Now.AddMinutes(-10))
                        vehiculo.online = true;
                    else
                        vehiculo.online = false;

                    lista.Add(vehiculo);
                }
            }

            var nombrecanal = hojasRuta.FirstOrDefault().Canal.Nombre;

            List<Flota> listaFlota = new List<Flota>
            {
                new Flota
                {
                    Name = nombrecanal,
                    vehiculos = lista.OrderBy(o => o.WayBillId).ToList(),
                    TotalDetainee = lista.Where(v => v.Speed < 5).Count(),
                    TotalDisconeted = 0,
                    TotalRunning = lista.Where(v => v.Speed > 5).Count(),
                    TotalStopped = 0,
                    TotalVehiclesRoutes = 0,
                    FullName = nombrecanal,
                    canal = new Filtros.Canal { Sucursal = canal.Sucursal }
                }
            };

            return listaFlota;
        }

        //TRK3-1610 - (177984) - 20/12/2023: Circuito Digital - Reporte de Presencia
        public PresenciaRep PresenciaReporte(FiltroPresencia filtro)
        {
            return _IReportesMiddleware.PresenciaReporte(filtro);
        }
    }
}
