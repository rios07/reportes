using Infraestructure.track3.Models.planificacion;
using Microsoft.Extensions.Primitives;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Text;
using track3_api_reportes_core.Aplicacion.Filtros;
using track3_api_reportes_core.Infraestructura.Dao;
using track3_api_reportes_core.Infraestructura.Interfaces;
using track3_api_reportes_core.Middleware.Dto;

namespace track3_api_reportes_core.Infraestructura.Repositorios
{
    public class HojaRutaRepository: IHojaRutaRepository
    {
        private readonly IOracleDao _OracleDao;
        public static IConfiguration _configuration;
        public string ConexTrack;
        public HojaRutaRepository(IOracleDao OracleDao, IConfiguration config)
        {
            _configuration = config;
            _OracleDao = OracleDao;
            ConexTrack = _configuration.GetConnectionString("Track3");
        }
        public DataTable getHojaruta(long idHojaRuta)
        {

            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            List<OracleParameter> parametros = new List<OracleParameter>();
            _OracleDao.CrearConnection(ConexTrack);

            sb.Append("  SELECT H.ID, H.WAYBILLID, H.STAMP FECHA, H.FECHA_PLAN FECHA_PLAN, H.FECHA_SALIDA FECHA_SALIDA, H.FECHA_CIERRE FECHA_CIERRE, H.PRIORIDAD, E.ID ID_ESTADO, ");
            sb.Append("  E.NOMBRE ESTADO, H.DESCRIPCION DESCRIPCION, H.DISTANCIA_TOTAL, H.TIEMPO_ESTIMADO_TOTAL, DO.CALLE CALLE_ORIGEN, DO.NUMERO NUMERO_ORIGEN, ");
            sb.Append("  DO.CODIGOPOSTAL CODIGOPOSTAL_ORIGEN, DO.COORDENADAX COORDENADAX_ORIGEN, DO.COORDENADAY COORDENADAY_ORIGEN, DO.LOCALIDAD LOCALIDAD_ORIGEN, ");
            sb.Append("  DD.CALLE CALLE_DESTINO, DD.NUMERO NUMERO_DESTINO,DD.CODIGOPOSTAL CODIGOPOSTAL_DESTINO, DD.COORDENADAX COORDENADAX_DESTINO, DD.COORDENADAY COORDENADAY_DESTINO, ");
            sb.Append("  DO.PISO PISO_ORIGEN, DO.PISO_DEPTO PISO_DEPTO_ORIGEN, DD.LOCALIDAD LOCALIDAD_DESTINO, DD.PISO PISO_DESTINO, DD.PISO_DEPTO PISO_DEPTO_DESTINO,  ");
            sb.Append("  C.ID ID_CANAL, C.SUCURSAL SUCURSAL, C.NOMBRE CANAL, C.CONEXION_BD, C.BD, C.USUARIO, C.PASSWORD, ");
            sb.Append("  C.TIPO CANAL_TIPO,C.SUCURSAL SUCURSAL, C.GENERA_COT GENERA_COT,DC.ID CANAL_ID_DIRECCION, DC.CALLE CANAL_CALLE, DC.NUMERO CANAL_ALTURA_CALLE, ");
            sb.Append("  DC.CODIGOPOSTAL CANAL_CODIGO_POSTAL, DC.COORDENADAX CANAL_LATITUDE,DC.COORDENADAY CANAL_LONGITUDE, DC.LOCALIDAD CANAL_LOCALIDAD, DC.PISO CANAL_PISO, ");
            sb.Append("  DC.PISO_DEPTO CANAL_PISO_DEPTO,HRM.ID ID_HOJARUTAMOVIL,HRM.PATENTE PATENTE ,HRM.CAPACIDAD CAPACIDAD,HRM.APILABILIDAD APILABILIDAD,HRM.PROVEEDOR,M.ID ID_MOVILTIPO, ");
            sb.Append("  M.DESCRIPCION TIPO, M.PESO PESO, M.UMP UMP, M.VOLUMEN VOLUMEN, M.UMV UMV ,MO.ID ID_MOVIL , MO.NUMERO ,P.ID PROVEEDOR_ID, p.cuit CUIT, p.nombre NOMBRE_PROVEEDOR, ");
            sb.Append("  A.ID ID_ANDEN,A.ANDEN ANDEN, A.HORA_DESDE ANDEN_HORA_DESDE,A.HORA_HASTA ANDEN_HORA_HASTA,A.DESCRIPCION ANDEN_DESCRIPCION ");
            sb.Append("  FROM GPS_HOJARUTA H ");
            sb.Append("  LEFT JOIN GPS_MOVILTIPO M ON M.ID = H.ID_MOVILTIPO ");
            sb.Append("  LEFT JOIN GPS_HOJARUTAMOVIL HRM ON hrm.id_hojaruta =H.ID AND HRM.ACTIVO='S' ");
            sb.Append("  LEFT JOIN  GPS_MOVIL MO ON mo.patente=hrm.patente ");
            sb.Append("  INNER JOIN GPS_ESTADO E ON E.ID = H.ID_ESTADO ");
            sb.Append("  INNER JOIN GPS_DIRECCION DO ON DO.ID = H.ID_ORIGEN ");
            sb.Append("  INNER JOIN GPS_DIRECCION DD ON DD.ID = H.ID_DESTINO ");
            sb.Append("  LEFT JOIN GPS_HOJARUTAPROVEEDOR HRP ON HRP.ID_HOJARUTA =H.ID ");
            sb.Append("  LEFT JOIN GPS_PROVEEDOR P ON P.ID = HRP.ID_PROVEEDOR ");
            sb.Append("  INNER JOIN GPS_CANAL C ON C.ID = H.ID_CANAL ");
            sb.Append("  INNER JOIN GPS_DIRECCION DC ON DC.ID = C.ID_DIRECCION ");
            sb.Append("  LEFT JOIN GPS_HOJARUTAANDEN A ON A.ID_HOJARUTA = H.ID ");
            sb.Append("  WHERE H.ID=:ID_HOJARUTA ");

            parametros.Add(new OracleParameter(":ID_HOJARUTA", idHojaRuta));
            dt = _OracleDao.ExcecuteAdapterFill(sb.ToString(), parametros);
            _OracleDao.CerrarConexion();
            return dt;

        }

        public DataTable getPedidoDetalle(long id)
        {
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            List<OracleParameter> parametros = new List<OracleParameter>();

            sb.Append("SELECT PD.ID, PD.PLU, PD.DESCRIPCION DESC_PLU, PD.CANTIDAD, PD.PESO, PD.UMP, PD.VOLUMEN, PD.UMV,");
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
            sb.Append(" WHERE PD.ID_PEDIDO = :ID AND PD.ID_ESTADO <> 95 ");

            parametros.Add(new OracleParameter(":ID", id));

            dt = _OracleDao.ExcecuteAdapterFill(sb.ToString(), parametros);
            _OracleDao.CerrarConexion();

            return dt;
        }


