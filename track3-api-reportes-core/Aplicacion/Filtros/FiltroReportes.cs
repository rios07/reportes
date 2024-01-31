namespace track3_api_reportes_core.Aplicacion.Filtros
{
    public class FiltroReportes
    {
        public FiltroReportes() { }

        public string strFechaDesde { get; set; }
        public string strFechaHasta { get; set; }
        public string strFecha { get; set; }
       
        public String? Legajo { get; set; }
        public int nid { get; set; }

    }

    public class FiltroEvento : FiltroReportes
    {
        public FiltroEvento() { }

        public string patente { get; set; }
    }

    public class Mobiliario : FiltroReportes
    {
        public int p_canal { get; set; }
    }

    public class Dispositivo : FiltroReportes
    {
        public int p_Id_Dispositivo { get; set; }
        public string? p_Identificador { get; set; }
    }

    public class Canal : FiltroReportes
    {
        public int Sucursal { get; set; }
    }
}
