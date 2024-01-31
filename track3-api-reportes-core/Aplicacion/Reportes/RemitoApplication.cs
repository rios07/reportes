using Application.interfaces;
using Middleware.DTO.response;
using Newtonsoft.Json;
using System.Net;
using track3_api_reportes_core.Aplicacion.Interfaces;
using track3_api_reportes_core.Controllers.Result;
using track3_api_reportes_core.Middleware.Dto;
using track3_api_reportes_core.Middleware.Interfaces;
using track3_api_reportes_core.Middleware.Interfaces;

namespace track3_api_reportes_core.Aplicacion.Reportes
{
    public class RemitoApplication:IRemitosApplication
    {

        private readonly IRemitosMiddleware _remitosMiddleware;
        private readonly IServiceHelper _helper;
        private readonly IConfiguration _configuration;
        private Logs.Logs sb;
        public RemitoApplication(IRemitosMiddleware remitosMiddleware, IServiceHelper helper , IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration=configuration;
            _helper=helper;
            _remitosMiddleware= remitosMiddleware;
            sb = new Logs.Logs(environment);
            sb.Proceso = "RemitoElectronico";
            sb.lineas = new List<string>();
        }


        public RemitoImpresion getRemitosImpresion(string idHojaRuta)
        {
            RemitoImpresion retorno=new RemitoImpresion();
            retorno=_remitosMiddleware.GetRemitos(idHojaRuta);

            if (retorno.PedidosRemitos==null || retorno.PedidosRemitos.Count==0 )
            {
                var servicio = _configuration.GetSection("ControlApps").Value;
                var metodo= _configuration.GetSection("ReintentarRemitosCOT").Value;

                ApiCotResponse response = new ApiCotResponse();
                try
                {
                    response = _helper.GetRequest<ApiCotResponse>(servicio, metodo, new string[]{idHojaRuta});

                }
                catch (WebException web)
                {
                    this.sb.lineas.Add($"Se recibe la siguiente respuesta en web {JsonConvert.SerializeObject(web)}");
                    StreamReader sr = new StreamReader(web.Response.GetResponseStream()); /*;*/
                    var Mensaje = sr.ReadToEnd();
                    this.sb.lineas.Add($"Se recibe la siguiente respuesta {Mensaje}");
                    this.sb.GrabarLogs();
                    ErrorAplication result = new ErrorAplication("") { Codigo = 1, Mensaje = Mensaje };
                    ApiCotResponse resultado = new ApiCotResponse();
                    resultado.message = result;
                    throw new Exception(result.Mensaje + result.Codigo);
                }
                catch (Exception ex)
                {
                    this.sb.lineas.Add($"Se recibe la siguiente respuesta de error en api {JsonConvert.SerializeObject(ex)}");
                    this.sb.GrabarLogs();
                    ErrorAplication result = new ErrorAplication("") { Codigo = 1, Mensaje = ex.Message };
                    ApiCotResponse resultado = new ApiCotResponse();
                    resultado.message = result;
                    throw new Exception(result.Mensaje + result.Codigo);
                }

                retorno = _remitosMiddleware.GetRemitos(idHojaRuta);
            }

            return retorno;
        }
    }
}
