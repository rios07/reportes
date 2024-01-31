using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.track3.Models.planificacion
{
    public class GpsCanal
    {
        #region Models CANAL        
        [Key]
        [Column("ID", TypeName = "INT32")]
        public Int32 Id { get; set; }

        [Column("STAMP", TypeName = "TIMESTAMP")]
        public DateTime Stamp { get; set; }

        [Column("ACTIVO", TypeName = "CHAR")]
        public string Activo { get; set; }

        [Column("VERSION", TypeName = "INT64")]
        public Int64 Version { get; set; }

        [Column("NOMBRE", TypeName = "VARCHAR2")]
        public string Nombre { get; set; }

        [Column("SUCURSAL", TypeName = "INT32")]
        public Int32 Sucursal { get; set; }

        [Column("ID_DIRECCION", TypeName = "INT32")]
        public Int32 IdDireccion { get; set; }

        [Column("TIPO", TypeName = "VARCHAR2")]
        public string Tipo { get; set; }
        public string BD { get; set; }
        public string ConexionDB { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        [NotMapped]
        private string connstr { get; set; }
        public int GeneraCot { get; set; }
        public string getStringConexion()
        {
            return base64Decode(this.connstr);
        }
        #endregion
        public virtual GpsDireccion Direccion { get; set; }
        ~GpsCanal()
        {
            //Unmanaged code clean up
        }

        public GpsCanal(DataRow row)
        {
            this.Id = int.Parse(row["ID_CANAL"].ToString());
            this.Nombre = row["CANAL"].ToString();
            this.Sucursal = int.Parse(row["SUCURSAL"].ToString());
            this.Direccion = new GpsDireccion()
            {
                Id = int.Parse(row["CANAL_ID_DIRECCION"].ToString()),
                Calle = row["CANAL_CALLE"].ToString(),
                CodigoPostal = row["CANAL_CODIGO_POSTAL"].ToString(),
                Localidad = row["CANAL_LOCALIDAD"].ToString(),
                Numero = int.Parse(row["CANAL_ALTURA_CALLE"].ToString()),
                CoordenadaX = row["CANAL_LATITUDE"].ToString(),
                CoordenadaY = row["CANAL_LONGITUDE"].ToString(),
            };

            //String conexion
            this.BD = row["BD"].ToString();
            this.ConexionDB = row["CONEXION_BD"].ToString();
            this.Usuario = row["USUARIO"].ToString();
            this.Password = row["PASSWORD"].ToString();
            this.Tipo = row["CANAL_TIPO"].ToString();
            this.connstr = base64Encode(string.Format(this.ConexionDB, this.BD, this.Usuario, this.Password));
            //if (row.Table.Columns.Contains("ID_TIPOCANAL"))
            //    this. = Common.ToInt(row["ID_TIPOCANAL"]);

            if (row.Table.Columns.Contains("GENERA_COT"))
                this.GeneraCot = int.Parse(row["GENERA_COT"].ToString());
        }

        public GpsCanal()
        {

        }
        private static string base64Encode(string str)
        {
            string text = "";
            try
            {
                return Convert.ToBase64String(Encoding.ASCII.GetBytes(str));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static string base64Decode(string str)
        {
            string text = "";
            try
            {
                byte[] bytes = Convert.FromBase64String(str);
                return Encoding.UTF8.GetString(bytes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
