using System.Data;

namespace track3_api_reportes_core.Middleware.Modelos.usuarios
{
    public class Usuario
    {
        public Usuario()
        {
            
        }
        public Usuario(DataRow row)
        {
            this.Id = int.Parse(row["ID_USUARIO"].ToString());
            this.Legajo = row["LEGAJO"].ToString();
            this.Nombre = row["NOMBRE"].ToString();
        }
        public int Id { get; set; }
        public string Legajo { get; set; }
        public string Foto { get; set; }
        public string Nombre { get; set; }
        public string Perfil { get; set; }
    }
}
