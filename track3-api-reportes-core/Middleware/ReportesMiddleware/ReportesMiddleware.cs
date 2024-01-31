using System.Data;
using track3_api_reportes_core.Aplicacion.Requests;
using track3_api_reportes_core.Infraestructura.Interfaces;
using track3_api_reportes_core.Middleware.Dto;
using track3_api_reportes_core.Middleware.Interfaces;
using track3_api_reportes_core.Middleware.Modelos.Track;

namespace track3_api_reportes_core.Middleware.ReportesMiddleware
{
    public class ReportesMiddleware : IReportesMiddleware
    {
        public readonly IDaoReportes _IDaoReportes;
        public readonly ISqlDaoReportes _SqlDaoReportes;

       
        public ReportesMiddleware(IDaoReportes IDaoReportes, ISqlDaoReportes sqlDaoReportes)
        {
            _IDaoReportes = IDaoReportes;
            _SqlDaoReportes = sqlDaoReportes;   
        }

        public List<SpCdProdViaje> ReporteMovilesProveedor(String Desde, String Hasta)
        {
            DataTable dt = _IDaoReportes.ReporteMovilesProveedor(Desde, Hasta);
            List<SpCdProdViaje> retorno = new List<SpCdProdViaje>();
            foreach (DataRow row in dt.Rows)
            {
                SpCdProdViaje item = new SpCdProdViaje(row);
                retorno.Add(item);
            }

            return retorno;
        }

        public List<SpDigViajMovPed> ReporteViajesMovilesPedidos(String Desde, String Hasta, int p_canal)
        {
            DataTable dt = _IDaoReportes.ReporteViajesMovilesPedidos(Desde, Hasta, p_canal);
            List<SpDigViajMovPed> retorno = new List<SpDigViajMovPed>();
            foreach (DataRow row in dt.Rows)
            {
                SpDigViajMovPed item = new SpDigViajMovPed(row);
                retorno.Add(item);
            }

            return retorno;
        }

        public List<SpObtenerPosiciones> ObtenerPosiciones(String Desde, String Hasta, int? p_Id_Dispositivo, string? p_Identificador)
        {
            DataTable dt = _IDaoReportes.ObtenerPosiciones(Desde, Hasta, p_Id_Dispositivo, p_Identificador);
            List<SpObtenerPosiciones> retorno = new List<SpObtenerPosiciones>();
            foreach (DataRow row in dt.Rows)
            {
                SpObtenerPosiciones item = new SpObtenerPosiciones(row);
                retorno.Add(item);
            }
            return retorno;
        }


        public List<SpObtenerPosiciones> ObtenerPosicionesSW(String Desde, String Hasta, int p_Id_Dispositivo, string? p_Identificador)
        {
            DataTable dt = _IDaoReportes.ObtenerPosiciones(Desde, Hasta, p_Id_Dispositivo, p_Identificador);
            List<SpObtenerPosiciones> retorno = new List<SpObtenerPosiciones>();

            ////Primero reviso GPS
            if (dt.Rows.Count > 5)
            {
                foreach (DataRow row in dt.Rows)
                {
                    SpObtenerPosiciones item = new SpObtenerPosiciones(row);
                    retorno.Add(item);
                }
              
            }
            else
            {
                               
                //Realizar la misma busqueda pero en SecureWay
                DataTable dt2 = _SqlDaoReportes.ObtenerPosiciones(Desde, Hasta, _IDaoReportes.getIMEI(p_Id_Dispositivo));
                List<SpObtenerPosiciones> retornoSW = new List<SpObtenerPosiciones>();

                foreach (DataRow row in dt2.Rows)
                {
                    SpObtenerPosiciones item = new SpObtenerPosiciones(row);
                    retorno.Add(item);
                }
              
            }

            return retorno;
    

        }



