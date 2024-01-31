using Infraestructure.track3.Models.hojaruta;
using Infraestructure.track3.Models.pedido;
using System.Data;
using Infraestructure.track3.Models.planificacion;
using track3_api_reportes_core.Infraestructura.Interfaces;
using track3_api_reportes_core.Middleware.Interfaces;
using track3_api_reportes_core.Middleware.Modelos.Track;
using track3_api_reportes_core.Middleware.Dto;
using Infraestructure.track3.Models.movil;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using track3_api_reportes_core.Aplicacion.Responses;
using track3_api_reportes_core.Middleware.Modelos.movil;

namespace track3_api_reportes_core.Middleware.ReportesMiddleware
{
    public class ServiciosMiddleware: IServiciosMiddleware
    {
        private readonly IMappingFormularios _mapping;
        private readonly IHojaRutaRepository _hojaRutaRepository;
        private readonly IPedidosRepositorio _PedidoRepositorio;
        public static IConfiguration _configuration;
        private readonly IDaoReportes _DaoReporte;

        public ServiciosMiddleware(IMappingFormularios  mapping , IHojaRutaRepository hojaRutaRepository, IPedidosRepositorio PedidoRepositorio, IConfiguration configuration)
        {
            _mapping = mapping;
            _hojaRutaRepository = hojaRutaRepository;
            _PedidoRepositorio = PedidoRepositorio;
            _configuration = configuration;
        }
        public async Task<GpsHojaRuta> GetHojaRuta(long IdHojaRuta, Boolean DetallePedido)
        {
            string IdPedidos = "";
            DataTable dt = _hojaRutaRepository.getHojaruta(IdHojaRuta);
            GpsHojaRuta retorno = _mapping.mapHojaRuta(dt);

            //Pedidos con el detalle si DetallePedido es true
           
            retorno.Detalle = _mapping.mapHRDetallePedidos(_hojaRutaRepository.getHojaRutaDetallePedidos(IdHojaRuta), ref IdPedidos, DetallePedido);
           
            retorno.listaPersonal = _mapping.mapListPersonal(_hojaRutaRepository.getListPersonal(IdHojaRuta));

            retorno.listaElementosViaje = _mapping.ElementosViajes(_hojaRutaRepository.getElementosHojaRuta(IdHojaRuta));
            retorno.HojaRutaEstado = _mapping.mapHojaRutaEstado(_hojaRutaRepository.getHojaRutaEstado(IdHojaRuta));
            if (IdPedidos != "")
            {
                List<GpsPedidoRefAudit> listPedidoRefAudits = _mapping.mapListpPedidosRefAudit(_PedidoRepositorio.getHistoricoPedidos(IdPedidos, IdHojaRuta));
                List<GpsContenedor> listContenedors = _mapping.mapPedidosContenedor(_PedidoRepositorio.getContenedores(IdPedidos));
                retorno.Detalle.ForEach(item =>
                {
                    item.Pedido.Contenedores = listContenedors.Where(x => x.IdPedido == item.Pedido.Id).ToList();
                    item.Pedido.listTokenAudit = listPedidoRefAudits.Where(x => x.idPedido == item.Pedido.Id).ToList();
                });
            }
         
            return retorno;
        }
               
        public async Task<List<GpsBandaHoraria>> getCanalBandaHoraria(GpsCanal canal)
        {
            return _mapping.mapBandaHoraria(_PedidoRepositorio.getCanalBandaHoraria(canal.Id));
        }        
        
        public async Task<List<GpsBandaHoraria>> getCanalBandaHoraria(int idCanal)
        {
            return _mapping.mapBandaHoraria(_PedidoRepositorio.getCanalBandaHoraria(idCanal));
        }

