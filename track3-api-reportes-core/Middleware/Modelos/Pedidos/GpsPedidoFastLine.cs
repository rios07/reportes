using System.Data;

namespace track3_api_reportes_core.Middleware.Modelos.Pedidos
{
    public class GpsPedidoFastLine
    {
        public GpsPedidoFastLine(DataRow row)
        {
            PedidoFastLine = row["FASTLINE"].ToString();
            PedidoOriginal = row["PED_ORIGINAL"].ToString();
            if (!string.IsNullOrEmpty(PedidoFastLine))
            {
                FastLine = true;
            }
        }
        public int Id { get; set; }
        public string PedidoOriginal { get; set; }
        public string PedidoFastLine { get; set; }
        public bool FastLine { get; set; }
    }

}

