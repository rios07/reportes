using Infraestructure.track3.Models.pedido;
using Infraestructure.track3.Models.planificacion;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Text;
using track3_api_reportes_core.Infraestructura.Dao;
using track3_api_reportes_core.Infraestructura.Interfaces;

namespace track3_api_reportes_core.Infraestructura.Repositorios
{
    public class PedidosRepositorio: IPedidosRepositorio
    {

        private readonly IOracleDao _OracleDao;
        public static IConfiguration _configuration;
        public string ConexTrack;

        public PedidosRepositorio(IOracleDao OracleDao, IConfiguration config)
        {
            _configuration = config;
            _OracleDao = OracleDao;
            ConexTrack = _configuration.GetConnectionString("Track3");
        }

        public DataTable getHistoricoPedidos(string pedidos,long idHojaRuta)
        {
             DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            List<OracleParameter> parametros = new List<OracleParameter>();
            _OracleDao.CrearConnection(ConexTrack);


            sb.Append(" SELECT PREF.ID ID_PEDIDO_REF_AUDIT, PREF.ID_PEDIDO , PREF.ID_PEDIDOREF,  PREF.NRO_REFERENCIA, PREF.CRE, PREF.CAE,  ");
            sb.Append("  E.ID ID_ESTADO, E.NOMBRE ESTADO , OP.ID TIPO_OPERACION ,OP.NOMBRE OPERACION, PREF.FECHA_EVENTO,PREF.USUARIO, PREF.ID_HOJARUTA ");
            sb.Append("  FROM   GPS_PEDIDOREF_AUDIT PREF  ");
            sb.Append("  INNER JOIN GPS_ESTADO E ON E.ID = PREF.ID_ESTADO ");
            sb.Append("  INNER JOIN GPS_ESTADO OP ON OP.id = PREF.TIPO_OPERACION ");
            sb.Append("  WHERE PREF.ID_HOJARUTA = :ID_HOJARUTA   ");
            sb.Append($" AND PREF.ID_PEDIDO IN ({pedidos}) ");
            sb.Append(" ORDER BY PREF.STAMP ");

            parametros.Add(new OracleParameter(":ID_HOJARUTA", idHojaRuta));
            dt = _OracleDao.ExcecuteAdapterFill(sb.ToString(), parametros);
            _OracleDao.CerrarConexion();
            return dt;
        }

        public DataTable getCanalBandaHoraria(int canalId)
        {
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            List<OracleParameter> parametros = new List<OracleParameter>();
            _OracleDao.CrearConnection(ConexTrack);

            sb.Append("  SELECT BH.ID ID_BANDAHORARIA, BH.DESCRIPCION BANDA_HORARIA, BH.HORADESDE, BH.HORAHASTA, BH.ORDEN, BH.COLOR COLOR, ");
            sb.Append(" C.ID ID_CANAL, C.SUCURSAL SUCURSAL, C.NOMBRE CANAL, C.CONEXION_BD, C.BD, C.USUARIO, C.PASSWORD, C.TIPO CANAL_TIPO, ");
            sb.Append(" DC.ID CANAL_ID_DIRECCION, DC.CALLE CANAL_CALLE, DC.NUMERO CANAL_ALTURA_CALLE, ");
            sb.Append(" DC.PISO CANAL_PISO, DC.PISO_DEPTO CANAL_PISO_DPTO, DC.CODIGOPOSTAL CANAL_CODIGO_POSTAL, ");
            sb.Append(" DC.LOCALIDAD CANAL_LOCALIDAD, DC.COORDENADAX CANAL_LATITUDE, DC.COORDENADAY CANAL_LONGITUDE ");
            sb.Append(" FROM GPS_BANDAHORARIA BH ");
            sb.Append(" INNER JOIN GPS_CANAL C ON C.ID = BH.ID_CANAL ");
            sb.Append(" INNER JOIN GPS_DIRECCION DC ON DC.ID = C.ID_DIRECCION ");
            sb.Append(" WHERE ID_CANAL = :ID_CANAL ");

            parametros.Add(new OracleParameter(":ID_CANAL", canalId));

            dt = _OracleDao.ExcecuteAdapterFill(sb.ToString(), parametros);
            _OracleDao.CerrarConexion();
            return dt;
        }

