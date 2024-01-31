using System.Data;

namespace track3_api_reportes_core.Infraestructura.track3.Models.Servicios.equivalencias
{
    public class GpsEquivalenciaHojaRutaDetalle
    {
        public GpsEquivalenciaHojaRutaDetalle()
        {

        }

        public GpsEquivalenciaHojaRutaDetalle(DataRow row)
        {
            this.Id = long.Parse(row["ID_EQUIVALENCIAHRDETALLE"].ToString());
            this.DistanciaLineal = row["DISTANCIALINEAL"].ToString();
            this.Equivalencia = new GpsEquivalencia(row);
        }

        public long Id { get; set; }
        public DateTime Stamp { get; set; }
        public char Activo { get; set; }
        public int IdHojaRutaDetalle { get; set; }
        public GpsEquivalencia Equivalencia { get; set; }
        public string DistanciaLineal { get; set; }
    }
}
