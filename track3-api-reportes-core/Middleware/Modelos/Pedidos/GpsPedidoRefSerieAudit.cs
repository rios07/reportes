using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.track3.Models.pedido
{
    public class GpsPedidoRefSerieAudit
    {
        public GpsPedidoRefSerieAudit()
        {
            
        }
        ~GpsPedidoRefSerieAudit(){}
        [Key]
        public int Id { get; set; }
        public DateTime Stamp { get; set; }
        public string Activo { get; set; }
        public string NroReferencia { get; set; }
        public int IdTipo { get; set; }
        public string Serie { get; set; }
        public int IdEstado { get; set; }
        public string Observaciones { get; set; }
    }
}
