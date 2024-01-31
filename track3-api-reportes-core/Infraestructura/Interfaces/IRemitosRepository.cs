using System.Data;
using Infraestructure.track3.Models.planificacion;

namespace track3_api_reportes_core.Infraestructura.Interfaces
{
    public interface IRemitosRepository
    {
        abstract DataTable getDatosRemitos(long parse);
        abstract DataTable getCAI(GpsCanal canal);
        abstract DataTable getPedidosByRef(string remitos, long idHojaRuta);
        abstract DataTable getItemsById(string pedidos);
    }
}