        public DataTable getHojaRutaDetallePedidos(long idHojaRuta)
        {
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            List<OracleParameter> parametros = new List<OracleParameter>();
            _OracleDao.CrearConnection(ConexTrack);

            sb.Append("SELECT HRD.ID,LLEGADA_ESTIMADA, HRD.LLEGADA_DESCRIPCION, HRD.DISTANCIA_OPTIMA, HRD.DISTANCIA_DESCRIPCION, HRD.TIEMPO_OPTIMO, HRD.TIEMPO_DESCRIPCION,   ");
            sb.Append("  HRD.ORDEN, HRD.DIRECCION_INICIO, D.CALLE || ' ' || D.NUMERO || CASE WHEN D.PISO IS NOT NULL THEN ' ' || D.PISO END ||   ");
            sb.Append("  CASE WHEN D.PISO_DEPTO IS NOT NULL THEN ' ' || D.PISO_DEPTO END || ', ' || D.CODIGOPOSTAL || ' ' || D.LOCALIDAD DIRECCION_FIN,   ");
            sb.Append("  HRD.COORDENADAX, HRD.COORDENADAY, E.ID ID_EQUIVALENCIAHRDETALLE , E.DISTANCIALINEAL, EQ.ID ID_EQUIVALENCIA ,EQ.DISTANCIA_DESDE,EQ.DISTANCIA_HASTA,   ");
            sb.Append("  EQ.UNIDADMEDIDA,EQ.VALOR,EQ.DESCRIPCION,  PR.ID ID_PROPIEDAD ,PR.NOMBRE PROPIEDAD, PT.ID ID_PROPIEDADTIPO , PT.NOMBRE PROPIEDADTIPO ,   ");
            sb.Append("  P.ID ID_PEDIDO, P.NRO_PEDIDO_REF NRO_PEDIDO_REF, PREF.fecha_evento,pref.fecha_evento,P.OBSERVACION, C.ID ID_CLIENTE, C.APELLIDO, C.NOMBRE, C.DNI DOCUMENTO, C.TELEFONO, C.CELULAR, C.MAIL MAIL,  ");
            sb.Append("  ES.ID ID_ESTADO, ES.NOMBRE ESTADO, DE.ID ID_DIRECCION, DE.CALLE, DE.NUMERO ALTURA_CALLE, DE.PISO, DE.PISO_DEPTO PISO_DPTO, DE.CODIGOPOSTAL CODIGO_POSTAL,   ");
            sb.Append("  DE.LOCALIDAD, DE.PROVINCIA, DE.COORDENADAX LATITUDE, DE.COORDENADAY LONGITUDE,   ");
            sb.Append("  T.ID ID_TIPO, T.NOMBRE TIPO,B.ID ID_BANDAHORARIA, B.DESCRIPCION BANDA_HORARIA, B.HORADESDE, B.HORAHASTA, B.ORDEN, B.COLOR,   ");
            sb.Append("   TD.ID ID_TIPODOMICILIO, TD.NOMBRE TIPODOMICILIONOMBRE,    ");
            sb.Append("  P.TERMINAL, P.TRANSACCION, P.IMPORTE_TOTAL, PF.PEDIDO_FASTLINE FASTLINE ,PF.PEDIDO_ORIGINAL PED_ORIGINAL ,PD.TIPO_OPERACION ,COUNT(PD.ID) ITEMS ,");
            sb.Append("  PREF.ID ID_PEDIDO_REF,   PREF.NRO_REFERENCIA, pref.fecha_evento,PREF.CRE, PREF.CAE, EDO.ID ID_ESTADO_REF, EDO.NOMBRE ESTADO_REF , OP.ID TIPO_OPERACION_REF ,OP.NOMBRE OPERACION");
            sb.Append("  FROM GPS_HOJARUTADETALLE HRD   ");
            sb.Append("  INNER JOIN GPS_PEDIDO P ON P.ID = HRD.ID_PEDIDO  ");
            sb.Append("  INNER JOIN GPS_PEDIDOREF PREF ON PREF.ID_PEDIDO=P.ID");
            sb.Append("  INNER JOIN GPS_ESTADO EDO ON EDO.ID = PREF.ID_ESTADO");
            sb.Append("  INNER JOIN GPS_ESTADO OP ON OP.id = PREF.TIPO_OPERACION");
            sb.Append("  INNER JOIN GPS_PEDIDODETALLE PD ON P.ID = PD.ID_PEDIDO     ");
            sb.Append("  INNER JOIN GPS_DIRECCION D ON D.ID = P.ID_DIRECCION_ENTREGA   ");
            sb.Append("  LEFT JOIN GPS_EQUIVALENCIA_HRDETALLE E ON HRD.ID=E.ID_HOJARUTADETALLE AND E.ACTIVO='S'    ");
            sb.Append("  LEFT JOIN GPS_EQUIVALENCIA EQ ON EQ.ID=E.ID_EQUIVALENCIA     ");
            sb.Append("  LEFT JOIN GPS_PROPIEDAD PR  ON EQ.ID_PROPIEDAD =PR.ID     ");
            sb.Append("  LEFT JOIN GPS.GPS_PROPIEDADTIPO PT ON PT.ID=PR.ID_PROPIEDADTIPO     ");
            sb.Append("  INNER JOIN GPS_CLIENTE C ON C.ID = P.ID_CLIENTE   ");
            sb.Append("  INNER JOIN GPS_ESTADO ES ON ES.ID = P.ID_ESTADO   ");
            sb.Append("  INNER JOIN GPS_DIRECCION DE ON DE.ID = P.ID_DIRECCION_ENTREGA   ");
            sb.Append("  INNER JOIN GPS_PEDIDOTIPO T ON T.ID = P.ID_TIPO   ");
            sb.Append("  INNER JOIN GPS_BANDAHORARIA B ON B.ID =  P.ID_BANDA_HORARIA   ");
            sb.Append("  INNER JOIN GPS.GPS_TIPODOMICILIO TD ON TD.ID = DE.ID_TIPODOMICILIO   ");
            sb.Append("  LEFT JOIN  GPS_PEDIDOFASTLINE PF ON PF.ID_PEDIDO=P.ID   ");
            sb.Append("  WHERE HRD.ID_HOJARUTA = :ID_HOJARUTA  ");
            sb.Append("  AND HRD.ID_ESTADO = 7   ");
            sb.Append("  GROUP BY HRD.ID,LLEGADA_ESTIMADA, HRD.LLEGADA_DESCRIPCION, HRD.DISTANCIA_OPTIMA, HRD.DISTANCIA_DESCRIPCION, HRD.TIEMPO_OPTIMO, HRD.TIEMPO_DESCRIPCION,   ");
            sb.Append("  HRD.ORDEN, HRD.DIRECCION_INICIO, D.CALLE || ' ' || D.NUMERO || CASE WHEN D.PISO IS NOT NULL THEN ' ' || D.PISO END ||   ");
            sb.Append("  CASE WHEN D.PISO_DEPTO IS NOT NULL THEN ' ' || D.PISO_DEPTO END || ', ' || D.CODIGOPOSTAL || ' ' || D.LOCALIDAD ,   ");
            sb.Append("  HRD.COORDENADAX, HRD.COORDENADAY, E.ID  , E.DISTANCIALINEAL, EQ.ID  ,EQ.DISTANCIA_DESDE,EQ.DISTANCIA_HASTA,   ");
            sb.Append("  EQ.UNIDADMEDIDA,EQ.VALOR,EQ.DESCRIPCION,  PR.ID  ,PR.NOMBRE , PT.ID  , PT.NOMBRE  ,   ");
            sb.Append("  P.ID , P.NRO_PEDIDO_REF ,  PREF.fecha_evento,P.OBSERVACION, C.ID  , C.APELLIDO, C.NOMBRE, C.DNI  , C.TELEFONO, C.CELULAR, C.MAIL  ,    ");
            sb.Append("  ES.ID  , ES.NOMBRE  , DE.ID  , DE.CALLE, DE.NUMERO , DE.PISO, DE.PISO_DEPTO , DE.CODIGOPOSTAL ,   ");
            sb.Append("  DE.LOCALIDAD, DE.PROVINCIA, DE.COORDENADAX , DE.COORDENADAY , T.ID  , T.NOMBRE  ,B.ID  , B.DESCRIPCION  , B.HORADESDE, B.HORAHASTA, B.ORDEN, B.COLOR,   ");
            sb.Append("   TD.ID  , TD.NOMBRE ,  P.TERMINAL, P.TRANSACCION, P.IMPORTE_TOTAL, PF.PEDIDO_FASTLINE  ,PF.PEDIDO_ORIGINAL   ,PD.TIPO_OPERACION,");
            sb.Append("   PREF.ID, PREF.ID_PEDIDO, PREF.NRO_REFERENCIA, PREF.CRE, PREF.CAE, EDO.ID , EDO.NOMBRE  , OP.ID  ,OP.NOMBRE ");
            sb.Append("  ORDER BY HRD.ORDEN ");

            parametros.Add(new OracleParameter(":ID_HOJARUTA", idHojaRuta));
            dt = _OracleDao.ExcecuteAdapterFill(sb.ToString(), parametros);
            _OracleDao.CerrarConexion();
            return dt;
        }

