using System;
using track3_api_reportes_core.Aplicacion.Interfaces;
using track3_api_reportes_core.Aplicacion.Logs;
using track3_api_reportes_core.Aplicacion.Requests;
using track3_api_reportes_core.Controllers.Result;
using track3_api_reportes_core.Infraestructura.track3.Models.Servicios.Posicion;
using track3_api_reportes_core.Middleware.Dto;
using track3_api_reportes_core.Middleware.Interfaces;
using track3_api_reportes_core.Middleware.Modelos.Track;

namespace track3_api_reportes_core.Aplicacion.Reportes
{
    public class DispatchApplication:IDispatchApplication
    {
        private readonly IServiciosMiddleware _servicios;
        private readonly ISecureWayMiddleware _secureWay;
        private readonly IReportes _reportes;
        public static IConfiguration _configuration;
        private Logs.Logs sb;
       
        public DispatchApplication(IWebHostEnvironment environment,IServiciosMiddleware servicios,IConfiguration configuration, IReportes reportes , ISecureWayMiddleware secureway)
        {
            _servicios=servicios;
            _configuration=configuration;
            _reportes = reportes;
            _secureWay=secureway;
            sb = new Logs.Logs(environment);
            sb.lineas = new List<string>();
            sb.Proceso = "CalculoKM";
        }


        public async Task<ErrorAplication> CalcularKmRealHojaRuta(long idHojaRuta)
        {
            ErrorAplication retorno = new ErrorAplication("");
            var HojaRuta = await _servicios.GetHojaRuta(idHojaRuta, false);

            string estadosValidos = _configuration.GetSection("HR_CERRADA").Value + "," + _configuration.GetSection("HR_LIQUIDADA").Value;

            //Se valida que se hayan obtenido datos
            if (!estadosValidos.Contains(HojaRuta.Estado.Id.ToString()))
                throw new Exception("La hoja de ruta no se encuentra en un estado valido para reprocesar");
            if (string.IsNullOrEmpty(HojaRuta.FechaSalida.ToString()) || string.IsNullOrEmpty(HojaRuta.FechaCierre.ToString()))
                throw new Exception("No se obtuvieron fechas de salida y cierre de la hoja de ruta");

            if (string.IsNullOrEmpty(HojaRuta.HojaRutaMovil.Patente))
                throw new Exception("No se pudo obtener móvil de la hoja de ruta");

            List<Elemento> elementos =await _reportes.GetListElementosToGetRecorrido(idHojaRuta, HojaRuta);

            retorno=await _secureWay.calcularKmHojaRuta(elementos,HojaRuta);
            return retorno;
        }
    }
}