        public async Task<List<GpsHojaRuta>> getPanelHojarutasCD(int canal, DateTime fDesde, DateTime fHasta)
        {
            DataTable dt = _hojaRutaRepository.ObtenerHojasRutasCD(canal, fDesde, fHasta);
            string IdHojaRutas = "";
            List<GpsHojaRuta> lista = _mapping.mapPanelHojaRutaCD(dt, ref IdHojaRutas);
            List<GpsHojaRuta> retorno = unificarHdrs(lista);
            if (retorno.Count > 0)
            {
                List<GpsHojaRutaPersonal> listaPersonal = _mapping.mapListPersonal(_hojaRutaRepository.getListPersonalChoferCadetePanel(IdHojaRutas));
                List<GpsBandaHoraria> listabandasBandaHorarias = mapBandaHorariaMayoritaria(_PedidoRepositorio.obtenerBandaHorariaMayoritaria(IdHojaRutas));

                retorno.ForEach(x =>
                {
                    x.bandaHorariaMayoritaria = listabandasBandaHorarias.FirstOrDefault(s => s.IdHojaRuta == x.Id);
                    x.listaPersonal = listaPersonal.Where(p => p.IdHojaRuta == x.Id).ToList();
                });
            }

            return retorno;
        }

        public async Task<List<GpsHojaRuta>> getPanelHojarutasDigital(int canal, DateTime fDesde, DateTime fHasta)
        {
            DataTable dt = _hojaRutaRepository.ObtenerHojasRutasDigital(canal, fDesde, fHasta);
            string IdHojaRutas = "";
            List<GpsHojaRuta> lista = _mapping.mapPanelHojaRuta(dt, ref IdHojaRutas);
            List<GpsHojaRuta> retorno = unificarHdrs(lista);
            if (retorno.Count > 0)
            {
                List<GpsHojaRutaPersonal> listaPersonal = _mapping.mapListPersonal(_hojaRutaRepository.getListPersonalChoferCadetePanel(IdHojaRutas));
                List<GpsBandaHoraria> listabandasBandaHorarias = mapBandaHorariaMayoritaria(_PedidoRepositorio.obtenerBandaHorariaMayoritaria(IdHojaRutas));

                retorno.ForEach(x =>
                {
                    x.bandaHorariaMayoritaria = listabandasBandaHorarias.FirstOrDefault(s => s.IdHojaRuta == x.Id);
                    x.listaPersonal = listaPersonal.Where(p => p.IdHojaRuta == x.Id).ToList();
                });
            }
            
            return retorno;
        }

        private List<GpsBandaHoraria> mapBandaHorariaMayoritaria(DataTable dt)
        {
            List<GpsBandaHoraria> reList = new List<GpsBandaHoraria>();
            foreach (DataRow row in dt.Rows)
            {
                GpsBandaHoraria item = new GpsBandaHoraria
                {
                    IdHojaRuta = long.Parse(row["ID_HOJARUTA"].ToString()),
                    color = row["COLOR"].ToString(),
                    id = int.Parse(row["ID_BANDAHORARIA"].ToString()),
                    nombre = row["BANDA_HORARIA"].ToString(),
                    horaDesde = row["HORADESDE"].ToString(),
                    horaHasta = row["HORAHASTA"].ToString(),
                    Canal = new GpsCanal { Id = int.Parse(row["ID_CANAL"].ToString()) },
                    orden = int.Parse(row["ORDEN"].ToString()),
                    Mayoritaria = int.Parse(row["SUMA"].ToString())
                };

                if (reList.Count == 0)
                {
                    reList.Add(item);
                }
                else
                {
                    var bh = reList.Find(x => x.IdHojaRuta == item.IdHojaRuta);
                    if (bh == null)
                    {
                        reList.Add(item);
                    }
                    else
                    {
                        if (item.Mayoritaria > bh.Mayoritaria)
                        {
                            reList.Remove(bh);
                            reList.Add(item);
                        }
                    }
                }
            }

            return reList;
        }

