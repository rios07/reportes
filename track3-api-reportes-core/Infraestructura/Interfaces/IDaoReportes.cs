using System.Data;
using track3_api_reportes_core.Middleware.Dto;

namespace track3_api_reportes_core.Infraestructura.Interfaces
{
    public interface IDaoReportes
    {
        public DataTable ReporteMovilesProveedor(String Desde, String Hasta);

        public DataTable ReporteViajesMovilesPedidos(String Desde, String Hasta, int p_canal);

        public DataTable ObtenerPosiciones(String Desde, String Hasta, int? p_Id_Dispositivo, string? p_Identificador);

        public string getIMEI(int p_id_dispositivo);

        public DataTable GetKilometrosHojaRuta(String Desde, String Hasta, int? Sucursal);

        public DataTable getMovilesEnViaje(int p_canal);

        public DataTable getCantidadHDR(string p_fecha,string p_patente);

        public DataTable get_DiarioPDT(string p_fecha, int p_canal);

        public DataTable getCumplimientoEntrega(string desde, string hasta);
        public DataTable getCumplimientoEntregaDet(string desde, string hasta,int nid);

        public DataTable getReiteracionPedidos(string desde, string hasta);
        public DataTable getReporteEstados(string fecha, int nid);
        public DataTable getAllCanal();


        public DataTable getReiteracionPedidosSucDet(string desde, string hasta);
        public DataTable getReiteracionPedidosSucDetx(int nid,string desde, string hasta);

        public DataTable getRecorrido(long IdHojaRuta);

        public DataTable GetActividadPorPatente(string desde, string hasta, string patente);

        public DataTable getReportesPDT(string fecha, string canales);


        public DataTable getReporteVersionAPP(String Desde, String Hasta, int Sucursal);

        public DataTable getReservasCD(String Desde, String Hasta, string waybillid);

        //TRK3-1610 - (177984) - 20/12/2023: Circuito Digital - Reporte de Presencia.
        public DataTable getPlanesTrabajoProveedor(FiltroPresencia filtro);

        //TRK3-1610 - (177984) - 20/12/2023: Circuito Digital - Reporte de Presencia.
        public DataTable getPresencias(int id, string canales);

        //TRK3-1610 - (177984) - 20/12/2023: Circuito Digital - Reporte de Presencia.
        public DataTable getHojasRuta(int id, string canales);
    }
}
