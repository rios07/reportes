using track3_api_reportes_core.Aplicacion.Requests;

namespace track3_api_reportes_core.Middleware.Modelos.Track
{
    public class Elemento
    {
            public Elemento() { }


            public int id { get; set; }
            public string nombre { get; set; }
            public string activo { get; set; }
            public int id_elementoTipo { get; set; }
            public ElementoTipo tipo { get; set; }
            public int orden { get; set; }
            public List<Location> listPosition { get; set; } 
            public string fecha_salida { get; set; }
            public string fecha_cierre { get; set; }
            public int id_dispositivo { get; set; } 
            public int b2bacces_id { get; set; }
            public long id_hojaruta { get; set; }


        }


   
}
