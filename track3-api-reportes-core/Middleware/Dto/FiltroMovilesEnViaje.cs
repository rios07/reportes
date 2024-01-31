
namespace track3_api_reportes_core.Middleware.Dto
{
    public class FiltroMovilesEnViaje
    {
        public FiltroMovilesEnViaje() { }

        public int idCanal { get; set; }
       
        public int id_plan { get; set; }

        public String? Legajo { get; set; }

        //TRK3-1275 (177984) 02/10/2023: Se agrego el atributo Canales para que lo utilice
        //la lógica de la API de vehículos en viajes.
        public string? Canales { get; set; }

    }
}
