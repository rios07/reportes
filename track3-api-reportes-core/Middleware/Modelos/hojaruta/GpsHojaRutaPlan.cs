using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.track3.Models.hojaruta
{
    public class GpsHojaRutaPlan
    {
        public Int32 Id { get; set; }
        public long IdHojaRuta { get; set; }
        public Int32 IdPlanTrabajoProveedor { get; set; }
    }
}
