using System.Data;

namespace track3_api_reportes_core.Infraestructura.track3.Models.Servicios.equivalencias
{
    public class GpsEquivalencia
    {

        public GpsEquivalencia(DataRow row)
        {
            this.Id = int.Parse(row["ID_EQUIVALENCIA"].ToString());
            this.DistanciaDesde = int.Parse(row["DISTANCIA_DESDE"].ToString());
            this.DistanciHasta = int.Parse(row["DISTANCIA_HASTA"].ToString());
            this.UnidadMedida = row["UNIDADMEDIDA"].ToString();
            this.valor = row["VALOR"].ToString();
            this.Descripcion = row["DESCRIPCION"].ToString();
            this.Propiedad = new GpsPropiedad(row);
        }

        public GpsEquivalencia()
        {

        }

        public long Id { get; set; }
        public DateTime Stamp { get; set; }
        public char Activo { get; set; }
        public int DistanciaDesde { get; set; }
        public int DistanciHasta { get; set; }
        public string UnidadMedida { get; set; }
        public string valor { get; set; }
        public string Descripcion { get; set; }
        public GpsPropiedad Propiedad { get; set; }

    }
}