        public DataTable getContenedores(string idPedidos)
        {
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            List<OracleParameter> parametros = new List<OracleParameter>();
            _OracleDao.CrearConnection(ConexTrack);

            sb.Append("SELECT PC.ID ID_PEDIDOCONTENEDOR, PC.ID_PEDIDO, PC.ID_CONTENEDOR, PC.NRO_CONTENEDOR_REF , PC.NRO_SERIE_REF");
            sb.Append(" FROM GPS_PEDIDOCONTENEDOR PC ");
            sb.Append(" WHERE PC.ID_CONTENEDOR=1 AND PC.ID_CONTENEDOR=1 AND PC.ACTIVO='S' ");
            sb.Append($" AND PC.ID_PEDIDO IN ({idPedidos})   ");
            sb.Append(" ORDER BY NRO_CONTENEDOR_REF DESC");

            dt = _OracleDao.ExcecuteAdapterFill(sb.ToString(), parametros);
            _OracleDao.CerrarConexion();
            return dt;
        }

        public DataTable obtenerBandaHorariaMayoritaria(string idHojaRutas)
        {
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            List<OracleParameter> parametros = new List<OracleParameter>();
            _OracleDao.CrearConnection(ConexTrack);

            sb.Append(" SELECT ID_HOJARUTA ,  ID_BANDAHORARIA, BANDA_HORARIA, COLOR ,HORADESDE, HORAHASTA, ORDEN, ID_CANAL,SUMA ");
            sb.Append(" FROM ( ");
            sb.Append("   SELECT HRD.ID_HOJARUTA ,bh.id ID_BANDAHORARIA,bh.descripcion BANDA_HORARIA, bh.color,BH.HORADESDE, BH.HORAHASTA, bh.orden,BH.ID_CANAL, COUNT(1) suma ");
            sb.Append("   FROM GPS_HOJARUTADETALLE HRD ");
            sb.Append("   INNER JOIN GPS_PEDIDO p on p.ID = HRD.ID_PEDIDO ");
            sb.Append("   INNER JOIN GPS_BANDAHORARIA bh on bh.id = p.ID_BANDA_HORARIA ");
            sb.Append($"   WHERE HRD.ID_HOJARUTA IN ({idHojaRutas})  ");
            sb.Append("   GROUP BY HRD.ID_HOJARUTA,P.ID_BANDA_HORARIA,bh.id,bh.descripcion, bh.color,BH.HORADESDE, BH.HORAHASTA, bh.orden,BH.ID_CANAL  )   ");
            sb.Append(" ORDER BY SUMA desc ");


            dt = _OracleDao.ExcecuteAdapterFill(sb.ToString(), parametros);
            _OracleDao.CerrarConexion();
            return dt;
        }
  
        public DataTable getPedidoDetalle(long id)
        {
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            List<OracleParameter> parametros = new List<OracleParameter>();
            _OracleDao.CrearConnection(ConexTrack);

            sb.Append(" SELECT PD.ID, PD.PLU, PD.DESCRIPCION DESC_PLU, PD.CANTIDAD, PD.PESO, PD.UMP, PD.VOLUMEN, PD.UMV, ");
            sb.Append(" C.ID ID_CONTENEDOR, C.DESCRIPCION CONTENEDOR, E.ID ID_ESTADO, E.NOMBRE ESTADO, O.ID ID_OPERACION, O.NOMBRE OPERACION, ");
            sb.Append(" D.ID ID_DIRECCION, D.CALLE CALLE, D.NUMERO ALTURA_CALLE, D.CODIGOPOSTAL CODIGO_POSTAL, D.PROVINCIA PROVINCIA, D.COORDENADAX LATITUDE, D.COORDENADAY LONGITUDE, ");
            sb.Append(" D.LOCALIDAD LOCALIDAD, D.PISO PISO, D.PISO_DEPTO PISO_DPTO, PD.NRO_RESERVA NRO_RESERVA, PD.NRO_RESERVA_ORI NRO_RESERVA_ORI, PD.OBSERVACIONES OBSERVACIONES, TD.ID ID_TIPODOMICILIO, TD.NOMBRE TIPODOMICILIONOMBRE ");
            sb.Append(" FROM GPS_PEDIDODETALLE PD ");
            sb.Append(" INNER JOIN GPS_CONTENEDOR C ON C.ID = PD.ID_CONTENEDOR ");
            sb.Append(" INNER JOIN GPS_ESTADO E ON E.ID = PD.ID_ESTADO ");
            sb.Append(" INNER JOIN GPS_ESTADO O ON O.ID = PD.TIPO_OPERACION ");
            sb.Append(" INNER JOIN GPS_DIRECCION D ON D.ID = PD.ID_DIRECCION ");
            sb.Append(" INNER JOIN GPS.GPS_TIPODOMICILIO TD ON TD.ID = D.ID_TIPODOMICILIO ");
            sb.Append(" WHERE PD.ID_PEDIDO = :ID ");

            parametros.Add(new OracleParameter(":ID", id));

            dt = _OracleDao.ExcecuteAdapterFill(sb.ToString(), parametros);
            _OracleDao.CerrarConexion();
            return dt;
        }
    


