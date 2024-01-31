using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.track3.Models.pedido
{
    public class GpsPedidosCOT
    {
        public GpsPedidosCOT()
        {
            
        }

        [Key]
        [Column("ID", TypeName = "INT32")]
        public Int32 Id { get; set; }
        [Column("STAMP", TypeName = "TIMESTAMP")]
        public DateTime Stamp { get; set; }
        [Column("ACTIVO", TypeName = "CHAR")]
        public string Activo { get; set; }
        [Column("VERSION", TypeName = "INT32")]
        public Int64 Version { get; set; }
        [Column("IDPEDIDO", TypeName = "INT32")]
        public int Idpedido { get; set; }
        [Column("IDHOJARUTA", TypeName = "INT32")]
        public int Idhojaruta { get; set; }
        [Column("MENSAJE", TypeName = "VARCHAR2")]
        public string? Mensaje { get; set; }
        [Column("PEDIDOREF", TypeName = "VARCHAR2")]
        public string Pedidoref { get; set; }
        [Column("CODIGOCOT", TypeName = "VARCHAR2")]
        public string Codigocot { get; set; }
        [Column("GENERADO", TypeName = "INT32")]
        public int Generado { get; set; }
        [Column("REMITO", TypeName = "VARCHAR2")]
        public string Remito { get; set; }

    }
}
