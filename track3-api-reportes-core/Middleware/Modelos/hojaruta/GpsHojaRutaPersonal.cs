using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.track3.Models.planificacion;
using System.Data;

namespace Infraestructure.track3.Models.hojaruta
{
    public class GpsHojaRutaPersonal
    {
        public GpsHojaRutaPersonal() { }

        public GpsHojaRutaPersonal(DataRow row)
        {
            Id = int.Parse(row["ID_HOJARUTAPERSONAL"].ToString());
            Tipo = new GpsPersonalTipo() { IdPersonaTipo = int.Parse(row["ID_PERSONALTIPO"].ToString()), Nombre = row["NOMBRE_PERSONALTIPO"].ToString() };
            HojaRuta = new GpsHojaRuta() {  Id= int.Parse(row["ID_HOJARUTA"].ToString()) };
            IdHojaRuta = int.Parse(row["ID_HOJARUTA"].ToString());
            Identificador = row["IDENTIFICADOR"].ToString();
            Nombre = row["NOMBRE_HOJARUTAPERSONAL"].ToString();
        }

        [Key]
        public Int32 Id { get; set; }
        public DateTime Stamp { get; set; }
        public string Activo { get; set; }
        public Int32 IdHojaRuta { get; set; }
        public Int32 IdPersonalTipo { get; set; }
        public string Nombre { get; set; }
        public string Identificador { get; set; }
        public virtual GpsHojaRuta HojaRuta { get; set; }
        [NotMapped]
        public GpsPersonalTipo Tipo { get; set; }
        ~GpsHojaRutaPersonal()
        {

        }


    }
}
