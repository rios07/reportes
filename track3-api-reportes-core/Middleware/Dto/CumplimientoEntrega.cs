using System.Data;

namespace track3_api_reportes_core.Middleware.Dto
{
    public class CumplimientoEntrega
    {
       
            public CumplimientoEntrega() { }

            public int id_sucursal { get; set; }
            public string nombre { get; set; }
            public int sucursal { get; set; }
            public int t_pedidos { get; set; }
            public int p_out_banda { get; set; }
            public double porcentaje { get; set; }
            public int orden { get; set; }

            public CumplimientoEntrega(DataRow row)
            {
                orden = 0;
                id_sucursal = int.Parse(row["id_sucursal"].ToString());
                nombre = row["nombre"].ToString();
                sucursal = int.Parse(row["sucursal"].ToString());
                t_pedidos = int.Parse(row["t_pedidos"].ToString());
                p_out_banda = int.Parse(row["p_out_banda"].ToString());
                porcentaje = double.Parse(row["porcentaje"].ToString());
             
            }
       


    }
}
