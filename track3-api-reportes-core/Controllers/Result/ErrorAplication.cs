namespace track3_api_reportes_core.Controllers.Result
{
    public class ErrorAplication
    {
        ~ErrorAplication() { }
        public ErrorAplication(string mensaje)
        {
            this.Mensaje = mensaje;
        }
        public int Codigo { get; set; }
        public string Mensaje { get; set; }
        public object Exception { get; set; }
    }
}
