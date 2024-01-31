using Infraestructure.track3.Models.hojaruta;
using track3_api_reportes_core.Aplicacion.Filtros;
using track3_api_reportes_core.Middleware.Dto;
using track3_api_reportes_core.Middleware.Modelos.movil;
using track3_api_reportes_core.Middleware.Modelos.Track;    


namespace track3_api_reportes_core.Aplicacion.Interfaces
{
    public interface IReportes
    {
        public List<SpCdProdViaje> ReporteMovilesProveedor(FiltroReportes filtro);

        public List<SpDigViajMovPed> ReporteViajesMovilesPedidos(Mobiliario filtro);

        public List<SpObtenerPosiciones>ObtenerPosiciones(Dispositivo filtro);

        public List<SpObtenerPosiciones> ObtenerPosicionesSW(Dispositivo filtro);

        public List<SPGetKilometrosHojaRuta>GetKilometrosHojaRuta(Filtros.Canal Filtro);

        public List<spGetMovilesEnViaje> getMovilesEnViaje(Mobiliario Filtro);
        
        public List<diarioPDT> get_DiarioPDT(FiltroPDT Filtro);

        public List<CumplimientoEntrega> getCumplimientoEntrega(FiltroReportes filtro);

        public List<CumplimientoEntregaDetalle> getCumplimientoEntregaDet(FiltroReportes filtro);

        public List<ReiteracionPedidos> getReiteracionPedidos(FiltroReportes filtro);
        public List<HojaRutaEstados>getReporteEstados(FiltroEstadosHR filtro);

        public List<track3_api_reportes_core.Middleware.Dto.Canal>getAllCanal();


        public List<ReiteracionPedidosSucDet> getReiteracionPedidosSucDet(FiltroReportes filtro);

        public List<ReiteracionPedidosSucDet> getReiteracionPedidosSucDetx(FiltroReportes filtro);

        public Task<List<Elemento>> getRecorrido(long IdHojaRuta);
        public Task<List<Elemento>> GetListElementosToGetRecorrido(long IdHojaRuta, GpsHojaRuta HojaRuta);

        public ActividadPorPatente ActividadPorPatente(FiltroEvento filtro);

        public List<ReporteVersionAPP> getReporteVersionAPP(Mobiliario filtro);
        public List<ReporteReservasCD> getReservasCD(FiltroReservasCD filtro);

        public List<ReportePDT> ReportesPDT(FiltroPDT filtro);

        abstract Task<List<Flota>> FlotaPorCanal(string idCanal);

        abstract PresenciaRep PresenciaReporte(FiltroPresencia filtro);
    }
}