        public DataTable getListPersonal(long idHojaRuta)
        {
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            List<OracleParameter> parametros = new List<OracleParameter>();
            _OracleDao.CrearConnection(ConexTrack);

            sb.Append("SELECT HRP.ID ID_HOJARUTAPERSONAL, HRP.ID_HOJARUTA, HRP.NOMBRE NOMBRE_HOJARUTAPERSONAL,");
            sb.Append(" HRP.IDENTIFICADOR, PT.ID ID_PERSONALTIPO, PT.NOMBRE NOMBRE_PERSONALTIPO");
            sb.Append(" FROM GPS.GPS_HOJARUTAPERSONAL HRP");
            sb.Append(" INNER JOIN GPS_PERSONALTIPO PT ON PT.ID = HRP.ID_PERSONALTIPO");
            sb.Append(" WHERE ID_HOJARUTA = :ID_HOJARUTA AND HRP.ACTIVO = 'S'");


            parametros.Add(new OracleParameter(":ID_HOJARUTA", idHojaRuta));
            dt = _OracleDao.ExcecuteAdapterFill(sb.ToString(), parametros);
            _OracleDao.CerrarConexion();
            return dt;
        }

        public DataTable ObtenerHojasRutasDigital(int canal, DateTime fDesde, DateTime fHasta)
        {
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            List<OracleParameter> parametros = new List<OracleParameter>();
            var idEstadoCancelada = _configuration.GetSection("HR_CANCELADA").Value;

            _OracleDao.CrearConnection(ConexTrack);

            sb.Append(" SELECT H.ID, H.WAYBILLID, H.STAMP FECHA, H.FECHA_PLAN FECHA_PLAN, H.FECHA_SALIDA FECHA_SALIDA, H.DISTANCIA_TOTAL,  ");
            sb.Append("        H.FECHA_CIERRE FECHA_CIERRE, H.PRIORIDAD, E.ID ID_ESTADO, E.NOMBRE ESTADO,H.DESCRIPCION DESCRIPCION,");
            sb.Append("        C.ID ID_CANAL, C.SUCURSAL SUCURSAL, C.NOMBRE CANAL, C.CONEXION_BD, C.BD, C.USUARIO, C.PASSWORD,C.GENERA_COT ,");
            sb.Append("        C.TIPO CANAL_TIPO, C.GENERA_COT GENERA_COT, DC.ID CANAL_ID_DIRECCION, DC.CALLE CANAL_CALLE, ");
            sb.Append("        DC.CODIGOPOSTAL CANAL_CODIGO_POSTAL, DC.COORDENADAX CANAL_LATITUDE, DC.COORDENADAY CANAL_LONGITUDE, ");
            sb.Append("        DC.LOCALIDAD CANAL_LOCALIDAD,DC.PISO CANAL_PISO, DC.PISO_DEPTO CANAL_PISO_DEPTO,  DC.NUMERO CANAL_ALTURA_CALLE,");
            sb.Append("        M.ID ID_MOVILTIPO, M.DESCRIPCION TIPO, M.PESO PESO, M.UMP UMP, M.VOLUMEN VOLUMEN, M.UMV UMV, COUNT(DISTINCT HRD.ID) CANTIDADPEDIDOS, ");
            sb.Append("        MO.ID ID_MOVIL,MO.NUMERO ,P.ID PROVEEDOR_ID,P.CUIT, ");
            sb.Append("        COUNT(DISTINCT HRE.ID_ELEMENTO) EMC,HRM.ID ID_HOJARUTAMOVIL,HRM.PATENTE,HRM.CAPACIDAD,HRM.APILABILIDAD,HRM.PROVEEDOR, HRM.OCUPACION OCUPACION, ");
            sb.Append("        X.NOMBRE NOMBRE_ESTADO ,X.CANTIDAD CANTIDAD_ENTREGADOS   ");
            sb.Append("   FROM GPS_HOJARUTA H    ");
            sb.Append("   INNER JOIN GPS_HOJARUTADETALLE HRD ON HRD.ID_HOJARUTA = H.ID    ");
            sb.Append("   INNER JOIN GPS_MOVILTIPO M ON M.ID = H.ID_MOVILTIPO    ");
            sb.Append("   INNER JOIN GPS_ESTADO E ON E.ID = H.ID_ESTADO    ");
            sb.Append("   INNER JOIN GPS_CANAL C ON C.ID = H.ID_CANAL    ");
            sb.Append("   INNER JOIN GPS_DIRECCION DC ON DC.ID = C.ID_DIRECCION   ");
            sb.Append("   LEFT JOIN GPS_HOJARUTAELEMENTO HRE ON HRE.ID_HOJARUTA = H.ID AND ID_ELEMENTO = 1    ");
            sb.Append("   LEFT JOIN GPS_HOJARUTAMOVIL HRM ON HRM.ID_HOJARUTA = H.ID    ");
            sb.Append("   LEFT JOIN GPS_MOVIL MO ON MO.PATENTE = HRM.PATENTE AND HRM.ACTIVO='S'   ");
            sb.Append("   LEFT JOIN GPS_PROVEEDOR P ON P.ID=MO.ID_PROVEEDOR AND P.ACTIVO='S' ");
            sb.Append("   LEFT JOIN   (SELECT HR.ID AS ID , CASE PA.ID_ESTADO WHEN 70 THEN 'ENTREGADO' WHEN 80 THEN 'ENTREGADO' WHEN 90 THEN 'NO ENTREGADO' WHEN 92 THEN 'NO ENTREGADO' WHEN 24 THEN 'NO ENTREGADO'WHEN 96 THEN 'CANCELADO' ELSE 'NA' END   NOMBRE,COUNT(DISTINCT NRO_REFERENCIA) CANTIDAD   ");
            sb.Append("         FROM GPS_PEDIDOREF_AUDIT PA   ");
            sb.Append("                INNER JOIN GPS_HOJARUTADETALLE HRD ON HRD.ID_PEDIDO = PA.ID_PEDIDO AND HRD.ID_ESTADO<>8 ");
            sb.Append("                INNER JOIN GPS_HOJArUTA HR ON HR.ID = hrd.id_hojaruta  ");
            sb.Append("                INNER JOIN GPS_ESTADO E ON E.ID = PA.ID_ESTADO ");
            sb.Append("          WHERE  PA.ID_ESTADO IN(70,80,90,24,96)                                      ");
            sb.Append("                 AND PA.ID_HOJARUTA = HR.ID                           ");
            sb.Append("           GROUP BY HR.ID ,CASE PA.ID_ESTADO WHEN 70 THEN 'ENTREGADO' WHEN 80 THEN 'ENTREGADO' WHEN 90 THEN 'NO ENTREGADO'  WHEN 92 THEN 'NO ENTREGADO' WHEN 24 THEN 'NO ENTREGADO' WHEN 96 THEN 'CANCELADO' ELSE 'NA' END  ");
            sb.Append("               ) X ON X.ID=H.ID  ");
            sb.Append("   WHERE HRD.ID_ESTADO <> 8 ");
            sb.Append("  AND TRUNC(H.FECHA_PLAN) BETWEEN :FECHADESDE AND :FECHAHASTA ");
            sb.Append("  AND C.ID = :ID_CANAL");
            sb.Append("  AND E.ID <> :ID_ESTADO");
            sb.Append("   GROUP BY H.ID, H.WAYBILLID, H.STAMP, H.FECHA_PLAN, H.FECHA_SALIDA, H.FECHA_CIERRE, H.PRIORIDAD, E.ID, E.NOMBRE,H.DESCRIPCION,H.DISTANCIA_TOTAL, ");
            sb.Append("  C.ID, C.SUCURSAL, C.NOMBRE, C.CONEXION_BD, C.BD, C.USUARIO, C.PASSWORD, C.TIPO, C.GENERA_COT, DC.ID,");
            sb.Append("  DC.CALLE, DC.NUMERO, DC.CODIGOPOSTAL, DC.COORDENADAX, DC.COORDENADAY, DC.LOCALIDAD, DC.PISO, DC.PISO_DEPTO,");
            sb.Append("     M.ID, M.DESCRIPCION, M.PESO, M.UMP, M.VOLUMEN, M.UMV ,MO.ID  ,MO.NUMERO ,P.ID  ,P.CUIT,");
            sb.Append("   HRM.ID,HRM.PATENTE,HRM.CAPACIDAD,HRM.APILABILIDAD,HRM.PROVEEDOR,HRM.OCUPACION ,X.NOMBRE  ,X.CANTIDAD ");
            sb.Append("   ORDER BY H.WAYBILLID, H.ID ");


            parametros.Add(new OracleParameter(":FECHADESDE", fDesde));
            parametros.Add(new OracleParameter(":FECHAHASTA", fHasta));
            parametros.Add(new OracleParameter(":ID_CANAL", canal));
            parametros.Add(new OracleParameter(":ID_ESTADO", idEstadoCancelada));

            dt = _OracleDao.ExcecuteAdapterFill(sb.ToString(), parametros);
            _OracleDao.CerrarConexion();
            return dt;
        }