        private List<GpsHojaRuta> unificarHdrs(List<GpsHojaRuta> HDRs)
        {
            List<GpsHojaRuta> retorno = new List<GpsHojaRuta>();
            foreach (GpsHojaRuta item in HDRs)
            {
                if (retorno.Count == 0)
                {
                    retorno.Add(item);
                }
                else
                {

                    var hdr = retorno.Find(x => x.Id == item.Id);
                    if (hdr == null)
                    {
                        retorno.Add(item);
                    }
                    else
                    {
                        int indice = retorno.FindIndex(x => x.Id == item.Id);
                        retorno.Where(x => x.Id == item.Id).FirstOrDefault().EstadosPedidos.AddRange(item.EstadosPedidos);
                    }
                }

            }

            return retorno;
        }
    
        public async Task<GpsPedido> GetPedido(long idPedido)
        {
            DataTable dt = _PedidoRepositorio.getPedido(idPedido);

            //Mapping de pedido
            GpsPedido retorno = _mapping.mapPedido(dt);
            
            //Digital buscamos por idPedido
            retorno.items = _mapping.mapItems(_PedidoRepositorio.getPedidoDetalle(retorno.Id));
        
            return retorno;
        }

        public async Task<GpsPedido> getReservasByIdPedido(long idPedido, long idHojaRuta)
        {
            DataTable dt = _PedidoRepositorio.getPedido(idPedido);

            //Mapping de pedido
            GpsPedido retorno = _mapping.mapPedido(dt);

            //En CD buscamos por idHojaRuta y coordenadas (x,y) 
            retorno.items = _mapping.mapItems(_PedidoRepositorio.getPedidoDetalleCD(idPedido,idHojaRuta));

            return retorno;
        }



        public Task<Elemento> getRecorrido(long IdHojaruta)
        {
            throw new NotImplementedException();
        }

        public List<GpsPedido> getPedidoSearch(string nroRef)
        {
            List<GpsPedido> retorno= new List<GpsPedido>();
            DataTable dt = _PedidoRepositorio.getPedidoSearch(nroRef);

            foreach(DataRow row in dt.Rows)
            {
                GpsPedido p = mapperPedidoSearch(row);
                retorno.Add(p);
            }
            
            return retorno;
        }
        
        public async Task<List<GpsMovilesEnViaje>> getGpsMovilesEnViaje(string canales)
        { 
            List<GpsMovilesEnViaje> retorno = new List<GpsMovilesEnViaje>();
            List<GpsCanal> listaCanales = new List<GpsCanal>();
            int HREnViaje = int.Parse(_configuration.GetSection("HR_ENVIAJE").Value);

            DataTable dtCanales = _hojaRutaRepository.getAllCanales();
            listaCanales = mapCanales(dtCanales);

            DataTable dtReporte = _hojaRutaRepository.getCantidadMovilesEnViaje(canales);

            retorno = _mapping.mapMovilesEnViaje(dtReporte, listaCanales);

            if(retorno.Count > 0)
            {
                List<GpsMovilesEnViajeDetalle> listaViaje = new List<GpsMovilesEnViajeDetalle>();

                string estadoEntregados = _configuration.GetSection("ENTREGADO_CRE").Value + "," + _configuration.GetSection("ENTREGADO_CAE").Value;
                
                string estadoNoEntregados = _configuration.GetSection("NO_ENTREGADO").Value + "," +
                    _configuration.GetSection("NO_ENTREGADO_TRANSPORTE").Value + "," +
                    _configuration.GetSection("NO_ENTREGADO_CLIENTE").Value;

                string cancelados = _configuration.GetSection("CANCELADO_EN_VIAJE").Value + "," + _configuration.GetSection("CANCELADO_RETIRO_EN_VIAJE").Value;

                DataTable dtMovilesEnViajeDetalle = _hojaRutaRepository.getPedidosTotalesPlanificados(canales, HREnViaje, estadoEntregados, estadoNoEntregados, cancelados);
                DataTable dtPuntosDeEntregas = _hojaRutaRepository.getPuntosDeEntrega(canales, HREnViaje);

                listaViaje = _mapping.mapMovilesEnViajeDetalle(dtMovilesEnViajeDetalle);
                listaViaje = _mapping.mapPuntoDeEntrega(ref listaViaje, dtPuntosDeEntregas);

                foreach (var item in retorno)
                {
                    item.listaMovilesEnViaje = new List<GpsMovilesEnViajeDetalle>();
                    item.listaMovilesEnViaje = listaViaje.Where(r => r.idCanal == item.idCanal).ToList();
                }
            }            

            return retorno;
        }
        
