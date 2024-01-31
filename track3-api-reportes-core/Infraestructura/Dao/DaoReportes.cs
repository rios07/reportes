using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Text;
using track3_api_reportes_core.Aplicacion.Filtros;
using track3_api_reportes_core.Infraestructura.Interfaces;
using track3_api_reportes_core.Middleware.Dto;

namespace track3_api_reportes_core.Infraestructura.Dao
{
    public class DaoReportes : IDaoReportes
    {
        private readonly IOracleDao _OracleDao;
        public static IConfiguration _configuration;
        public string ConnectionString;

        public DaoReportes(IOracleDao OracleDao, IConfiguration config)

        {
            _OracleDao = OracleDao;
            _configuration = config;
            ConnectionString = _configuration.GetConnectionString("Track3");
        }


        /// <summary>
        /// Genera un informe de viajes gestionados por distitnos proveedores basado en un rango de fechas.
        /// </summary>
        /// <param name="Desde">Fecha de inicio del rango.</param>
        /// <param name="Hasta">Fecha de fin del rango.</param>
        /// <returns>Un objeto DataTable con los datos del informe.</returns>
        //Version para obtener los dato de una consulta o vista.
        public DataTable ReporteMovilesProveedor_old(DateTime Desde, DateTime Hasta)
        {
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            List<OracleParameter> parametros = new List<OracleParameter>();

            _OracleDao.CrearConnection(ConnectionString);

            sb.Append(" select hr.fecha_plan,hr.waybillid,m.numero numero_movil,m.patente,c.dni,c.apellido,c.nombre,p.nombre proveedor ");
            sb.Append(" FROM GPS_HOJARUTA hr,                                                               ");
            sb.Append(" gps_plantrabajoproveedor ptp,                                                           ");
            sb.Append("    gps_hojarutaplan hrp,  ");
            sb.Append("    gps_movil m,      ");
            sb.Append("    gps_chofer c,       ");
            sb.Append("    gps_proveedor p       ");
            sb.Append(" WHERE hr.ID_CANAL = 1        ");
            sb.Append("    AND hr.ID_ESTADO <> 8    ");
            sb.Append("    AND hr.ID_MOVILTIPO <> 6    ");
            sb.Append("    AND waybillid IS NOT NULL  ");
            sb.Append("    AND TRUNC(fecha_plan) > TRUNC(sysdate - 31) ");
            sb.Append("    AND hr.id = hrp.id_hojaruta  ");
            sb.Append("    AND hrp.id_plantrabajoproveedor = ptp.id  " );
            sb.Append("    and ptp.id_movil = m.id  ");
            sb.Append("    and ptp.id_chofer = c.id  ");
            sb.Append("   and m.id_proveedor = p.id " );
            sb.Append(" order by  hr.fecha_plan desc, p.nombre  ");


            dt = _OracleDao.ExcecuteAdapterFill(sb.ToString(), parametros);

            _OracleDao.CerrarConexion();

            return dt;
        }

        //Version para obtenerla de un SP
        /// <summary>
        /// Genera un informe de movilidad de proveedores basado en un rango de fechas llamando a un procedimiento almacenado en Oracle.
        /// </summary>
        /// <param name="Desde">Fecha de inicio del rango.</param>
        /// <param name="Hasta">Fecha de fin del rango.</param>
        /// <returns>Un objeto DataTable con los datos del informe.</returns>
        public DataTable ReporteMovilesProveedor(String Desde, String Hasta)
        {
            DataTable dt = new DataTable();
            List<OracleParameter> parametros = new List<OracleParameter>();

            //Crear la conexion
            _OracleDao.CrearConnection(ConnectionString);
            
            //Agregar los parámetros necesarios al comando
            OracleParameter paramDesde = new OracleParameter("p_desde", OracleDbType.Varchar2);
            paramDesde.Value = Desde;
            parametros.Add(paramDesde);

            OracleParameter paramHasta = new OracleParameter("p_hasta", OracleDbType.Varchar2);
            paramHasta.Value = Hasta;
            parametros.Add(paramHasta);


            //Agregar el parámetro de salida para la tabla
            OracleParameter paramTabla = new OracleParameter("p_resultado", OracleDbType.RefCursor);
            paramTabla.Direction = ParameterDirection.Output;
            parametros.Add(paramTabla);


            //Ejecutar el comando y llenar el objeto DataTable con los resultados
            dt=_OracleDao.ExceuteStoreProcedure("sp_cd_prov_viaje", parametros);

            //Cerrar la conexion
            _OracleDao.CerrarConexion();

            return dt;
        }

