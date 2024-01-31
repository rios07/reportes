using System.Data;

namespace track3_api_reportes_core.Infraestructura.Interfaces
{
    public interface ISqlDaoReportes
    {
        public DataTable ObtenerPosiciones(string Desde, string Hasta, string p_Identificador);

    }
}