        private List<GpsCanal> mapCanales(DataTable dt)
        {
            List<GpsCanal> listaCanal = new List<GpsCanal>();

            foreach (DataRow item in dt.Rows)
            {
                GpsCanal canal = new GpsCanal
                {
                    Id = int.Parse(item["ID_CANAL"].ToString()),
                    Nombre = item["CANAL"].ToString()
                };

                listaCanal.Add(canal);
            }

            return listaCanal;
        }
            
        public GpsPedido mapperPedidoSearch(DataRow row)
        {
            GpsPedido pedido = new GpsPedido()
            {
                Id = long.Parse(row["ID_PEDIDO"].ToString()),
                Stamp = !string.IsNullOrEmpty(row["FECHA"].ToString()) ? (DateTime)row["FECHA"] : DateTime.MinValue,
                Fecha = !string.IsNullOrEmpty(row["FECHA_PLAN"].ToString()) ? (DateTime)row["FECHA_PLAN"] : DateTime.MinValue,
                nroPedidoRef = row["NRO_PEDIDO_REF"].ToString(),
                Cliente = new GpsCliente() { Dni = row["DNI"].ToString(), Apellido = row["APELLIDO"].ToString(), Nombre = row["NOMBRE"].ToString(), Telefono = row["TELEFONO"].ToString() },
                DireccionEntrega = new GpsDireccion(row),
                Canal = new GpsCanal() { Id = int.Parse(row["ID_CANAL"].ToString()), Nombre = row["CANAL"].ToString(), Sucursal = int.Parse(row["SUCURSAL"].ToString())},
                BandaHoraria = new GpsBandaHoraria() { nombre = row["BANDAHORARIA"].ToString(), horaDesde = row["HORADESDE"].ToString(), horaHasta = row["HORAHASTA"].ToString() },
                Estado = new GpsEstado() { Nombre = row["ESTADO_PEDIDO"].ToString() },
                Terminal = string.IsNullOrEmpty(row["TERMINAL"].ToString()) ? 0 : int.Parse(row["TERMINAL"].ToString()),
                Transaccion = string.IsNullOrEmpty(row["TRANSACCION"].ToString()) ? 0 : int.Parse(row["TRANSACCION"].ToString()),
            };

            //Si tiene hoja de ruta se la asigno
            if (!string.IsNullOrEmpty(row["ID_HOJARUTA"].ToString()))
            {
                pedido.GpshojaRuta = new GpsHojaRuta() {
                    Id = int.Parse(row["ID_HOJARUTA"].ToString()),
                    WayBillId = row["WAYBILLID"].ToString(),
                    Estado = new GpsEstado() { Id = !string.IsNullOrEmpty(row["ID_ESTADO_HOJARUTA"].ToString()) ? int.Parse(row["ID_ESTADO_HOJARUTA"].ToString()) : 0, Nombre = row["ESTADO_HOJARUTA"].ToString() },
                    Canal = new GpsCanal() { Nombre = row["SUCURSAL"].ToString() },
                    FechaSalida = new DateTime(),
                    FechaCierre = new DateTime(),
                    FechaPlan = new DateTime(),
                    DistanciaTotal= 0,
                    TiempoEstimadoTotal= 0,
                    DistanciaReal= 0,
                    tiempoReal = 0
                };
                pedido.listTokenAudit = _mapping.mapListpPedidosRefAudit(_PedidoRepositorio.getHistoricoPedidos(pedido.Id.ToString(), pedido.GpshojaRuta.Id));
            }


            return pedido;
        }
    
        public List<GpsHojaRutaElemento> getHojaRutaElementoViaje(int idHojaruta)
        {
            return _mapping.ElementosViajes(_hojaRutaRepository.getElementosHojaRuta(idHojaruta)); ;
        }
    }
}