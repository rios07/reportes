using Infraestructure.track3.Models.planificacion;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace track3_api_reportes_core.Infraestructura.track3.Models.Servicios
{
    public class GpsBandaHoraria
    {
        public GpsBandaHoraria()
        {

        }

        public GpsBandaHoraria(DataRow row)
        {
            this.Id = int.Parse(row["ID_BANDAHORARIA"].ToString());
            this.Nombre = row["BANDA_HORARIA"].ToString();
            this.HoraDesde = row["HORADESDE"].ToString();
            this.HoraHasta = row["HORAHASTA"].ToString();
            this.Orden = int.Parse(row["ORDEN"].ToString());
            this.Color = row["COLOR"].ToString();
            //this.canal = new Canal(row);
        }
        public Int32 Id { get; set; }
        public string? Nombre { get; set; }
        public string? HoraDesde { get; set; }
        public string? HoraHasta { get; set; }
        public int Orden { get; set; }
        public string Color { get; set; }
        [NotMapped]
        public GpsCanal? Canal { get; set; }
        public Int16 IdCanal { get; set; }
        public string? Activo { get; set; }

    }
}
