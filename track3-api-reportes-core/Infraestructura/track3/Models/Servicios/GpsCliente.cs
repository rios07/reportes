using Infraestructure.track3.Models.planificacion;
using System.Data;

namespace track3_api_reportes_core.Infraestructura.track3.Models.Servicios
{
    public class GpsCliente
    {

        public GpsCliente()
        {

        }

        public GpsCliente(DataRow row)
        {
            Id = long.Parse(row["ID_CLIENTE"].ToString());
            Apellido = row["APELLIDO"].ToString() != String.Empty ? row["APELLIDO"].ToString() : "";
            Nombre = row["NOMBRE"].ToString() != String.Empty ? row["NOMBRE"].ToString() : "";
            Dni = SoloNumeros(row["DOCUMENTO"].ToString() != String.Empty ? row["DOCUMENTO"].ToString() : "");
            Telefono = SoloNumeros(row["TELEFONO"].ToString() != String.Empty ? row["TELEFONO"].ToString() : "");
            Celular = SoloNumeros(row["CELULAR"].ToString() != String.Empty ? row["CELULAR"].ToString() : "");
            Mail = row["MAIL"].ToString() != String.Empty ? row["MAIL"].ToString() : "";
        }
        ~GpsCliente() { }
        public long Id { get; set; }
        public DateTime Stamp { get; set; }
        public string Activo { get; set; }
        public int Version { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string Dni { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Id_Direccion { get; set; }
        public string Mail { get; set; }
        public virtual GpsDireccion Direccion { get; set; }
        public virtual List<GpsDireccion> Direcciones { get; set; }
        private string SoloNumeros(string s)
        {
            int largo = s.Length;
            string s_in = s;
            string s_modificada = "";

            int f;
            for (f = 0; f <= (largo - 1); f++)
            {
                string x = s_in.Substring(f, 1);

                if (IsNumeric(x))
                    s_modificada = s_modificada + s_in.Substring(f, 1);
            }

            return s_modificada;
        }
        private static bool IsNumeric(string v)
        {
            float output;
            return float.TryParse(v, out output);
        }

    }

}
