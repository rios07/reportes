using System.Data;

namespace track3_api_reportes_core.Middleware.Dto
{

    //Corregir el nombre del archivo


    public class SpCdProdViaje
    {
        public SpCdProdViaje() { }

        public string FechaPlan { get; set; }
        public string Waybillid { get; set; }
        /// Obtiene o establece el número del móvil.
        public int NumeroMovil { get; set; }
        public string Patente { get; set; }
        /// Obtiene o establece el DNI del conductor.
        public int Dni { get; set; }

        /// Obtiene o establece el apellido del conductor.
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        /// Obtiene o establece el nombre del proveedor.
        public string NombreProveedor { get; set; }

        public SpCdProdViaje(DataRow row)
        {
            FechaPlan = row["FECHA_PLAN"].ToString();
            Waybillid = row["WAYBILLID"].ToString();
            NumeroMovil = int.Parse(row["NUMERO_MOVIL"].ToString());
            Patente = row["PATENTE"].ToString();
            Dni = int.Parse(row["DNI"].ToString());
            Apellido = row["APELLIDO"].ToString();
            Nombre = row["NOMBRE"].ToString();
            NombreProveedor = row["PROVEEDOR"].ToString();
        }
    }
   

    public class SpDigViajMovPed
    {
        public SpDigViajMovPed() { }

        public string Fecha { get; set; } 
        public int Viajes { get; set; } 
        public int Moviles { get; set; }
        public int Pedidos { get; set; }

        public SpDigViajMovPed(DataRow row)
        {
            
            Fecha = row["FECHA"].ToString();
            Viajes= int.Parse(row["Q_VIAJES"].ToString());
            Moviles = int.Parse(row["Q_MOVILES"].ToString());
            Pedidos = int.Parse(row["Q_PEDIDOS"].ToString());
        }
    }

    public class SpObtenerPosiciones
    {
        public SpObtenerPosiciones() { }
        public DateTime fecha_posicion { get; set; }
        public decimal gpslatitud { get; set; }
        public decimal gpslongitud { get; set; }
      
        public SpObtenerPosiciones(DataRow row)
        {
            fecha_posicion = DateTime.Parse(row["fecha_posicion"].ToString());
            gpslatitud = decimal.Parse(row["gpslatitud"].ToString());
            gpslongitud = decimal.Parse(row["gpslongitud"].ToString());
        }
     

    }


    public class SPGetKilometrosHojaRuta
    {
        // Constructor sin parámetros
        public SPGetKilometrosHojaRuta() { }

        // Propiedades de la clase
        public int IdHojaRuta { get; set; }
        public string Fecha { get; set; }
        public int Sucursal { get; set; }
        public string HojaRuta { get; set; }
        public int CantidadPedidos { get; set; }
        public string Patente { get; set; }
        public string Chofer { get; set; }
        public string KmEstimados { get; set; }

        // Constructor con parámetro DataRow
        // Este constructor se utiliza para inicializar la clase a partir de un DataRow
        // que contiene datos recuperados de una fuente de datos como una base de datos.
        public SPGetKilometrosHojaRuta(DataRow row)
        {
            // Asignación de valores a las propiedades desde los datos del DataRow
            IdHojaRuta = int.Parse(row["Id_HojaRuta"].ToString());
            Fecha = row["Fecha"].ToString();
            Sucursal = int.Parse(row["Sucursal"].ToString());
            HojaRuta = row["HojaRuta"].ToString();
            CantidadPedidos = int.Parse(row["Cantidad_Pedidos"].ToString());
            Patente = row["Patente"].ToString();
            Chofer = row["Chofer"].ToString();
            KmEstimados = row["KmEstimados"].ToString();
        }
    }

    public class spGetMovilesEnViaje
    {
        // Constructor sin parámetros
        public spGetMovilesEnViaje() { }

        // Propiedades de la clase
        public int IdHojaRuta { get; set; }
        public int waybillid { get; set; }
        public string patente { get; set; }
        public int pedidostotales { get; set; }
        public int pedidosentregados { get; set; }
        public int pedidosnoentregados { get; set; }
        public int puntosdeentrega { get; set; }

        // Constructor con parámetro DataRow
        // Este constructor se utiliza para inicializar la clase a partir de un DataRow
        // que contiene datos recuperados de una fuente de datos como una base de datos.
        public spGetMovilesEnViaje(DataRow row)
        {
            // Asignación de valores a las propiedades desde los datos del DataRow
            IdHojaRuta = int.Parse(row["idHojaRuta"].ToString());
            waybillid = int.Parse(row["waybillid"].ToString());
            patente = row["patente"].ToString();
            pedidostotales = int.Parse(row["pedidostotales"].ToString());
            pedidosentregados = int.Parse(row["pedidosentregados"].ToString());
            pedidosnoentregados = int.Parse(row["pedidosnoentregados"].ToString());
            puntosdeentrega = int.Parse(row["puntosdeentrega"].ToString());
        }
    }



}
