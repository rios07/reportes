using Infraestructure.track3.Models.planificacion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using track3_api_reportes_core.Middleware.Modelos.usuarios;

namespace Infraestructure.track3.Models.pedido
{
    public class GpsPedidoRefAudit
    {
        public GpsPedidoRefAudit(DataRow row)
        {
            this.id = long.Parse(row["ID_PEDIDO_REF_AUDIT"].ToString());
            this.idPedido = long.Parse(row["ID_PEDIDO"].ToString());
            this.idPedidoRef = long.Parse(row["ID_PEDIDOREF"].ToString());
            this.IdHojaRuta = long.Parse(row["ID_HOJARUTA"].ToString());
            this.NroReferencia = row["NRO_REFERENCIA"].ToString();
            this.Cre = row["CRE"].ToString();
            this.Cae = row["CAE"].ToString();
            this.FechaEvento = DateTime.Parse(row["FECHA_EVENTO"].ToString());
            this.estado = new GpsEstado(row);
            this.Operacion = new GpsEstado { Id = long.Parse(row["TIPO_OPERACION"].ToString()), Nombre = row["OPERACION"].ToString() };
            this.User = row["USUARIO"].ToString();
            this.Usuario = new Usuario { Legajo = User };

        }

        public GpsPedidoRefAudit()
        {

        }
        public Int64 id { get; set; }
        public DateTime Stamp { get; set; }
        public Int64 idPedido { get; set; }
        public Int64 idPedidoRef { get; set; }
        public Int64? IdHojaRuta { get; set; }
        public string NroReferencia { get; set; }
        public string? User { get; set; }
        public string Cre { get; set; }
        public string Cae { get; set; }
        public int IdEstado { get; set; }
        public string? Motivo { get; set; }
        public int TipoOperacion { get; set; }
        public DateTime? FechaEvento { get; set; }

        [NotMapped]
        public GpsEstado estado { get; set; }
        public virtual GpsPedido pedido { get; set; }
        [NotMapped]
        public string wayBillId { get; set; }
        [NotMapped]
        public GpsEstado Operacion { get; set; }
        public Usuario Usuario { get; set; }
    }
}
