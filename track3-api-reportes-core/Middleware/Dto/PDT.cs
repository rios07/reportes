using static track3_api_reportes_core.Middleware.Dto.Moviles;

namespace track3_api_reportes_core.Middleware.Dto
{
    public class PDT
    {
        public class FiltroPDT
        {
            public FiltroPDT() { }

            public int idCanal { get; set; }
            public DateTime fechaDesde { get; set; }
            public DateTime fechaHasta { get; set; }
            public int idProveedor { get; set; }
            public int id_plan { get; set; }
            public GPS_MOVIL movil { get; set; }
            public Chofer chofer { get; set; }
            public bool presencia { get; set; }
            public string canales { get; set; }
        }


    }
}
