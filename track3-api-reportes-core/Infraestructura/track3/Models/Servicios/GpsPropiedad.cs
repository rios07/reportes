using Infraestructure.track3.Models.planificacion;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace track3_api_reportes_core.Infraestructura.track3.Models.Servicios
{
    public class GpsPropiedad
    {

        public GpsPropiedad() { }
        public GpsPropiedad(DataRow row)
        {
            this.Id = int.Parse(row["ID_PROPIEDAD"].ToString());
            this.Nombre = row["PROPIEDAD"].ToString();
            this.Tipo = new GpsPropiedadTipo() { Id = int.Parse(row["ID_PROPIEDADTIPO"].ToString()), Nombre = row["PROPIEDADTIPO"].ToString() };
            this.IdPropiedadTipo = Tipo.Id;
        }

        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public int Orden { get; set; }
        public int IdPropiedadTipo { get; set; }

        public GpsPropiedadTipo Tipo { get; set; }
    }
}
