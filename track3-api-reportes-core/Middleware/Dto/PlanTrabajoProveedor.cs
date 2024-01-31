using static track3_api_reportes_core.Middleware.Dto.Moviles;
using System.Data;
using Infraestructure.track3.Models.planificacion;

namespace track3_api_reportes_core.Middleware.Dto
{
    public class PlanTrabajoProveedor
    {
        public PlanTrabajoProveedor() { }
        public int Id { get; set; }
        public int Id_planTrabajoDetalle { get; set; }
        public int Id_planTrabajo { get; set; }
        public int IndexDiv { get; set; }
        public DateTime HoraDesde { get; set; }
        public string StrHoraDesde { get; set; }
        public DateTime HoraHasta { get; set; }
        public string StrHoraHasta { get; set; }
        public movil Movil { get; set; }
        public Chofer Chofer { get; set; }
        public string StrFecha { get; set; }
        public string NombreChofer { get; set; }
        public DateTime InicioJornada { get; set; }
        public DateTime FinJornada { get; set; }
        public string ViajeActivo { get; set; }
        public GpsEstado Estado { get; set; }

        public int Id_hojaRuta { get; set; }

        public PlanTrabajoProveedor(DataRow row)
        {
            this.Id = int.Parse(row["ID"].ToString());
            this.Id_planTrabajoDetalle = int.Parse(row["ID_PLANTRABAJODETALLE"].ToString());
            this.IndexDiv = int.Parse(row["INDEXDIV"].ToString());
            this.HoraDesde = DateTime.Parse(row["HORA_DESDE"].ToString());
            this.StrHoraDesde = HoraDesde.ToString("HH:mm");
            this.HoraHasta = DateTime.Parse(row["HORA_HASTA"].ToString());
            this.StrHoraHasta = HoraHasta.ToString("HH:mm");

            this.Estado = new GpsEstado()
            {
                Id = int.Parse(row["ID_ESTADO"].ToString())
            };

            this.Movil = new movil()
            {
                ID = int.Parse(row["ID_MOVIL"].ToString()),
                PATENTE = row["PATENTE"].ToString()
            };

            this.Chofer = new Chofer()
            {
                ID = int.Parse(row["ID_CHOFER"].ToString()),
                NOMBRE = row["NOMBRE_CHOFER"].ToString(),
                APELLIDO = row["APELLIDO_CHOFER"].ToString(),
                DNI = row["DNI"].ToString(),
                IDENTIFICADOR = int.Parse(row["IDENTIFICADOR"].ToString())
            };

            this.NombreChofer = this.Chofer.APELLIDO + " " + this.Chofer.NOMBRE;
            if (row["INICIO_JORNADA"].ToString() != "")
                this.InicioJornada = DateTime.Parse(row["INICIO_JORNADA"].ToString());
            if (row["FIN_JORNADA"].ToString() != "")
                this.FinJornada = DateTime.Parse(row["FIN_JORNADA"].ToString());
        }
    }
}
