using Infraestructure.track3.Models.hojaruta;
using Infraestructure.track3.Models.planificacion;
using track3_api_reportes_core.Controllers.Result;
using track3_api_reportes_core.Infraestructura.SecureWay.Models;
using track3_api_reportes_core.Middleware.Modelos.Track;

namespace track3_api_reportes_core.Middleware.Interfaces
{
    public interface ISecureWayMiddleware
    {
        abstract Task<List<Target>> getTargets(string movilPatente, List<GpsHojaRutaElemento> hojaRutaListaElementosViaje, List<Elemento> elementos);
        abstract Target getTarget(string movilPatente, string Propiedad);
        abstract List<Elemento> getPosiciones(string movilPatente, List<GpsHojaRutaElemento> hojaRutaListaElementosViaje, DateTime desde, DateTime hasta,List<Elemento> elementos);
        abstract Task<ErrorAplication> calcularKmHojaRuta(List<Elemento> elementos, GpsHojaRuta hojaRuta);
    }
}
