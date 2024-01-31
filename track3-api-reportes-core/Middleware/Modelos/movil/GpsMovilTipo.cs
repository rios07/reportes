using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.track3.Models.movil
{
    public class GpsMovilTipo
    {
        ~GpsMovilTipo()
        {

        }

        [Key]
        public Int32 Id { get; set; }

        [Column("STAMP", TypeName = "TIMESTAMP")]
        public DateTime Stamp { get; set; }

        [Column("ACTIVO", TypeName = "CHAR")]
        public string Activo { get; set; }

        [Column("VERSION", TypeName = "INT64")]
        public Int64 Version { get; set; }

        [Column("DESCRIPCION", TypeName = "VARCHAR2")]
        public string Descripcion { get; set; }

        [Column("PESO", TypeName = "NUMBER(10,2)")]
        public decimal Peso { get; set; }

        [Column("UMP", TypeName = "VARCHAR2")]
        public string UMP { get; set; }

        [Column("VOLUMEN", TypeName = "NUMBER(10,2)")]
        public decimal Volumen { get; set; }

        [Column("UMV", TypeName = "VARCHAR2")]
        public string UMV { get; set; }

        [NotMapped]
        public List<GpsMovil> Moviles { get; set; }
    }
}
