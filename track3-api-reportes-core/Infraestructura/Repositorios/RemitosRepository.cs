using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Text;
using Infraestructure.track3.Models.planificacion;
using track3_api_reportes_core.Infraestructura.Dao;
using track3_api_reportes_core.Infraestructura.Interfaces;

namespace track3_api_reportes_core.Infraestructura.Repositorios
{
    public class RemitosRepository:IRemitosRepository
    {
        private readonly IOracleDao _OracleDao;
        public static IConfiguration _configuration;
        public string ConexTrack;

        public RemitosRepository(IOracleDao OracleDao, IConfiguration config)
        {
            _configuration=config;
            _OracleDao=OracleDao;
            ConexTrack = _configuration.GetConnectionString("Track3");
        }

        public DataTable getDatosRemitos(long idHojaRuta)
        {
            DataTable dt = new DataTable();
            List<OracleParameter> parametros = new List<OracleParameter>();
            _OracleDao.CrearConnection(ConexTrack);

            OracleParameter paramIdHR = new OracleParameter("p_id_hr", OracleDbType.Int32);
            paramIdHR.Value = idHojaRuta;
            parametros.Add(paramIdHR);

            //Agregar el parámetro de salida para la tabla
            OracleParameter paramTabla = new OracleParameter("p_resultado", OracleDbType.RefCursor);
            paramTabla.Direction = ParameterDirection.Output;
            parametros.Add(paramTabla);
            dt = _OracleDao.ExceuteStoreProcedure("sp_get_remitosimpresion", parametros);
            
            _OracleDao.CerrarConexion();
            return dt;
        }

        public DataTable getCAI(GpsCanal canal)
        {
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            List<OracleParameter> parametros = new List<OracleParameter>();

            _OracleDao.CrearConnection(_configuration.GetConnectionString("StringConexWF"));

            sb.Append(" SELECT NCAICEN1, FECHCAIU  ");
            sb.Append(" FROM  F013ALMA  ");
            sb.Append(" WHERE CEMPRESA = '1'  ");
            sb.Append(" AND sysdate < fechcaiu  ");

            dt = _OracleDao.ExcecuteAdapterFill(sb.ToString(), parametros);

            _OracleDao.CerrarConexion();

            return dt;
        }

        public DataTable getPedidosByRef(string remitos, long idHojaRuta)
        {
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            List<OracleParameter> parametros = new List<OracleParameter>();
            _OracleDao.CrearConnection(ConexTrack);

            sb.Append("  SELECT P.ID, P.FECHA,P.OBSERVACION, NRO_PEDIDO_REF RESERVA, C.ID ID_CLIENTE, C.APELLIDO, C.NOMBRE, C.DNI DOCUMENTO, C.TELEFONO, C.CELULAR, C.MAIL MAIL, ");
            sb.Append("  E.ID ID_ESTADO, E.NOMBRE ESTADO, D.ID ID_DIRECCION, D.CALLE, D.NUMERO ALTURA_CALLE, D.PISO, D.PISO_DEPTO PISO_DPTO, D.CODIGOPOSTAL CODIGO_POSTAL, ");
            sb.Append("  D.LOCALIDAD, D.PROVINCIA,D.COORDENADAX LATITUDE, D.COORDENADAY LONGITUDE, T.ID ID_TIPO, T.NOMBRE TIPO, ");
            sb.Append("  CA.ID ID_CANAL, CA.SUCURSAL SUCURSAL, CA.NOMBRE CANAL, CA.CONEXION_BD, CA.BD, CA.USUARIO, CA.PASSWORD, CA.TIPO CANAL_TIPO, DC.ID CANAL_ID_DIRECCION, ");
            sb.Append("  DC.CALLE CANAL_CALLE, DC.NUMERO CANAL_ALTURA_CALLE, DC.PISO CANAL_PISO, DC.PISO_DEPTO CANAL_PISO_DPTO, DC.CODIGOPOSTAL CANAL_CODIGO_POSTAL, DC.LOCALIDAD CANAL_LOCALIDAD, ");
            sb.Append("  DC.COORDENADAX CANAL_LATITUDE, DC.COORDENADAY CANAL_LONGITUDE, B.ID ID_BANDAHORARIA, B.DESCRIPCION BANDA_HORARIA, B.HORADESDE, B.HORAHASTA, B.ORDEN, B.COLOR, ");
            sb.Append("  (SELECT COUNT(PLU) FROM GPS_PEDIDODETALLE WHERE ID_PEDIDO = P.ID  AND ID_ESTADO<>95) ITEMS, TD.ID ID_TIPODOMICILIO, TD.NOMBRE TIPODOMICILIONOMBRE,  ");
            sb.Append("  P.TERMINAL, P.TRANSACCION, P.IMPORTE_TOTAL, CA.GENERA_COT,PC.REMITO  ");
            sb.Append("  FROM GPS_PEDIDO P ");
            sb.Append("  INNER JOIN GPS_PEDIDOSCOT PC ON PC.IDPEDIDO=P.ID  ");
            sb.Append("  INNER JOIN GPS_HOJARUTA H ON H.ID =PC.IDHOJARUTA AND H.ID_ESTADO<>8  ");
            sb.Append("  INNER JOIN GPS_CLIENTE C ON C.ID = P.ID_CLIENTE ");
            sb.Append("  INNER JOIN GPS_ESTADO E ON E.ID = P.ID_ESTADO ");
            sb.Append("  INNER JOIN GPS_DIRECCION D ON D.ID = P.ID_DIRECCION_ENTREGA ");
            sb.Append("  INNER JOIN GPS_PEDIDOTIPO T ON T.ID = P.ID_TIPO ");
            sb.Append("  INNER JOIN GPS_CANAL CA ON CA.ID = P.ID_CANAL ");
            sb.Append("  INNER JOIN GPS_DIRECCION DC ON DC.ID = CA.ID_DIRECCION ");
            sb.Append("  INNER JOIN GPS_BANDAHORARIA B ON B.ID =  P.ID_BANDA_HORARIA ");
            sb.Append("  INNER JOIN GPS.GPS_TIPODOMICILIO TD ON TD.ID = D.ID_TIPODOMICILIO ");
            sb.Append($" WHERE H.ID=:ID_HOJARUTA AND PC.REMITO IN ({remitos}) ");
            sb.Append("  ORDER BY PC.REMITO ASC ");

            parametros.Add(new OracleParameter(":ID_HOJARUTA", idHojaRuta));

            dt = _OracleDao.ExcecuteAdapterFill(sb.ToString() , parametros);
            _OracleDao.CerrarConexion();
            return dt;
        }

