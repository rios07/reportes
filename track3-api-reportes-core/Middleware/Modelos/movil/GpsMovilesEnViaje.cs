
namespace Infraestructure.track3.Models.movil
{
    public class GpsMovilesEnViaje
    {
        public GpsMovilesEnViaje()
        {
            this.listaMovilesEnViaje = new List<GpsMovilesEnViajeDetalle>();
        }

        public int idCanal { get; set; }
        public string nombreCanal { get; set; }
        public int movilesEnViaje { get; set; }
        public int? pedidosReservasEnViaje { get; set; }
        public List<GpsMovilesEnViajeDetalle> listaMovilesEnViaje { get; set; }
    }

    public class GpsMovilesEnViajeDetalle
    {
        public GpsMovilesEnViajeDetalle()
        {
        }

        public int idCanal { get; set; }
        public long idHojaruta { get; set; }
        public string waybillid { get; set; }
        public string Movil { get; set; }
        public int pedidosTotales { get; set; }
        public int pedidosEntregados { get; set; }
        public int pedidosNoEntregados { get; set; }
        public int pedidosRetirados { get; set; }
        public int pedidosNoRetirados { get; set; }
        public int pedidosCancelados { get; set; }
        public int puntosDeEntrega { get; set; }
        public DateTime fechaCierre { get; set; }
    }
}

