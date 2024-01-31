namespace track3_api_reportes_core.Middleware.Dto
{
    public class Presencia
    {
        public Presencia() { }

        public int Id { get; set; }
        public Boolean Estado { get; set; }
        public int DniChofer { get; set; }
        public string Patente { get; set; }
        public DateTime Stamp { get; set; }
        public DateTime Aprobada { get; set; }
        public string CoordenadaX { get; set; }
        public string CoordenadaY { get; set; }
        public int Id_planTrabajoProveedor { get; set; }
        public int Id_planTrabajo { get; set; }
        public PresenciaTipo Tipo { get; set; }
        public Modulo Modulo { get; set; }
        public int LegajoAutorizante { get; set; }
        public int Clave { get; set; }
        public int Id_autorizacion { get; set; }
        public string Sucursal { get; set; }
    }
}
