using System.Data;
using Infraestructure.track3.Models.pedido;

namespace track3_api_reportes_core.Middleware.Dto
{
    public class PedidoRemito
    {
        public PedidoRemito(DataRow row)
        {
            Remito = row["REMITO"].ToString();
            Generado = int.Parse(row["GENERADO"].ToString());
            COT = row["CODIGOCOT"].ToString();
            Mensaje = row["MENSAJE"].ToString();
            Pedidos = new List<GpsPedido>();
        }
        public string Remito { get; set; }
        public int Generado { get; set; }
        public string COT { get; set; }
        public string Mensaje { get; set; }
        public List<GpsPedido> Pedidos { get; set; }
    }
}