        public DataTable ReporteViajesMovilesPedidos(String Desde, String Hasta,int id_canal)
        {
            DataTable dt = new DataTable();
            List<OracleParameter> parametros = new List<OracleParameter>();

            //Crear la conexion
            _OracleDao.CrearConnection(ConnectionString);

            //Agregar los parámetros necesarios al comando
            OracleParameter paramDesde = new OracleParameter("p_desde", OracleDbType.Varchar2);
            paramDesde.Value = Desde;
            parametros.Add(paramDesde);

            OracleParameter paramHasta = new OracleParameter("p_hasta", OracleDbType.Varchar2);
            paramHasta.Value = Hasta;
            parametros.Add(paramHasta);

            OracleParameter paramCanal = new OracleParameter("p_canal", OracleDbType.Int16);
            paramCanal.Value = id_canal;
            parametros.Add(paramCanal);


            //Agregar el parámetro de salida para la tabla
            OracleParameter paramTabla = new OracleParameter("p_resultado", OracleDbType.RefCursor);
            paramTabla.Direction = ParameterDirection.Output;
            parametros.Add(paramTabla);


            //Ejecutar el comando y llenar el objeto DataTable con los resultados
            dt = _OracleDao.ExceuteStoreProcedure("sp_dig_q_viaj_mov_ped", parametros);

            //Cerrar la conexion
            _OracleDao.CerrarConexion();

            return dt;
        }

        public DataTable ObtenerPosiciones(String Desde, String Hasta, int? p_id_dispositivo,string? p_identificador)
        {
            DataTable dt = new DataTable();
            List<OracleParameter> parametros = new List<OracleParameter>();

            //Crear la conexion
            _OracleDao.CrearConnection(ConnectionString);

            //Agregar el Id del dispositivo si existe
            OracleParameter paramId_Dispo = new OracleParameter("p_id_dispositivo", OracleDbType.Int16);
            paramId_Dispo.Value = p_id_dispositivo;
            parametros.Add(paramId_Dispo);


            //Agregar el Identificador
            OracleParameter paramIdentificador = new OracleParameter("p_identificador", OracleDbType.Varchar2);
            paramIdentificador.Value = p_identificador;
            parametros.Add(paramIdentificador);


            //Agregar los parámetros necesarios al comando
            OracleParameter paramDesde = new OracleParameter("p_desde", OracleDbType.Varchar2);
            paramDesde.Value = Desde;
            parametros.Add(paramDesde);


            OracleParameter paramHasta = new OracleParameter("p_hasta", OracleDbType.Varchar2);
            paramHasta.Value = Hasta;
            parametros.Add(paramHasta);



            //Agregar el parámetro de salida para la tabla
            OracleParameter paramTabla = new OracleParameter("p_resultado", OracleDbType.RefCursor);
            paramTabla.Direction = ParameterDirection.Output;
            parametros.Add(paramTabla);

           
            //Ejecutar el comando y llenar el objeto DataTable con los resultados
            dt = _OracleDao.ExceuteStoreProcedure("sp_obtener_posiciones", parametros);

            //Cerrar la conexion
            _OracleDao.CerrarConexion();

            return dt;
        }

        public string getIMEI(int p_id_dispositivo)
        {
            string imei = null;

            List<OracleParameter> parametros = new List<OracleParameter>();

            // Crear la conexión
            _OracleDao.CrearConnection(ConnectionString);

            // Agregar el Id del dispositivo si existe
            OracleParameter paramId_Dispo = new OracleParameter("p_id_dispositivo", OracleDbType.Int32);
            paramId_Dispo.Value = p_id_dispositivo;
            parametros.Add(paramId_Dispo);

            // Agregar el parámetro de salida para el IMEI
            OracleParameter paramImei = new OracleParameter("p_resultado", OracleDbType.Varchar2, 100); 
            paramImei.Direction = ParameterDirection.Output;
            parametros.Add(paramImei);

            // Ejecutar el procedimiento almacenado
            _OracleDao.ExceuteStoreProcedure("SP_GET_IMEI", parametros);

            // Obtener el IMEI del parámetro de salida
            if (paramImei.Value != DBNull.Value)
            {
                imei = paramImei.Value.ToString();
            }

            // Cerrar la conexión
            _OracleDao.CerrarConexion();

            return imei;
        }





