using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Security.Policy;
using System.Text;
using track3_api_reportes_core.Infraestructura.Dao;
using track3_api_reportes_core.Infraestructura.Interfaces;
using track3_api_reportes_core.Infraestructura.SecureWay.Models;

namespace track3_api_reportes_core.Infraestructura.Repositorios
{
    public class SecurewayRepository:ISecurewayRepository
    {
        private readonly  ISqlDao _sqlDao;
        public SecurewayRepository(ISqlDao sqlDao)
        {
            _sqlDao = sqlDao;
        }

        public DataTable getTargets(string movilPatente, string propiedad, string conex)
        {
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            List<SqlParameter> parametros = new List<SqlParameter>();
            _sqlDao.CrearConnection(conex);

            //Consulta a SecureWay
            sb.Append(" SELECT TOP 1 TargetId, TrackerId, MovilPlateId, MovilTag, GPSLatitude, GPSLongitude, MovilId, GPSLastPositionAT, GPSSpeed ");
            sb.Append(" FROM Secureway.atrack.Target ");

            if (propiedad != "")
            {
                sb.Append(" WHERE GPSLastPositionAT is not null ");
                sb.Append(" AND TrackerId= @PROPIEDAD ");

                //Paso los parametros
                parametros.Add(new SqlParameter("@PROPIEDAD", propiedad));
            }

            if (movilPatente != "")
            {
                sb.Append("   WHERE GPSLastPositionAT is not null ");
                sb.Append("   AND MovilPlateId = @PATENTE ");

                //Paso los parametros
                parametros.Add(new SqlParameter("@PATENTE", movilPatente));
            }

            dt = _sqlDao.ExcecuteAdapterFill(sb.ToString(), parametros);

            //Cierro le Conexion a la Base
            _sqlDao.CerrarConexion();

            return dt;
        }

        public DataTable getPosiciones(DateTime desde, DateTime hasta, long targetId, string conex)
        {
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            List<SqlParameter> parametros = new List<SqlParameter>();
            _sqlDao.CrearConnection(conex);

            sb.Append(" SELECT  Id,TargetId,Date,PositionRepeats,PositionRepeatAt,GPSLatitude,GPSLongitude  ");
            sb.Append(" FROM Secureway.atrack.Position  ");
            sb.Append(" WHERE TargetId=@Target  ");
            sb.Append(" AND Date >= @Desde  ");
            sb.Append(" AND Date <=  @Hasta ");
            sb.Append(" ORDER BY  Date ");
            parametros.Add(new SqlParameter("@Target", targetId));
            parametros.Add(new SqlParameter("@Desde", desde.ToString("yyyy/MM/dd HH:mm:ss")));
            parametros.Add(new SqlParameter("@Hasta", hasta.ToString("yyyy/MM/dd HH:mm:ss")));
            
            dt = _sqlDao.ExcecuteAdapterFill(sb.ToString(), parametros);

            //Cierro le Conexion a la Base
            _sqlDao.CerrarConexion();

            return dt;
        }

        public DataTable getPosicionesHistoricas(DateTime desde, DateTime hasta, long targetId, string conexHist)
        {
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            List<SqlParameter> parametros = new List<SqlParameter>();
            _sqlDao.CrearConnection(conexHist);

            sb.Append(" SELECT  Id,TargetId,Date, GPSLatitude,GPSLongitude  ");
          
            sb.Append(" FROM SecureWay_Hist.atrack.Position_historicas_depuradas  ");
            sb.Append(" WHERE TargetId=@Target  ");
            sb.Append(" AND Date >= @Desde  ");
            sb.Append(" AND Date <=  @Hasta ");
            sb.Append(" ORDER BY  Date ");
            parametros.Add(new SqlParameter("@Target", targetId));
            parametros.Add(new SqlParameter("@Desde", desde.ToString("yyyy/MM/dd HH:mm:ss")));
            parametros.Add(new SqlParameter("@Hasta", hasta.ToString("yyyy/MM/dd HH:mm:ss")));

            dt = _sqlDao.ExcecuteAdapterFill(sb.ToString(), parametros);

            //Cierro le Conexion a la Base
            _sqlDao.CerrarConexion();

            return dt;
        }
    }
}
