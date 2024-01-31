using System.ComponentModel.DataAnnotations;
using System.Data;

namespace track3_api_reportes_core.Middleware.Dto
{
    public class HojaRutaActividad
    {
        public HojaRutaActividad(DataRow row)
        {
            Id = int.Parse(row["ID"].ToString());
            WayBillId = row["WAYBILLID"].ToString();
            IdEstado = int.Parse(row["ID_ESTADO"].ToString());
        }

        public HojaRutaActividad(){ }

        [Key]
        public Int32 Id { get; set; }
        public string? WayBillId { get; set; }
        public Int32 IdEstado { get; set; }

        ~HojaRutaActividad(){ }
    }
}
