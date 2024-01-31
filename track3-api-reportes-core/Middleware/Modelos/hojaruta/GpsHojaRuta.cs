using Infraestructure.track3.Models.planificacion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.track3.Models.movil;
using Infraestructure.track3.Models.pedido;
using track3_api_reportes_core.Middleware.Dto;


namespace Infraestructure.track3.Models.hojaruta
{
    public class GpsHojaRuta
    {
        public GpsHojaRuta(DataRow row)
        {
            this.Id = int.Parse(row["ID"].ToString());
            this.Stamp = DateTime.Parse(row["FECHA"].ToString());
            this.FechaPlan = DateTime.Parse(row["FECHA_PLAN"].ToString());
            if (row["FECHA_SALIDA"].ToString() != String.Empty)
                this.FechaSalida = DateTime.Parse(row["FECHA_SALIDA"].ToString());
            if (row["FECHA_CIERRE"].ToString() != String.Empty)
                this.FechaCierre = DateTime.Parse(row["FECHA_CIERRE"].ToString());
            this.Prioridad = int.Parse(row["PRIORIDAD"].ToString());
            this.WayBillId = row["WAYBILLID"].ToString();
            this.Descripcion = row["DESCRIPCION"].ToString();
            this.DistanciaTotal = !row.IsNull("DISTANCIA_TOTAL")
                ? Convert.ToDecimal(row["DISTANCIA_TOTAL"].ToString())
                : 0;


            //si recupero el anden creo el objeto
            if (row.Table.Columns.Contains("ANDEN"))
            {
                if (!row.IsNull("ANDEN"))
                {
                    this.Anden = new GpsHojaRutaAnden(row);

                    if (row.Table.Columns.Contains("ID_ANDEN"))
                        this.Anden.Id = Convert.ToInt32(row["ID_ANDEN"].ToString());

                    if (row.Table.Columns.Contains("ANDEN"))
                        this.Anden.Anden = row.Field<string>("ANDEN");
                    if (row.Table.Columns.Contains("ANDEN_HORA_DESDE"))
                        this.Anden.HoraDesde = row.Field<string>("ANDEN_HORA_DESDE");

                    if (row.Table.Columns.Contains("ANDEN_HORA_HASTA"))
                        this.Anden.HoraHasta = row.Field<string>("ANDEN_HORA_HASTA");
                }
            }
            this.HojaRutaMovil = new GpsHojaRutaMovil();//getHojaRutaMovil(id);
            this.Proveedor = new GpsProveedor();
            this.Movil = new GpsMovil { };
            this.EstadosPedidos = new List<EstadosPedidos>();
        }

        public GpsHojaRuta()
        {
            
        }
        [Key]
        public Int32 Id { get; set; }
        public DateTime Stamp { get; set; }
        public string Activo { get; set; }
        public Int64 IdEstado { get; set; }
        public Int64 Prioridad { get; set; }
        public string? WayBillId { get; set; }
        public Int32 IdOrigen { get; set; }
        public Int32 IdDestino { get; set; }
        public Int32 IdCanal { get; set; }
        public Int32 IdUsuario { get; set; }
        public string? MotivoCancelacion { get; set; }
        public Int32 IdMovilTipo { get; set; }
        public DateTime FechaPlan { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaSalida { get; set; }
        public DateTime? FechaCierre { get; set; }
        [NotMapped]
        public int cantidadPedidos { get; set; }
        public decimal? DistanciaTotal { get; set; }
        public decimal? TiempoEstimadoTotal { get; set; }
        public bool EMC { get; set; }
        public string? Observaciones { get; set; }
        public string PrintCot { get; set; }
        public string? Contenido { get; set; }
        public Int32? DistanciaReal { get; set; }
        public Int32? tiempoReal { get; set; }
        [NotMapped]
        public GpsEstado Estado { get; set; }
        [NotMapped]
        public GpsBandaHoraria bandaHorariaMayoritaria { get; set; }
        [NotMapped]
        public GpsDireccion Origen { get; set; }
        [NotMapped]
        public GpsDireccion Destino { get; set; }
        [NotMapped]
        public GpsCanal Canal { get; set; }
        [NotMapped]
        public GpsHojaRutaMovil HojaRutaMovil { get; set; }
        [NotMapped]
        public List<GpsPedidoRefAudit> ListaPedidoRefAudit { get; set; }
        [NotMapped]
        public List<GpsHojaRutaElemento> listaElementosViaje { get; set; }
        public virtual List<GpsHojaRutaDetalle> Detalle { get; set; }
        public virtual List<GpsHojaRutaEstado> HojaRutaEstado { get; set; }
        public virtual List<GpsHojaRutaPersonal> listaPersonal { get; set; }
        public virtual GpsProveedor Proveedor { get; set; }
        public virtual List<EstadosPedidos> EstadosPedidos { get; set; }
        public virtual GpsMovil Movil { get; set; }
        public virtual GpsHojaRutaAnden? Anden { get; set; }
        ~GpsHojaRuta()
        {

        }


    }
}
