using track3_api_reportes_core.Middleware.Dto;

namespace track3_api_reportes_core.Aplicacion.Interfaces
{
    public interface IRemitosApplication
    {
        RemitoImpresion getRemitosImpresion(string idHojaRuta);
    }
}
