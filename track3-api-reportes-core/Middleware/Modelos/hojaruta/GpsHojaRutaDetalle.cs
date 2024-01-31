using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.track3.Models.pedido;
using track3_api_reportes_core.Infraestructura.track3.Models.Servicios.Posicion;
using track3_api_reportes_core.Infraestructura.track3.Models.Servicios.equivalencias;

namespace Infraestructure.track3.Models.hojaruta
{
    public class GpsHojaRutaDetalle
    {

        public GpsHojaRutaDetalle(DataRow row)
        {
            Id = long.Parse(row["ID"].ToString());
            Orden = int.Parse(row["ORDEN"].ToString());
            LlegadaEstimada = DateTime.Parse(row["LLEGADA_ESTIMADA"].ToString());
            LlegadaDescripcion = LlegadaEstimada.ToString("dd/MM/yyyy HH:mm");
            // LlegadaEstimada = LlegadaEstimada.ToString("dd/MM/yyyy HH:mm");
            DistanciaOptima = decimal.Parse(row["DISTANCIA_OPTIMA"].ToString());
            DistanciaDescripcion = row["DISTANCIA_DESCRIPCION"].ToString();
            TiempoOptimo = decimal.Parse(row["TIEMPO_OPTIMO"].ToString());
            TiempoDescripcion = row["TIEMPO_DESCRIPCION"].ToString();
            DireccionInicio = row["DIRECCION_INICIO"].ToString();
            DireccionFin = row["DIRECCION_FIN"].ToString();
            //pedido = new Pedido() { id = long.Parse(row["ID_PEDIDO"].ToString()), nroPedidoRef = long.Parse(row["NRO_PEDIDO_REF"].ToString()) };
            Pedido = new GpsPedido() { Id = long.Parse(row["ID_PEDIDO"].ToString()), nroPedidoRef = row["NRO_PEDIDO_REF"].ToString() };
            ListPedidosRef = new List<string>();
            this.CoordenadaX = row["COORDENADAX"].ToString();
            this.CoordenadaY = row["COORDENADAY"].ToString();
            Posicion = new GpsPosicion()
            {
                GpsLatitude = decimal.Parse(row["COORDENADAX"].ToString()),
                GpsLongitude = decimal.Parse(row["COORDENADAY"].ToString()),
                CoordenadaX = this.CoordenadaX,
                CoordenadaY = this.CoordenadaY
            };

            //Reemplazar , por .
            //Posicion.GpsLatitude = Posicion.GpsLatitude != null ? Posicion.GpsLatitude.(',', '.') : null;
            //Posicion.GpsLongitude = Posicion.GpsLongitude != null ? Posicion.GpsLongitude.Replace(',', '.') : null;
        }

        public GpsHojaRutaDetalle()
        {

        }

        [Key]
        public long Id { get; set; }
        public DateTime Stamp { get; set; }
        public string Activo { get; set; }
        public Int32 IdEstado { get; set; }
        public Int32 IdHojaDeRuta { get; set; }
        public Int64 IdPedido { get; set; }
        public Int32 Orden { get; set; }

        public Int32? OrdenOriginal { get; set; }
        public DateTime LlegadaEstimada { get; set; }
        public string LlegadaDescripcion { get; set; }
        public decimal DistanciaOptima { get; set; }
        public string DistanciaDescripcion { get; set; }
        public decimal TiempoOptimo { get; set; }
        public string TiempoDescripcion { get; set; }
        public string DireccionInicio { get; set; }
        public string DireccionFin { get; set; }
        public string CoordenadaX { get; set; }
        public string CoordenadaY { get; set; }
        public List<string> ListPedidosRef { get; set; }
        public virtual GpsPedido Pedido { get; set; }
        public virtual GpsHojaRuta HojaRuta { get; set; }
        public virtual GpsPosicion Posicion { get; set; }
        public GpsEquivalenciaHojaRutaDetalle EquivalentePago { get; set; }
        ~GpsHojaRutaDetalle()
        {

        }
    }
}