        public DataTable getPedidoDetalleCD(long idPedido, long idHojaRuta)
        {
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            List<OracleParameter> parametros = new List<OracleParameter>();
            _OracleDao.CrearConnection(ConexTrack);


            //Retorna los mismos campos que digital, pero la busqueda no la realiza por pedido, sino que por (x,y) del pedido para una determinada id_hojaruta - 26/01/2024
            sb.Append(" SELECT PD.ID, PD.PLU, PD.DESCRIPCION DESC_PLU, PD.CANTIDAD, PD.PESO, PD.UMP, PD.VOLUMEN, PD.UMV, ");
            sb.Append(" C.ID ID_CONTENEDOR, C.DESCRIPCION CONTENEDOR, E.ID ID_ESTADO, E.NOMBRE ESTADO, ");
            sb.Append(" PDA.TIPO_OPERACION ID_OPERACION, O.NOMBRE OPERACION, D.ID ID_DIRECCION, D.CALLE CALLE, D.NUMERO ALTURA_CALLE, ");
            sb.Append(" D.CODIGOPOSTAL CODIGO_POSTAL, D.PROVINCIA PROVINCIA, D.COORDENADAX LATITUDE, D.COORDENADAY LONGITUDE, ");
            sb.Append(" D.LOCALIDAD LOCALIDAD, D.PISO PISO, D.PISO_DEPTO PISO_DPTO, PD.NRO_RESERVA NRO_RESERVA, ");
            sb.Append(" PD.NRO_RESERVA_ORI NRO_RESERVA_ORI, PD.OBSERVACIONES OBSERVACIONES, ");
            sb.Append(" TD.ID ID_TIPODOMICILIO ,TD.NOMBRE TIPODOMICILIONOMBRE,PD.CANTIDAD_COMPONENTE ");
            sb.Append(" FROM GPS_PEDIDODETALLE PD ");
            sb.Append(" INNER JOIN GPS_HOJARUTADETALLE HRD ON PD.ID_PEDIDO = HRD.ID_PEDIDO ");
            sb.Append(" INNER JOIN GPS_CONTENEDOR C ON C.ID = PD.ID_CONTENEDOR ");
            sb.Append(" INNER JOIN GPS_ESTADO E ON E.ID = PD.ID_ESTADO ");
            sb.Append(" INNER JOIN GPS_PEDIDOREF_AUDIT PDA ON ");
            sb.Append(" (PDA.NRO_REFERENCIA = PD.NRO_RESERVA AND PDA.ID_PEDIDO = PD.ID_PEDIDO AND PDA.ID_HOJARUTA = HRD.ID_HOJARUTA) ");
            sb.Append(" INNER JOIN GPS_ESTADO O ON O.ID = PDA.TIPO_OPERACION ");
            sb.Append(" INNER JOIN GPS_DIRECCION D ON D.ID = PD.ID_DIRECCION ");
            sb.Append(" INNER JOIN GPS.GPS_TIPODOMICILIO TD ON TD.ID = D.ID_TIPODOMICILIO ");
            sb.Append(" WHERE (HRD.COORDENADAX, HRD.COORDENADAY) IN( ");
            sb.Append(" SELECT HRD.COORDENADAX, HRD.COORDENADAY ");
            sb.Append(" FROM GPS_HOJARUTADETALLE HRD, ");
            sb.Append(" GPS_PEDIDODETALLE PD ");
            sb.Append(" WHERE PD.ID_PEDIDO = HRD.ID_PEDIDO AND HRD.ID_ESTADO <> 8 ");
            sb.Append(" AND PD.ID_PEDIDO = :ID ) ");
            sb.Append(" AND PD.ID_ESTADO <> 95 AND HRD.ID_ESTADO <> 8 AND HRD.ID_HOJARUTA = :ID_HOJARUTA ");
            sb.Append(" GROUP BY  PD.ID, PD.PLU, PD.DESCRIPCION, PD.CANTIDAD, PD.PESO, PD.UMP, PD.VOLUMEN, PD.UMV, ");
            sb.Append(" C.ID, C.DESCRIPCION, E.ID, E.NOMBRE, PDA.TIPO_OPERACION, O.NOMBRE, ");
            sb.Append(" D.ID, D.CALLE, D.NUMERO, D.CODIGOPOSTAL, D.PROVINCIA, D.COORDENADAX, D.COORDENADAY, ");
            sb.Append(" D.LOCALIDAD, D.PISO, D.PISO_DEPTO, PD.NRO_RESERVA, PD.NRO_RESERVA_ORI, PD.OBSERVACIONES, TD.ID, TD.NOMBRE, PD.CANTIDAD_COMPONENTE ");

            parametros.Add(new OracleParameter(":ID", idPedido));
            parametros.Add(new OracleParameter(":ID_HOJARUTA", idHojaRuta));

            dt = _OracleDao.ExcecuteAdapterFill(sb.ToString(), parametros);
            _OracleDao.CerrarConexion();
            return dt;
        }