        public List<SPGetKilometrosHojaRuta> GetKilometrosHojaRuta(String Desde, String Hasta, int? Sucursal)
        {
            DataTable dt = _IDaoReportes.GetKilometrosHojaRuta(Desde, Hasta, Sucursal);
            List<SPGetKilometrosHojaRuta> retorno = new List<SPGetKilometrosHojaRuta>();
            foreach (DataRow row in dt.Rows)
            {
                SPGetKilometrosHojaRuta item = new SPGetKilometrosHojaRuta(row);
                retorno.Add(item);
            }
            return retorno;
        }

        public List<spGetMovilesEnViaje> getMovilesEnViaje(int p_canal)
        {
            DataTable dt = _IDaoReportes.getMovilesEnViaje(p_canal);
            List<spGetMovilesEnViaje> retorno = new List<spGetMovilesEnViaje>();
            foreach (DataRow row in dt.Rows)
            {
                spGetMovilesEnViaje item = new spGetMovilesEnViaje(row);
                retorno.Add(item);
            }
            return retorno;
        }

        public List<CumplimientoEntrega> getCumplimientoEntrega(string Desde, string Hasta)
        {
            DataTable dt = _IDaoReportes.getCumplimientoEntrega(Desde, Hasta);
            List<CumplimientoEntrega> retorno = new List<CumplimientoEntrega>();
            int var_orden = 1;
            foreach (DataRow row in dt.Rows)
            {
                CumplimientoEntrega item = new CumplimientoEntrega(row);
                item.orden = var_orden++;
                retorno.Add(item);
            }
            return retorno;
        }
        public List<CumplimientoEntregaDetalle>getCumplimientoEntregaDet(string Desde, string Hasta,int nid)
        {
            DataTable dt = _IDaoReportes.getCumplimientoEntregaDet(Desde, Hasta,nid);
            List<CumplimientoEntregaDetalle> retorno = new List<CumplimientoEntregaDetalle>();
            foreach (DataRow row in dt.Rows)
            {
                CumplimientoEntregaDetalle item = new CumplimientoEntregaDetalle(row);
                retorno.Add(item);
            }
            return retorno;
        }

        public List<ReiteracionPedidos>getReiteracionPedidos(string Desde, string Hasta)
        {
            DataTable dt = _IDaoReportes.getReiteracionPedidos(Desde, Hasta);
            List<ReiteracionPedidos> retorno = new List<ReiteracionPedidos>();
            int v_orden = 1;

            foreach (DataRow row in dt.Rows)
            {
                ReiteracionPedidos item = new ReiteracionPedidos(row);
                item.orden = v_orden++;
                retorno.Add(item);
            }
            return retorno;
        }

        public List<HojaRutaEstados>getReporteEstados(string Fecha, int nid)
        {
            DataTable dt = _IDaoReportes.getReporteEstados(Fecha, nid);
            List<HojaRutaEstados> retorno = new List<HojaRutaEstados>();
            foreach (DataRow row in dt.Rows)
            {
                HojaRutaEstados item = new HojaRutaEstados(row);
                retorno.Add(item);
            }
            return retorno;
        }

        public List<track3_api_reportes_core.Middleware.Dto.Canal>getAllCanal()
        {
            DataTable dt = _IDaoReportes.getAllCanal();
            List<track3_api_reportes_core.Middleware.Dto.Canal> retorno = new List<track3_api_reportes_core.Middleware.Dto.Canal>();
            foreach (DataRow row in dt.Rows)
            {
                track3_api_reportes_core.Middleware.Dto.Canal item = new Dto.Canal(row);
                retorno.Add(item);
            }
            return retorno;
        }


        public List<ReiteracionPedidosSucDet> getReiteracionPedidosSucDet(string Desde, string Hasta)
        {
            DataTable dt = _IDaoReportes.getReiteracionPedidosSucDet(Desde, Hasta);
            List<ReiteracionPedidosSucDet> retorno = new List<ReiteracionPedidosSucDet>();
            foreach (DataRow row in dt.Rows)
            {
                ReiteracionPedidosSucDet item = new ReiteracionPedidosSucDet(row);

           

                retorno.Add(item);
            }
            return retorno;
        }


