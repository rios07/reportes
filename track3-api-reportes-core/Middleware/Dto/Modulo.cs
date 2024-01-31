using System.Data;

namespace track3_api_reportes_core.Middleware.Dto
{
    public class Modulo
    {
        public Modulo() { }
        public int Id { get; set; }
        public string Nombre { get; set; }

        public Modulo(DataRow row)
        {
            this.Id = int.Parse(row["ID_MODULO"].ToString());
            this.Nombre = row["NOMBRE_MODULO"].ToString();
        }
    }
}
