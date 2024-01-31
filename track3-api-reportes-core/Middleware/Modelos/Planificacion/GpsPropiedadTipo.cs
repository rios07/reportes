using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.track3.Models.planificacion
{
    public class GpsPropiedadTipo
    {
        public GpsPropiedadTipo() { }

        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }
    }
}
