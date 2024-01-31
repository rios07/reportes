using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using track3_api_reportes_core.Aplicacion.Interfaces;
using track3_api_reportes_core.Aplicacion.Logs;
using track3_api_reportes_core.Controllers.Result;
using track3_api_reportes_core.Middleware.Dto;

namespace track3_api_reportes_core.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RemitoController : ControllerBase
    {

        public readonly IRemitosApplication _RemitosApplication;
        private Logs logs;
        public RemitoController( IWebHostEnvironment environment, IRemitosApplication remitosApplication)
        {
            logs = new Logs(environment);
            logs.lineas = new List<string>();
            logs.Proceso = "Reportes";
            _RemitosApplication = remitosApplication;
        }

        [HttpGet]
        [Route("getRemitoImpresion/{IdHojaRuta}")]
        [AllowAnonymous]
        public IActionResult getRemitoImpresion(string IdHojaRuta)
        {
            BasicResult<RemitoImpresion> retorno = new BasicResult<RemitoImpresion>();
            try
            {
                logs.lineas.Add($"datos de Entrada getRemitoImpresion:  {IdHojaRuta}");
                logs.GrabarLogs();
                retorno.Data = _RemitosApplication.getRemitosImpresion(IdHojaRuta);

            }
            catch (Exception ex)
            {
                retorno.oError.Codigo = 220;
                retorno.oError.Mensaje = ex.Message;
                retorno.Status = "NOk";
                logs.lineas.Add($"datos de Excepcion por error:  {ex.Message}");
                logs.lineas.Add($" Interfaz ---> ReportesCORE -----> Método getRemitoImpresion");
                logs.lineas.Add($"Error ----> {ex.Message}");
                if (ex.InnerException != null)
                    logs.lineas.Add($" mas detalles -----> {ex.InnerException.Message}");

                logs.lineas.Add($"{ex.StackTrace}");
                logs.GrabarLogs();
            }
            logs.GrabarLogs();
            return Ok(retorno);
        }
    }
}
