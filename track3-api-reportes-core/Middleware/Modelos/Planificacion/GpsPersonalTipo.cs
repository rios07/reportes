using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.track3.Models.planificacion
{
    public class GpsPersonalTipo
    {
        [Key]
        public Int32 IdPersonaTipo { get; set; }
        public string Activo { get; set; }
        public string Nombre { get; set; }
    }
}
