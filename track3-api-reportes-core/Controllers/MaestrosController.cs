using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;
using track3_api_reportes_core.Aplicacion.Filtros;
using track3_api_reportes_core.Aplicacion.Interfaces;
using track3_api_reportes_core.Aplicacion.Logs;
using track3_api_reportes_core.Controllers.Result;
using track3_api_reportes_core.Middleware.Dto;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using track3_api_reportes_core.Aplicacion.Reportes;
using static track3_api_reportes_core.Middleware.Dto.Moviles;
using static track3_api_reportes_core.Middleware.Dto.Movil;
using track3_api_reportes_core.Aplicacion.Responses;

namespace track3_api_reportes_core.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MaestrosController : Controller
    {
        public readonly IReportes _IReportes;
        public readonly IMaestros _IMaestros;

        private Logs logs;
        public MaestrosController(IReportes IReportes,IMaestros IMaestros, IWebHostEnvironment environment)
        {
            _IReportes = IReportes;
            _IMaestros = IMaestros;

            logs = new Logs(environment);
            logs.lineas = new List<string>();
            logs.Proceso = "Reportes";
        }


        [HttpGet]
        [Route("getMovil/{idMovil}")]
        public IActionResult GetMovil(string idMovil)
        {
            BasicResult<List<sp_get_movil>> retorno = new BasicResult<List<sp_get_movil>>();
            try
            {

                int nid;
                int.TryParse(idMovil, out nid);

                retorno.Data = _IMaestros.GetMovil(nid);
            }
            catch (Exception ex)
            {
                retorno.oError = new ErrorAplication(ex.Message);
            }

            return Ok(retorno);
        }

        //TRK3-1601 - 177984 - 14/12/2023: Endpoint que recupera el X e Y del canal.
        [HttpGet]
        [Route("getPosicionCanal/{idCanal}")]
        public IActionResult GetPosicionCanal(string idCanal)
        {
            BasicResult<PosicionCanalResponse> retorno = new BasicResult<PosicionCanalResponse>();

            logs.lineas.Add($"INFO | APU getPosicionCanal | Comienzo de ejecución de la API. ");
            logs.GrabarLogs();
            try
            {
                retorno.Data = _IMaestros.GetPosicionCanal(idCanal);

                logs.lineas.Add($"INFO | API getPosicionCanal | Se retorna la posición del canal de manera exitosa.");
                logs.GrabarLogs();
            }
            catch (Exception ex)
            {
                logs.lineas.Add($"ERROR | API getPosicionCanal | Detalle: {ex.Message} ");
                logs.lineas.Add($"ERROR | API getPosicionCanal | {ex.InnerException} ");

                logs.GrabarLogs();
                retorno.oError = new ErrorAplication(ex.Message);
            }

            return Ok(retorno);
        }








    }
}
