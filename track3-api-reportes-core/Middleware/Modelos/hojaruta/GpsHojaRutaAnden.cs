using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.track3.Models.hojaruta
{
    public class GpsHojaRutaAnden
    {
        public GpsHojaRutaAnden()
        {
            
        }
        public GpsHojaRutaAnden(DataRow row)
        {
            if (row != null)
            {
                if (row.Table.Columns.Contains("ID_ANDEN"))
                    this.Id = Convert.ToInt32(row["ID_ANDEN"].ToString());

                if (row.Table.Columns.Contains("ID_HOJARUTA"))
                    this.IdHojaruta = Convert.ToInt32(row["ID_HOJARUTA"].ToString());
                //if (row.Table.Columns.Contains("STAMP"))
                //    this.Stamp = row.Field<DateTime>("STAMP").ToString("yyyy-MM-dd");

                if (row.Table.Columns.Contains("ANDEN"))
                    this.Anden = row.Field<string>("ANDEN");

                if (row.Table.Columns.Contains("HORA_DESDE"))
                    this.HoraDesde = row.Field<string>("HORA_DESDE");

                if (row.Table.Columns.Contains("HORA_HASTA"))
                    this.HoraHasta = row.Field<string>("HORA_HASTA");
            }
        }
        [Key]
        public int Id { get; set; }
        public DateTime Stamp { get; set; }
        public string Activo { get; set; }
        public int IdHojaruta { get; set; }
        public string Anden { get; set; }
        public string HoraDesde { get; set; }
        public string HoraHasta { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaPlan { get; set; }
        [NotMapped]
        public GpsHojaRuta HojaRuta { get; set; }
        ~GpsHojaRutaAnden(){}
    }
}
