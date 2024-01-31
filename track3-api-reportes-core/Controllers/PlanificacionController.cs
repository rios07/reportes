using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using track3_api_reportes_core.Aplicacion.Interfaces;
using track3_api_reportes_core.Aplicacion.Logs;
using track3_api_reportes_core.Controllers.Result;
using track3_api_reportes_core.Middleware.Dto;
using track3_api_reportes_core.Aplicacion.Data;
using Infraestructure.track3.Models.hojaruta;
using Infraestructure.track3.Models.pedido;

namespace track3_api_reportes_core.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class PlanificacionController : Controller
    {
        public readonly IReportes _IReportes;
        public readonly IMaestros _IMaestros;
        public readonly IServicios _IServicios;

        private Logs logs;
        public PlanificacionController(IReportes IReportes, IMaestros IMaestros, IServicios IServicios, IWebHostEnvironment environment)
        {
            _IReportes = IReportes;
            _IMaestros = IMaestros;
            _IServicios = IServicios;

            logs = new Logs(environment);
            logs.lineas = new List<string>();
            logs.Proceso = "Reportes";

        }


        [HttpGet]
        [Route("getHojaRuta/{idPlanificacion}/{DetallePedido?}")]
        public async Task<IActionResult> getHojaRuta(string idPlanificacion, Boolean DetallePedido = false)
        {
            
            BasicResult<ResponsePlanificacion> retorno = new BasicResult<ResponsePlanificacion>();

            try
            {   
                logs.lineas.Add($"Inicia llamado a getHojaRuta con la idPlanificacion ==> {idPlanificacion} ");
                logs.GrabarLogs();

                retorno.Data = await _IServicios.GetHojaRuta(int.Parse(idPlanificacion),DetallePedido);
            
                retorno.Data.WayBillId = retorno.Data.HojaRuta.WayBillId;
                retorno.Status = BASICMESSAGES.OK;

                logs.lineas.Add($"Finaliza OK llamado a getHojaRuta con la idPlanificacion ==> {idPlanificacion} ");
                logs.GrabarLogs();


            }
            catch (Exception ex) 
            {
                logs.lineas.Add("Error al obtener HojaRuta: " + ex.Message);
                logs.GrabarLogs();
                retorno.oError.Mensaje = ex.Message;
                //retorno.oError.Exception = ex;
                retorno.oError.Codigo = 201;
                retorno.Status = BASICMESSAGES.NOK;
            }
            return Ok(retorno);
        }

        [HttpGet]
        [Route("PanelHojasDeRutas/{idCanal}/{fechaDesde}/{fechaHasta}")]
        public async Task<IActionResult> PanelHojasDeRutas(string idCanal ,string fechaDesde ,string fechaHasta)
        {

            BasicResult<List<GpsHojaRuta>> retorno = new BasicResult<List<GpsHojaRuta>>();

            try
            {
                logs.lineas.Add($"Inicia llamado a PanelHojasDeRutas con id canal y fechas ==> {idCanal},{fechaDesde},{fechaHasta} ");
                logs.GrabarLogs();

                DateTime fDesde = DateTime.ParseExact(fechaDesde, "yyyyMMdd", CultureInfo.InvariantCulture);
                DateTime fHasta = DateTime.ParseExact(fechaHasta, "yyyyMMdd", CultureInfo.InvariantCulture);

                retorno.Data = await _IServicios.getPanelHojaRutas(int.Parse(idCanal),fDesde,fHasta);
                retorno.Status = BASICMESSAGES.OK;

                logs.lineas.Add($"Finaliza OK llamado a getHojaRuta con id canal y fechas ==> {idCanal},{fechaDesde},{fechaHasta} ");
                logs.GrabarLogs();


            }
            catch (Exception ex)
            {
                logs.lineas.Add("Error al obtener HojaRuta: " + ex.Message);
                logs.GrabarLogs();
                retorno.oError.Mensaje = ex.Message;
                retorno.oError.Exception = ex;
                retorno.oError.Codigo = 201;
                retorno.Status = BASICMESSAGES.NOK;
            }
            return Ok(retorno);
        }
        
        [HttpGet]
        [Route("GetBandasHorarias/{idCanal}")]
        public async Task<ActionResult> GetBandasHorarias(string idCanal)
        {
            BasicResult<List<GpsBandaHoraria>> retorno = new BasicResult<List<GpsBandaHoraria>>();

            try
            {
                int _idCanal = 0;
                if(int.TryParse(idCanal, out _idCanal))
                {
                    if (_idCanal < 1)
                        throw new Exception("Id canal incorrecto");
                    retorno.Data = await _IServicios.GetBandasHorarias(_idCanal);
                }
            }
            catch(Exception ex)
            {
                logs.lineas.Add("Error al obtener Bandas Horarias: " + ex.Message);
                logs.GrabarLogs();
                retorno.oError.Mensaje = ex.Message;
                retorno.oError.Exception = ex;
                retorno.oError.Codigo = 201;
                retorno.Status = BASICMESSAGES.NOK;
            }
            return Ok(retorno);
        }

        [HttpGet]
        [Route("getPedido/{id}")]
        public async Task<IActionResult> getPedido(string id)
        {
            BasicResult<GpsPedido> retorno = new BasicResult<GpsPedido>();

            try
            {
                logs.lineas.Add($"Inicia llamado a getPedido con id de Pedido ==> {id}. ");
                logs.GrabarLogs();

                retorno.Data = await _IServicios.GetPedido(long.Parse(id));
                retorno.Status = BASICMESSAGES.OK;
            }
            catch (Exception ex)
            {
                logs.lineas.Add($"ERROR - getPedido - Detalle: {ex.Message}");
                logs.GrabarLogs();

                retorno.oError.Mensaje = ex.Message;
                retorno.oError.Exception = ex;
                retorno.oError.Codigo = 201;
                retorno.Status = BASICMESSAGES.NOK;
            }
            return Ok(retorno);
        }

        [HttpGet]
        [Route("getBandasHorarias/{idCanal}")]
        public async Task<ActionResult> getBandasHorarias(string idCanal)
        {
            BasicResult<List<GpsBandaHoraria>> retorno = new BasicResult<List<GpsBandaHoraria>>();

            try
            {
                int _idCanal = 0;
                if(int.TryParse(idCanal, out _idCanal))
                {
                    if (_idCanal < 1)
                        throw new Exception("Id canal incorrecto");
                    retorno.Data = await _IServicios.GetBandasHorarias(_idCanal);
                }
            }
            catch(Exception ex)
            {
                logs.lineas.Add("Error al obtener Bandas Horarias: " + ex.Message);
                logs.GrabarLogs();
                retorno.oError.Mensaje = ex.Message;
                retorno.oError.Exception = ex;
                retorno.oError.Codigo = 201;
                retorno.Status = BASICMESSAGES.NOK;
            }
            return Ok(retorno);
        }

        //Se agrega nuevo endpoint para obtener un pedido a partir de una planificacion y de su (x,y)
        [HttpGet]
        [Route("getReservasByIdPedido/{idPedido}/{idHojaRuta}")]
        public async Task<IActionResult> getReservasByIdPedido(string idPedido,string idHojaRuta)
        {
            BasicResult<GpsPedido> retorno = new BasicResult<GpsPedido>();

            try
            {
                logs.lineas.Add($"Inicia llamado a getReservasByIdPedido con id de Pedido ==> {idPedido} y con idHojaRuta.{idHojaRuta}  ");
                logs.GrabarLogs();

                retorno.Data = await _IServicios.getReservasByIdPedido(long.Parse(idPedido), long.Parse(idHojaRuta));
                retorno.Status = BASICMESSAGES.OK;
            }
            catch (Exception ex)
            {
                logs.lineas.Add($"ERROR - getReservasByIdPedido - Detalle: {ex.Message}");
                logs.GrabarLogs();

                retorno.oError.Mensaje = ex.Message;
                retorno.oError.Exception = ex;
                retorno.oError.Codigo = 201;
                retorno.Status = BASICMESSAGES.NOK;
            }
            return Ok(retorno);
        }




        [HttpGet]
        [Route("pedidoSearch/{pedido}")]
        public IActionResult PedidoSearch(string pedido) 
        {
            BasicResult<List<GpsPedido>> retorno = new BasicResult<List<GpsPedido>>();

            try
            {
                logs.lineas.Add($"INFO - Inicia llamado a PedidoSearch con id de Pedido ==> {pedido}. ");
                logs.GrabarLogs();

                retorno.Data = _IServicios.PedidoSearch(pedido);
                retorno.Status = BASICMESSAGES.OK;

            }
            catch(Exception ex)
            {
                logs.lineas.Add($"ERROR - PedidoSearch - Detalle: {ex.Message}");
                logs.GrabarLogs();

                retorno.oError.Mensaje = ex.Message;
                retorno.oError.Exception = ex;
                retorno.oError.Codigo = 1;
                retorno.Status = BASICMESSAGES.NOK;
            }

            return Ok(retorno);
        }

    }
}