        public DataTable GetKilometrosHojaRuta(String Desde, String Hasta, int? p_canal)
        {
            DataTable dt = new DataTable();
            List<OracleParameter> parametros = new List<OracleParameter>();

            //Crear la conexion
            _OracleDao.CrearConnection(ConnectionString);

            
            //Agregar los parámetros necesarios al comando
            OracleParameter paramDesde = new OracleParameter("p_desde", OracleDbType.Varchar2);
            paramDesde.Value = Desde;
            parametros.Add(paramDesde);


            OracleParameter paramHasta = new OracleParameter("p_hasta", OracleDbType.Varchar2);
            paramHasta.Value = Hasta;
            parametros.Add(paramHasta);


            //Agregar la sucrusal de forma opcional
            OracleParameter paramSucursal = new OracleParameter("p_canal", OracleDbType.Int16);
            paramSucursal.Value = p_canal;
            parametros.Add(paramSucursal);


            //Agregar el parámetro de salida para la tabla
            OracleParameter paramTabla = new OracleParameter("p_resultado", OracleDbType.RefCursor);
            paramTabla.Direction = ParameterDirection.Output;
            parametros.Add(paramTabla);


            //Ejecutar el comando y llenar el objeto DataTable con los resultados
            dt = _OracleDao.ExceuteStoreProcedure("sp_distancia_hr", parametros);

            //Cerrar la conexion
            _OracleDao.CerrarConexion();

            return dt;
        }

        public DataTable getMovilesEnViaje(int p_canal)
        {
            DataTable dt = new DataTable();
            List<OracleParameter> parametros = new List<OracleParameter>();

            //Crear la conexion
            _OracleDao.CrearConnection(ConnectionString);

            //Agregar la sucrusal de forma opcional
            OracleParameter paramSucursal = new OracleParameter("p_canal", OracleDbType.Int16);
            paramSucursal.Value = p_canal;
            parametros.Add(paramSucursal);


            //Agregar el parámetro de salida para la tabla
            OracleParameter paramTabla = new OracleParameter("p_resultado", OracleDbType.RefCursor);
            paramTabla.Direction = ParameterDirection.Output;
            parametros.Add(paramTabla);


            //Ejecutar el comando y llenar el objeto DataTable con los resultados
            dt = _OracleDao.ExceuteStoreProcedure("sp_moviles_en_viaje", parametros);

            //Cerrar la conexion
            _OracleDao.CerrarConexion();

            return dt;
        }

        public DataTable getCumplimientoEntrega(String Desde, String Hasta)
        {
            DataTable dt = new DataTable();
            List<OracleParameter> parametros = new List<OracleParameter>();

            //Crear la conexion
            _OracleDao.CrearConnection(ConnectionString);

            //Agregar los parámetros necesarios al comando
            OracleParameter paramDesde = new OracleParameter("p_desde", OracleDbType.Varchar2);
            paramDesde.Value = Desde;
            parametros.Add(paramDesde);


            OracleParameter paramHasta = new OracleParameter("p_hasta", OracleDbType.Varchar2);
            paramHasta.Value = Hasta;
            parametros.Add(paramHasta);


            //Agregar el parámetro de salida para la tabla
            OracleParameter paramTabla = new OracleParameter("p_resultado", OracleDbType.RefCursor);
            paramTabla.Direction = ParameterDirection.Output;
            parametros.Add(paramTabla);


           
            dt = _OracleDao.ExceuteStoreProcedure("sp_get_cumplimiento_entrega", parametros);

            //Cerrar la conexion
            _OracleDao.CerrarConexion();

            return dt;
        }