        public DataTable getPedido(long id)
        {
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            List<OracleParameter> parametros = new List<OracleParameter>();
            _OracleDao.CrearConnection(ConexTrack);

            sb.Append(" SELECT P.ID, P.FECHA,P.OBSERVACION, NRO_PEDIDO_REF RESERVA, C.ID ID_CLIENTE, C.APELLIDO, C.NOMBRE, C.DNI DOCUMENTO, C.TELEFONO, C.CELULAR, C.MAIL MAIL, ");
            sb.Append(" E.ID ID_ESTADO, E.NOMBRE ESTADO, D.ID ID_DIRECCION, D.CALLE, D.NUMERO ALTURA_CALLE, D.PISO, D.PISO_DEPTO PISO_DPTO, D.CODIGOPOSTAL CODIGO_POSTAL, D.LOCALIDAD, D.PROVINCIA, D.COORDENADAX LATITUDE, D.COORDENADAY LONGITUDE, ");
            sb.Append(" S.ID_SUC ID_SUCURSAL, S.DESC_SUC SUCURSAL, T.ID ID_TIPO, T.NOMBRE TIPO, ");
            sb.Append(" CA.ID ID_CANAL, CA.SUCURSAL SUCURSAL, CA.NOMBRE CANAL, CA.CONEXION_BD, CA.BD, CA.USUARIO, CA.PASSWORD, CA.TIPO CANAL_TIPO, DC.ID CANAL_ID_DIRECCION, DC.CALLE CANAL_CALLE, DC.NUMERO CANAL_ALTURA_CALLE, DC.PISO CANAL_PISO, DC.PISO_DEPTO CANAL_PISO_DPTO, DC.CODIGOPOSTAL CANAL_CODIGO_POSTAL, DC.LOCALIDAD CANAL_LOCALIDAD, DC.COORDENADAX CANAL_LATITUDE, DC.COORDENADAY CANAL_LONGITUDE, ");
            sb.Append(" B.ID ID_BANDAHORARIA, B.DESCRIPCION BANDA_HORARIA, B.HORADESDE, B.HORAHASTA, B.ORDEN, B.COLOR, ");
            sb.Append(" (SELECT COUNT(PLU) FROM GPS_PEDIDODETALLE WHERE ID_PEDIDO = P.ID) ITEMS, TD.ID ID_TIPODOMICILIO, TD.NOMBRE TIPODOMICILIONOMBRE, ");
            sb.Append(" P.TERMINAL, P.TRANSACCION, P.IMPORTE_TOTAL, CA.GENERA_COT , PF.PEDIDO_FASTLINE FASTLINE ,PF.PEDIDO_ORIGINAL PED_ORIGINAL ");
            sb.Append(" FROM GPS_PEDIDO P ");
            sb.Append(" INNER JOIN GPS_CLIENTE C ON C.ID = P.ID_CLIENTE ");
            sb.Append(" INNER JOIN GPS_ESTADO E ON E.ID = P.ID_ESTADO ");
            sb.Append(" INNER JOIN GPS_DIRECCION D ON D.ID = P.ID_DIRECCION_ENTREGA ");
            sb.Append(" INNER JOIN LT_SUC_IPAD S ON S.ID_SUC = P.ID_SUCURSAL ");
            sb.Append(" INNER JOIN GPS_PEDIDOTIPO T ON T.ID = P.ID_TIPO ");
            sb.Append(" INNER JOIN GPS_CANAL CA ON CA.ID = P.ID_CANAL ");
            sb.Append(" INNER JOIN GPS_DIRECCION DC ON DC.ID = CA.ID_DIRECCION ");
            sb.Append(" INNER JOIN GPS_BANDAHORARIA B ON B.ID =  P.ID_BANDA_HORARIA ");
            sb.Append(" INNER JOIN GPS.GPS_TIPODOMICILIO TD ON TD.ID = D.ID_TIPODOMICILIO ");
            sb.Append(" LEFT JOIN  GPS_PEDIDOFASTLINE PF ON PF.ID_PEDIDO=P.ID ");
            sb.Append(" WHERE P.ID = :ID ");

            parametros.Add(new OracleParameter(":ID", id));

            dt = _OracleDao.ExcecuteAdapterFill(sb.ToString(), parametros);
            _OracleDao.CerrarConexion();
            return dt;
        }

