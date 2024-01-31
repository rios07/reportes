using System.Data;

namespace track3_api_reportes_core.Infraestructura.Interfaces
{
    public interface IPedidosRepositorio
    {
        abstract DataTable getHistoricoPedidos(string pedidos, long idHojaRuta);
        abstract DataTable getCanalBandaHoraria(int canalId);
        abstract DataTable getContenedores(string idPedidos);
        abstract DataTable obtenerBandaHorariaMayoritaria(string idHojaRutas);
        abstract DataTable getPedidoDetalle(long id);
        abstract DataTable getPedidoDetalleCD(long idPedido,long idHojaRuta);
        abstract DataTable getPedido(long idPedido);

        abstract DataTable getPedidoSearch(string nroRef);
    }
}