        public DataTable getCumplimientoEntregaDet(String Desde, String Hasta,int nid)
        {
            DataTable dt = new DataTable();
            List<OracleParameter> parametros = new List<OracleParameter>();

            //Crear la conexion
            _OracleDao.CrearConnection(ConnectionString);

            //Agregar los parámetros necesarios al comando
            OracleParameter paramDesde = new OracleParameter("p_desde", OracleDbType.Varchar2);
            paramDesde.Value = Desde;
            parametros.Add(paramDesde);


            OracleParameter paramHasta = new OracleParameter("p_hasta", OracleDbType.Varchar2);
            paramHasta.Value = Hasta;
            parametros.Add(paramHasta);

            OracleParameter paramnid = new OracleParameter("p_id_canal", OracleDbType.Int32);
            paramnid.Value = nid;
            parametros.Add(paramnid);


            //Agregar el parámetro de salida para la tabla
            OracleParameter paramTabla = new OracleParameter("p_resultado", OracleDbType.RefCursor);
            paramTabla.Direction = ParameterDirection.Output;
            parametros.Add(paramTabla);



            dt = _OracleDao.ExceuteStoreProcedure("sp_get_cumplimiento_entrega_d", parametros);

            //Cerrar la conexion
            _OracleDao.CerrarConexion();

            return dt;
        }

        public DataTable getReiteracionPedidos(String Desde, String Hasta)
        {
            DataTable dt = new DataTable();
            List<OracleParameter> parametros = new List<OracleParameter>();

            //Crear la conexion
            _OracleDao.CrearConnection(ConnectionString);

            //Agregar los parámetros necesarios al comando
            OracleParameter paramDesde = new OracleParameter("p_desde", OracleDbType.Varchar2);
            paramDesde.Value = Desde;
            parametros.Add(paramDesde);


            OracleParameter paramHasta = new OracleParameter("p_hasta", OracleDbType.Varchar2);
            paramHasta.Value = Hasta;
            parametros.Add(paramHasta);


            //Agregar el parámetro de salida para la tabla
            OracleParameter paramTabla = new OracleParameter("p_resultado", OracleDbType.RefCursor);
            paramTabla.Direction = ParameterDirection.Output;
            parametros.Add(paramTabla);



            dt = _OracleDao.ExceuteStoreProcedure("sp_get_reiteracion_ped", parametros);

            //Cerrar la conexion
            _OracleDao.CerrarConexion();

            return dt;
        }

        public DataTable getReporteEstados(String Fecha, int nid)
        {
            DataTable dt = new DataTable();
            List<OracleParameter> parametros = new List<OracleParameter>();

            //Crear la conexion
            _OracleDao.CrearConnection(ConnectionString);

            //Agregar los parámetros necesarios al comando
            OracleParameter paramFecha = new OracleParameter("p_fecha", OracleDbType.Varchar2);
            paramFecha.Value = Fecha;
            parametros.Add(paramFecha);


            OracleParameter paramCanal = new OracleParameter("p_canal", OracleDbType.Int16);
            paramCanal.Value = nid;
            parametros.Add(paramCanal);


            //Agregar el parámetro de salida para la tabla
            OracleParameter paramTabla = new OracleParameter("p_resultado", OracleDbType.RefCursor);
            paramTabla.Direction = ParameterDirection.Output;
            parametros.Add(paramTabla);

            dt = _OracleDao.ExceuteStoreProcedure("sp_get_reporteestados", parametros);

            //Cerrar la conexion
            _OracleDao.CerrarConexion();

            return dt;
        }


