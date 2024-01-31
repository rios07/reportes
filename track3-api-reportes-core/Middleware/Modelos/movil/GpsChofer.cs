using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.track3.Models.movil
{
    public class GpsChofer
    {
        [Key]
        public Int32 Id { get; set; }
        public DateTime Stamp { get; set; }
        public string Activo { get; set; }
        public Int64 Version { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public string Telefono { get; set; }
        public Int32 Identificador { get; set; }

        ~GpsChofer()
        {

        }
    }
}
