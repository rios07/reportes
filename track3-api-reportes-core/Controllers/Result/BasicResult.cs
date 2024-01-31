using System.Runtime.Serialization;
using System.Xml.Linq;

namespace track3_api_reportes_core.Controllers.Result
{
    public class BasicResult<T>
    {
        ~BasicResult() { }



        [DataMember(Name = "Data")]
        public T Data { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember]
        public ErrorAplication oError { get; set; }



        public BasicResult()
        {
            oError = new ErrorAplication("No hay Errores");

        }
    }
}
