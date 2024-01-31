using Infraestructure.track3.Models.movil;
using System.ComponentModel.DataAnnotations;

namespace track3_api_reportes_core.Middleware.Dto
{
    public class Moviles
    {
        public class GPS_BASE
        {

        }
        public class GPS_MOVIL : GPS_BASE
        {
            [Key]
            public int ID { get; set; }
            public string PATENTE { get; set; }
            public int NUMERO { get; set; }
            public string DESCRIPCION { get; set; }
            public int ANIO { get; set; }
            public string MODELO { get; set; }
            public string MARCA { get; set; }
            public string COLOR { get; set; }
            public string REFERENTE { get; set; }
            //public DateTime STAMP { get; set; }
            public string ACTIVO { get; set; }
            public decimal SUPERFICIE { get; set; }
            public decimal ALTURA { get; set; }
            public decimal PESOMAXIMO { get; set; }
            public int ID_MOVILTIPO { get; set; }
            public int ID_PROVEEDOR { get; set; }
            public GPS_MOVILTIPO MovilTipo { get; set; }
        }

        public class movil : GPS_MOVIL
        {
            public movil() { }
            public string nombreProveedor { get; set; }
            public string nombreMovilTipo { get; set; }
           // public List<MovilPropiedad> propiedades { get; set; }
            public Boolean editable { get; set; }
        }
    }
}
