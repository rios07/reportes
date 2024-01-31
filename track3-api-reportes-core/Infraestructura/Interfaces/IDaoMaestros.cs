using System.Data;

namespace track3_api_reportes_core.Infraestructura.Interfaces
{
    public interface IDaoMaestros
    {
        public DataTable GetMovil(int nid);

        public abstract DataTable GetPosicionCanal(int idCanal);

    }
}
