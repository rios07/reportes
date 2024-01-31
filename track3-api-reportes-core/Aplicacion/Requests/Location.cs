using System.Security.Cryptography.X509Certificates;

namespace track3_api_reportes_core.Aplicacion.Requests
{
    public class Location
    {
        public Location() { }
        
    
            

        public double Lat { get; set; }
        public double Lng { get; set; }
        public DateTime? Stamp { get; set; }
    }
}
