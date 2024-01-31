using track3_api_reportes_core.Aplicacion.Filtros;
using track3_api_reportes_core.Aplicacion.Responses;
using static track3_api_reportes_core.Middleware.Dto.Movil;

namespace track3_api_reportes_core.Middleware.Interfaces
{
    public interface IMaestrosMiddleware
    {
        public List<sp_get_movil> GetMovil(int nid);

        public abstract PosicionCanalResponse GetPosicionCanal(string idCanal);
    }
}
