using Infraestructure.track3.Models.hojaruta;
using Infraestructure.track3.Models.movil;
using Infraestructure.track3.Models.pedido;
using track3_api_reportes_core.Middleware.Dto;
using Infraestructure.track3.Models.planificacion;
using track3_api_reportes_core.Middleware.Modelos.Track;
using track3_api_reportes_core.Middleware.Modelos.movil;
using track3_api_reportes_core.Aplicacion.Filtros;

namespace track3_api_reportes_core.Aplicacion.Interfaces
{
    public interface IServicios
    {
             
        abstract Task<ResponsePlanificacion> GetHojaRuta(int idPlanificacion,Boolean DetallePedido);

        abstract Task<List<GpsHojaRuta>> getPanelHojaRutas(int parse, DateTime fDesde, DateTime fHasta);

        abstract Task<List<GpsBandaHoraria>> GetBandasHorarias(int idCanal);

        abstract Task<GpsPedido> GetPedido(long id);
        
        abstract Task<GpsPedido> getReservasByIdPedido(long idPedido, long idHojaRuta);

        abstract Task<List<GpsPedido>> GetPedidos(string pedidos, long idHojaRuta);

        abstract Task<Elemento> getRecorrido(long IdHojaRuta);

        abstract List<GpsPedido> PedidoSearch(string nroRef);

        abstract Task<List<GpsMovilesEnViaje>> GetGpsMovilesEnViaje(FiltroMovilesEnViaje filtro);        
    }
}
