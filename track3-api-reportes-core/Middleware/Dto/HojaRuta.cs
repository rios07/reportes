using Microsoft.AspNetCore.Components;
using static track3_api_reportes_core.Middleware.Dto.Moviles;
using System.Data;
using System;
using track3_api_reportes_core.Aplicacion.Filtros;

namespace track3_api_reportes_core.Middleware.Dto
{
    
        public class HojaRuta
        {
            public HojaRuta() { }

            public HojaRuta(DataRow row)
            {
                this.id = int.Parse(row["ID"].ToString());
                this.fecha = DateTime.Parse(row["FECHA"].ToString());
                this.fechaPlan = DateTime.Parse(row["FECHA_PLAN"].ToString());
                if (row["FECHA_SALIDA"].ToString() != String.Empty) this.fechaSalida = DateTime.Parse(row["FECHA_SALIDA"].ToString());
                if (row["FECHA_CIERRE"].ToString() != String.Empty) this.fecha_cierre = DateTime.Parse(row["FECHA_CIERRE"].ToString());
                this.prioridad = int.Parse(row["PRIORIDAD"].ToString());
                this.wayWillId = row["WAYBILLID"].ToString();
                this.descripcion = row["DESCRIPCION"].ToString();
                this.distancia_total = !row.IsNull("DISTANCIA_TOTAL") ? Convert.ToDouble(row["DISTANCIA_TOTAL"].ToString()) : 0;


                //si recupero el anden creo el objeto
                if (row.Table.Columns.Contains("ANDEN"))
                {
                    if (!row.IsNull("ANDEN"))
                    {
                        this.Anden = new Anden(row);

                        if (row.Table.Columns.Contains("ID_ANDEN"))
                            this.Anden.Id = Convert.ToInt32(row["ID_ANDEN"].ToString());

                        if (row.Table.Columns.Contains("ANDEN"))
                            this.Anden.id_anden = row.Field<string>("ANDEN");
                        if (row.Table.Columns.Contains("ANDEN_HORA_DESDE"))
                            this.Anden.hora_desde = row.Field<string>("ANDEN_HORA_DESDE");

                        if (row.Table.Columns.Contains("ANDEN_HORA_HASTA"))
                            this.Anden.hora_hasta = row.Field<string>("ANDEN_HORA_HASTA");

                        //    Id = Convert.ToInt32(row["ID_ANDEN"].ToString()) ,
                        //    id_anden = row.Field<string>("ANDEN"),
                        //    hora_desde= row.Field<string>("ANDEN_HORA_DESDE"),
                        //    hora_hasta = row.Field<string>("ANDEN_HORA_HASTA") 
                    }
                }

            }

            public int id { get; set; }
            public string wayWillId { get; set; }
            public DateTime fecha { get; set; }
            public DateTime fechaPlan { get; set; }
            public DateTime? fechaSalida { get; set; }
            public DateTime? fecha_cierre { get; set; }
            public bool activo { get; set; }
            public int version { get; set; }
         //   public Estado estado { get; set; }
            public int prioridad { get; set; }
            //public Movil movil { get; set; }
         //   public Direccion origen { get; set; }
           // public Direccion destino { get; set; }
            public string observaciones { get; set; }
            public string descripcion { get; set; }
            public Canal canal { get; set; }
            public int cantidadItems { get; set; }
            public int cantidadPedidos { get; set; }
           // public UnidadMedida peso { get; set; }
            //public UnidadMedida volumen { get; set; }
           // public HojaRutaMovil hojaRutaMovil { get; set; }
            //public List<HojaRutaDetalle> detalle { get; set; }
          //  public List<Dispatch> dispatchs { get; set; }
            //public object EstadosPedidos { get; set; }
            public List<EstadosPedidos> EstadosPedidos { get; set; }
            //public List<HojaRutaPersonal> listaPersonal { get; set; }
           // public BandaHoraria bandaHorariaMayoritaria { get; set; }
           // public List<hojaRutaElemento> listaElementosViaje { get; set; }
           // public Proveedor Proveedor { get; set; }
            //public WayPoint retorno { get; set; }
            public double distancia_total { get; set; }
            public double tiempo_estimado_total { get; set; }
            public bool EMC { get; set; }

            public Anden Anden { get; set; }
        }

        public class EstadosPedidos
        {
            public EstadosPedidos() { }
            public int Key { get; set; }
            public string Nombre { get; set; }
            public int Cantidad { get; set; }
        }

}
