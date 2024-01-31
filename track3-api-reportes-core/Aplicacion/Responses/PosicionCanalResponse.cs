namespace track3_api_reportes_core.Aplicacion.Responses
{
    public class PosicionCanalResponse
    {
        public PosicionCanalResponse() { }

        public Int32 IdCanal { get; set; }

        public string Nombre { get; set; }

        public Int32 Sucursal { get; set; }

        public string CoordenadaX { get; set; }

        public string CoordenadaY { get; set; }
    }
}
