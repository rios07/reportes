using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using track3_api_reportes_core.Aplicacion.Data;
using track3_api_reportes_core.Aplicacion.Interfaces;
using track3_api_reportes_core.Aplicacion.Logs;
using track3_api_reportes_core.Controllers.Result;
using track3_api_reportes_core.Middleware.Modelos.Track;

namespace track3_api_reportes_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DispatchController : ControllerBase
    {
        private readonly IDispatchApplication _dispatch;
        private Logs logs;
        public DispatchController(IWebHostEnvironment environment , IDispatchApplication dispatch)
        {
            _dispatch = dispatch;
            logs = new Logs(environment);
            logs.lineas = new List<string>();
            logs.Proceso = "CalculoKM";

        }

        [HttpGet]
        [Route("CalculoKilometraje/{IdHojaRuta}")]
        public async Task<IActionResult> CalculoKilometraje(long IdHojaRuta)
        {
            BasicResult<ErrorAplication> retorno = new BasicResult<ErrorAplication>();

            try
            {
                logs.lineas.Add($"Inicia llamado a getRecorrido con id de Pedido ==> {IdHojaRuta}. ");
                logs.GrabarLogs();

                retorno.Data = await _dispatch.CalcularKmRealHojaRuta(IdHojaRuta);
                retorno.Status = BASICMESSAGES.OK;

                logs.lineas.Add($"Termino el llamado a getRecorrido de manera exitosa con id de Pedido ==> {IdHojaRuta}. ");
                logs.GrabarLogs();

            }
            catch (Exception ex)
            {
                logs.lineas.Add($"ERROR - getRecorrido - Detalle: {ex.Message}");
                logs.GrabarLogs();

                retorno.oError.Mensaje = ex.Message;
                retorno.oError.Exception = ex;
                retorno.oError.Codigo = 201;
                retorno.Status = BASICMESSAGES.NOK;
            }
            return Ok(retorno);


        }
    }
}
