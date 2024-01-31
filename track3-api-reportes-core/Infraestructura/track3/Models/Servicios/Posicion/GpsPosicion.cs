using Infraestructure.track3.Models.planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using track3_api_reportes_core.Aplicacion.Requests;

namespace track3_api_reportes_core.Infraestructura.track3.Models.Servicios.Posicion
{
   
        public class GpsPosicion
        {
            public GpsPosicion() { }

            public GpsPosicion(Location w)
            {
                this.GpsLatitude = decimal.Parse(w.Lat.ToString());
                this.GpsLongitude = decimal.Parse(w.Lng.ToString());
                this.CoordenadaX = w.Lat.ToString();
                this.CoordenadaY = w.Lng.ToString();
            }
            ~GpsPosicion() { }

            public long Id { get; set; }
            public DateTime Stamp { get; set; }
            public string Activo { get; set; } = null!;
            public long Version { get; set; }
            public int DispositivoId { get; set; }
            public DateTime FechaPosicion { get; set; }
            public decimal GpsLatitude { get; set; }
            public decimal GpsLongitude { get; set; }
            public string CoordenadaX { get; set; }
            public string CoordenadaY { get; set; }
        public virtual GpsDispositivo Dispositivo { get; set; }


        }

  
}
