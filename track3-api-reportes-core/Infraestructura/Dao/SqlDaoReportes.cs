using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using track3_api_reportes_core.Infraestructura.Interfaces;

namespace track3_api_reportes_core.Infraestructura.Dao
{
    public class SqlDaoReportes : ISqlDaoReportes
    {
        public readonly ISqlDao _ISqlDao;
    
        public SqlDaoReportes()
        {
        }
              

        public DataTable ObtenerPosiciones(string Desde, string Hasta, string p_Identificador)
        {
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            List<SqlParameter> parametros = new List<SqlParameter>();

            //Consulta a SecureWay
            sb.Append(" select P.Date fecha_posicion,P.GPSLatitude gpslatitud,p.GPSLongitude  gpslongitud ");
            sb.Append(" FROM[Secureway].[atrack].[Position] P,                                            ");
            sb.Append("     [Secureway].[atrack].[Target] T                                               ");
            sb.Append("  where P.TargetId = T.TargetId                                                    ");
            sb.Append("     and T.TrackerId = @IMEI                                                       ");
            sb.Append("     and P.Date >= @Desde                                                          ");
            sb.Append("     and P.Date <= @Hasta                                                          ");


            //Paso los parametros
            parametros.Add(new SqlParameter("@Desde", DateTime.ParseExact(Desde, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd HH:mm:ss")));
            parametros.Add(new SqlParameter("@Hasta", DateTime.ParseExact(Hasta, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd HH:mm:ss")));
            parametros.Add(new SqlParameter("@IMEI", p_Identificador));


            //Abrir la Conexion a al SQLServer
            SqlDao sqlDao = new SqlDao();
                    
            //Ejecuto la consulta
            dt = sqlDao.ExcecuteAdapterFill(sb.ToString(), parametros);
            
            //Cierro le Conexion a la Base
            sqlDao.CerrarConexion();

            return dt;

        }



    }
}