        public DataTable getAllCanal()
        {
            DataTable dt = new DataTable();
            List<OracleParameter> parametros = new List<OracleParameter>();

            //Crear la conexion
            _OracleDao.CrearConnection(ConnectionString);

            //Agregar el parámetro de salida para la tabla
            OracleParameter paramTabla = new OracleParameter("p_resultado", OracleDbType.RefCursor);
            paramTabla.Direction = ParameterDirection.Output;
            parametros.Add(paramTabla);

            dt = _OracleDao.ExceuteStoreProcedure("sp_get_allcanal", parametros);

            //Cerrar la conexion
            _OracleDao.CerrarConexion();

            return dt;
        }
        public DataTable getReiteracionPedidosSuc(String Desde, String Hasta)
        {
            DataTable dt = new DataTable();
            List<OracleParameter> parametros = new List<OracleParameter>();

            //Crear la conexion
            _OracleDao.CrearConnection(ConnectionString);

            //Agregar los parámetros necesarios al comando
            OracleParameter paramDesde = new OracleParameter("p_desde", OracleDbType.Varchar2);
            paramDesde.Value = Desde;
            parametros.Add(paramDesde);


            OracleParameter paramHasta = new OracleParameter("p_hasta", OracleDbType.Varchar2);
            paramHasta.Value = Hasta;
            parametros.Add(paramHasta);


            //Agregar el parámetro de salida para la tabla
            OracleParameter paramTabla = new OracleParameter("p_resultado", OracleDbType.RefCursor);
            paramTabla.Direction = ParameterDirection.Output;
            parametros.Add(paramTabla);



            dt = _OracleDao.ExceuteStoreProcedure("sp_get_reiteracion_ped", parametros);

            //Cerrar la conexion
            _OracleDao.CerrarConexion();

            return dt;
        }

        public DataTable getReiteracionPedidosSucDet(String Desde, String Hasta)
        {
            DataTable dt = new DataTable();
            List<OracleParameter> parametros = new List<OracleParameter>();

            //Crear la conexion
            _OracleDao.CrearConnection(ConnectionString);

            //Agregar los parámetros necesarios al comando
            OracleParameter paramDesde = new OracleParameter("p_desde", OracleDbType.Varchar2);
            paramDesde.Value = Desde;
            parametros.Add(paramDesde);


            OracleParameter paramHasta = new OracleParameter("p_hasta", OracleDbType.Varchar2);
            paramHasta.Value = Hasta;
            parametros.Add(paramHasta);


            //Agregar el parámetro de salida para la tabla
            OracleParameter paramTabla = new OracleParameter("p_resultado", OracleDbType.RefCursor);
            paramTabla.Direction = ParameterDirection.Output;
            parametros.Add(paramTabla);



            dt = _OracleDao.ExceuteStoreProcedure("sp_get_reiteracion_ped_suc", parametros);

            //Cerrar la conexion
            _OracleDao.CerrarConexion();

            return dt;
        }

        public DataTable getReiteracionPedidosSucDetx(int nid,String Desde, String Hasta)
        {
            DataTable dt = new DataTable();
            List<OracleParameter> parametros = new List<OracleParameter>();

            //Crear la conexion
            _OracleDao.CrearConnection(ConnectionString);

            //Agregar los parámetros necesarios al comando
            OracleParameter paramDesde = new OracleParameter("p_desde", OracleDbType.Varchar2);
            paramDesde.Value = Desde;
            parametros.Add(paramDesde);


            OracleParameter paramHasta = new OracleParameter("p_hasta", OracleDbType.Varchar2);
            paramHasta.Value = Hasta;
            parametros.Add(paramHasta);


            OracleParameter paramIdCanal = new OracleParameter("p_id_canal", OracleDbType.Int16);
            paramIdCanal.Value = nid;
            parametros.Add(paramIdCanal);


            //Agregar el parámetro de salida para la tabla
            OracleParameter paramTabla = new OracleParameter("p_resultado", OracleDbType.RefCursor);
            paramTabla.Direction = ParameterDirection.Output;
            parametros.Add(paramTabla);



            dt = _OracleDao.ExceuteStoreProcedure("sp_get_reiteracion_ped_suc_x", parametros);

            //Cerrar la conexion
            _OracleDao.CerrarConexion();

            return dt;
        }


        public DataTable getCantidadHDR(string p_fecha,string p_patente)
        {
            DataTable dt = new DataTable();
            List<OracleParameter> parametros = new List<OracleParameter>();

            //Crear la conexion
            _OracleDao.CrearConnection(ConnectionString);

            //Agregar la Fecha  en la que se buscara PDT
            OracleParameter paramFecha = new OracleParameter("p_fecha", OracleDbType.Varchar2);
            paramFecha.Value = p_fecha;
            parametros.Add(paramFecha);

            //Agregar la Patente 
            OracleParameter paramPatente = new OracleParameter("p_patente", OracleDbType.Varchar2);
            paramPatente.Value = p_patente;
            parametros.Add(paramPatente);


            //Agregar el parámetro de salida para la tabla
            OracleParameter paramTabla = new OracleParameter("p_resultado", OracleDbType.RefCursor);
            paramTabla.Direction = ParameterDirection.Output;
            parametros.Add(paramTabla);


            //Ejecutar el comando y llenar el objeto DataTable con los resultados
            dt = _OracleDao.ExceuteStoreProcedure("sp_get_cantidadhdr", parametros);

            //Cerrar la conexion
            _OracleDao.CerrarConexion();

            return dt;
        }

