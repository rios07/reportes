using Infraestructure.track3.Models.hojaruta;
using Infraestructure.track3.Models.pedido;

namespace track3_api_reportes_core.Middleware.Dto
{
    public class ResponsePlanificacion
    {
        public ResponsePlanificacion()
        {

        }
        public GpsHojaRuta HojaRuta { get; set; }
        public List<GpsBandaHoraria> ListBandaHoraria { get; set; }
        public string? WayBillId { get; set; }
        ~ResponsePlanificacion() { }

    }
}
