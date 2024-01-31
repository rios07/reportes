using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using track3_api_reportes_core.Infraestructura.track3.Models.Servicios;

namespace Infraestructure.track3.Models.pedido
{
    public class GpsPedidoPropiedad
    {
        [Key]
        public Int64 Id { get; set; }
        public DateTime Stamp { get; set; }
        public string Activo { get; set; }
        public Int64 Version { get; set; }
        public Int64 IdPedido { get; set; }
        public Int64 IdPropiedad { get; set; }
        public virtual GpsPropiedad Propiedad { get; set; }
        public virtual GpsPedido Pedido { get; set; }
        ~GpsPedidoPropiedad()
        {

        }


    }
}