        public DataTable get_DiarioPDT(string p_fecha, int p_canal)
        {
            DataTable dt = new DataTable();
            List<OracleParameter> parametros = new List<OracleParameter>();

            //Crear la conexion
            _OracleDao.CrearConnection(ConnectionString);

            //Agregar la Fecha  en la que se buscara PDT
            OracleParameter paramFecha = new OracleParameter("p_fecha", OracleDbType.Varchar2);
            paramFecha.Value = p_fecha;
            parametros.Add(paramFecha);

            //Agregar la Patente 
            OracleParameter paramCanal = new OracleParameter("p_canal", OracleDbType.Int16);
            paramCanal.Value = p_canal;
            parametros.Add(paramCanal);


            //Agregar el parámetro de salida para la tabla
            OracleParameter paramTabla = new OracleParameter("p_resultado", OracleDbType.RefCursor);
            paramTabla.Direction = ParameterDirection.Output;
            parametros.Add(paramTabla);


            //Ejecutar el comando y llenar el objeto DataTable con los resultados
            dt = _OracleDao.ExceuteStoreProcedure("sp_diarioPDT", parametros);

            //Cerrar la conexion
            _OracleDao.CerrarConexion();

            return dt;
        }

        public DataTable getRecorrido(long p_Id_HojaRuta)
        {
            DataTable dt = new DataTable();
            List<OracleParameter> parametros = new List<OracleParameter>();

            //Crear la conexion
            _OracleDao.CrearConnection(ConnectionString);

            //Agregar la Patente 
            OracleParameter paramCanal = new OracleParameter("p_Id_HojaRuta", OracleDbType.Int32);
            paramCanal.Value = p_Id_HojaRuta;
            parametros.Add(paramCanal);


            //Agregar el parámetro de salida para la tabla
            OracleParameter paramTabla = new OracleParameter("p_resultado", OracleDbType.RefCursor);
            paramTabla.Direction = ParameterDirection.Output;
            parametros.Add(paramTabla);


            //Ejecutar el comando y llenar el objeto DataTable con los resultados
            dt = _OracleDao.ExceuteStoreProcedure("sp_get_recorrido", parametros);

            //Cerrar la conexion
            _OracleDao.CerrarConexion();

            return dt;
        }

        public DataTable GetActividadPorPatente(string desde, string hasta, string patente)
        {
            DataTable dt = new DataTable();
            List<OracleParameter> parametros = new List<OracleParameter>();

            _OracleDao.CrearConnection(ConnectionString);

            OracleParameter paramDesde = new OracleParameter("p_desde", OracleDbType.Varchar2);
            paramDesde.Value = desde;
            parametros.Add(paramDesde);

            OracleParameter paramHasta = new OracleParameter("p_hasta", OracleDbType.Varchar2);
            paramHasta.Value = hasta;
            parametros.Add(paramHasta);

            OracleParameter paramPatente = new OracleParameter("p_patente", OracleDbType.Varchar2);
            paramPatente.Value = patente;
            parametros.Add(paramPatente);

            OracleParameter paramTabla = new OracleParameter("p_resultado", OracleDbType.RefCursor);
            paramTabla.Direction = ParameterDirection.Output;
            parametros.Add(paramTabla);

            dt = _OracleDao.ExceuteStoreProcedure("sp_actividad_patente", parametros);

            //Cerrar la conexion
            _OracleDao.CerrarConexion();

            return dt;
        }



