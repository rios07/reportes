using System.Data;
using Infraestructure.track3.Models.hojaruta;
using track3_api_reportes_core.Aplicacion.Filtros;
using track3_api_reportes_core.Middleware.Dto;
using track3_api_reportes_core.Middleware.Modelos.Track;

namespace track3_api_reportes_core.Middleware.Interfaces
{
    public interface IReportesMiddleware
    {
        public List<SpCdProdViaje> ReporteMovilesProveedor(String Desde, String Hasta);
        public List<SpDigViajMovPed> ReporteViajesMovilesPedidos(String Desde, String Hasta, int p_canal);
        public List<SpObtenerPosiciones> ObtenerPosiciones(String Desde, String Hasta, int? p_Id_Dispositivo, string? p_Identificador);
        public List<SpObtenerPosiciones> ObtenerPosicionesSW(String Desde, String Hasta, int p_Id_Dispositivo, string? p_Identificador);
        public List<SPGetKilometrosHojaRuta> GetKilometrosHojaRuta(String Desde, String Hasta, int? Sucursal);
        public List<spGetMovilesEnViaje>getMovilesEnViaje(int p_canal);
        public List<diarioPDT>get_DiarioPDT(string p_fecha, int p_canal);
        List<CumplimientoEntrega> getCumplimientoEntrega(string strFechaDesde, string strFechaHasta);
        List<CumplimientoEntregaDetalle> getCumplimientoEntregaDet(string strFechaDesde, string strFechaHasta,int nid);
        public List<ReiteracionPedidos> getReiteracionPedidos(string strFechaDesde, string strFechaHasta);
        public List<HojaRutaEstados>getReporteEstados(string strFecha,int nid);

        public List<track3_api_reportes_core.Middleware.Dto.Canal>getAllCanal();

        public List<ReiteracionPedidosSucDet> getReiteracionPedidosSucDet(string strFechaDesde, string strFechaHasta);
        public List<ReiteracionPedidosSucDet> getReiteracionPedidosSucDetx(int nid,string strFechaDesde, string strFechaHasta);
        List<Elemento> getRecorrido(long IdHojaRuta);
        public ActividadPorPatente ActividadPorPatente(string desde, string hasta, string patente);
        public List<ReportePDT> ReportesPDT(string fecha, string canales);
        public List<ReporteVersionAPP> getReporteVersionAPP(String Desde, String Hasta, int Sucursal);
        public List<ReporteReservasCD> getReservasCD(String Desde, String Hasta, string waybillid );
        public PresenciaRep PresenciaReporte(FiltroPresencia filtro);
    }
}
