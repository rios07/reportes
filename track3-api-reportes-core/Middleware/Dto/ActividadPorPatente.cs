
namespace track3_api_reportes_core.Middleware.Dto
{
    public class ActividadPorPatente
    {
        public ActividadPorPatente() { }

        public double KmTotales { get; set; }

        public List<HojaRutaActividad> listaHR { get; set; }
    }
}
