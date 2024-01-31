namespace track3_api_reportes_core.Middleware.Dto
{
    public class ReportePDT
    {
        public ReportePDT() { }

        public int idCanal { get; set; }
        public string nombreCanal { get; set; }
        public int cantidadPlanes { get; set; }
        public int movilesPedidos { get; set; }
        public int movilesAsignados { get; set; }
        public int movilesPresentes { get; set; }
        public int movilesEnViaje { get; set; }
    }
}
