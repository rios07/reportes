using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.track3.Models.planificacion
{
    public class GpsDispositivo
    {
        ~GpsDispositivo()
        {

        }
        [Key]
        [Column("ID", TypeName = "INT32")]
        public Int32 Id { get; set; }

        [Column("STAMP", TypeName = "TIMESTAMP")]
        public DateTime Stamp { get; set; }

        [Column("ACTIVO", TypeName = "CHAR")]
        public string Activo { get; set; }

        [Column("VERSION", TypeName = "INT64")]
        public Int64 Version { get; set; }

        [Column("IDENTIFICADOR", TypeName = "VARCHAR2")]
        public string Identificador { get; set; }

        [Column("NUMERO", TypeName = "VARCHAR2")]
        public string Numero { get; set; }

        [Column("SUCURSAL", TypeName = "INT32")]
        public Int32 Sucursal { get; set; }

        [Column("ID_ELEMENTO", TypeName = "INT32")]
        public Int32 IdElemento { get; set; }

        [Column("ROTULO", TypeName = "INT32")]
        public Int32? Rotulo { get; set; }

        [Column("OBSERVACIONES", TypeName = "VARCHAR2")]
        public string Observaciones { get; set; }

        [Column("B2BACCES_ID", TypeName = "INT32")]
        public Int32 B2AccesId { get; set; }

        [NotMapped]
        public GpsElemento elemento { get; set; }

    }
}
