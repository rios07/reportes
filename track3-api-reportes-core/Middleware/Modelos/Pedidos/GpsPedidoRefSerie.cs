using Infraestructure.track3.Models.planificacion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.track3.Models.pedido
{
    public class GpsPedidoRefSerie
    {
        public GpsPedidoRefSerie()
        {
            
        }
        ~GpsPedidoRefSerie(){}
        [Key]
        public long Id { get; set; }
        public DateTime Stamp { get; set; }
        public string Activo { get; set; }
        public string NroReferencia { get; set; }
        public int IdTipo { get; set; }
        public string Serie { get; set; }
        public int IdEstado { get; set; }
        public string? Observaciones { get; set; }
        [NotMapped]
        public GpsEstado Estado { get; set; }
    }
}