        public DataTable getPedidoSearch(string nroRef)
        {
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            List<OracleParameter> parametros = new List<OracleParameter>();
            _OracleDao.CrearConnection(ConexTrack);

            sb.Append(" SELECT P.ID ID_PEDIDO, P.FECHA, H.FECHA_PLAN, P.ID_SUCURSAL SUCURSAL, BH.DESCRIPCION BANDAHORARIA, BH.HORADESDE, ");
            sb.Append(" BH.HORAHASTA, P.NRO_PEDIDO_REF,D.ID ID_DIRECCION, D.CALLE, D.COORDENADAX LATITUDE, D.COORDENADAY LONGITUDE, ");
            sb.Append(" D.NUMERO ALTURA_CALLE, D.PISO, D.PISO_DEPTO PISO_DPTO, D.LOCALIDAD, D.CODIGOPOSTAL CODIGO_POSTAL, D.PROVINCIA PROVINCIA, D.ID_TIPODOMICILIO, ");
            sb.Append(" CLI.DNI, CLI.APELLIDO, CLI.NOMBRE, CLI.TELEFONO, H.WAYBILLID WAYBILLID, EH.ID ID_ESTADO_HOJARUTA, EH.NOMBRE ESTADO_HOJARUTA, H.ID ID_HOJARUTA, ");
            sb.Append(" P.TERMINAL, P.TRANSACCION, P.IMPORTE_TOTAL, EP.NOMBRE ESTADO_PEDIDO, ");
            sb.Append(" C.ID ID_CANAL, C.NOMBRE CANAL, C.BD, C.CONEXION_BD, C.USUARIO, C.PASSWORD, C.TIPO CANAL_TIPO, C.GENERA_COT ");
            sb.Append(" FROM GPS_PEDIDO P ");
            sb.Append(" INNER JOIN GPS_PEDIDOREF PR ON PR.NRO_REFERENCIA = P.NRO_PEDIDO_REF ");
            sb.Append(" INNER JOIN GPS_ESTADO EP ON EP.ID = PR.ID_ESTADO ");
            sb.Append(" INNER JOIN GPS.GPS_CLIENTE CLI ON CLI.ID = P.ID_CLIENTE ");
            sb.Append(" INNER JOIN GPS.GPS_DIRECCION D ON D.ID = P.ID_DIRECCION_ENTREGA ");
            sb.Append(" INNER JOIN GPS.GPS_BANDAHORARIA BH ON BH.ID = P.ID_BANDA_HORARIA ");
            sb.Append(" INNER JOIN GPS.GPS_CANAL C ON P.ID_CANAL = C.ID ");
            sb.Append(" LEFT JOIN GPS.GPS_HOJARUTADETALLE DET ON DET.ID_PEDIDO = P.ID ");
            sb.Append(" LEFT JOIN GPS.GPS_HOJARUTA H ON H.ID = DET.ID_HOJARUTA ");
            sb.Append(" LEFT JOIN GPS.GPS_ESTADO EH ON EH.ID = H.ID_ESTADO ");
            sb.Append(" WHERE P.NRO_PEDIDO_REF = :NRO_REFERENCIA ");
            sb.Append(" UNION ALL ");
            sb.Append(" SELECT P.ID ID_PEDIDO, P.FECHA, H.FECHA_PLAN, P.ID_SUCURSAL SUCURSAL, BH.DESCRIPCION BANDAHORARIA, BH.HORADESDE, ");
            sb.Append(" BH.HORAHASTA, P.NRO_PEDIDO_REF,D.ID ID_DIRECCION, D.CALLE, D.COORDENADAX LATITUDE, D.COORDENADAY LONGITUDE, ");
            sb.Append(" D.NUMERO ALTURA_CALLE, D.PISO, D.PISO_DEPTO PISO_DPTO, D.LOCALIDAD, D.CODIGOPOSTAL CODIGO_POSTAL, D.PROVINCIA PROVINCIA, D.ID_TIPODOMICILIO, ");
            sb.Append(" CLI.DNI, CLI.APELLIDO, CLI.NOMBRE, CLI.TELEFONO, H.WAYBILLID WAYBILLID, EH.ID ID_ESTADO_HOJARUTA, EH.NOMBRE ESTADO_HOJARUTA, H.ID ID_HOJARUTA, ");
            sb.Append(" P.TERMINAL, P.TRANSACCION, P.IMPORTE_TOTAL, EP.NOMBRE ESTADO_PEDIDO, ");
            sb.Append(" C.ID ID_CANAL, C.NOMBRE CANAL, C.BD, C.CONEXION_BD, C.USUARIO, C.PASSWORD, C.TIPO CANAL_TIPO, C.GENERA_COT ");
            sb.Append(" FROM GPS_PEDIDO P ");
            sb.Append(" INNER JOIN GPS.GPS_PEDIDOREF_SERIE SPR ON SPR.NRO_REFERENCIA = P.NRO_PEDIDO_REF ");
            sb.Append(" INNER JOIN GPS_PEDIDOREF PR ON PR.NRO_REFERENCIA = P.NRO_PEDIDO_REF ");
            sb.Append(" INNER JOIN GPS_ESTADO EP ON EP.ID = PR.ID_ESTADO ");
            sb.Append(" INNER JOIN GPS.GPS_CLIENTE CLI ON CLI.ID = P.ID_CLIENTE ");
            sb.Append(" INNER JOIN GPS.GPS_DIRECCION D ON D.ID = P.ID_DIRECCION_ENTREGA ");
            sb.Append(" INNER JOIN GPS.GPS_BANDAHORARIA BH ON BH.ID = P.ID_BANDA_HORARIA ");
            sb.Append(" INNER JOIN GPS.GPS_CANAL C ON P.ID_CANAL = C.ID ");
            sb.Append(" LEFT JOIN GPS.GPS_HOJARUTADETALLE DET ON DET.ID_PEDIDO = P.ID ");
            sb.Append(" LEFT JOIN GPS.GPS_HOJARUTA H ON H.ID = DET.ID_HOJARUTA ");
            sb.Append(" LEFT JOIN GPS.GPS_ESTADO EH ON EH.ID = H.ID_ESTADO ");
            sb.Append(" WHERE SPR.SERIE = :SERIE ");
            sb.Append(" ORDER BY ID_HOJARUTA ASC ");

            parametros.Add(new OracleParameter(":NRO_REFERENCIA", nroRef));
            parametros.Add(new OracleParameter(":SERIE", nroRef));

            dt = _OracleDao.ExcecuteAdapterFill(sb.ToString(), parametros);
            _OracleDao.CerrarConexion();
            return dt;
        }
    }
}
