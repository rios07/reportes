using Infraestructure.track3.Models.planificacion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using track3_api_reportes_core.Middleware.Modelos.usuarios;
using System.Data;

namespace Infraestructure.track3.Models.hojaruta
{
    public class GpsHojaRutaEstado
    {
        public GpsHojaRutaEstado()
        {
            
        }

        public GpsHojaRutaEstado(DataRow row)
        {
            this.IdHojaRuta = int.Parse(row["ID_HOJARUTA"].ToString());
            this.Estado = new GpsEstado(row);
            this.Stamp = DateTime.Parse(row["FECHA"].ToString());
            this.IdUsuario = int.Parse(row["ID_USUARIO"].ToString());
            if (long.Parse(this.IdUsuario.ToString()) == 5034)
            {
                this.Usuario = new Usuario
                {
                    Legajo = "5034",
                    Nombre = "PRERUTEO_AUTOMATICO"
                };
            }
            else
            {
                this.Usuario = new Usuario(row);
            }
        }
        [Key]
        public long Id { get; set; }
        public DateTime Stamp { get; set; }
        public string Activo { get; set; }
        public Int64 Version { get; set; }
        public Int32 IdHojaRuta { get; set; }
        public Int32 IdEstado { get; set; }
        public Int32 IdUsuario { get; set; }
        public GpsEstado Estado { get; set; }   
        public virtual GpsHojaRuta HojaRuta { get; set; }
        public virtual Usuario Usuario { get; set; }
   
        ~GpsHojaRutaEstado()
        {

        }
    }
}