        public List<ReiteracionPedidosSucDet> getReiteracionPedidosSucDetx(int nid,string Desde, string Hasta)
        {
            DataTable dt = _IDaoReportes.getReiteracionPedidosSucDetx(nid,Desde, Hasta);
            List<ReiteracionPedidosSucDet> retorno = new List<ReiteracionPedidosSucDet>();
            int cantVeces = 0;
            int ref_In_Anterio = 0;
            int refInValue = 0;

            foreach (DataRow row in dt.Rows)
            {
                ReiteracionPedidosSucDet item = new ReiteracionPedidosSucDet(row);
                //Considerando que el dt viene ordenado por ref_id asc y fecha_evento asc le daremos la cantidad de repeticiones que este tiene del de las uplas.
                //Esto es 0 para el primer pedido en viaje de la seleccion,  1 para el segundo pedido en viaje de la seleccion y asi sucesivamente.
                //int cantVeces = 0;
                //int ref_In_Anterio = 0;
                if (int.TryParse(row["ref_in"].ToString(), out refInValue))
                {
                    if (ref_In_Anterio == refInValue)
                    {
                        cantVeces++;
                    }
                    else
                    {
                        cantVeces = 0;
                    }

                }
                else
                {
                    cantVeces = 0;
                }

                ref_In_Anterio = refInValue;
                item.veces = cantVeces;

                retorno.Add(item);
            }
            return retorno;
        }


        public List<diarioPDT> get_DiarioPDT(string p_fecha, int p_canal)
        {
            DataTable dt = _IDaoReportes.get_DiarioPDT(p_fecha, p_canal);
            List<diarioPDT> retorno = new List<diarioPDT>();
            foreach (DataRow row in dt.Rows)
            {
                diarioPDT item = new diarioPDT(row);

                // Inicializar una lista vacía para almacenar los objetos HojaRuta asociados a este registro diarioPDT.
                item.hdrs = new List<HojaRuta>();

                // Obtener la cantidad de Hojas de Ruta asociadas a este registro diarioPDT utilizando el objeto DaoPDT y otros filtros específicos.
                DataTable data = _IDaoReportes.getCantidadHDR(p_fecha, item.patente);

                // Iterar sobre cada fila en el DataTable para crear objetos HojaRuta y agregarlos a la lista de Hojas de Ruta del diarioPDT actual.
                foreach (DataRow dRow in data.Rows)
                {
                    // Crear un nuevo objeto HojaRuta a partir de la fila actual y agregarlo a la lista de Hojas de Ruta del diarioPDT.
                    HojaRuta hdr = new HojaRuta();
                    hdr.id = int.Parse(dRow["ID_HOJARUTA"].ToString());
                    hdr.wayWillId = dRow["WAYBILLID"].ToString();
                    item.hdrs.Add(hdr);
                }

                // Agregar el objeto diarioPDT completo, incluyendo la lista de Hojas de Ruta, a la lista de retorno.
               
                retorno.Add(item);
            }
            return retorno;
        }

