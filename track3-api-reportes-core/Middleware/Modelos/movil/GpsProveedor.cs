using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.track3.Models.planificacion;

namespace Infraestructure.track3.Models.movil
{
    public class GpsProveedor
    {

        public GpsProveedor()
        {
            
        }
        ~GpsProveedor(){}
        public int Id { get; set; }
        public DateTime Stamp { get; set; }
        public string Activo { get; set; }
        public int Version { get; set; }
        public string Nombre { get; set; }
        public string Mail { get; set; }
        public string Cuit { get; set; }
        public string Telefono { get; set; }
        public int Id_Direccion { get; set; }
        public virtual GpsDireccion Direccion { get; set; }
    }
}
