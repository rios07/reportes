using System.Data;

namespace track3_api_reportes_core.Middleware.Dto
{
    public class ReiteracionPedidosSucDet
    {
            public ReiteracionPedidosSucDet() { }

            public int id_pa { get; set; }
            public DateTime stamp_pedidoref { get; set; }
            public string ref_in { get; set; }
            public string nro_referencia { get; set; }  
            public string waybillid { get; set; }
            public int id_suc { get; set; }
            public int sucursal { get; set; }
            public int veces { get; set; }
        
            public ReiteracionPedidosSucDet(DataRow row)
            {
                id_pa = int.Parse(row["ID_PA"].ToString());
                stamp_pedidoref = DateTime.Parse(row["STAMP_PEDIDOREF"].ToString());
                ref_in = row["REF_IN"].ToString();
                nro_referencia = row["NRO_REFERENCIA"].ToString();
                waybillid = row["WAYBILLID"].ToString();
                id_suc = int.Parse(row["ID_SUC"].ToString());
                sucursal = int.Parse(row["SUCURSAL"].ToString());
                veces = 0;            

            }
    
    }
}