        public DataTable getElementosHojaRuta(long idHojaRuta)
        {
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            List<OracleParameter> parametros = new List<OracleParameter>();
            _OracleDao.CrearConnection(ConexTrack);

            sb.Append(" SELECT H.ID ID_HOJARUTAELEMENTO, H.ID_HOJARUTA,H.PROPIEDAD ,H.OBSERVACIONES,H.STAMP, ");
            sb.Append("         E.ID ID_ELEMENTO ,E.NOMBRE ELEMENTO ,T.ID ID_ELEMENTOTIPO ,T.NOMBRE ELEMENTOTIPO, ");
            sb.Append("         S.ID ID_ESTADO , S.NOMBRE ESTADO ,dc.observaciones observaciones_dispositivo,dc.nro_telefono , dc.id ID_CEL ");
            sb.Append(" FROM GPS_HOJARUTAELEMENTO H ");
            sb.Append(" INNER JOIN GPS_ELEMENTO E ON  E.ID=H.ID_ELEMENTO  ");
            sb.Append(" INNER JOIN GPS_ELEMENTOTIPO T ON E.ID_ELEMENTOTIPO=T.ID ");
            sb.Append(" INNER JOIN GPS_ESTADO S ON S.ID=H.ID_ESTADO ");
            sb.Append(" LEFT JOIN GPS_DISPOSITIVO D ON D.IDENTIFICADOR=H.PROPIEDAD ");
            sb.Append(" LEFT JOIN GPS_DISPOSITIVO_CEL DC ON D.ID=DC.DISPOSITIVO_ID ");
            sb.Append(" WHERE H.ID_HOJARUTA=:ID_HOJARUTA ");

            parametros.Add(new OracleParameter(":ID_HOJARUTA", idHojaRuta));
            dt = _OracleDao.ExcecuteAdapterFill(sb.ToString(), parametros);
            _OracleDao.CerrarConexion();
            return dt;
        }

        public DataTable getListPersonalChoferCadetePanel(string idHojaRutas)
        {
            var tipoCadeteChofer = _configuration.GetSection("CHOFER").Value + ',' +
                                   _configuration.GetSection("CADETE").Value;
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            List<OracleParameter> parametros = new List<OracleParameter>();
            _OracleDao.CrearConnection(ConexTrack);

            sb.Append("SELECT HRP.ID ID_HOJARUTAPERSONAL, HRP.ID_HOJARUTA, HRP.NOMBRE NOMBRE_HOJARUTAPERSONAL,");
            sb.Append(" HRP.IDENTIFICADOR, PT.ID ID_PERSONALTIPO, PT.NOMBRE NOMBRE_PERSONALTIPO");
            sb.Append(" FROM GPS.GPS_HOJARUTAPERSONAL HRP");
            sb.Append($" INNER JOIN GPS_PERSONALTIPO PT ON PT.ID = HRP.ID_PERSONALTIPO AND PT.ID IN ({tipoCadeteChofer}) ");
            sb.Append($" WHERE ID_HOJARUTA IN ({idHojaRutas}) AND HRP.ACTIVO = 'S'");

            dt = _OracleDao.ExcecuteAdapterFill(sb.ToString(), parametros);
            _OracleDao.CerrarConexion();
            return dt;
        }

        public DataTable getHojaRutaEstado(long idHojaRuta)
        {
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            List<OracleParameter> parametros = new List<OracleParameter>();
            _OracleDao.CrearConnection(ConexTrack);

            sb.Append("  SELECT HE.ID_HOJARUTA,E.ID ID_ESTADO,E.NOMBRE ESTADO, HE.STAMP FECHA , HE.ID_USUARIO,U.LEGAJO , U.NOMBRE ");
            sb.Append("  FROM GPS.GPS_HOJARUTAESTADO HE ");
            sb.Append("  INNER JOIN GPS.GPS_ESTADO E ON E.ID = HE.ID_ESTADO ");
            sb.Append("  LEFT JOIN  USU_USUARIO U ON LTRIM (U.LEGAJO,0)=HE.ID_USUARIO ");
            sb.Append("  WHERE HE.ID_HOJARUTA = :ID_HOJARUTA ");
            sb.Append("  ORDER BY HE.STAMP  , E.ORDEN  ");

            parametros.Add(new OracleParameter(":ID_HOJARUTA", idHojaRuta));
            dt = _OracleDao.ExcecuteAdapterFill(sb.ToString(), parametros);
            _OracleDao.CerrarConexion();
            return dt;
        }

        public DataTable getCantidadMovilesEnViaje(string canales)
        {
            StringBuilder sb = new StringBuilder();
            List<OracleParameter> parametros = new List<OracleParameter>();
            _OracleDao.CrearConnection(ConexTrack);

            sb.Append(" SELECT PT.ID_CANAL, C.SUCURSAL, COUNT(DISTINCT PTP.ID) MOVILES_EN_VIAJE ");
            sb.Append(" FROM GPS_PLANTRABAJOPROVEEDOR PTP ");
            sb.Append(" INNER JOIN GPS_PLANTRABAJODETALLE PTD ON PTD.ID = PTP.ID_PLANTRABAJODETALLE ");
            sb.Append(" INNER JOIN GPS_PLANTRABAJO PT ON PT.ID = PTD.ID_PLANTRABAJO ");
            sb.Append(" INNER JOIN GPS_CANAL C ON C.ID = PT.ID_CANAL ");
            sb.Append(" WHERE PTP.VIAJE_ACTIVO = 'S' ");
            sb.Append(" AND PTP.ID_ESTADO != 105 ");
            sb.Append(" AND TRUNC(PT.FECHA_DESDE) >= TRUNC(SYSDATE-1) ");
            sb.Append(" AND PT.ID_CANAL IN(" + canales + ") ");
            sb.Append(" AND PTP.ACTIVO = 'S' ");
            sb.Append(" GROUP BY PT.ID_CANAL, C.SUCURSAL ");
            sb.Append(" ORDER BY C.SUCURSAL");

            DataTable dt = _OracleDao.ExcecuteAdapterFill(sb.ToString(), parametros);
            _OracleDao.CerrarConexion();
            return dt;
        }

