using System.Data;

namespace track3_api_reportes_core.Middleware.Dto
{
    public class HojaRutaEstados
    {
        public HojaRutaEstados() { }
        public int id_hojaruta { get; set; }
        public string hr { get; set; }
        public int sucursal { get; set; }
        public string fecha { get; set; }
        public int planificada { get; set; }
        public int precargada { get; set; }
      
        public int cargada { get; set; }
     
        public int creada { get; set; }
      
        public int EnViaje { get; set; }
      
        public int cerrada { get; set; }
    
        public int liquidada { get; set; }
   
        public HojaRutaEstados(DataRow row)
        {
            sucursal = int.Parse(row["sucursal"].ToString());
            id_hojaruta = int.Parse(row["id_hojaruta"].ToString());
            hr = row["hr"].ToString();
            fecha = row["fecha"].ToString();
            planificada = int.Parse(row["planificada"].ToString());
            precargada = int.Parse(row["precargada"].ToString());
            cargada = int.Parse(row["cargada"].ToString());
            creada = int.Parse(row["creada"].ToString());
            EnViaje = int.Parse(row["EnViaje"].ToString());
            cerrada = int.Parse(row["cerrada"].ToString());
            liquidada = int.Parse(row["liquidada"].ToString());
        }



    }
}
