using System.ComponentModel.DataAnnotations;

namespace track3_api_reportes_core.Middleware.Dto
{
    public class MovilTipo
    {
        [Key]
        public int ID { get; set; }
        public int VERSION { get; set; }
        public string DESCRIPCION { get; set; }
        public int PESO { get; set; }
        public string UMP { get; set; }
        public int VOLUMEN { get; set; }
        public string UMV { get; set; }
        public string ACTIVO { get; set; }
    }

    public class GPS_MOVILTIPO : MovilTipo
    {

    }


}
