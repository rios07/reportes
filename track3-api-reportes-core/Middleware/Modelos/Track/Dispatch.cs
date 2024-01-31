using System.ComponentModel.DataAnnotations.Schema;

namespace track3_api_reportes_core.Middleware.Modelos.Track
{
    public class Dispatch
    {
        public long DispatchId { get; set; }
        public string WayBillId { get; set; }
        public System.DateTime DispatchDate { get; set; }
        public Nullable<bool> Closed { get; set; }
        public string VehiclePlate { get; set; }
        public long VehicleId { get; set; }
        public string TransportCarrierName { get; set; }
        public Nullable<long> TransportCarrierId { get; set; }
        public Nullable<System.DateTime> GPSLastPositionAT { get; set; }
        public string DriverDocumentId { get; set; }
        public string DriverName { get; set; }
        public string DriverFileId { get; set; }
        public string AddedTrack { get; set; }
        public long SourceSystemId { get; set; }
        public string SourceSystemName { get; set; }
        public string SourceSystemAlias { get; set; }
        public Nullable<System.DateTime> ClosedDate { get; set; }
        public Nullable<int> RoutePointCount { get; set; }
        public Nullable<int> RoutePointGoingTo { get; set; }
        public Nullable<long> GeoplacemarkGoingTo { get; set; }
        public string GeoplacemarkDescGoingTo { get; set; }
        public string GeoplacemarkDescSalida { get; set; }
        public Nullable<long> GeoplaceMarkIn { get; set; }
        public Nullable<long> GeoplaceMarkFirst { get; set; }
        public Nullable<int> RoutePointIn { get; set; }
        public string GeoplaceMarkDescIn { get; set; }
        public Nullable<double> GPSLatitude { get; set; }
        public Nullable<double> GPSLongitude { get; set; }
        public string DevicePlate { get; set; }
        public string DevicePlateNoGps { get; set; }
        public double Distancia { get; set; }
        public int Duracion { get; set; }
        [NotMapped]
        public bool MainTruck { get; set; }
        public string TravelTimeStr
        {
            get
            {
                string retVal = "";
                if (!this.Closed.HasValue || (this.Closed.HasValue && !this.Closed.Value))
                {
                    TimeSpan span = DateTime.Now - this.DispatchDate;
                    retVal = String.Format("{0} {1:00}:{2:00}", span.Days > 1 ? String.Format("{0}", span.Days - 1) : "", span.Hours, span.Minutes);
                }
                return retVal;
            }
        }

    }
    }
