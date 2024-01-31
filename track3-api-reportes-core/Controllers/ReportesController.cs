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
using track3_api_reportes_core.Aplicacion.Data;
using track3_api_reportes_core.Middleware.Modelos.Track;
using Infraestructure.track3.Models.movil;
using track3_api_reportes_core.Middleware.Modelos.movil;

namespace track3_api_reportes_core.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ReportesController : Controller 
    {
        public readonly IReportes _IReportes;
        public readonly IServicios _IServicios;

        private Logs logs;
        public ReportesController(IReportes IReportes, IServicios IServicios, IWebHostEnvironment environment)
        {
            _IReportes = IReportes;
            _IServicios = IServicios;
            logs = new Logs(environment);
            logs.lineas = new List<string>();
            logs.Proceso = "Reportes";          
        }

        [HttpPost("ReporteMovilesProveedor")]
        public IActionResult ReporteMovilesProveedor(FiltroReportes Filtro)
        {
            BasicResult<List<SpCdProdViaje>> retorno= new BasicResult<List<SpCdProdViaje>>();

            logs.lineas.Add($"MovilesProveedor - Recepcion de Pedido  de {Filtro.Legajo} con Filtro :  {JsonConvert.SerializeObject(Filtro)} ");
            logs.GrabarLogs();

            
            DateTime f_desde;
            try
            {
                f_desde = DateTime.ParseExact(Filtro.strFechaDesde, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {
                Console.WriteLine("Error deserializar Fecha Desde");
                logs.lineas.Add($"ReporteViajesMovilesPedidos - Deserializacion de Fecha Desde fallo!!   {JsonConvert.SerializeObject(Filtro)} ");
                logs.GrabarLogs();

                return Ok(new { Error = true, ErrorMessage = "Deserializacion de Fecha Desde fallo!" });

            }

            DateTime f_hasta;
            try
            {
                f_hasta = DateTime.ParseExact(Filtro.strFechaHasta, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {
                Console.WriteLine("Error deserializar Fecha Hasta ");
                logs.lineas.Add($"ReporteViajesMovilesPedidos - Deserializacion de Fecha Hasta fallo!!   {JsonConvert.SerializeObject(Filtro)} ");
                logs.GrabarLogs();

                return Ok(new { Error = true, ErrorMessage = "Deserializacion de Fecha Desde fallo!" });

            }


            //Verificacion que la fecha Desde no supere a la fecha Hasta
            if (DateTime.Compare(f_desde,f_hasta)>0)
            {
                 logs.lineas.Add($"MovilesProveedor - Fecha Desde MAYOR A Fecha Hasta :  {JsonConvert.SerializeObject(Filtro)} ");
                 logs.GrabarLogs();
                 return Ok(new { Error = true, ErrorMessage = "La Fecha Desde supera a la fecha Hasta" });
             }

            //Por ultimo que la fecha Hasta no supere a la fecha del dia
            if (DateTime.Compare(f_hasta, DateTime.Now.Date) > 0)
            {
                logs.lineas.Add($"MovilesProveedor - Fecha Hasta Supera a la fecha del dia :  {JsonConvert.SerializeObject(Filtro)} ");
                logs.GrabarLogs();
                return Ok(new { Error = true, ErrorMessage = "La Fecha Hasta supera al dia de la fecha" });
            }

            try
            {
                retorno.Data = _IReportes.ReporteMovilesProveedor(Filtro);

                logs.lineas.Add($"MovilesProveedor - Se retorna el informa de manera exitosa al LEGAJO ");
                logs.GrabarLogs();

                return Ok(retorno);
            }

            catch (Exception ex)
            {
                logs.lineas.Add($"MovilesProveedor - - ERROR - {ex.Message} ");
                logs.GrabarLogs();
                return Ok(new { Error = true, ErrorMessage = ex.Message });
            }

        }

   
        [HttpPost("ReporteViajesMovilesPedidos")]
        public IActionResult ReporteViajesMovilesPedidos(Mobiliario Filtro)
        {
            BasicResult<List<SpDigViajMovPed>> retorno = new BasicResult<List<SpDigViajMovPed>>();

            logs.lineas.Add($"ReporteViajesMovilesPedidos - Recepcion de Pedido  de {Filtro.Legajo} con Filtro :  {JsonConvert.SerializeObject(Filtro)} ");
            logs.GrabarLogs();


            DateTime fechaD;
            //Fijar formato de fecha a ver si funciona
            try
            {
                fechaD= DateTime.ParseExact(Filtro.strFechaDesde, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {
                Console.WriteLine("Error deserializar Fecha Desde") ;
                logs.lineas.Add($"ReporteViajesMovilesPedidos - Deserializacion de Fecha Desde fallo!!   {JsonConvert.SerializeObject(Filtro)} ");
                logs.GrabarLogs();
                
                return Ok(new { Error = true, ErrorMessage = "Deserializacion de Fecha Desde fallo!" });

            }

            DateTime fechaH;
            try
            {
                 fechaH= DateTime.ParseExact(Filtro.strFechaHasta, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {
                Console.WriteLine("Error deserializar Fecha Hasta ");
                logs.lineas.Add($"ReporteViajesMovilesPedidos - Deserializacion de Fecha Hasta fallo!!   {JsonConvert.SerializeObject(Filtro)} ");
                logs.GrabarLogs();

                return Ok(new { Error = true, ErrorMessage = "Deserializacion de Fecha Desde fallo!" });

            }


        
            //Verificacion que la fecha Desde no supere a la fecha Hasta
            if (DateTime.Compare(fechaD, fechaH) > 0)
            {
                logs.lineas.Add($"ReporteViajesMovilesPedidos - Fecha Desde MAYOR A Fecha Hasta :  {JsonConvert.SerializeObject(Filtro)} ");
                logs.GrabarLogs();
                return Ok(new { Error = true, ErrorMessage = "La Fecha Desde supera a la fecha Hasta!!!" });
            }

            //Por ultimo que la fecha Hasta no supere a la fecha del dia
            if (DateTime.Compare(fechaH, DateTime.Now.Date) > 0)
            {
                logs.lineas.Add($"ReporteViajesMovilesPedidos - Fecha Hasta Supera a la fecha del dia :  {JsonConvert.SerializeObject(Filtro)} ");
                logs.GrabarLogs();
                return Ok(new { Error = true, ErrorMessage = "La Fecha Hasta supera al dia de la fecha!!!" });
            }

            TimeSpan diferencia = fechaH - fechaD;
            int cantidadDias = diferencia.Days;

            if (cantidadDias > 40)
            {
                logs.lineas.Add($"ReporteViajesMovilesPedidos - Desde la Fecha Desde hasta la fecha Hasta hay mas de 40 dias  {JsonConvert.SerializeObject(Filtro)} ");
                logs.GrabarLogs();
                return Ok(new { Error = true, ErrorMessage = "Desde la Fecha Desde hasta la fecha Hasta hay mas de 40 dias" });
            }


            //Termino pasando la fecha limpias
            Filtro.strFechaDesde = fechaD.ToString("dd/MM/yyyy");
            Filtro.strFechaHasta = fechaH.ToString("dd/MM/yyyy");


            try
            {
                retorno.Data = _IReportes.ReporteViajesMovilesPedidos(Filtro);
                                 
                logs.lineas.Add($"ReporteViajesMovilesPedidos - Se retorna el informa de manera exitosa al LEGAJO ");
                logs.GrabarLogs();

                return Ok(retorno);
            }

            catch (Exception ex)
            {
                                 
                logs.lineas.Add($"ReporteViajesMovilesPedidos - ERROR - {ex.Message} ");
                logs.GrabarLogs();
                return Ok(new { Error = true, ErrorMessage = ex.Message });
            }

        }


        [HttpPost("ObtenerPosiciones")]
        public IActionResult ObtenerPosiciones(Dispositivo Filtro)
        {
            BasicResult<List<SpObtenerPosiciones>> retorno = new BasicResult<List<SpObtenerPosiciones>>();

            logs.lineas.Add($"ObtenerPosiciones - Recepcion de Pedido  de {Filtro.Legajo} con Filtro :  {JsonConvert.SerializeObject(Filtro)} ");
            logs.GrabarLogs();

            DateTime fechaD;
            //Fijar formato de fecha a ver si funciona
            try
            {
                fechaD = DateTime.ParseExact(Filtro.strFechaDesde.Substring(0,10), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {
                Console.WriteLine("Error deserializar Fecha Desde");
                logs.lineas.Add($"ObtenerPosiciones - Deserializacion de Fecha Desde fallo!!   {JsonConvert.SerializeObject(Filtro)} ");
                logs.GrabarLogs();

                return Ok(new { Error = true, ErrorMessage = "Deserializacion de Fecha Desde fallo!" });

            }

            DateTime fechaH;
            try
            {
                fechaH = DateTime.ParseExact(Filtro.strFechaHasta.Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {
                Console.WriteLine("Error deserializar Fecha Hasta ");
                logs.lineas.Add($"ObtenerPosiciones - Deserializacion de Fecha Hasta fallo!!   {JsonConvert.SerializeObject(Filtro)} ");
                logs.GrabarLogs();

                return Ok(new { Error = true, ErrorMessage = "Deserializacion de Fecha Desde fallo!" });

            }



            //Verificacion que la fecha Desde no supere a la fecha Hasta
            if (DateTime.Compare(fechaD, fechaH) > 0)
            {
                logs.lineas.Add($"ObtenerPosiciones - Fecha Desde MAYOR A Fecha Hasta :  {JsonConvert.SerializeObject(Filtro)} ");
                logs.GrabarLogs();
                return Ok(new { Error = true, ErrorMessage = "La Fecha Desde supera a la fecha Hasta!!!" });
            }

            //Por ultimo que la fecha Hasta no supere a la fecha del dia
            if (DateTime.Compare(fechaH, DateTime.Now.Date) > 0)
            {
                logs.lineas.Add($"ObtenerPosiciones - Fecha Hasta Supera a la fecha del dia :  {JsonConvert.SerializeObject(Filtro)} ");
                logs.GrabarLogs();
                return Ok(new { Error = true, ErrorMessage = "La Fecha Hasta supera al dia de la fecha!!!" });
            }

            TimeSpan diferencia = fechaH - fechaD;
            int cantidadDias = diferencia.Days;

            if (cantidadDias > 40)
            {
                logs.lineas.Add($"ObtenerPosiciones - Desde la Fecha Desde hasta la fecha Hasta hay mas de 40 dias  {JsonConvert.SerializeObject(Filtro)} ");
                logs.GrabarLogs();
                return Ok(new { Error = true, ErrorMessage = "Desde la Fecha Desde hasta la fecha Hasta hay mas de 40 dias" });
            }
            
            //Si el ID es 0 el Identificador(IMEI) debe tener un largo mayor a 12 caracteres
            if ((Filtro.p_Id_Dispositivo==0 || Filtro.p_Id_Dispositivo ==null) && Filtro.p_Identificador.Length<12)

            {
                logs.lineas.Add($"ObtenerPosiciones - El identificador es 0 y el Identificador tiene menos de 20 caracteres  {JsonConvert.SerializeObject(Filtro)} ");
                logs.GrabarLogs();
                return Ok(new { Error = true, ErrorMessage = "El id_identificador es 0 y el Identificador(IMEI) tiene menos de 12 caracteres" });

            }
            if (!Regex.IsMatch(Filtro.Legajo, @"^\d+$"))
            {
                logs.lineas.Add($"ObtenerPosiciones - El legajo debe contener estrictamente numeros  {JsonConvert.SerializeObject(Filtro)} ");
                logs.GrabarLogs();
                return Ok(new { Error = true, ErrorMessage = "El legajo debe contener estrictamente numeros " });
            }



            try
            {
                retorno.Data = _IReportes.ObtenerPosiciones(Filtro);

                logs.lineas.Add($"ObtenerPosiciones - Se retorna el informa de manera exitosa al LEGAJO : {Filtro.Legajo}");
                logs.GrabarLogs();

                return Ok(retorno);
            }

            catch (Exception ex)
            {
                logs.lineas.Add($"ObtenerPosiciones - ERROR - {ex.Message} ");
                logs.GrabarLogs();
                return Ok(new { Error = true, ErrorMessage = ex.Message });
            }

        }


        [HttpPost("GetKilometrosHojaRuta")]
        public IActionResult GetKilometrosHojaRuta(Aplicacion.Filtros.Canal Filtro)
        {
            BasicResult<List<SPGetKilometrosHojaRuta>> retorno = new BasicResult<List<SPGetKilometrosHojaRuta>>();

            logs.lineas.Add($"GetKilometrosHojaRuta - Recepcion de Pedido  de {Filtro.Legajo} con Filtro :  {JsonConvert.SerializeObject(Filtro)} ");
            logs.GrabarLogs();

            DateTime fechaD;
            //Fijar formato de fecha a ver si funciona
            try
            {
                fechaD = DateTime.ParseExact(Filtro.strFechaDesde.Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {
                Console.WriteLine("Error deserializar Fecha Desde");
                logs.lineas.Add($"GetKilometrosHojaRuta - Deserializacion de Fecha Desde fallo!!   {JsonConvert.SerializeObject(Filtro)} ");
                logs.GrabarLogs();

                return Ok(new { Error = true, ErrorMessage = "Deserializacion de Fecha Desde fallo!" });

            }

            DateTime fechaH;
            try
            {
                fechaH = DateTime.ParseExact(Filtro.strFechaHasta.Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {
                Console.WriteLine("Error deserializar Fecha Hasta ");
                logs.lineas.Add($"GetKilometrosHojaRuta - Deserializacion de Fecha Hasta fallo!!   {JsonConvert.SerializeObject(Filtro)} ");
                logs.GrabarLogs();

                return Ok(new { Error = true, ErrorMessage = "Deserializacion de Fecha Hasta fallo!" });

            }


            //Verificacion que la fecha Desde no supere a la fecha Hasta
            if (DateTime.Compare(fechaD, fechaH) > 0)
            {
                logs.lineas.Add($"GetKilometrosHojaRuta - Fecha Desde MAYOR A Fecha Hasta :  {JsonConvert.SerializeObject(Filtro)} ");
                logs.GrabarLogs();
                return Ok(new { Error = true, ErrorMessage = "La Fecha Desde supera a la fecha Hasta!!!" });
            }

            //Por ultimo que la fecha Hasta no supere a la fecha del dia
            if (DateTime.Compare(fechaH, DateTime.Now.Date) > 0)
            {
                logs.lineas.Add($"GetKilometrosHojaRuta - Fecha Hasta Supera a la fecha del dia :  {JsonConvert.SerializeObject(Filtro)} ");
                logs.GrabarLogs();
                return Ok(new { Error = true, ErrorMessage = "La Fecha Hasta supera al dia de la fecha!!!" });
            }

            TimeSpan diferencia = fechaH - fechaD;
            int cantidadDias = diferencia.Days;

            if (cantidadDias > 40)
            {
                logs.lineas.Add($"GetKilometrosHojaRuta - Desde la Fecha Desde hasta la fecha Hasta hay mas de 40 dias  {JsonConvert.SerializeObject(Filtro)} ");
                logs.GrabarLogs();
                return Ok(new { Error = true, ErrorMessage = "Desde la Fecha Desde hasta la fecha Hasta hay mas de 40 dias" });
            }

            
            try
            {
                retorno.Data = _IReportes.GetKilometrosHojaRuta(Filtro);

                logs.lineas.Add($"GetKilometrosHojaRuta - Se retorna el informa de manera exitosa al LEGAJO : {Filtro.Legajo}");
                logs.GrabarLogs();

                return Ok(retorno);
            }

            catch (Exception ex)
            {
                logs.lineas.Add($"GetKilometrosHojaRuta - ERROR - {ex.Message} ");
                logs.lineas.Add($"GetKilometrosHojaRuta - ERROR - {ex.InnerException} ");

                logs.GrabarLogs();
                return Ok(new { Error = true, ErrorMessage = ex.Message });
            }

        }


        [HttpPost("getMovilesEnViaje")]
        public IActionResult getMovilesEnViaje(Mobiliario Filtro)
        {
            BasicResult<List<spGetMovilesEnViaje>> retorno = new BasicResult<List<spGetMovilesEnViaje>>();

            logs.lineas.Add($"getMovilesEnViaje - Recepcion de Pedido  de {Filtro.Legajo} con Filtro :  {JsonConvert.SerializeObject(Filtro)} ");
            logs.GrabarLogs();

            try
            {
                retorno.Data = _IReportes.getMovilesEnViaje(Filtro);

                logs.lineas.Add($"getMovilesEnViaje - Se retorna el informa de manera exitosa al LEGAJO : {Filtro.Legajo}");
                logs.GrabarLogs();

                return Ok(retorno);
            }

            catch (Exception ex)
            {
                logs.lineas.Add($"getMovilesEnViaje - ERROR - {ex.Message} ");
                logs.lineas.Add($"getMovilesEnViaje - ERROR - {ex.InnerException} ");

                logs.GrabarLogs();
                return Ok(new { Error = true, ErrorMessage = ex.Message });
            }

        }


        [HttpPost("getCumplimientoEntrega")]
        public IActionResult getCumplimientoEntrega(FiltroReportes Filtro)
        {
            BasicResult<List<CumplimientoEntrega>> retorno = new BasicResult<List<CumplimientoEntrega>>();

            logs.lineas.Add($"getCumpliminetoEntrega - Recepcion de Pedido  de {Filtro.Legajo} con Filtro :  {JsonConvert.SerializeObject(Filtro)} ");
            logs.GrabarLogs();

            try
            {
                retorno.Data = _IReportes.getCumplimientoEntrega(Filtro);

                logs.lineas.Add($"getCumpliminetoEntrega - Se retorna el informa de manera exitosa al LEGAJO : {Filtro.Legajo}");
                logs.GrabarLogs();

                return Ok(retorno);
            }

            catch (Exception ex)
            {
                logs.lineas.Add($"getMovilesEnViaje - ERROR - {ex.Message} ");
                logs.lineas.Add($"getMovilesEnViaje - ERROR - {ex.InnerException} ");

                logs.GrabarLogs();
                return Ok(new { Error = true, ErrorMessage = ex.Message });
            }

        }


        [HttpPost("getCumplimientoEntregaDet")]
        public IActionResult getCumplimientoEntregaDet(FiltroReportes Filtro)
        {
            BasicResult<List<CumplimientoEntregaDetalle>> retorno = new BasicResult<List<CumplimientoEntregaDetalle>>();

            logs.lineas.Add($"getCumpliminetoEntregaDetalle - Recepcion de Pedido  de {Filtro.Legajo} con Filtro :  {JsonConvert.SerializeObject(Filtro)} ");
            logs.GrabarLogs();

            try
            {
                retorno.Data = _IReportes.getCumplimientoEntregaDet(Filtro);

                logs.lineas.Add($"getCumpliminetoEntrega - Se retorna el informa de manera exitosa al LEGAJO : {Filtro.Legajo}");
                logs.GrabarLogs();

                return Ok(retorno);
            }

            catch (Exception ex)
            {
                logs.lineas.Add($"getMovilesEnViajeDetalle - ERROR - {ex.Message} ");
                logs.lineas.Add($"getMovilesEnViajeDetalle - ERROR - {ex.InnerException} ");

                logs.GrabarLogs();
                return Ok(new { Error = true, ErrorMessage = ex.Message });
            }

        }



        [HttpPost("getReiteracionPedidoSuc")]
        public IActionResult getReiteracionPedidoSuc(FiltroReportes Filtro)
        {
            BasicResult<List<ReiteracionPedidos>> retorno = new BasicResult<List<ReiteracionPedidos>>();

            logs.lineas.Add($"getReiteracionPedidoSuc - Recepcion de Pedido  de {Filtro.Legajo} con Filtro :  {JsonConvert.SerializeObject(Filtro)} ");
            logs.GrabarLogs();

            try
            {
                retorno.Data = _IReportes.getReiteracionPedidos(Filtro);

                logs.lineas.Add($"getReiteracionPedidoSuc - Se retorna el informa de manera exitosa al LEGAJO : {Filtro.Legajo}");
                logs.GrabarLogs();

                return Ok(retorno);
            }

            catch (Exception ex)
            {
                logs.lineas.Add($"getReiteracionPedidoSuc - ERROR - {ex.Message} ");
                logs.lineas.Add($"getReiteracionPedidoSuc - ERROR - {ex.InnerException} ");

                logs.GrabarLogs();
                return Ok(new { Error = true, ErrorMessage = ex.Message });
            }

        }

        [HttpPost("getReporteEstados")]
        public IActionResult getReporteEstados(FiltroEstadosHR Filtro)
        {
            BasicResult<List<HojaRutaEstados>> retorno = new BasicResult<List<HojaRutaEstados>>();

            logs.lineas.Add($"getReporteEstados - Recepcion de Pedido  de {Filtro.legajo} con Filtro :  {JsonConvert.SerializeObject(Filtro)} ");
            logs.GrabarLogs();

            try
            {
                retorno.Data = _IReportes.getReporteEstados(Filtro);

                logs.lineas.Add($"getReporteEstados - Se retorna el informa de manera exitosa al LEGAJO : {Filtro.legajo}");
                logs.GrabarLogs();

                return Ok(retorno);
            }

            catch (Exception ex)
            {
                logs.lineas.Add($"getReporteEstados - ERROR - {ex.Message} ");
                logs.lineas.Add($"getReporteEstados - ERROR - {ex.InnerException} ");

                logs.GrabarLogs();
                return Ok(new { Error = true, ErrorMessage = ex.Message });
            }

        }



        [HttpPost("getAllCanal")]
        public IActionResult getAllCanal(FiltroEstadosHR Filtro)
        {
            BasicResult<List<track3_api_reportes_core.Middleware.Dto.Canal>> retorno = new BasicResult<List<track3_api_reportes_core.Middleware.Dto.Canal>>();

            logs.lineas.Add($"getAllCanal - Recepcion de Pedido  de {Filtro} con Filtro :  {JsonConvert.SerializeObject(Filtro)} ");
            logs.GrabarLogs();

            try
            {
                retorno.Data = _IReportes.getAllCanal();

                logs.lineas.Add($"getAllCanal - Se retorna el informa de manera exitosa al LEGAJO : {Filtro}");
                logs.GrabarLogs();

                return Ok(retorno);
            }

            catch (Exception ex)
            {
                logs.lineas.Add($"getAllCanal - ERROR - {ex.Message} ");
                logs.lineas.Add($"getAllCanal - ERROR - {ex.InnerException} ");

                logs.GrabarLogs();
                return Ok(new { Error = true, ErrorMessage = ex.Message });
            }

        }



        [HttpPost("getReiteracionPedidoSucDet")]
        public IActionResult getReiteracionPedidoSucDet(FiltroReportes Filtro)
        {
            BasicResult<List<ReiteracionPedidosSucDet>> retorno = new BasicResult<List<ReiteracionPedidosSucDet>>();

            logs.lineas.Add($"getReiteracionPedidoSucDet - Recepcion de Pedido  de {Filtro.Legajo} con Filtro :  {JsonConvert.SerializeObject(Filtro)} ");
            logs.GrabarLogs();

            try
            {
                retorno.Data = _IReportes.getReiteracionPedidosSucDet(Filtro);

                logs.lineas.Add($"getReiteracionPedidoSucDet - Se retorna el informa de manera exitosa al LEGAJO : {Filtro.Legajo}");
                logs.GrabarLogs();

                return Ok(retorno);
            }

            catch (Exception ex)
            {
                logs.lineas.Add($"getReiteracionPedidoSuc - ERROR - {ex.Message} ");
                logs.lineas.Add($"getReiteracionPedidoSuc - ERROR - {ex.InnerException} ");

                logs.GrabarLogs();
                return Ok(new { Error = true, ErrorMessage = ex.Message });
            }

        }


        [HttpPost("getReiteracionPedidoSucDetx")]
        public IActionResult getReiteracionPedidoSucDetx(FiltroReportes Filtro)
        {
            BasicResult<List<ReiteracionPedidosSucDet>> retorno = new BasicResult<List<ReiteracionPedidosSucDet>>();

            logs.lineas.Add($"getReiteracionPedidoSucDetx - Recepcion de Pedido  de {Filtro.Legajo} con Filtro :  {JsonConvert.SerializeObject(Filtro)} ");
            logs.GrabarLogs();

            try
            {
                retorno.Data = _IReportes.getReiteracionPedidosSucDetx(Filtro);

                logs.lineas.Add($"getReiteracionPedidoSucDetx - Se retorna el informa de manera exitosa al LEGAJO : {Filtro.Legajo}");
                logs.GrabarLogs();

                return Ok(retorno);
            }

            catch (Exception ex)
            {
                logs.lineas.Add($"getReiteracionPedidoSucDetx - ERROR - {ex.Message} ");
                logs.lineas.Add($"getReiteracionPedidoSucDetx - ERROR - {ex.InnerException} ");

                logs.GrabarLogs();
                return Ok(new { Error = true, ErrorMessage = ex.Message });
            }

        }


        [HttpPost("diarioPDT")]
        public IActionResult diarioPDT(FiltroPDT Filtro)
        {
            BasicResult<List<diarioPDT>> retorno = new BasicResult<List<diarioPDT>>();

            logs.lineas.Add($"diarioPDT - Recepcion de Pedido  de {Filtro.Legajo} con Filtro :  {JsonConvert.SerializeObject(Filtro)} ");
            logs.GrabarLogs();

            try
            {
               // Filtro.strFecha = Filtro.strFecha;
                retorno.Data = _IReportes.get_DiarioPDT(Filtro);

                logs.lineas.Add($"diarioPDT - Se retorna el informa de manera exitosa al LEGAJO : {Filtro.Legajo}");
                logs.GrabarLogs();

                return Ok(retorno);
            }

            catch (Exception ex)
            {
                logs.lineas.Add($"diarioPDT - ERROR - {ex.Message} ");
                logs.lineas.Add($"diarioPDT - ERROR - {ex.InnerException} ");

                logs.GrabarLogs();
                return Ok(new { Error = true, ErrorMessage = ex.Message });
            }

        }


        [HttpGet]
        [Route("getRecorrido/{IdHojaRuta}")]
        public async Task<IActionResult> getRecorrido(long IdHojaRuta)
        {
            BasicResult<List<Elemento>> retorno = new BasicResult<List<Elemento>>();

            try
            {
                logs.lineas.Add($"Inicia llamado a getRecorrido con id de Pedido ==> {IdHojaRuta}. ");
                logs.GrabarLogs();

                retorno.Data = await  _IReportes.getRecorrido(IdHojaRuta);
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


        [HttpPost("reporteMovilesEnViaje")]
        public async Task<IActionResult> reporteMovilesEnViaje(FiltroMovilesEnViaje Filtro)
        {
            BasicResult<List<GpsMovilesEnViaje>> retorno = new BasicResult<List<GpsMovilesEnViaje>>();

            logs.lineas.Add($"INFO | APU reporteMovilesEnViaje | Recepcion de pedido de {Filtro.Legajo} con filtro:  {JsonConvert.SerializeObject(Filtro)} ");
            logs.GrabarLogs();

            try
            {
                retorno.Data = await _IServicios.GetGpsMovilesEnViaje(Filtro);

                logs.lineas.Add($"INFO | API reporteMovilesEnViaje | Se retorna el informe de manera exitosa al legajo {Filtro.Legajo}.");
                logs.GrabarLogs();

                return Ok(retorno);
            }

            catch (Exception ex)
            {
                logs.lineas.Add($"Error | API reporteMovilesEnViaje | Detalle: {ex.Message} ");
                logs.lineas.Add($"Error | API reporteMovilesEnViaje | {ex.InnerException} ");

                logs.GrabarLogs();
                return Ok(new { Error = true, ErrorMessage = ex.Message });
            }

        }


        [HttpPost("actividadPorPatente")]
        public IActionResult actividadPorPatente(FiltroEvento filtro)
        {
            BasicResult<ActividadPorPatente> retorno = new BasicResult<ActividadPorPatente>();

            logs.lineas.Add($"INFO | API actividadPorPatente | Recepcion de pedido de {filtro.Legajo} con filtro:  {JsonConvert.SerializeObject(filtro)} ");
            logs.GrabarLogs();

            try
            {
                retorno.Data = _IReportes.ActividadPorPatente(filtro);

                if(retorno.oError.Codigo == 0)
                {
                    logs.lineas.Add($"INFO | API actividadPorPatente | Se retorna el informe de Actividad por Patente de forma exitosa al legajo {filtro.Legajo}.");
                    logs.GrabarLogs();

                    retorno.Status = BASICMESSAGES.OK;
                    return Ok(retorno);
                }

                retorno.Status = BASICMESSAGES.NOK;

                logs.lineas.Add($"INFO | API actividadPorPatente | El informe de Actividad por Patente no terminó correctamente, detalle: {retorno.oError.Mensaje}.");
                logs.GrabarLogs();
            }
            catch(Exception ex)
            {
                logs.lineas.Add($"ERROR | API actividadPorPatente | Detalle: {ex.Message}.");
                logs.GrabarLogs();

                retorno.oError.Codigo = 1;
                retorno.oError.Mensaje = ex.Message;
                retorno.oError.Exception = ex;
                retorno.Status = BASICMESSAGES.NOK;
            }

            return Ok(retorno);
        }

        
        [HttpPost("ReportePDT")]
        public IActionResult ReportePDT(FiltroPDT filtro)
        {
            BasicResult <List<ReportePDT>> retorno = new BasicResult<List<ReportePDT>>();

            logs.lineas.Add($"INFO | API reportePDT | Comienzo del reporte PDT con Filtro :  {JsonConvert.SerializeObject(filtro)} ");
            logs.GrabarLogs();

            try
            {
                retorno.Data = _IReportes.ReportesPDT(filtro);

                if (retorno.oError.Codigo == 0)
                {
                    logs.lineas.Add($"INFO | API reportePDT | Finalización del reporte PDT.");
                    logs.GrabarLogs();

                    retorno.Status = BASICMESSAGES.OK;
                    return Ok(retorno);
                }

                retorno.Status = BASICMESSAGES.NOK;

                logs.lineas.Add($"INFO | API reportePDT | Finalización del reporte PDT.");
                logs.GrabarLogs();
            }
            catch(Exception ex)
            {
                logs.lineas.Add($"ERROR | API reportePDT | Detalle: {ex.Message}.");
                logs.GrabarLogs();

                retorno.oError.Codigo = 1;
                retorno.oError.Mensaje = ex.Message;
                retorno.oError.Exception = ex;
                retorno.Status = BASICMESSAGES.NOK;
            }

            return Ok(retorno);
        }


        [HttpPost]
        [Route("getReporteVersionApp")]
        public IActionResult getReporteVersionApp(Mobiliario filtro)
        {

            BasicResult<List<ReporteVersionAPP>> retorno = new BasicResult<List<ReporteVersionAPP>>();
            try
            {
                retorno.Data = _IReportes.getReporteVersionAPP(filtro);
                logs.lineas.Add($"getReporteVersionApp - Se retorna el informa de manera exitosa al LEGAJO : {filtro.Legajo}");
                logs.GrabarLogs();


            }
            catch (Exception ex)
            {
                logs.lineas.Add($"getReporteVersionApp - ERROR - {ex.Message} ");
                logs.lineas.Add($"getReporteVersionApp - ERROR - {ex.InnerException} ");

                logs.GrabarLogs();
                return Ok(new { Error = true, ErrorMessage = ex.Message });
            }

            return Json(retorno);
        }


        [HttpPost("ReservasCD")]
        public IActionResult ReservasCD(FiltroReservasCD filtro)
        {
            BasicResult<List<ReporteReservasCD>> retorno = new BasicResult<List<ReporteReservasCD>>();

            logs.lineas.Add($"ReservasCD - Comienzo para  {filtro.Legajo}  con Filtro :  {JsonConvert.SerializeObject(filtro)} ");
            logs.GrabarLogs();

            try
            {
                retorno.Data = _IReportes.getReservasCD(filtro);

                if (retorno.oError.Codigo == 0)
                {
                    logs.lineas.Add($"ReservasCD | Finalización del reporte para  {filtro.Legajo} ");
                    logs.GrabarLogs();

                    retorno.Status = BASICMESSAGES.OK;
                    return Ok(retorno);
                }

                retorno.Status = BASICMESSAGES.NOK;

                logs.lineas.Add($" ReservasCD | Finalización del reporte con Error {filtro.Legajo} ");
                logs.GrabarLogs();
            }
            catch (Exception ex)
            {
                logs.lineas.Add($"ERROR | ReservasCD para  {filtro.Legajo} | Detalle: {ex.Message}.");
                logs.GrabarLogs();

                retorno.oError.Codigo = 1;
                retorno.oError.Mensaje = ex.Message;
                retorno.oError.Exception = ex;
                retorno.Status = BASICMESSAGES.NOK;
            }

            return Ok(retorno);
        }


        [HttpGet("flotaPorCanal/{idCanal}")]
        public async Task<IActionResult> flotaPorCanal(string idCanal)
        {
            BasicResult<List<Flota>> retorno = new BasicResult<List<Flota>>();

            logs.lineas.Add($"INFO | APU flotaPorCanal | Comienza la ejecución de la API.");
            logs.GrabarLogs();

            try
            {
                retorno.Data = await _IReportes.FlotaPorCanal(idCanal);

                logs.lineas.Add($"INFO | API flotaPorCanal | Se retorna el listado de vehiculos en viaje correctamente.");
                logs.GrabarLogs();

                return Ok(retorno);
            }

            catch (Exception ex)
            {
                logs.lineas.Add($"Error | API flotaPorCanal | Detalle: {ex.Message} ");
                logs.lineas.Add($"Error | API flotaPorCanal | {ex.InnerException} ");

                logs.GrabarLogs();
                return Ok(new { Error = true, ErrorMessage = ex.Message });
            }

        }


        //TRK3-1610 - (177984) - 20/12/2023: Circuito Digital - Reporte de Presencia.
        [HttpPost]
        [Route("PresenciaReporte")]
        public IActionResult PresenciaReporte(FiltroPresencia filtro)
        {
            BasicResult<PresenciaRep> retorno = new BasicResult<PresenciaRep>();

            logs.lineas.Add($"INFO | API presenciaReporte | Recepcion del pedido con filtro:  {JsonConvert.SerializeObject(filtro)} ");
            logs.GrabarLogs();

            try
            {
                retorno.Data = _IReportes.PresenciaReporte(filtro);

                if (retorno.oError.Codigo == 0)
                {
                    logs.lineas.Add($"INFO | API presenciaReporte | Se retorna el informe de Presencia de forma exitosa.");
                    logs.GrabarLogs();

                    retorno.Status = BASICMESSAGES.OK;
                    return Ok(retorno);
                }

                retorno.Status = BASICMESSAGES.NOK;

                logs.lineas.Add($"INFO | API presenciaReporte | El informe de Presencia no terminó correctamente, detalle: {retorno.oError.Mensaje}.");
                logs.GrabarLogs();
            }
            catch (Exception ex)
            {
                logs.lineas.Add($"ERROR | API presenciaReporte | Detalle: {ex.Message}.");
                logs.GrabarLogs();

                retorno.oError.Codigo = 1;
                retorno.oError.Mensaje = ex.Message;
                retorno.oError.Exception = ex;
                retorno.Status = BASICMESSAGES.NOK;
            }

            return Ok(retorno);
        }
    }
}
