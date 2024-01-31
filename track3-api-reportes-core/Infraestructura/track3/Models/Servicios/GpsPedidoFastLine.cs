using System.Data;

namespace track3_api_reportes_core.Infraestructura.track3.Models.Servicios
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
