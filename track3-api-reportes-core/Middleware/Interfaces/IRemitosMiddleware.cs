using track3_api_reportes_core.Middleware.Dto;

namespace track3_api_reportes_core.Middleware.Interfaces
{
    public interface IRemitosMiddleware
    {
        RemitoImpresion GetRemitos(string idHojaRuta);
    }
}
