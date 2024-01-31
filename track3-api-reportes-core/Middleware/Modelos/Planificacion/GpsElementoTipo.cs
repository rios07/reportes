using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Infraestructure.track3.Models.planificacion
{
    public class GpsElementoTipo
    {
        public GpsElementoTipo(DataRow row)
        {
            this.Id = int.Parse(row["ID_ELEMENTOTIPO"].ToString());
            this.Nombre = row["ELEMENTOTIPO"].ToString();
        }
        [Key]
        [Column("ID", TypeName = "INT32")]
        public int Id { get; set; }

        [Column("STAMP", TypeName = "TIMESTAMP")]
        public DateTime Stamp { get; set; }

        [Column("ACTIVO", TypeName = "CHAR")]
        public string Activo { get; set; }

        [Column("VERSION", TypeName = "INT32")]
        public Int64 Version { get; set; }

        [Column("NOMBRE", TypeName = "VARCHAR2")]
        public string Nombre { get; set; }

        ~GpsElementoTipo()
        {

        }
    }
}