        public List<Elemento> getRecorrido(long p_idHojaRuta)
        {
            DataTable dt = _IDaoReportes.getRecorrido(p_idHojaRuta);
            List<Elemento> retorno = new List<Elemento>();
            foreach (DataRow row in dt.Rows)
            {
                Elemento item = new Elemento();
                item.nombre = row["PROPIEDAD"].ToString();
                item.fecha_cierre = row["FECHA_CIERRE"].ToString();
                item.fecha_salida = row["FECHA_SALIDA"].ToString();
                item.id_dispositivo = int.Parse(row["ID_DISPOSITIVO"].ToString());
                item.b2bacces_id = int.Parse(row["B2BACCES_ID"].ToString());
                item.id_hojaruta = int.Parse(row["ID_HOJARUTA"].ToString());


                //----------------------------------------------------------------------------------
                // Traigo las posiciones de cada elemento dentro de esta ventana de tiempo
                List<Location> posiciones = new List<Location>();

                // Obtener las Posiciones del dispositivo dentro de la ventana de tiempo especificada.
                DataTable data = _IDaoReportes.ObtenerPosiciones(item.fecha_salida, item.fecha_cierre, item.id_dispositivo,"0");
                if (data.Rows.Count > 5)
                {
                    // Iterar sobre cada fila en el DataTable para crear objetos SpObtenerPosiciones y agregarlos a la lista de posiciones.
                    foreach (DataRow rowData in data.Rows)
                    {
                        // Crear un nuevo objeto SpObtenerPosiciones a partir de la fila actual y agregarlo a la lista de posiciones.
                        Location ps = new Location();

                        ps.Stamp = DateTime.Parse(rowData["fecha_posicion"].ToString());
                        ps.Lat = double.Parse(rowData["gpslatitud"].ToString());
                        ps.Lng = double.Parse(rowData["gpslongitud"].ToString());

                        posiciones.Add(ps);
                    }

                    // Asignar la lista de posiciones al elemento actual.
                    item.listPosition = posiciones;
                    //----------------------------------------------------------------
                    retorno.Add(item);
                }
              
                
            }
            return retorno;
        }

        public ActividadPorPatente ActividadPorPatente(string desde, string hasta, string patente)
        {
            ActividadPorPatente actividadPorPatente = new ActividadPorPatente();
            List<HojaRutaActividad> listaHR = new List<HojaRutaActividad>();

            DataTable dt = _IDaoReportes.GetActividadPorPatente(desde, hasta, patente);
            double kmReales = 0;

            foreach(DataRow row in dt.Rows)
            {
                if (!string.IsNullOrEmpty(row["FECHA_SALIDA"].ToString()))
                {
                    HojaRutaActividad hdr = new HojaRutaActividad();
                    hdr.Id = int.Parse(row["ID"].ToString());
                    hdr.IdEstado = int.Parse(row["ID_ESTADO"].ToString());

                    if (!string.IsNullOrEmpty(row["WAYBILLID"].ToString()))
                        hdr.WayBillId = row["WAYBILLID"].ToString();

                    listaHR.Add(hdr);

                    //Se suma el kilometraje real
                    kmReales += double.Parse(row["DISTANCIA_REAL"].ToString());
                }                
            }

            //Agrego las hojas de rutas que recupero a la actividad
            //Además agrrego el kilometraje real de todas las hojas de rutas.
            actividadPorPatente.listaHR = listaHR;
            actividadPorPatente.KmTotales = Math.Round(kmReales/1000, 2);

            return actividadPorPatente;
        }

        public List<ReporteVersionAPP>getReporteVersionAPP(String Desde, String Hasta, int Sucursal)
        {
            DataTable dt = _IDaoReportes.getReporteVersionAPP(Desde, Hasta, Sucursal);

            List<ReporteVersionAPP> retorno = new List<ReporteVersionAPP>();
            foreach (DataRow row in dt.Rows)
            {
                ReporteVersionAPP item = new ReporteVersionAPP(row);
                retorno.Add(item);
            }
            return retorno;
        }

        public List<ReporteReservasCD>getReservasCD(string Desde,string Hasta,string waybillid)
        {
            DataTable dt = _IDaoReportes.getReservasCD(Desde, Hasta, waybillid);

            List<ReporteReservasCD> retorno = new List<ReporteReservasCD>();
            foreach (DataRow row in dt.Rows)
            {
                ReporteReservasCD item = new ReporteReservasCD(row);
                retorno.Add(item);
            }
            return retorno;
        }
        
