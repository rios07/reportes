using track3_api_reportes_core.Controllers.Result;
using track3_api_reportes_core.Middleware.Modelos.Track;

namespace track3_api_reportes_core.Aplicacion.Interfaces
{
    public interface IDispatchApplication
    {
        abstract Task<ErrorAplication> CalcularKmRealHojaRuta(long idHojaRuta);
    }
}
