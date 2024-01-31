using System.Data;

namespace track3_api_reportes_core.Middleware.Dto
{
    public class PresenciaTipo
    {
        public PresenciaTipo() { }
        public int Id { get; set; }
        public string Nombre { get; set; }

        public PresenciaTipo(DataRow row)
        {
            this.Id = int.Parse(row["ID_PRESENCIATIPO"].ToString());
            this.Nombre = row["NOMBRE_PRESENCIATIPO"].ToString();
        }
    }
}
