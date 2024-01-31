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
    public class GpsEstado
    {
        [Key]
        public Int64 Id { get; set; }

        [Column("STAMP", TypeName = "TIMESTAMP")]
        public DateTime Fecha { get; set; }

        [Column("ACTIVO", TypeName = "CHAR")]
        public string Activo { get; set; }

        [Column("VERSION", TypeName = "INT64")]
        public Int64 Version { get; set; }

        [Column("NOMBRE", TypeName = "VARCHAR2")]
        public string Nombre { get; set; }

        [Column("ID_ESTADOTIPO", TypeName = "INT32")]
        public Int32 IdEstadoTipo { get; set; }

        [Column("ORDEN", TypeName = "INT")]
        public int Orden { get; set; }

        ~GpsEstado()
        {

        }
        public GpsEstado(DataRow row)
        {

            if (row.Table.Columns.Contains("ID_ESTADO"))
            {
                this.Id = long.Parse(row["ID_ESTADO"].ToString());
            }

            if (row.Table.Columns.Contains("ESTADO"))
            {
                this.Nombre = row["ESTADO"].ToString();
            }

            if (row.Table.Columns.Contains("ESTADO_PEDIDO"))
            {
                this.Nombre = row["ESTADO_PEDIDO"].ToString();
            }

        }

        public GpsEstado()
        {
            
        }

    }
}
