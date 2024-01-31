using Infraestructure.track3.Models.planificacion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.track3.Models.pedido
{
    public class GpsPedidoRef
    {
        public GpsPedidoRef(DataRow row)
        {
            id = long.Parse(row["ID"].ToString());
            idPedido = long.Parse(row["ID_PEDIDO"].ToString());
            NroReferencia = row["NRO_REFERENCIA"].ToString();
            Cre = row["CRE"].ToString();
            Cae = row["CAE"].ToString();
            IdEstado= int.Parse(row["ID_ESTADO"].ToString());
            FechaEvento = DateTime.Parse(row["FECHA_EVENTO"].ToString());
            Estado = new GpsEstado(row);
             
        }

        public GpsPedidoRef()
        {
            
        }
        public Int64 id { get; set; }
        public Int64 idPedido { get; set; }
        public string NroReferencia { get; set; }
        public string Cre { get; set; }
        public string Cae { get; set; }
        public int IdEstado { get; set; }
        public int TipoOperacion { get; set; }
        public DateTime? FechaEvento { get; set; }
        public string? Motivo { get; set; }
        public virtual GpsPedido pedido { get; set; }

        [NotMapped]
        public GpsEstado Estado { get; set; }
        [NotMapped]
        public GpsEstado Operacion { get; set; }
    }
}