        public DataTable getReporteVersionAPP(String Desde, String Hasta, int Sucursal)
        {
            DataTable dt = new DataTable();
            List<OracleParameter> parametros = new List<OracleParameter>();

            //Crear la conexion
            _OracleDao.CrearConnection(ConnectionString);


            //Agregar los parámetros necesarios al comando
            OracleParameter paramDesde = new OracleParameter("p_desde", OracleDbType.Varchar2);
            paramDesde.Value = Desde;
            parametros.Add(paramDesde);


            OracleParameter paramHasta = new OracleParameter("p_hasta", OracleDbType.Varchar2);
            paramHasta.Value = Hasta;
            parametros.Add(paramHasta);


            //Agregar la sucrusal de forma opcional
            OracleParameter paramSucursal = new OracleParameter("p_canal", OracleDbType.Int16);
            paramSucursal.Value = Sucursal;
            parametros.Add(paramSucursal);


            //Agregar el parámetro de salida para la tabla
            OracleParameter paramTabla = new OracleParameter("p_resultado", OracleDbType.RefCursor);
            paramTabla.Direction = ParameterDirection.Output;
            parametros.Add(paramTabla);


            //Ejecutar el comando y llenar el objeto DataTable con los resultados
            dt = _OracleDao.ExceuteStoreProcedure("sp_versionado_de_emc", parametros);

            //Cerrar la conexion
            _OracleDao.CerrarConexion();

            return dt;
        }





        public DataTable getReservasCD(string desde, string hasta, string waybillid)
        {
            DataTable dt = new DataTable();
            List<OracleParameter> parametros = new List<OracleParameter>();

            _OracleDao.CrearConnection(ConnectionString);

            OracleParameter paramDesde = new OracleParameter("p_fecha_ini", OracleDbType.Varchar2);
            paramDesde.Value = desde;
            parametros.Add(paramDesde);

            OracleParameter paramHasta = new OracleParameter("p_fecha_fin", OracleDbType.Varchar2);
            paramHasta.Value = hasta;
            parametros.Add(paramHasta);

            OracleParameter paramSucursal = new OracleParameter("p_hr", OracleDbType.Varchar2);
            paramSucursal.Value = waybillid;
            parametros.Add(paramSucursal);

            OracleParameter paramTabla = new OracleParameter("p_resultado", OracleDbType.RefCursor);
            paramTabla.Direction = ParameterDirection.Output;
            parametros.Add(paramTabla);

            dt = _OracleDao.ExceuteStoreProcedure("sp_reporteReservasCD", parametros);

            //Cerrar la conexion
            _OracleDao.CerrarConexion();

            return dt;
        }
    
        public DataTable getReportesPDT(string fecha, string canales)
        {
            DataTable dt = new DataTable();
            List<OracleParameter> parametros = new List<OracleParameter>();

            _OracleDao.CrearConnection(ConnectionString);

            OracleParameter paramDesde = new OracleParameter("p_fecha", OracleDbType.Varchar2);
            paramDesde.Value = fecha;
            parametros.Add(paramDesde);

            OracleParameter paramCanales = new OracleParameter("p_canal", OracleDbType.Varchar2);
            paramCanales.Value = canales;
            parametros.Add(paramCanales);

            OracleParameter paramTabla = new OracleParameter("p_resultado", OracleDbType.RefCursor);
            paramTabla.Direction = ParameterDirection.Output;
            parametros.Add(paramTabla);

            dt = _OracleDao.ExceuteStoreProcedure("sp_get_reporte_pdt", parametros);

            //Cerrar la conexion
            _OracleDao.CerrarConexion();

            return dt;
        }

