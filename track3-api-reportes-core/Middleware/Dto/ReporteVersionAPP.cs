using System.Data;

namespace track3_api_reportes_core.Middleware.Dto
{
    public class ReporteVersionAPP
    {

        public ReporteVersionAPP() { }

        // <summary>
        /// Obtiene o establece la fecha del informe.
        /// </summary>
        public string strfecha { get; set; }

        /// <summary>
        /// Obtiene o establece el número de sucursal.
        /// </summary>
        public int sucursal { get; set; }


        public int waybillid { get; set; }


        /// <summary>
        /// Obtiene o establece el número IMEI del dispositivo.
        /// </summary>
        public string imei { get; set; }

        /// <summary>
        /// Obtiene o establece el número de teléfono.
        /// </summary>
        public string telefono { get; set; }

        /// <summary>
        /// Obtiene o establece la versión de la aplicación.
        /// </summary>
        public string version { get; set; }

        /// <summary>
        /// Crea una nueva instancia de la clase ReporteVersionAPP a partir de una fila de datos.
        /// </summary>
        /// <param name="row">Fila de datos que contiene información del informe.</param>
        public ReporteVersionAPP(DataRow row)
        {
            strfecha = row["strfecha"].ToString();
            sucursal = int.Parse(row["sucursal"].ToString());
            waybillid = int.Parse(row["waybillid"].ToString());
            imei = row["imei"].ToString();
            telefono = row["telefono"].ToString();
            version = row["version"].ToString();
        }

    }
}
