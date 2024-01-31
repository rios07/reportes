
using System.Data;

namespace track3_api_reportes_core.Middleware.Dto
{
    public class Movil
    {
        public class sp_get_movil
        {
            public sp_get_movil() { }

            public int id{ get; set; }
            public string patente { get; set; }
            /// Obtiene o establece el número del móvil.
            public int numero { get; set; }
            
            public sp_get_movil(DataRow row)
            {
                id = int.Parse(row["ID"].ToString());
                patente = row["PATENTE"].ToString();
                numero = int.Parse(row["NUMERO"].ToString());
            }
        }
    }
}
