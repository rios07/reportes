using System.Data;

namespace track3_api_reportes_core.Middleware.Dto
{
    public class Canal
    {

        public Canal(){}

        public int id_canal { get; set; }
        public int sucursal { get; set; }
        public string canal { get; set; }
        public string conexion_bd { get; set; }
        public string bd { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
        public string canal_tipo { get; set; }
        public int canal_id_direccion { get; set; }
        public string canal_calle { get; set; }
        public string canal_altura_calle { get; set; }
        public string canal_piso { get; set; }
        public string canal_piso_dpto { get; set; }
        public string canal_codigo_postal { get; set; }
        public string canal_localidad { get; set; }
        public string canal_latitude { get; set; }
        public string canal_longitude { get; set; }
        public int genera_cot { get; set; }


        public Canal(DataRow row)
        {
            id_canal = int.Parse(row["id_canal"].ToString());
            sucursal = int.Parse(row["sucursal"].ToString());
            canal = row["canal"].ToString();
            conexion_bd = row["conexion_bd"].ToString();
            bd = row["bd"].ToString();
            usuario = row["usuario"].ToString();
            password = row["password"].ToString();
            canal_tipo = row["canal_tipo"].ToString();
            canal_id_direccion = int.Parse(row["canal_id_direccion"].ToString());
            canal_calle = row["canal_calle"].ToString();
            canal_altura_calle = row["canal_altura_calle"].ToString();
            canal_piso = row["canal_piso"].ToString();
            canal_piso_dpto = row["canal_piso_dpto"].ToString();
            canal_codigo_postal = row["canal_codigo_postal"].ToString();
            canal_localidad = row["canal_localidad"].ToString();
            canal_latitude = row["canal_latitude"].ToString();
            canal_longitude = row["canal_longitude"].ToString();
            genera_cot = int.Parse(row["genera_cot"].ToString());

        }

        public static implicit operator Canal(Aplicacion.Filtros.Canal v)
        {
            throw new NotImplementedException();
        }
    }


    
}