        public DataTable getAllCanales()
        {
            StringBuilder sb = new StringBuilder();
            List<OracleParameter> parametros = new List<OracleParameter>();
            _OracleDao.CrearConnection(ConexTrack);

            sb.Append(" SELECT C.ID ID_CANAL, C.SUCURSAL SUCURSAL, C.NOMBRE CANAL, C.CONEXION_BD, C.BD, C.USUARIO, C.PASSWORD, C.TIPO CANAL_TIPO, DC.ID CANAL_ID_DIRECCION, DC.CALLE CANAL_CALLE, ");
            sb.Append(" DC.NUMERO CANAL_ALTURA_CALLE, DC.PISO CANAL_PISO, DC.PISO_DEPTO CANAL_PISO_DPTO, DC.CODIGOPOSTAL CANAL_CODIGO_POSTAL, DC.LOCALIDAD CANAL_LOCALIDAD, DC.COORDENADAX CANAL_LATITUDE, ");
            sb.Append(" DC.COORDENADAY CANAL_LONGITUDE, C.SUCURSAL SUCURSAL, C.GENERA_COT ");
            sb.Append(" FROM GPS_CANAL C ");
            sb.Append(" INNER JOIN GPS_DIRECCION DC ON DC.ID = C.ID_DIRECCION ");
            sb.Append(" ORDER BY C.SUCURSAL ");

            DataTable dt = _OracleDao.ExcecuteAdapterFill(sb.ToString(), parametros);
            _OracleDao.CerrarConexion();
            return dt;
        }
    
