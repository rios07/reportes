using track3_api_reportes_core.Aplicacion.Filtros;

namespace track3_api_reportes_core.Middleware.Modelos.movil
{
    public class Vehiculo
    {
        public int TargetId { get; set; }
        public string Plate { get; set; }
        public string MovilTag { get; set; }
        public int DispatchId { get; set; }
        public string WayBillId { get; set; }
        public double? lat { get; set; }
        public double? lng { get; set; }
        public double? Speed { get; set; }
        public long HojaRutaId { get; set; }
        public DateTime Fecha { get; set; }
        public string MovilId { get; set; }
        public Boolean online { get; set; }
        public DateTime? lastPositionAt { get; set; }
        public string lastPositionAtStr { get; set; }

    }

    public class Flota
    {
        public string Name { get; set; }
        public List<Vehiculo> vehiculos { get; set; }
        public int TotalDetainee { get; set; }
        public int TotalDisconeted { get; set; }
        public int TotalRunning { get; set; }
        public int TotalStopped { get; set; }
        public int TotalVehiclesRoutes { get; set; }
        public string FullName { get; set; }

        public Canal canal { get; set; }
    }
}
