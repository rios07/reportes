using static track3_api_reportes_core.Middleware.Dto.Movil;
using track3_api_reportes_core.Aplicacion.Responses;

namespace track3_api_reportes_core.Aplicacion.Interfaces
{
    public interface IMaestros
    {
        public List<sp_get_movil> GetMovil(int nid);
        public abstract PosicionCanalResponse GetPosicionCanal(string idCanal);
    }
}