        public DataTable getItemsById(string pedidos)
        {
            List<OracleParameter> parametros = new List<OracleParameter>();
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT PD.ID,PD.ID_PEDIDO, PD.PLU, PD.DESCRIPCION DESC_PLU, PD.CANTIDAD, PD.PESO, PD.UMP, PD.VOLUMEN, PD.UMV,");
            sb.Append(" C.ID ID_CONTENEDOR, C.DESCRIPCION CONTENEDOR, E.ID ID_ESTADO, E.NOMBRE ESTADO, O.ID ID_OPERACION, O.NOMBRE OPERACION,");
            sb.Append(" D.ID ID_DIRECCION, D.CALLE CALLE, D.NUMERO ALTURA_CALLE, D.CODIGOPOSTAL CODIGO_POSTAL, D.PROVINCIA PROVINCIA, D.COORDENADAX LATITUDE, D.COORDENADAY LONGITUDE,");
            sb.Append(" D.LOCALIDAD LOCALIDAD, D.PISO PISO, D.PISO_DEPTO PISO_DPTO, PD.NRO_RESERVA NRO_RESERVA, PD.NRO_RESERVA_ORI NRO_RESERVA_ORI, PD.OBSERVACIONES OBSERVACIONES, TD.ID ID_TIPODOMICILIO ,TD.NOMBRE TIPODOMICILIONOMBRE,PD.CANTIDAD_COMPONENTE ");
            //sb.Append(" ,D.ID_BOLSA BOLSA "); //Descomentar cuando se agregue el campo en la DB
            sb.Append(" FROM GPS_PEDIDODETALLE PD");
            sb.Append(" INNER JOIN GPS_CONTENEDOR C ON C.ID = PD.ID_CONTENEDOR");
            sb.Append(" INNER JOIN GPS_ESTADO E ON E.ID = PD.ID_ESTADO");
            sb.Append(" INNER JOIN GPS_ESTADO O ON O.ID = PD.TIPO_OPERACION");
            sb.Append(" INNER JOIN GPS_DIRECCION D ON D.ID = PD.ID_DIRECCION");
            sb.Append(" INNER JOIN GPS.GPS_TIPODOMICILIO TD ON TD.ID = D.ID_TIPODOMICILIO");
            sb.Append($" WHERE PD.ID_PEDIDO IN ({pedidos}) AND PD.ID_ESTADO <> 95 ");

            dt = _OracleDao.ExcecuteAdapterFill(sb.ToString(), parametros);
            _OracleDao.CerrarConexion();
            return dt;
        }
    }
}
