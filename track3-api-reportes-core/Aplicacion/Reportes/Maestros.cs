using track3_api_reportes_core.Aplicacion.Interfaces;
using track3_api_reportes_core.Aplicacion.Responses;
using track3_api_reportes_core.Middleware.Interfaces;
using static track3_api_reportes_core.Middleware.Dto.Movil;


namespace track3_api_reportes_core.Aplicacion.Reportes
{
    public class Maestros : IMaestros
    {
        //public readonly IReportesMiddleware _IReportesMiddleware;
        public readonly IMaestrosMiddleware _IMaestrosMiddleware;

        public Maestros(IMaestrosMiddleware IMaestrosMiddleware)
        {
            _IMaestrosMiddleware = IMaestrosMiddleware;
        }

        public List<sp_get_movil> GetMovil(int nid)
        {
            //return _IReportesMiddleware.ReporteMovilesProveedor(filtro.strFechaDesde, filtro.strFechaHasta);

            return _IMaestrosMiddleware.GetMovil(nid);

        }

        //TRK3-1601 - 177984 - 15/12/2023: Llamada al servicio que recupera solo la posición del canal especifíco
        public PosicionCanalResponse GetPosicionCanal(string idCanal)
        {
            return _IMaestrosMiddleware.GetPosicionCanal(idCanal);
        }
    }
}
