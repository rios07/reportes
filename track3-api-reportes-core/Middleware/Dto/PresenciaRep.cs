namespace track3_api_reportes_core.Middleware.Dto
{
    public class PresenciaRep
    {
        public PresenciaRep() { }

        public List<PlanTrabajoProveedor> planes { get; set; }
        public List<Presencia> presencias { get; set; }
        public List<HojaRuta> hojaRutas { get; set; }
    }
}
