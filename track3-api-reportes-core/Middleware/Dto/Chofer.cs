using System.ComponentModel.DataAnnotations;

namespace track3_api_reportes_core.Middleware.Dto
{
    public class Chofer
    {
        public string ACTIVO { get; set; }
        [Key]
        public int ID { get; set; }
        public int IDENTIFICADOR { get; set; }
        public int VERSION { get; set; }
        public string TELEFONO { get; set; }
        public string APELLIDO { get; set; }
        public string NOMBRE { get; set; }
        public string DNI { get; set; }

    }

    public class GPS_CHOFER : Chofer
    {

    }

}
