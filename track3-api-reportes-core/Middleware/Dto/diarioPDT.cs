using Infraestructure.track3.Models.movil;
using System.Data;
using static track3_api_reportes_core.Middleware.Dto.Moviles;

namespace track3_api_reportes_core.Middleware.Dto
{
    public class diarioPDT
    {
        public diarioPDT() { }

        public GPS_MOVIL movil { get; set; }
        public string patente { get; set; }
        public int cantidadPedidos { get; set; }
        public int cantidadCanastos { get; set; }

        public List<HojaRuta> hdrs { get; set; }

        public diarioPDT(DataRow row)
        {
            //this.movil = new GPS_MOVIL() { PATENTE = row["PATENTE"].ToString() };

            this.patente =  row["PATENTE"].ToString();
            this.cantidadCanastos = int.Parse(row["CANT_CANASTOS"].ToString());
            this.cantidadPedidos = int.Parse(row["CANT_PEDIDOS"].ToString());
        }
    }
}
