using Infraestructure.track3.Models.hojaruta;
using Infraestructure.track3.Models.movil;
using Infraestructure.track3.Models.pedido;
using Infraestructure.track3.Models.planificacion;
using track3_api_reportes_core.Middleware.Modelos.movil;
using track3_api_reportes_core.Middleware.Modelos.Track;

namespace track3_api_reportes_core.Middleware.Interfaces
{
    public interface IServiciosMiddleware
    {
        abstract Task<GpsHojaRuta> GetHojaRuta(long idPlanificacion,Boolean DetallePedido);
        abstract Task<List<GpsBandaHoraria>> getCanalBandaHoraria(GpsCanal canal);
        abstract Task<List<GpsBandaHoraria>> getCanalBandaHoraria(int idCanal);
        abstract Task<List<GpsHojaRuta>> getPanelHojarutasCD(int canal, DateTime fDesde, DateTime fHasta);
        abstract Task<List<GpsHojaRuta>> getPanelHojarutasDigital(int canal, DateTime fDesde, DateTime fHasta);
        abstract Task<GpsPedido> GetPedido(long idPedido);
        abstract Task<GpsPedido> getReservasByIdPedido(long idPedido, long idHojaRuta); 
        abstract Task<Elemento> getRecorrido(long IdHojaruta);
        abstract List<GpsPedido> getPedidoSearch(string nroRef);
        abstract Task<List<GpsMovilesEnViaje>> getGpsMovilesEnViaje(string canales);
        abstract List<GpsHojaRutaElemento> getHojaRutaElementoViaje(int idHojaruta);
    }
}