        public DataTable getPedidosTotalesPlanificados(string canales, int HREnViaje, string estadoEntregados, string estadoNoEntregados, string cancelados) 
        {
            StringBuilder sb = new StringBuilder();
            List<OracleParameter> parametros = new List<OracleParameter>();
            _OracleDao.CrearConnection(ConexTrack);

            string hrCancelada = _configuration.GetSection("HR_CANCELADA").Value;
            string pedidoCancelado = _configuration.GetSection("PCANCELADO").Value;

            sb.Append("  SELECT ID_CANAL, SUCURSAL, ID_HOJARUTA, WAYBILLID, MOVIL, SUM(EN_VIAJE) EN_VIAJE, SUM(ENTREGADOS) ENTREGADOS, SUM(NO_ENTREGADOS) NO_ENTREGADOS, ");
            sb.Append("  SUM(RETIRO) RETIRO, SUM(NO_RETIRO) NO_RETIRO, SUM(CANCELADO) CANCELADO ");
            sb.Append("  FROM (SELECT HR.ID_CANAL, C.SUCURSAL, HR.ID ID_HOJARUTA, HR.WAYBILLID WAYBILLID, M.NUMERO MOVIL, ");
            sb.Append("  COUNT(DISTINCT PR.ID) EN_VIAJE, 0 ENTREGADOS, 0 NO_ENTREGADOS, 0 RETIRO, 0 NO_RETIRO, 0 CANCELADO ");
            sb.Append("  FROM GPS_HOJARUTA HR ");
            sb.Append("  INNER JOIN GPS_ESTADO E ON E.ID = HR.ID_ESTADO ");
            sb.Append("  INNER JOIN GPS_CANAL C ON C.ID = HR.ID_CANAL ");
            sb.Append("  INNER JOIN GPS_HOJARUTADETALLE HRD ON HRD.ID_HOJARUTA = HR.ID AND HRD.ID_ESTADO <> " + hrCancelada);
            sb.Append("  INNER JOIN GPS_PEDIDODETALLE PD ON PD.ID_PEDIDO = HRD.ID_PEDIDO AND PD.ID_ESTADO <> " + pedidoCancelado);
            sb.Append("  INNER JOIN GPS_HOJARUTAMOVIL HRM ON HRM.ID_HOJARUTA = HR.ID ");
            sb.Append("  INNER JOIN GPS_MOVIL M ON M.PATENTE = HRM.PATENTE ");
            sb.Append("  LEFT JOIN GPS_PEDIDOCONSOLIDADO PR ON PR.NROREFERENCIA = PD.NRO_RESERVA AND HR.ID = PR.ID_HOJARUTA AND PR.ID_ESTADO IN(" + _configuration.GetSection("PEDIDO_EN_VIAJE").Value + ") ");
            sb.Append("  WHERE TRUNC(HR.FECHA_PLAN) >= TRUNC(SYSDATE-1) AND HR.ID_ESTADO <> " + hrCancelada);
            sb.Append("  AND E.ID = :ID_ESTADO ");
            sb.Append("  AND HR.ID_CANAL IN(" + canales + ") ");
            sb.Append("  GROUP BY HR.ID_CANAL, C.SUCURSAL, HR.ID, HR.WAYBILLID, M.NUMERO ");
            sb.Append("  UNION ALL ");
            sb.Append("  SELECT HR.ID_CANAL ID_CANAL, C.SUCURSAL SUCURSAL, HR.ID ID_HOJARUTA, HR.WAYBILLID WAYBILLID, M.NUMERO MOVIL, ");
            sb.Append("  0 EN_VIAJE, COUNT(DISTINCT PR.ID) ENTREGADOS, 0 NO_ENTREGADOS, 0 RETIRO, 0 NO_RETIRO, 0 CANCELADO ");
            sb.Append("  FROM GPS_HOJARUTA HR ");
            sb.Append("  INNER JOIN GPS_ESTADO E ON E.ID = HR.ID_ESTADO ");
            sb.Append("  INNER JOIN GPS_CANAL C ON C.ID = HR.ID_CANAL");
            sb.Append("  INNER JOIN GPS_HOJARUTADETALLE HRD ON HRD.ID_HOJARUTA = HR.ID AND HRD.ID_ESTADO <> " + hrCancelada );
            sb.Append("  INNER JOIN GPS_PEDIDODETALLE PD ON PD.ID_PEDIDO = HRD.ID_PEDIDO AND PD.ID_ESTADO <> " + pedidoCancelado );
            sb.Append("  INNER JOIN GPS_HOJARUTAMOVIL HRM ON HRM.ID_HOJARUTA = HR.ID ");
            sb.Append("  INNER JOIN GPS_MOVIL M ON M.PATENTE = HRM.PATENTE ");
            sb.Append("  LEFT JOIN GPS_PEDIDOCONSOLIDADO PR ON PR.NROREFERENCIA = PD.NRO_RESERVA AND HR.ID = PR.ID_HOJARUTA AND PR.ID_ESTADO IN(" + estadoEntregados + ") ");
            sb.Append("  WHERE TRUNC(HR.FECHA_PLAN) >= TRUNC(SYSDATE-1) AND HR.ID_ESTADO <> " + hrCancelada );
            sb.Append("  AND E.ID = :ID_ESTADO ");
            sb.Append("  AND HR.ID_CANAL IN(" + canales + ") ");
            sb.Append("  GROUP BY HR.ID_CANAL, C.SUCURSAL, HR.ID, HR.WAYBILLID, M.NUMERO ");
            sb.Append("  UNION ALL ");
            sb.Append("  SELECT HR.ID_CANAL ID_CANAL, C.SUCURSAL SUCURSAL, HR.ID ID_HOJARUTA, HR.WAYBILLID WAYBILLID, M.NUMERO MOVIL, ");
            sb.Append("  0 EN_VIAJE, 0 ENTREGADOS, COUNT(DISTINCT PR.ID) NO_ENTREGADOS, 0 RETIRO, 0 NO_RETIRO, 0 CANCELADO ");
            sb.Append("  FROM GPS_HOJARUTA HR ");
            sb.Append("  INNER JOIN GPS_ESTADO E ON E.ID = HR.ID_ESTADO ");
            sb.Append("  INNER JOIN GPS_CANAL C ON C.ID = HR.ID_CANAL ");
            sb.Append("  INNER JOIN GPS_HOJARUTADETALLE HRD ON HRD.ID_HOJARUTA = HR.ID AND HRD.ID_ESTADO <> " + hrCancelada );
            sb.Append("  INNER JOIN GPS_PEDIDODETALLE PD ON PD.ID_PEDIDO = HRD.ID_PEDIDO AND PD.ID_ESTADO <> " + pedidoCancelado );
            sb.Append("  INNER JOIN GPS_HOJARUTAMOVIL HRM ON HRM.ID_HOJARUTA = HR.ID ");
            sb.Append("  INNER JOIN GPS_MOVIL M ON M.PATENTE = HRM.PATENTE ");
            sb.Append("  LEFT JOIN GPS_PEDIDOCONSOLIDADO PR ON PR.NROREFERENCIA = PD.NRO_RESERVA AND HR.ID = PR.ID_HOJARUTA AND PR.ID_ESTADO IN(" + estadoNoEntregados + ") ");
            sb.Append("  WHERE TRUNC(HR.FECHA_PLAN) >= TRUNC(SYSDATE-1) AND HR.ID_ESTADO <> " + hrCancelada );
            sb.Append("  AND E.ID = :ID_ESTADO ");
            sb.Append("  AND HR.ID_CANAL IN(" + canales + ") ");
            sb.Append("  GROUP BY HR.ID_CANAL, C.SUCURSAL, HR.ID, HR.WAYBILLID, M.NUMERO ");
            sb.Append("  UNION ALL ");
            sb.Append("  SELECT HR.ID_CANAL ID_CANAL, C.SUCURSAL SUCURSAL, HR.ID ID_HOJARUTA, HR.WAYBILLID WAYBILLID, M.NUMERO MOVIL, ");
            sb.Append("  0 EN_VIAJE, 0 ENTREGADOS, 0 NO_ENTREGADOS, COUNT(DISTINCT pr.ID) RETIRO, 0 NO_RETIRO, 0 CANCELADO");
            sb.Append("  FROM GPS_HOJARUTA HR ");
            sb.Append("  INNER JOIN GPS_ESTADO E ON E.ID = HR.ID_ESTADO ");
            sb.Append("  INNER JOIN GPS_CANAL C ON C.ID = HR.ID_CANAL ");
            sb.Append("  INNER JOIN GPS_HOJARUTADETALLE HRD ON HRD.ID_HOJARUTA = HR.ID AND HRD.ID_ESTADO <> " + hrCancelada );
            sb.Append("  INNER JOIN GPS_PEDIDODETALLE PD ON PD.ID_PEDIDO = HRD.ID_PEDIDO AND PD.ID_ESTADO <> " + pedidoCancelado );
            sb.Append("  INNER JOIN GPS_HOJARUTAMOVIL HRM ON HRM.ID_HOJARUTA = HR.ID ");
            sb.Append("  INNER JOIN  GPS_MOVIL M ON M.PATENTE = HRM.PATENTE ");
            sb.Append("  LEFT JOIN GPS_PEDIDOCONSOLIDADO PR ON PR.NROREFERENCIA = PD.NRO_RESERVA AND HR.ID = PR.ID_HOJARUTA AND PR.ID_ESTADO IN(" + _configuration.GetSection("RETIRO").Value + ") ");
            sb.Append("  WHERE TRUNC(HR.FECHA_PLAN) >= TRUNC(SYSDATE-1) AND HR.ID_ESTADO <> " + hrCancelada );
            sb.Append("  AND E.ID = :ID_ESTADO ");
            sb.Append("  AND HR.ID_CANAL IN(" + canales + ") ");
            sb.Append("  GROUP BY HR.ID_CANAL, C.SUCURSAL, HR.ID, HR.WAYBILLID, M.NUMERO ");
            sb.Append("  UNION ALL ");
            sb.Append("  SELECT HR.ID_CANAL ID_CANAL, C.SUCURSAL SUCURSAL, HR.ID ID_HOJARUTA, HR.WAYBILLID WAYBILLID, M.NUMERO MOVIL, ");
            sb.Append("  0 EN_VIAJE, 0 ENTREGADOS, 0 NO_ENTREGADOS, 0 RETIRO, COUNT(DISTINCT PR.ID) NO_RETIRO, 0 CANCELADO ");
            sb.Append("  FROM GPS_HOJARUTA HR ");
            sb.Append("  INNER JOIN GPS_ESTADO E ON E.ID = HR.ID_ESTADO ");
            sb.Append("  INNER JOIN GPS_CANAL C ON C.ID = HR.ID_CANAL");
            sb.Append("  INNER JOIN GPS_HOJARUTADETALLE HRD ON HRD.ID_HOJARUTA = HR.ID AND HRD.ID_ESTADO <> " + hrCancelada );
            sb.Append("  INNER JOIN GPS_PEDIDODETALLE PD ON PD.ID_PEDIDO = HRD.ID_PEDIDO AND PD.ID_ESTADO <> " + pedidoCancelado );
            sb.Append("  INNER JOIN GPS_HOJARUTAMOVIL HRM ON HRM.ID_HOJARUTA = HR.ID ");
            sb.Append("  INNER JOIN GPS_MOVIL M ON M.PATENTE = HRM.PATENTE ");
            sb.Append("  LEFT JOIN GPS_PEDIDOCONSOLIDADO PR ON PR.NROREFERENCIA = PD.NRO_RESERVA AND HR.ID = PR.ID_HOJARUTA AND PR.ID_ESTADO IN(" + _configuration.GetSection("NO_RETIRO").Value + ") ");
            sb.Append("  WHERE TRUNC(HR.FECHA_PLAN) >= TRUNC(SYSDATE-1) AND HR.ID_ESTADO <> " + hrCancelada );
            sb.Append("  AND E.ID = :ID_ESTADO ");
            sb.Append("  AND HR.ID_CANAL IN(" + canales + ") ");
            sb.Append("  GROUP BY HR.ID_CANAL, C.SUCURSAL, HR.ID, HR.WAYBILLID, M.NUMERO ");
            sb.Append("  UNION ALL ");
            sb.Append("  SELECT HR.ID_CANAL ID_CANAL, C.SUCURSAL SUCURSAL, HR.ID ID_HOJARUTA, HR.WAYBILLID WAYBILLID, M.NUMERO MOVIL, ");
            sb.Append("  0 EN_VIAJE, 0 ENTREGADOS, 0 NO_ENTREGADOS, 0 RETIRO, 0 NO_RETIRO, COUNT(DISTINCT PR.ID) CANCELADO ");
            sb.Append("  FROM GPS_HOJARUTA HR ");
            sb.Append("  INNER JOIN GPS_ESTADO E ON E.ID = HR.ID_ESTADO ");
            sb.Append("  INNER JOIN GPS_CANAL C ON C.ID = HR.ID_CANAL ");
            sb.Append("  INNER JOIN GPS_HOJARUTADETALLE HRD ON HRD.ID_HOJARUTA = HR.ID AND HRD.ID_ESTADO <> " + hrCancelada);
            sb.Append("  INNER JOIN GPS_PEDIDODETALLE PD ON PD.ID_PEDIDO = HRD.ID_PEDIDO AND PD.ID_ESTADO <> " + pedidoCancelado);
            sb.Append("  INNER JOIN GPS_HOJARUTAMOVIL HRM ON HRM.ID_HOJARUTA = HR.ID ");
            sb.Append("  INNER JOIN GPS_MOVIL M ON M.PATENTE = HRM.PATENTE ");
            sb.Append("  LEFT JOIN GPS_PEDIDOCONSOLIDADO PR ON PR.NROREFERENCIA = PD.NRO_RESERVA AND HR.ID = PR.ID_HOJARUTA AND PR.ID_ESTADO IN(" + cancelados + ") ");
            sb.Append("  WHERE TRUNC(HR.FECHA_PLAN) >= TRUNC(SYSDATE-1) AND HR.ID_ESTADO <> " + hrCancelada);
            sb.Append("  AND E.ID =:ID_ESTADO ");
            sb.Append("  AND HR.ID_CANAL IN(" + canales + ") ");
            sb.Append("  GROUP BY HR.ID_CANAL, C.SUCURSAL, HR.ID, HR.WAYBILLID, M.NUMERO) ");
            sb.Append("  GROUP BY ID_CANAL, SUCURSAL, ID_HOJARUTA, WAYBILLID, MOVIL ");
            sb.Append("  ORDER BY WAYBILLID");

            parametros.Add( new OracleParameter( ":ID_ESTADO", HREnViaje ) );
            DataTable dt = _OracleDao.ExcecuteAdapterFill( sb.ToString(), parametros );
            _OracleDao.CerrarConexion();
            return dt;
        }
    
