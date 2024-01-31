using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.track3.Models.pedido
{
    public class GpsPedidoTipo
    {
        [Key]
        [Column("ID", TypeName = "INT32")]
        public Int32 Id { get; set; }

        [Column("STAMP", TypeName = "TIMESTAMP")]
        public DateTime Stamp { get; set; }

        [Column("ACTIVO", TypeName = "CHAR")]
        public string Activo { get; set; }

        [Column("VERSION", TypeName = "INT32")]
        public Int64 Version { get; set; }

        [Column("NOMBRE", TypeName = "VARCHAR2")]
        public string Nombre { get; set; }

        ~GpsPedidoTipo()
        {

        }

        public GpsPedidoTipo(DataRow row)
        {
            this.Id = int.Parse(row["ID_TIPO"].ToString());
            this.Nombre = row["TIPO"].ToString();
        }

        public GpsPedidoTipo()
        {
            
        }
    }
}
