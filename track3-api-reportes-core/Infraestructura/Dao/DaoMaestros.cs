using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Text;
using track3_api_reportes_core.Infraestructura.Interfaces;


namespace track3_api_reportes_core.Infraestructura.Dao
{
    public class DaoMaestros :IDaoMaestros
    {
        private readonly IOracleDao _OracleDao;
        public static IConfiguration _configuration;
        public string ConnectionString;

        public DaoMaestros(IOracleDao OracleDao, IConfiguration config)

        {
            _OracleDao = OracleDao;
            _configuration = config;
            ConnectionString = _configuration.GetConnectionString("Track3");
        }

        public DataTable GetPosicionCanal(int idCanal)
        {
            DataTable dt = new DataTable();
            List<OracleParameter> parametros = new List<OracleParameter>();

            StringBuilder sb = new StringBuilder();

            _OracleDao.CrearConnection(ConnectionString);

            sb.Append(" SELECT CANAL.ID ID_CANAL, CANAL.NOMBRE, CANAL.SUCURSAL, ");
            sb.Append(" DIR.COORDENADAY CANAL_LONGITUDE, DIR.COORDENADAX CANAL_LATITUDE ");
            sb.Append(" FROM GPS_CANAL CANAL ");
            sb.Append(" LEFT JOIN GPS_DIRECCION DIR ON CANAL.ID_DIRECCION = DIR.ID ");
            sb.Append(" WHERE CANAL.ID = :CANAL ");

            parametros.Add(new OracleParameter(":CANAL", idCanal));

            dt = _OracleDao.ExcecuteAdapterFill(sb.ToString(), parametros);

            _OracleDao.CerrarConexion();

            return dt;
        }

        public DataTable GetMovil(int nid)
        {
            DataTable dt = new DataTable();
            List<OracleParameter> parametros = new List<OracleParameter>();

            //Crear la conexion
            _OracleDao.CrearConnection(ConnectionString);

            //Agregar los parámetros necesarios al comando
            OracleParameter paramId = new OracleParameter("p_id", OracleDbType.Int16);
            paramId.Value = nid;
            parametros.Add(paramId);


            //Agregar el parámetro de salida para la tabla
            OracleParameter paramTabla = new OracleParameter("p_resultado", OracleDbType.RefCursor);
            paramTabla.Direction = ParameterDirection.Output;
            parametros.Add(paramTabla);


            //Ejecutar el comando y llenar el objeto DataTable con los resultados
            dt = _OracleDao.ExceuteStoreProcedure("sp_get_movil", parametros);

            //Cerrar la conexion
            _OracleDao.CerrarConexion();

            return dt;
        }
    }
}
