using Infraestructure.track3.Models.planificacion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Infraestructure.track3.Models.hojaruta
{
    public class GpsHojaRutaElemento
    {
        public GpsHojaRutaElemento(DataRow row)
        {
            this.Id = long.Parse(row["ID_HOJARUTAELEMENTO"].ToString());
            this.Stamp=DateTime.Parse(row["STAMP"].ToString());
            this.IdHojaRuta = long.Parse(row["ID_HOJARUTA"].ToString());
            this.Propiedad = row["PROPIEDAD"].ToString();
            this.Observaciones = row["OBSERVACIONES"].ToString();
            this.Elemento = new GpsElemento(row);
            this.GpsEstado = new GpsEstado(row);
            if (!string.IsNullOrEmpty(row["ID_CEL"].ToString()))
            {
                this.Dispositivo = new GpsDispositivo
                {
                    Id = int.Parse(row["ID_CEL"].ToString()),
                    Numero = row["Nro_telefono"].ToString(),
                    Observaciones = row["Observaciones_Dispositivo"].ToString()
                };
            }
        }

        public GpsHojaRutaElemento()
        {

        }
        [Key]
        [Column("ID", TypeName = "INT32")]
        public long Id { get; set; }

        [Column("STAMP", TypeName = "TIMESTAMP")]
        public DateTime Stamp { get; set; }

        [Column("ACTIVO", TypeName = "CHAR")] public string Activo { get; set; }

        [Column("VERSION", TypeName = "INT32")]
        public int Version { get; set; }

        [Column("PROPIEDAD", TypeName = "VARCHAR2")]
        public string Propiedad { get; set; }

        [Column("ID_HOJARUTA", TypeName = "INT32")]
        public long IdHojaRuta { get; set; }

        [Column("ID_ESTADO", TypeName = "INT32")]
        public long IdEstado { get; set; }

        [Column("ID_ELEMENTO", TypeName = "INT32")]
        public int IdElemento { get; set; }

        [Column("OBSERVACIONES", TypeName = "VARCHAR2")]
        public string Observaciones { get; set; }

        [NotMapped] public virtual GpsElemento Elemento { get; set; }
        [NotMapped] public virtual GpsEstado GpsEstado { get; set; }
        [NotMapped] public GpsDispositivo Dispositivo { get; set; }

        ~GpsHojaRutaElemento()
        {

        }
    }
}
