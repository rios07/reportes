using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.track3.Models.planificacion
{
    public class GpsPropiedad
    {
        public GpsPropiedad() { }


        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public int Orden { get; set; }
        public int IdPropiedadTipo { get; set; }

        public GpsPropiedadTipo Tipo { get; set; }
    }
}
