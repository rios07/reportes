using Infraestructure.track3.Models.hojaruta;
using Infraestructure.track3.Models.movil;
using Infraestructure.track3.Models.pedido;
using Infraestructure.track3.Models.planificacion;
using System.Collections.Generic;
using track3_api_reportes_core.Aplicacion.Filtros;
using track3_api_reportes_core.Aplicacion.Interfaces;
using track3_api_reportes_core.Middleware.Dto;
using track3_api_reportes_core.Middleware.Interfaces;
using track3_api_reportes_core.Middleware.Modelos.movil;
using track3_api_reportes_core.Middleware.Modelos.Track;


namespace track3_api_reportes_core.Aplicacion.Main
{
    public class AppServicios : IServicios

    {
        private readonly IServiciosMiddleware _Servicios;
        public static IConfiguration _configuration;
        public AppServicios(IServiciosMiddleware Servicios, IConfiguration configuration)
        {
            _Servicios = Servicios;
            _configuration = configuration;
        }
        public async Task<ResponsePlanificacion> GetHojaRuta(int idPlanificacion, Boolean DetallePedido)
        {
            ResponsePlanificacion retorno = new ResponsePlanificacion();
            try
            {
                retorno.HojaRuta = await _Servicios.GetHojaRuta((long)idPlanificacion, DetallePedido);

                retorno.ListBandaHoraria = await _Servicios.getCanalBandaHoraria(retorno.HojaRuta.Canal);
            }
            catch (Exception ex) 
            {
                throw ex;
            }
            return retorno;
        }

      

        public Task<List<GpsHojaRuta>> getPanelHojaRutas(int canal, DateTime fDesde, DateTime fHasta)
        {
            if (canal == int.Parse(_configuration.GetSection("WF").Value))
            {
                return _Servicios.getPanelHojarutasCD(canal, fDesde, fHasta);
            }
            else
            {
                return _Servicios.getPanelHojarutasDigital(canal, fDesde, fHasta);
            }            
        }

        public async Task<GpsPedido> GetPedido(long idPedido)
        {
            GpsPedido retorno = new GpsPedido();

            retorno = await _Servicios.GetPedido(idPedido);

            return retorno;
        }

        public async Task<GpsPedido> getReservasByIdPedido(long idPedido,long idHojaRuta)
        {
            GpsPedido retorno = new GpsPedido();

            retorno = await _Servicios.getReservasByIdPedido(idPedido,idHojaRuta);

            return retorno;
        }


        public async Task<Elemento>getRecorrido(long IdHojaruta)
        {
            Elemento retorno = new Elemento();

            retorno = await _Servicios.getRecorrido(IdHojaruta);

            return retorno;
        }


        public async Task<GpsPedido> getHojaRutaById(long idPedido)
        {
            GpsPedido retorno = new GpsPedido();

            retorno = await _Servicios.GetPedido(idPedido);

            return retorno;
        }

        public List<GpsPedido> PedidoSearch(string nroRef)
        {
            List<GpsPedido> retorno = new List<GpsPedido>();

            retorno = _Servicios.getPedidoSearch(nroRef);

            return retorno;
        }




        public async Task<List<GpsBandaHoraria>> GetBandasHorarias(int idCanal)
        {
            return await _Servicios.getCanalBandaHoraria(idCanal);
        }     
    
        public async Task<List<GpsMovilesEnViaje>> GetGpsMovilesEnViaje(FiltroMovilesEnViaje filtro)
        {
            if (string.IsNullOrEmpty(filtro.Canales))
            {
                throw new Exception($"Error | API reporteMovilesEnViaje | No posee ningun canal disponible para visualizar este reporte.");
            }

            return await _Servicios.getGpsMovilesEnViaje(filtro.Canales);
        }

        public Task<List<GpsPedido>> GetPedidos(string pedidos, long idHojaRuta)
        {
            throw new NotImplementedException();
        }
    }
}
