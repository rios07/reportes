using System.Data;
using track3_api_reportes_core.Aplicacion.Responses;
using track3_api_reportes_core.Infraestructura.Interfaces;
using track3_api_reportes_core.Middleware.Interfaces;
using static track3_api_reportes_core.Middleware.Dto.Movil;

namespace track3_api_reportes_core.Middleware.ReportesMiddleware
{
    public class MaestrosMiddleware : IMaestrosMiddleware
    {
        public readonly IDaoReportes _IDaoReportes;
        public readonly IDaoMaestros _IDaoMaestros;


        public MaestrosMiddleware(IDaoReportes IDaoReportes, IDaoMaestros IDaoMaestros)
        {
            _IDaoReportes = IDaoReportes;
            _IDaoMaestros = IDaoMaestros;

        }

        public List<sp_get_movil> GetMovil(int nid)
        {
            DataTable dt = _IDaoMaestros.GetMovil(nid);
            List<sp_get_movil> retorno = new List<sp_get_movil>();
            foreach (DataRow row in dt.Rows)
            {
                sp_get_movil item = new sp_get_movil(row);
                retorno.Add(item);
            }

            return retorno;
        }

        public PosicionCanalResponse GetPosicionCanal(string idCanal)
        {
            DataTable dt = _IDaoMaestros.GetPosicionCanal(int.Parse(idCanal));
            PosicionCanalResponse retorno = new PosicionCanalResponse();

            if(dt.Rows.Count > 0)
                retorno = mapperPosicionCanal(dt.Rows[0]);

            return retorno;
        }

        private PosicionCanalResponse mapperPosicionCanal(DataRow row)
        {
            PosicionCanalResponse posicionCanal = new PosicionCanalResponse();

            posicionCanal.IdCanal = Int32.Parse(row["ID_CANAL"].ToString());
            posicionCanal.Nombre = row["NOMBRE"].ToString();
            posicionCanal.Sucursal = Int32.Parse(row["SUCURSAL"].ToString());
            posicionCanal.CoordenadaX = row["CANAL_LATITUDE"].ToString();
            posicionCanal.CoordenadaY = row["CANAL_LONGITUDE"].ToString();

            return posicionCanal;
        }
    }
}
