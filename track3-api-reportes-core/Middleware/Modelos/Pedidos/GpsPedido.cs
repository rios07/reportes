using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.track3.Models.hojaruta;
using Infraestructure.track3.Models.planificacion;
using track3_api_reportes_core.Middleware.Modelos.Pedidos;


namespace Infraestructure.track3.Models.pedido
{
    public class GpsPedido
    {
        public GpsPedido(DataRow row)
        {
            Id = int.Parse(row["ID"].ToString());
            nroPedidoRef = row["RESERVA"].ToString();
            this.Propiedades = new List<GpsPedidoPropiedad>();
        }

        public GpsPedido()
        {

        }


        public Int64 Id { get; set; }
        public DateTime Stamp { get; set; }
        public string Activo { get; set; }
        public Int64 IdCliente { get; set; }
        public Int64 IdEstado { get; set; }
        public Int64 IdDireccionEntrega { get; set; }
        public Int32 IdSucursal { get; set; }
        public Int32 IdTipo { get; set; }
        public Int32 IdCanal { get; set; }
        public Int32 IdBandaHoraria { get; set; }

        public DateTime Fecha { get; set; }
        public Int32? Terminal { get; set; }
        public Int32? Transaccion { get; set; }
        [NotMapped]
        public decimal ImporteTotal { get; set; }
        public string? Observacion { get; set; }
        [NotMapped]
        public GpsEstado Estado { get; set; }
        [NotMapped]
        public GpsEstado tipoOperacion { get; set; }
        [NotMapped]
        public GpsPedidoTipo Tipo { get; set; }
        [NotMapped]
        public GpsCliente Cliente { get; set; }
        [NotMapped]
        public GpsCanal Canal { get; set; }
        [NotMapped]
        public int cantidadItems { get; set; }
        [NotMapped]
        public GpsBandaHoraria BandaHoraria { get; set; }
        [NotMapped]
        public GpsDireccion DireccionEntrega { get; set; }

        public virtual List<GpsPedidoRefAudit> listTokenAudit { get; set; }

        public virtual List<GpsPedidoRef> listToken { get; set; }
        public List<GpsPedidoDetalle> items { get; set; }
        public List<GpsPedidoPropiedad> Propiedades { get; set; }
        public List<GpsContenedor> Contenedores { get; set; }
        public string nroPedidoRef { get; set; }
       
        [NotMapped]
        public GpsHojaRuta GpshojaRuta { get; set; }

        [NotMapped]
        public List<GpsPedidoRefSerie> series { get; set; }
        [NotMapped]
        public bool Reprogramado { get; set; }
        public string NroRemito { get; set; }
        public GpsPedidoFastLine FastLine { get; set; }


        ~GpsPedido()
        { }


    }
}
