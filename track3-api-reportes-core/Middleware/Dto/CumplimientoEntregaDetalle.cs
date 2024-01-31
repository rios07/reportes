using System.Data;

namespace track3_api_reportes_core.Middleware.Dto
{
    public class CumplimientoEntregaDetalle
    {
        //Constructor vacio
        public CumplimientoEntregaDetalle() { }

        //Propiedades de la clase
        public int id_hojaruta { get; set; }
        public string nro_pedido_ref { get; set; }
        public int r_desde { get; set; }
        public int r_hasta { get; set; }
        public string fecha_evento { get; set; }
        public string fecha_plan { get; set; }
        public string waybillid { get; set; }
        public string bandahoraria { get; set; }
        public int min_diferencia { get; set; }
        public int id_sucursal { get; set; }
        public int sucursal { get; set; }
        public int id_movil { get; set; }
        public string patente { get; set; }
        public int numero { get; set; }
        public int id_proveedor { get; set; }
        public string nombre { get; set; }



        //Mapeo de la tabla 
        public CumplimientoEntregaDetalle(DataRow row)
        {
            id_hojaruta = int.Parse(row["ID_HOJARUTA"].ToString());
            nro_pedido_ref = row["NRO_PEDIDO_REF"].ToString();
            r_desde = int.Parse(row["R_DESDE"].ToString());  
            r_hasta= int.Parse(row["R_HASTA"].ToString());
            fecha_evento = row["FECHA_EVENTO"].ToString(); // ((DateTime)row["FECHA_EVENTO"]).ToString("dd/MM/yyyy HH:mm");
            fecha_plan = row["FECHA_PLAN"].ToString(); // ((DateTime)row["FECHA_PLAN"]).ToString("dd/MM/yyyy HH:mm");
            waybillid = row["WAYBILLID"].ToString();
            bandahoraria = row["DESCRIPCION"].ToString();
            id_sucursal= int.Parse(row["id"].ToString());
            min_diferencia= int.Parse(row["min_diferencia"].ToString());
            sucursal = int.Parse(row["SUCURSAL"].ToString());
            id_movil = int.Parse(row["ID_MOVIL"].ToString());
            patente = row["PATENTE"].ToString();
            numero = int.Parse(row["NUMERO"].ToString());
            id_proveedor = int.Parse(row["ID_PROVEEDOR"].ToString());
            nombre = row["NOMBRE"].ToString();


        }

    }
}
