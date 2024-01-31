using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.track3.Models.planificacion;

namespace Infraestructure.track3.Models.movil
{
    public class GpsMovil
    {
        public GpsMovil()
        {
            this.MovilTipo = new GpsMovilTipo();
            this.Propiedades = new List<GpsPropiedad>();

        }
        public GpsMovil(DataRow row)
        {
            this.Id = int.Parse(row["ID_MOVIL"].ToString());
            this.Patente = row["PATENTE"].ToString();
            this.Numero = int.Parse(row["NUMERO"].ToString());
            this.Descripcion = row["DESCRIPCION"].ToString();
            this.Anio = int.Parse(row["ANIO"].ToString());
            this.Modelo = row["MODELO"].ToString();
            this.Marca = row["MARCA"].ToString();
            this.Color = row["COLOR"].ToString();
            this.Referente = row["REFERENTE"].ToString();
            this.Altura = decimal.Parse(row["ALTURA"].ToString().Trim().Replace(',', '.'));
            this.Superficie = decimal.Parse(row["SUPERFICIE"].ToString().Trim().Replace(',', '.'));
            this.Pesomaximo = decimal.Parse(row["PESOMAXIMO"].ToString().Trim().Replace(',', '.'));
            this.Id_Moviltipo = int.Parse(row["ID_MOVILTIPO"].ToString());
            this.Id_Proveedor = int.Parse(row["ID_PROVEEDOR"].ToString());
            this.Activo = "S";
            this.Propiedades = new List<GpsPropiedad>();
        }
        public int Id { get; set; } = 0;
        public DateTime? Stamp { get; set; }
        public string Activo { get; set; } = "S";
        public string? Patente { get; set; } = string.Empty;
        public int Numero { get; set; } = 0;
        public string? Descripcion { get; set; }
        public int Anio { get; set; }
        public string? Modelo { get; set; }
        public string? Marca { get; set; }
        public string? Color { get; set; }
        public string? Referente { get; set; }
        public decimal Altura { get; set; }
        public decimal Superficie { get; set; }
        public decimal Pesomaximo { get; set; }
        public int Id_Moviltipo { get; set; }
        public GpsMovilTipo? MovilTipo { get; set; }
        public int Id_Proveedor { get; set; }
        public List<GpsPropiedad>? Propiedades { get; set; }
    }

    public class GpsMovilProveedor
    {
        public GpsMovil item { get; set; }
        public GpsProveedor proveedor { get; set; }
    }
}

