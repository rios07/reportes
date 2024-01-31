using System.Data;


namespace track3_api_reportes_core.Middleware.Dto
{
    public class ReiteracionPedidos
    {


        public ReiteracionPedidos() { }

        public int id { get; set; }
   
        public int sucursal { get; set; }
        public int orden { get; set; }
        public int q_pedidos { get; set; }
        public int q_reiteracion { get; set; }

        public ReiteracionPedidos(DataRow row)
        {
            id = int.Parse(row["ID"].ToString());
            sucursal = int.Parse(row["SUCURSAL"].ToString());
            orden = 0; // int.Parse(row["orden"].ToString()) ;
            q_pedidos = int.Parse(row["Q_PEDIDOS"].ToString());
            q_reiteracion = int.Parse(row["Q_REITERACION"].ToString());
         
        }
    }
}
