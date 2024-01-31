
namespace track3_api_reportes_core.Middleware.Dto
{
    public class FiltroPDT
    {
        public FiltroPDT() { }

        public int idCanal { get; set; }
       
        public string strFecha { get; set; }

        //TRK3-1360 (177984) 26/10/2023: Se puso como nulleable el atributo
        //ya que el endpoint de Reporte PDT no envía ese dato.
        public String? Legajo { get; set; }

        //TRK3-1275 (177984) 02/10/2023: Se agrego el atributo Canales para que lo utilice
        //la lógica de la API de vehículos en viajes.
        public string? Canales { get; set; }

    }
}
