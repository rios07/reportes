using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.track3.Models.movil;

namespace Infraestructure.track3.Models.hojaruta
{
    public class GpsHojaRutaMovil
    {
        [Key]
        public int Id { get; set; }
        public int Numero { get; set; }
        public DateTime Stamp { get; set; }
        public string Activo { get; set; }
        public string Patente { get; set; }
        public decimal Capacidad { get; set; }
        public decimal Apilabilidad { get; set; }
        public string Proveedor { get; set; }
        public decimal Ocupacion { get; set; }
        public Int32 IdHojaRuta { get; set; }
        public virtual GpsHojaRuta HojaRuta { get; set; }
        public virtual GpsMovil Movil { get; set; }
        ~GpsHojaRutaMovil() { }
    }
}
