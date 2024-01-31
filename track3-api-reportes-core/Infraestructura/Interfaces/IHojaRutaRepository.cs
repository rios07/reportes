using System.Data;
using track3_api_reportes_core.Infraestructura.Dao;

namespace track3_api_reportes_core.Infraestructura.Interfaces
{
    public interface IHojaRutaRepository
    {
        abstract DataTable getHojaruta(long idHojaRuta);
        abstract DataTable getHojaRutaDetallePedidos(long idHojaRuta);
        abstract DataTable getHojaRutaEstado(long idHojaRuta);
        abstract DataTable getListPersonal(long idHojaRuta);
        abstract DataTable ObtenerHojasRutasDigital(int canal, DateTime fDesde, DateTime fHasta);
        abstract DataTable getElementosHojaRuta(long idHojaRuta);
        abstract DataTable getListPersonalChoferCadetePanel(string idHojaRutas);
        abstract DataTable getCantidadMovilesEnViaje(string canales);
        abstract DataTable getAllCanales();
        abstract DataTable getPedidosTotalesPlanificados(string canales, int HREnViaje,string estadoEntregados, string estadoNoEntregados, string cancelados);
        abstract DataTable getPuntosDeEntrega(string canalesQry, int HREnViaje);
        abstract DataTable ObtenerHojasRutasCD(int canal, DateTime fDesde, DateTime fHasta);

        abstract Task<string> updateDistanciaYTiempoTotal(int hojarutaId, long dostanciaReal, long tiempoReal, OracleDao oracleDao);
    }
}
