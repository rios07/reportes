using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace track3_api_reportes_core.Infraestructura.track3.Models.Servicios
{
    public class GpsDireccion
    {
        public GpsDireccion(DataRow row)
        {
            Id = int.Parse(row["ID_DIRECCION"].ToString());
            Calle = row["CALLE"].ToString();

            int number;
            bool success = int.TryParse(row["ALTURA_CALLE"].ToString(), out number);
            if (success)
                Numero = number;

            int idTipoDomicilio;
            bool successIdTipoDomicilio = int.TryParse(row["ID_TIPODOMICILIO"].ToString(), out idTipoDomicilio);
            //if (successIdTipoDomicilio)
            //    tipo = new TipoDomicilio() { id = idTipoDomicilio, nombre = string.Empty };
            IdTipoDomicilio = idTipoDomicilio;
            Piso = row["PISO"].ToString();
            PisoDepto = row["PISO_DPTO"].ToString();
            CodigoPostal = row["CODIGO_POSTAL"].ToString();
            Localidad = row["LOCALIDAD"].ToString();
            Provincia = row["PROVINCIA"].ToString();
            CoordenadaX = row["LATITUDE"].ToString();
            CoordenadaY = row["LONGITUDE"].ToString();
        }

        public GpsDireccion()
        {

        }

        [Key]
        [Column("ID", TypeName = "INT32")]
        public Int32 Id { get; set; }

        [Column("STAMP", TypeName = "TIMESTAMP")]
        public DateTime Stamp { get; set; }

        [Column("ACTIVO", TypeName = "CHAR")]
        public string Activo { get; set; }

        [Column("VERSION", TypeName = "INT32")]
        public Int64 Version { get; set; }

        [Column("CALLE", TypeName = "VARCHAR2")]
        public string Calle { get; set; }

        [Column("NUMERO", TypeName = "INT32")]
        public Int32 Numero { get; set; }

        [Column("CODIGOPOSTAL", TypeName = "VARCHAR2")]
        public string CodigoPostal { get; set; }

        [Column("COORDENADAX", TypeName = "VARCHAR2")]
        public string CoordenadaX { get; set; }

        [Column("COORDENADAY", TypeName = "VARCHAR2")]
        public string CoordenadaY { get; set; }

        [Column("LOCALIDAD", TypeName = "VARCHAR2")]
        public string Localidad { get; set; }

        [Column("PISO", TypeName = "VARCHAR2")]
        public string Piso { get; set; }

        [Column("PISO_DEPTO", TypeName = "VARCHAR2")]
        public string PisoDepto { get; set; }

        [Column("ID_TIPODOMICILIO", TypeName = "INT32")]
        public Int32 IdTipoDomicilio { get; set; }

        [Column("PROVINCIA", TypeName = "VARCHAR2")]
        public string Provincia { get; set; }

        // public virtual GpsCliente Cliente { get; set; }
        ~GpsDireccion()
        {

        }

    }
}
