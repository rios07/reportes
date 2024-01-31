using System.Data;

namespace track3_api_reportes_core.Infraestructura.Interfaces;

public interface ISecurewayRepository
{
    abstract DataTable getTargets(string movilPatente,string propiedad ,string conex);
    abstract DataTable getPosiciones(DateTime desde, DateTime hasta, long targetId, string conex);
    abstract DataTable getPosicionesHistoricas(DateTime desde, DateTime hasta, long targetTargetId, string conexHist);
}