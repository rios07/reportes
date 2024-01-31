namespace track3_api_reportes_core.Middleware.Dto
{
    public class FiltroPresencia
    {
        public FiltroPresencia() { }

        public string FechaDesde { get; set; }
        public string FechaHasta { get; set; }
        public MovilPresencia Movil { get; set; }
        public ChoferPresencia Chofer { get; set; }
        public string Canales { get; set; }
    }
}
