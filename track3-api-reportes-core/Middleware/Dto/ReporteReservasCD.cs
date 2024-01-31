using System.Data;

namespace track3_api_reportes_core.Middleware.Dto
{
    public class ReporteReservasCD
    {
        public ReporteReservasCD() { }

             
        public string Fecha { get; set; }
        public string Hoja_de_Ruta { get; set; }
        public string Tipo_de_Movil { get; set; }
        public int Orden { get; set; }  
        public string Reserva { get; set; }
        public string Km_Planificados { get; set; }
        public string Tiempo_Planificados { get; set; }
        public string Banda_Horaria { get; set; }
        public string Entrega_Estimada { get; set; }
        public string? Sigue_a_Destino { get; set; }
        public string? Llego_a_Destino { get; set; }
        public string? Entrego { get; set; }
        public string? No_Entrego { get; set; }



        public ReporteReservasCD(DataRow row)
        {

            Fecha = row["Fecha"].ToString();
            Hoja_de_Ruta = row["hoja_de_ruta"].ToString();
            Tipo_de_Movil = row["tipo_de_movil"].ToString();
            Orden = int.Parse(row["orden"].ToString());
            Reserva = row["reserva"].ToString();
            Km_Planificados = row["distancia_descripcion"].ToString();
            Tiempo_Planificados = row["tiempo_descripcion"].ToString();
            Banda_Horaria = row["banda_horaria"].ToString();
            Entrega_Estimada = row["Entrega_estimada"].ToString();
            Sigue_a_Destino = (row["SIGUE_A_DESTINO"] != null && !string.IsNullOrEmpty(row["SIGUE_A_DESTINO"].ToString())) ? row["SIGUE_A_DESTINO"].ToString() : "-";
            Llego_a_Destino = (row["LLEGO_A_DESTINO"] != null && !string.IsNullOrEmpty(row["LLEGO_A_DESTINO"].ToString())) ? row["LLEGO_A_DESTINO"].ToString() : "-";
            Entrego = (row["Entrego"] != null && !string.IsNullOrEmpty(row["Entrego"].ToString())) ? row["Entrego"].ToString() : "-";
            No_Entrego = (row["NO_ENTREGO"] != null && !string.IsNullOrEmpty(row["NO_ENTREGO"].ToString())) ? row["NO_ENTREGO"].ToString() : "-";
         
        }

    }
}