        public DataTable getPuntosDeEntrega(string canalesQry, int HREnViaje)
        {
            StringBuilder sb = new StringBuilder();
            List<OracleParameter> parametros = new List<OracleParameter>();
            _OracleDao.CrearConnection(ConexTrack);

            sb.Append("  SELECT ID_HOJARUTA, FECHA_CIERRE, SUM(CASE WHEN puntoDeEntrega = 1 THEN 1 ELSE 1 END) PUNTOSDEENTREGA ");
            sb.Append("  FROM (SELECT HRD.ID_HOJARUTA, C.DNI, HRD.COORDENADAX, HRD.COORDENADAY, H.FECHA_CIERRE, COUNT(HRD.ID) PUNTODEENTREGA ");
            sb.Append("  FROM GPS_HOJARUTADETALLE HRD ");
            sb.Append("  INNER JOIN GPS_HOJARUTA H ON H.ID = HRD.ID_HOJARUTA AND H.ID_ESTADO != " + _configuration.GetSection("HR_CANCELADA").Value);
            sb.Append("  INNER JOIN GPS_PEDIDO P ON P.ID = HRD.ID_PEDIDO ");
            sb.Append("  INNER JOIN GPS_CLIENTE C ON P.ID_CLIENTE = C.ID ");
            sb.Append("  WHERE HRD.ID_HOJARUTA IN(SELECT HR.ID ");
            sb.Append("  FROM GPS_HOJARUTA HR ");
            sb.Append("  WHERE TRUNC(HR.FECHA_PLAN) >= TRUNC(SYSDATE-1) ");
            sb.Append("  AND HR.ID_CANAL IN (" + canalesQry + ") ");
            sb.Append("  AND ID_ESTADO = :ID_ESTADO ) ");
            sb.Append("  AND HRD.ID_ESTADO != " + _configuration.GetSection("HR_CANCELADA").Value );
            sb.Append("  GROUP BY HRD.ID_HOJARUTA, C.DNI, HRD.COORDENADAX, HRD.COORDENADAY, H.FECHA_CIERRE ) ");
            sb.Append("  GROUP BY ID_HOJARUTA, FECHA_CIERRE ");

            parametros.Add(new OracleParameter(":ID_ESTADO", HREnViaje));
            DataTable dt = _OracleDao.ExcecuteAdapterFill(sb.ToString(), parametros);
            _OracleDao.CerrarConexion();

            return dt;
        }