        public List<ReportePDT> ReportesPDT(string fecha, string canales)
        {
            DataTable dt = _IDaoReportes.getReportesPDT(fecha, canales);
            List<ReportePDT> reportes = new List<ReportePDT>();

            foreach (DataRow row in dt.Rows)
            {
                ReportePDT reporte = new ReportePDT();

                reporte.idCanal = int.Parse(row["ID_CANAL"].ToString());
                reporte.nombreCanal = row["NOMBRE"].ToString();
                reporte.cantidadPlanes = string.IsNullOrEmpty(row["CANTIDAD_PLANES"].ToString()) ? 0 : int.Parse(row["CANTIDAD_PLANES"].ToString());
                reporte.movilesPedidos = string.IsNullOrEmpty(row["MOVILES_PEDIDOS"].ToString()) ? 0 : int.Parse(row["MOVILES_PEDIDOS"].ToString());
                reporte.movilesAsignados = string.IsNullOrEmpty(row["MOVILES_ASIGNADOS"].ToString()) ? 0 : int.Parse(row["MOVILES_ASIGNADOS"].ToString());
                reporte.movilesPresentes = string.IsNullOrEmpty(row["MOVILES_PRESENTES"].ToString()) ? 0 : int.Parse(row["MOVILES_PRESENTES"].ToString());
                reporte.movilesEnViaje = string.IsNullOrEmpty(row["MOVILES_ENVIAJES"].ToString()) ? 0 : int.Parse(row["MOVILES_ENVIAJES"].ToString());

                reportes.Add(reporte);
            }

            return reportes;

        }

        //TRK3-1610 - (177984) - 20/12/2023: Circuito Digital - Reporte de Presencia
        public PresenciaRep PresenciaReporte(FiltroPresencia filtro)
        {
            PresenciaRep retorno = new PresenciaRep();
            retorno.planes = new List<PlanTrabajoProveedor>();
            retorno.presencias = new List<Presencia>();
            retorno.hojaRutas = new List<HojaRuta>();

            DataTable dtPlanesTrabajadorProveedor = _IDaoReportes.getPlanesTrabajoProveedor(filtro);

            if (dtPlanesTrabajadorProveedor.Rows.Count < 1 ) 
            {
                return retorno;
            }

            foreach(DataRow row in dtPlanesTrabajadorProveedor.Rows)
            {
                PlanTrabajoProveedor plan = new PlanTrabajoProveedor(row);
                DataTable datos = _IDaoReportes.getPresencias(plan.Id, filtro.Canales);

                foreach(DataRow item in datos.Rows)
                {
                    Presencia presencia = new Presencia();
                    presencia.Aprobada = DateTime.Parse(item["FECHA_AUTORIZADA"].ToString());
                    presencia.Tipo = new PresenciaTipo() { Nombre = item["NOMBRE_PRESENCIATIPO"].ToString() };
                    presencia.Sucursal = item["SUCURSAL"].ToString();

                    retorno.presencias.Add(presencia);
                }

                DataTable hojasRuta = _IDaoReportes.getHojasRuta(plan.Id, filtro.Canales);

                foreach(DataRow item in hojasRuta.Rows)
                {
                    HojaRuta hoja = new HojaRuta();

                    hoja.id = int.Parse(item["ID_HOJARUTA"].ToString());
                    hoja.canal = new Dto.Canal() { sucursal = int.Parse(item["SUCURSAL"].ToString()) };

                    if (item["FECHA_SALIDA"].ToString() != "")
                       hoja.fechaSalida = DateTime.Parse(item["FECHA_SALIDA"].ToString());

                    if (item["FECHA_CIERRE"].ToString() != "")
                        hoja.fecha_cierre = DateTime.Parse(item["FECHA_CIERRE"].ToString());

                    retorno.hojaRutas.Add(hoja);
                }

                retorno.planes.Add(plan);
            }

            return retorno;
        }
    }
}
