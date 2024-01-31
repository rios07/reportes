using Infraestructure.track3.Models.planificacion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.track3.Models.pedido
{
    public class GpsBandaHoraria
    {
        public GpsBandaHoraria()
        {
            
        }

        public GpsBandaHoraria(DataRow row)
        {
            this.id = int.Parse(row["ID_BANDAHORARIA"].ToString());
            this.nombre = row["BANDA_HORARIA"].ToString();
            this.horaDesde = row["HORADESDE"].ToString();
            this.horaHasta = row["HORAHASTA"].ToString();
            this.orden = int.Parse(row["ORDEN"].ToString());
            this.color = row["COLOR"].ToString();
            //this.canal = new Canal(row);
        }
        public Int32 id { get; set; }
        public string nombre { get; set; }
        public string horaDesde { get; set; }
        public string horaHasta { get; set; }
        public int orden { get; set; }
        public string color { get; set; }
        [NotMapped]
        public GpsCanal Canal { get; set; }
        public string activo { get; set; }
        [NotMapped]
        public long IdHojaRuta { get; set; }
        [NotMapped]
        public virtual int Mayoritaria { get; set; }
    }
}
