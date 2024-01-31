using Infraestructure.track3.Models.movil;

namespace track3_api_reportes_core.Middleware.Dto
{
    public class RemitoImpresion
    {
        public RemitoImpresion()
        {

        }

        public long IdHojaruta { get; set; }
        public DateTime Fecha { get; set; }
        public string Hojaruta { get; set; }
        public int Sucursal { get; set; }
        public string DNI { get; set; }
        public string Chofer { get; set; }
        public string Cadete { get; set; }
        public string Legajo { get; set; }
        public string Patente { get; set; }
        public int NumeroMovil { get; set; }
        public List<PedidoRemito> PedidosRemitos { get; set; }
        public GpsProveedor Proveedor { get; set; }
        public string CAI { get; set; }
        public DateTime fechaVencimientoCAI { get; set; }
    }
}
