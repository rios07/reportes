using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.track3.Models.hojaruta;
using System.Data;

namespace Infraestructure.track3.Models.planificacion
{
    public class GpsElemento
    {
        public GpsElemento(DataRow row)
        {
            this.Id = int.Parse(row["ID_ELEMENTO"].ToString());
            this.Nombre = row["ELEMENTO"].ToString();
            this.Tipo = new GpsElementoTipo(row);
        }
        public GpsElemento()
        {

        }
        [Key]
        [Column("ID", TypeName = "INT32")]
        public int Id { get; set; }

        [Column("STAMP", TypeName = "TIMESTAMP")]
        public DateTime Stamp { get; set; }

        [Column("ACTIVO", TypeName = "CHAR")]
        public string Activo { get; set; }

        [Column("VERSION", TypeName = "INT32")]
        public int Version { get; set; }

        [Column("NOMBRE", TypeName = "VARCHAR2")]
        public string Nombre { get; set; }

        [Column("ID_ELEMENTOTIPO", TypeName = "INT32")]
        public int IdElementoTipo { get; set; }

        [Column("ORDEN", TypeName = "INT32")]
        public int Orden { get; set; }
        [NotMapped]
        public GpsElementoTipo Tipo { get; set; }
        [NotMapped]
        public virtual GpsHojaRutaElemento HojaRutaElemento { get; set; }
        ~GpsElemento()
        {

        }
    }
}
