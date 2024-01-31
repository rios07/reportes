using Infraestructure.track3.Models.hojaruta;
using Infraestructure.track3.Models.movil;
using Infraestructure.track3.Models.pedido;
using Infraestructure.track3.Models.planificacion;
using System.Data;
using track3_api_reportes_core.Infraestructura.SecureWay.Models;
using track3_api_reportes_core.Infraestructura.track3.Models.Servicios.Posicion;

namespace track3_api_reportes_core.Middleware.Interfaces
{
    public interface IMappingFormularios
    {
        abstract List<GpsHojaRutaDetalle> mapHRDetallePedidos(DataTable dt,ref string pedidos,Boolean DetallePedido);
        abstract GpsHojaRuta mapHojaRuta(DataTable dt);
        abstract List<GpsHojaRutaPersonal> mapListPersonal(DataTable dataTable);
        abstract List<GpsHojaRutaEstado> mapHojaRutaEstado(DataTable dataTable);
        abstract List<GpsPedidoRefAudit> mapListpPedidosRefAudit(DataTable dt);
        abstract List<GpsBandaHoraria> mapBandaHoraria(DataTable dt);
        abstract List<GpsContenedor> mapPedidosContenedor(DataTable dt);
        abstract List<GpsHojaRutaElemento> ElementosViajes(DataTable dt);
        abstract List<GpsHojaRuta> mapPanelHojaRuta(DataTable dt, ref string idHojaRutas);
        abstract List<GpsPedidoDetalle> mapItems(DataTable dt);
        abstract GpsPedido mapPedido(DataTable dt);
        abstract List<GpsMovilesEnViaje> mapMovilesEnViaje(DataTable dt, List<GpsCanal> listaCanal);
        abstract List<GpsMovilesEnViajeDetalle> mapMovilesEnViajeDetalle(DataTable dt);
        abstract List<GpsMovilesEnViajeDetalle> mapPuntoDeEntrega(ref List<GpsMovilesEnViajeDetalle>listaViaje, DataTable dtPuntosDeEntregas);
        abstract Target mapTargets(DataTable dt);
        abstract Target mapTarget(DataRow dt);
        abstract List<GpsPosicion> mapPosiciones(DataTable dt);
        abstract List<GpsHojaRuta> mapPanelHojaRutaCD(DataTable dt, ref string idHojaRutas);
    }
}