        //TRK3-1610 - (177984) - 21/12/2023: Circuito Digital - Reporte de Presencia.
        public DataTable getPlanesTrabajoProveedor(FiltroPresencia filtro)
        {
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            List<OracleParameter> parametros = new List<OracleParameter>();
            bool hayMovil = false;
            bool hayChofer = false;

            _OracleDao.CrearConnection(ConnectionString);

            sb.Append(" SELECT PTP.ID, PTP.ID_PLANTRABAJODETALLE, PTP.HORA_DESDE, PTP.HORA_HASTA, PTP.ID_ESTADO, M.ID ID_MOVIL, ");
            sb.Append(" M.PATENTE, C.ID ID_CHOFER, C.NOMBRE NOMBRE_CHOFER, C.APELLIDO APELLIDO_CHOFER, C.DNI, C.IDENTIFICADOR, ");
            sb.Append(" PTP.INDEXDIV, PTP.INICIO_JORNADA, PTP.FIN_JORNADA ");
            sb.Append(" FROM GPS_PLANTRABAJOPROVEEDOR PTP ");
            sb.Append(" INNER JOIN GPS_MOVIL M ON M.ID = PTP.ID_MOVIL ");
            sb.Append(" INNER JOIN GPS_CHOFER C ON C.ID = PTP.ID_CHOFER ");
            sb.Append(" INNER JOIN GPS_PLANTRABAJODETALLE PTD ON PTD.ID = PTP.ID_PLANTRABAJODETALLE ");
            sb.Append(" INNER JOIN GPS_PLANTRABAJO PT ON PT.ID = PTD.ID_PLANTRABAJO ");
            sb.Append(" WHERE PT.FECHA_DESDE >= :DESDE ");
            sb.Append(" AND PT.FECHA_HASTA <= :HASTA ");
            sb.Append(" AND PTP.ACTIVO = 'S' ");

            if (filtro.Movil.Id != 0)
            {
                sb.Append(" AND M.ID = :MOVIL");
                hayMovil = true;                
            }

            if (filtro.Chofer.Id != 0)
            {
                sb.Append(" AND C.ID = :CHOFER");
                hayChofer = true;                
            }

            sb.Append(" ORDER BY PTP.HORA_DESDE ");

          
            parametros.Add(new OracleParameter(":DESDE", DateTime.ParseExact(filtro.FechaDesde, "dd/MM/yyyy", null)));
            parametros.Add(new OracleParameter(":HASTA", DateTime.ParseExact(filtro.FechaHasta, "dd/MM/yyyy", null)));


            if (hayMovil)
                parametros.Add(new OracleParameter(":MOVIL", filtro.Movil.Id));
            if (hayChofer)
                parametros.Add(new OracleParameter(":CHOFER", filtro.Chofer.Id));

            dt = _OracleDao.ExcecuteAdapterFill(sb.ToString(), parametros);
            _OracleDao.CerrarConexion();

            return dt;
        }

        //TRK3-1610 - (177984) - 21/12/2023: Circuito Digital - Reporte de Presencia.
        public DataTable getPresencias(int id, string canales)
        {
            DataTable dt = new DataTable();
            List<OracleParameter> parametros = new List<OracleParameter>();

            _OracleDao.CrearConnection(ConnectionString);

            OracleParameter paramId = new OracleParameter("p_id", OracleDbType.Int32);
            paramId.Value = id;
            parametros.Add(paramId);

            OracleParameter paramCanales = new OracleParameter("p_canales", OracleDbType.Varchar2);
            paramCanales.Value = canales;
            parametros.Add(paramCanales);

            OracleParameter paramTabla = new OracleParameter("p_resultado", OracleDbType.RefCursor);
            paramTabla.Direction = ParameterDirection.Output;
            parametros.Add(paramTabla);

            dt = _OracleDao.ExceuteStoreProcedure("sp_get_presencias", parametros);

            //Cerrar la conexion
            _OracleDao.CerrarConexion();

            return dt;
        }

        //TRK3-1610 - (177984) - 21/12/2023: Circuito Digital - Reporte de Presencia.
        public DataTable getHojasRuta(int id, string canales)
        {
            DataTable dt = new DataTable();

            List<OracleParameter> parametros = new List<OracleParameter>();

            _OracleDao.CrearConnection(ConnectionString);

            OracleParameter paramId = new OracleParameter("p_id", OracleDbType.Int32);
            paramId.Value = id;
            parametros.Add(paramId);

            OracleParameter paramCanales = new OracleParameter("p_canales", OracleDbType.Varchar2);
            paramCanales.Value = canales;
            parametros.Add(paramCanales);

            OracleParameter paramTabla = new OracleParameter("p_resultado", OracleDbType.RefCursor);
            paramTabla.Direction = ParameterDirection.Output;
            parametros.Add(paramTabla);

            dt = _OracleDao.ExceuteStoreProcedure("sp_get_hdr_presencia", parametros);

            //Cerrar la conexion
            _OracleDao.CerrarConexion();

            return dt;
        }
    }
}