        public DataTable ObtenerHojasRutasCD(int canal, DateTime fDesde, DateTime fHasta)
        {
            StringBuilder sb = new StringBuilder();
            List<OracleParameter> parametros = new List<OracleParameter>();
            var idEstadoCancelada = int.Parse(Infraestructure.Infraestructura.GetSection("HR_CANCELADA"));
            _OracleDao.CrearConnection(ConexTrack);

            sb.Append(" SELECT  H.ID, H.WAYBILLID, H.STAMP FECHA, H.FECHA_PLAN FECHA_PLAN, H.FECHA_SALIDA FECHA_SALIDA, H.FECHA_CIERRE FECHA_CIERRE,  ");
            sb.Append(" H.PRIORIDAD, E.ID ID_ESTADO, E.NOMBRE ESTADO,   H.DESCRIPCION DESCRIPCION,   H.DISTANCIA_TOTAL DISTANCIA_TOTAL, DO.CALLE CALLE_ORIGEN, ");
            sb.Append(" DO.NUMERO NUMERO_ORIGEN, DO.CODIGOPOSTAL CODIGOPOSTAL_ORIGEN, DO.COORDENADAX COORDENADAX_ORIGEN,   DO.COORDENADAY COORDENADAY_ORIGEN,  ");
            sb.Append(" DO.LOCALIDAD LOCALIDAD_ORIGEN,DO.PISO PISO_ORIGEN, DO.PISO_DEPTO PISO_DEPTO_ORIGEN,DD.CALLE CALLE_DESTINO, DD.NUMERO NUMERO_DESTINO,  ");
            sb.Append(" DD.CODIGOPOSTAL CODIGOPOSTAL_DESTINO, DD.COORDENADAX COORDENADAX_DESTINO,     DD.COORDENADAY COORDENADAY_DESTINO, DD.LOCALIDAD LOCALIDAD_DESTINO, ");
            sb.Append(" DD.PISO PISO_DESTINO,    DD.PISO_DEPTO PISO_DEPTO_DESTINO,C.ID ID_CANAL, C.SUCURSAL SUCURSAL, C.NOMBRE CANAL, C.CONEXION_BD, C.BD, C.USUARIO,   ");
            sb.Append(" C.PASSWORD, C.TIPO CANAL_TIPO, C.GENERA_COT GENERA_COT,   DC.ID CANAL_ID_DIRECCION, DC.CALLE CANAL_CALLE, DC.NUMERO CANAL_ALTURA_CALLE,  ");
            sb.Append(" DC.CODIGOPOSTAL CANAL_CODIGO_POSTAL, DC.COORDENADAX CANAL_LATITUDE,    DC.COORDENADAY CANAL_LONGITUDE,    DC.LOCALIDAD CANAL_LOCALIDAD, ");
            sb.Append(" DC.PISO CANAL_PISO, DC.PISO_DEPTO CANAL_PISO_DEPTO, M.ID ID_MOVILTIPO, M.DESCRIPCION TIPO, M.PESO PESO, M.UMP UMP, M.VOLUMEN VOLUMEN, M.UMV UMV,  ");
            sb.Append(" C.GENERA_COT , COUNT(DISTINCT P.ID) CANTIDADPEDIDOS, COUNT(DISTINCT HRE.ID_ELEMENTO) EMC, X.NOMBRE NOMBRE_ESTADO ,X.CANTIDAD CANTIDAD_ENTREGADOS, ");
            sb.Append(" A.ANDEN ANDEN , BH.ID ,BH.DESCRIPCION    ,HRM.ID ID_HOJARUTAMOVIL, HRM.PATENTE ,HRM.PROVEEDOR,HRM.CAPACIDAD,HRM.APILABILIDAD,MO.ID ID_MOVIL,MO.NUMERO,MO.ID_PROVEEDOR PROVEEDOR_ID,PR.CUIT ");
            sb.Append(" FROM GPS_HOJARUTA H         ");
            sb.Append(" INNER JOIN GPS_HOJARUTADETALLE HRD ON HRD.ID_HOJARUTA = H.ID    ");
            sb.Append(" INNER JOIN GPS_PEDIDO P ON P.ID =HRD.ID_PEDIDO       ");
            sb.Append(" INNER JOIN GPS_MOVILTIPO M ON M.ID = H.ID_MOVILTIPO        ");
            sb.Append(" INNER JOIN GPS_ESTADO E ON E.ID = H.ID_ESTADO      ");
            sb.Append(" INNER JOIN GPS_DIRECCION DO ON DO.ID = H.ID_ORIGEN        ");
            sb.Append(" INNER JOIN GPS_DIRECCION DD ON DD.ID = H.ID_DESTINO       ");
            sb.Append(" INNER JOIN GPS_CANAL C ON C.ID = H.ID_CANAL ");
            sb.Append(" INNER JOIN GPS_DIRECCION DC ON DC.ID = C.ID_DIRECCION     ");
            sb.Append(" INNER JOIN GPS_BANDAHORARIA BH ON BH.ID = H.ID_BANDA_MAYORITARIA  ");
            sb.Append(" LEFT JOIN GPS_HOJARUTAANDEN A ON A.ID_HOJARUTA = H.ID    ");
            sb.Append(" LEFT JOIN GPS_HOJARUTAELEMENTO HRE ON HRE.ID_HOJARUTA = H.ID AND ID_ELEMENTO = 1   ");
            sb.Append(" LEFT JOIN GPS_HOJARUTAMOVIL HRM ON HRM.ID_HOJARUTA = H.ID   ");
            sb.Append(" LEFT JOIN GPS_MOVIL MO ON MO.PATENTE = HRM.PATENTE   ");
            sb.Append(" LEFT JOIN GPS_PROVEEDOR PR ON MO.ID_PROVEEDOR = PR.ID   ");
            sb.Append(" LEFT JOIN( SELECT PC.ID_HOJARUTA AS ID ,   CASE E.ID  WHEN 70 THEN 'ENTREGADO' WHEN 80 THEN 'ENTREGADO'  ");
            sb.Append("                                         WHEN 126 THEN 'NO ENTREGADO'   WHEN 130 THEN 'NO ENTREGADO' ");
            sb.Append("                                         WHEN 96 THEN 'CANCELADO'  WHEN 97 THEN 'CANCELADO'  ");
            sb.Append("                                         WHEN 128 THEN 'RETIRO' WHEN 132 THEN 'NO RETIRO' ELSE 'NA' END   NOMBRE, COUNT(PC.NROREFERENCIA) CANTIDAD        ");
            sb.Append("              FROM GPS_PEDIDOCONSOLIDADO PC   ");
            sb.Append("              INNER JOIN GPS_HOJARUTA H  ON H.ID = PC.ID_HOJARUTA AND H.ID_ESTADO<>8             ");
            sb.Append("              INNER JOIN GPS_PEDIDO P ON PC.NROREFERENCIA=P.NRO_PEDIDO_REF     ");
            sb.Append("              INNER JOIN GPS_ESTADO E ON E.ID = PC.ID_ESTADO                                    ");
            sb.Append("              WHERE  E.ID  IN(70,80,126,130,96,97,128,132)  ");
           // sb.Append("              AND TRUNC(H.FECHA_PLAN) BETWEEN TRUNC(:FECHADESDE) AND TRUNC(:FECHAHASTA)            ");
            sb.Append("              GROUP BY PC.ID_HOJARUTA,    CASE E.ID WHEN 70 THEN 'ENTREGADO'  WHEN 80 THEN 'ENTREGADO' ");
            sb.Append("                             WHEN 126 THEN 'NO ENTREGADO'   WHEN 130 THEN 'NO ENTREGADO' ");
            sb.Append("                             WHEN 96 THEN 'CANCELADO'  WHEN 97 THEN 'CANCELADO'             ");
            sb.Append("                             WHEN 128 THEN 'RETIRO' WHEN 132 THEN 'NO RETIRO'           ");

            sb.Append("                             ELSE 'NA' END ) X ON X.ID=H.ID        ");
            sb.Append(" WHERE  TRUNC(H.FECHA_PLAN) BETWEEN TRUNC(:FECHADESDE) AND TRUNC(:FECHAHASTA) ");
            sb.Append(" AND HRD.ID_ESTADO <> 8  AND P.ID_ESTADO<>95  AND C.ID = :ID_CANAL  AND E.ID <> :ID_ESTADO   ");

            parametros.Add(new OracleParameter(":FECHADESDE", fDesde));
            parametros.Add(new OracleParameter(":FECHAHASTA", fHasta));
            parametros.Add(new OracleParameter(":ID_CANAL", canal));
            parametros.Add(new OracleParameter(":ID_ESTADO", idEstadoCancelada));

            sb.Append(" GROUP BY H.ID, A.ANDEN, H.WAYBILLID, H.STAMP, H.FECHA_PLAN, H.FECHA_SALIDA, H.FECHA_CIERRE, H.PRIORIDAD, E.ID, E.NOMBRE,H.DESCRIPCION,H.DISTANCIA_TOTAL,         ");
            sb.Append(" DO.CALLE, DO.NUMERO, DO.CODIGOPOSTAL, DO.COORDENADAX, DO.COORDENADAY, DO.LOCALIDAD, DO.PISO, DO.PISO_DEPTO,  DD.CALLE, DD.NUMERO, DD.CODIGOPOSTAL,      ");
            sb.Append(" DD.COORDENADAX, DD.COORDENADAY, DD.LOCALIDAD, DD.PISO, DD.PISO_DEPTO,  C.ID, C.SUCURSAL, C.NOMBRE, C.CONEXION_BD, C.BD, C.USUARIO, C.PASSWORD, C.TIPO,    ");
            sb.Append(" C.GENERA_COT, DC.ID,  DC.CALLE, DC.NUMERO, DC.CODIGOPOSTAL, DC.COORDENADAX, DC.COORDENADAY, DC.LOCALIDAD, DC.PISO, DC.PISO_DEPTO,  M.ID, M.DESCRIPCION,    ");
            sb.Append(" M.PESO, M.UMP, M.VOLUMEN, M.UMV  ,X.NOMBRE  ,X.CANTIDAD , BH.ID ,BH.DESCRIPCION ,HRM.ID  , HRM.PATENTE ,HRM.PROVEEDOR,HRM.CAPACIDAD,HRM.APILABILIDAD,MO.ID,MO.NUMERO,MO.ID_PROVEEDOR,PR.CUIT ");
            sb.Append(" ORDER BY H.WAYBILLID, H.ID ");
    
            DataTable dt = _OracleDao.ExcecuteAdapterFill(sb.ToString(), parametros);
            _OracleDao.CerrarConexion();

            return dt;
        }

        public async Task<string> updateDistanciaYTiempoTotal(int hojarutaId, long dostanciaReal, long tiempoReal, OracleDao oracleDao)
        {
            string sentencia = "";
            StringBuilder sb = new StringBuilder();
            List<OracleParameter> parametros = new List<OracleParameter>();

            sb.Append("UPDATE GPS.GPS_HOJARUTA SET DISTANCIA_REAL=:DISTANCIA_REAL, TIEMPO_REAL=:TIEMPO_REAL");
            sb.Append(" WHERE ID= :ID");


            parametros.Add(new OracleParameter(":DISTANCIA_REAL", dostanciaReal));
            parametros.Add(new OracleParameter(":TIEMPO_REAL", tiempoReal));
            parametros.Add(new OracleParameter(":ID", hojarutaId));

            sentencia = oracleDao.AddOrUpdate(sb.ToString(), parametros);
            return sentencia;
        }
    }
}
