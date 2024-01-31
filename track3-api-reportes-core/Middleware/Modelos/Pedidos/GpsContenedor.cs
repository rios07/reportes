using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.track3.Models.pedido
{
    public class GpsContenedor
    {
        [Key]
        [Column("ID", TypeName = "INT32")]
        public Int32 Id { get; set; }

        [Column("STAMP", TypeName = "TIMESTAMP")]
        public DateTime Stamp { get; set; }

        [Column("ACTIVO", TypeName = "CHAR")]
        public string Activo { get; set; }

        [Column("VERSION", TypeName = "INT64")]
        public Int64 Version { get; set; }

        [Column("ID_PEDIDO", TypeName = "INT64")]
        public Int64 IdPedido { get; set; }

        [Column("ID_CONTENEDOR", TypeName = "INT64")]
        public Int64 IdContenedor { get; set; }

        [Column("NRO_CONTENEDOR_REF", TypeName = "INT32")]
        public Int32 NroContenedorRef { get; set; }

        [Column("NRO_SERIE_REF", TypeName = "VARCHAR2")]
        public string? NroSerieRef { get; set; }
        public virtual GpsPedido Pedido { get; set; }
        ~GpsContenedor()
        {

        }
    }
}
