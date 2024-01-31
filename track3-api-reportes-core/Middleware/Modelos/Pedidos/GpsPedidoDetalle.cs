using Infraestructure.track3.Models.planificacion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Infraestructure.track3.Models.pedido
{
    public class GpsPedidoDetalle
    {

        public GpsPedidoDetalle()
        {

        }
        public GpsPedidoDetalle(DataRow row)
        {
            this.ValidaCampoObligatorio(row);

            Plu = row["PLU"].ToString();
            Descripcion = row["DESC_PLU"].ToString();
            Cantidad = decimal.Parse(row["CANTIDAD"].ToString());
            Peso = decimal.Parse(row["PESO"].ToString());

            if (row.Table.Columns.Contains("CANTIDAD_COMPONENTE"))
            {
                CantidadComponentes = Convert.ToInt32(row["CANTIDAD_COMPONENTE"].ToString());
            }

            Ump = row["UMP"].ToString();
            Volumen = decimal.Parse(row["VOLUMEN"].ToString());
            Umv = row["UMV"].ToString();
            if (row["ID"] != null)
            {
                Id = int.Parse(row["ID"].ToString());

            }
            if (row.Table.Columns.Contains("Observaciones"))
            {
                Observaciones = row["Observaciones"].ToString();
                Observaciones = this.Truncate(600, Observaciones);
            }
        }
        [Key]
        [Column("ID", TypeName = "INT32")]
        public Int32 Id { get; set; }
        [Column("STAMP", TypeName = "TIMESTAMP")]
        public DateTime Stamp { get; set; }

        [Column("ACTIVO", TypeName = "CHAR")]
        public string Activo { get; set; }

        [Column("ID_PEDIDO", TypeName = "NUMBER(19,0)")]
        public Int64 IdPedido { get; set; }

        [Column("PLU", TypeName = "VARCHAR2")]
        public string Plu { get; set; }

        [Column("DESCRIPCION", TypeName = "VARCHAR2")]
        public string Descripcion { get; set; }
        [Column("CANTIDAD", TypeName = "DECIMAL")]
        public decimal Cantidad { get; set; }

        [Column("ID_CONTENEDOR", TypeName = "INT32")]
        public Int32 IdContenedor { get; set; }

        [Column("TIPO_OPERACION", TypeName = "INT32")]
        public Int32 TipoOperacion { get; set; }

        [Column("PESO", TypeName = "DECIMAL")]
        public decimal Peso { get; set; }

        [Column("UMP", TypeName = "VARCHAR2")]
        public string Ump { get; set; }

        [Column("VOLUMEN", TypeName = "DECIMAL")]
        public decimal Volumen { get; set; }


        [Column("UMV", TypeName = "VARCHAR2")]
        public string Umv { get; set; }

        [Column("ID_ESTADO", TypeName = "INT32")]
        public Int32 IdEstado { get; set; }

        [Column("ID_DIRECCION", TypeName = "INT32")]
        public Int32 IdDireccion { get; set; }

        [Column("NRO_RESERVA", TypeName = "VARCHAR2")]
        public string NroReserva { get; set; }

        [Column("NRO_RESERVA_ORI", TypeName = "VARCHAR2")]
        public string NroReservaOri { get; set; }

        [Column("OBSERVACIONES", TypeName = "VARCHAR2")]
        public string Observaciones { get; set; }

        [Column("CANTIDAD_COMPONENTE", TypeName = "INT32")]
        public Int32 CantidadComponentes { get; set; }

        public virtual GpsPedido Pedido { get; set; }
       
        [NotMapped]
        public GpsEstado Operacion { get; set; }

        [NotMapped]
        public bool esElectro { get; set; }

        [NotMapped]
        public bool pequenoElectro { get; set; }

        [NotMapped]
        public GpsDireccion Direccion { get; set; }
        ~GpsPedidoDetalle()
        {

        }

        private string Truncate(int length, string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= length ? value : value.Substring(0, length);
        }
        public void ValidaCampoObligatorio(DataRow row)
        {
            if (row.IsNull("UMP") || string.IsNullOrEmpty(row["UMP"].ToString()))
                throw new Exception($"Fallo al recuperar la variable Logística UMP en el Sistema GDM para la reserva " + row["RESERVA"].ToString());

            if (row.IsNull("UMV") || string.IsNullOrEmpty(row["UMV"].ToString()))
                throw new Exception($"Fallo al recuperar la variable Logística UMV en el Sistema GDM para la reserva " + row["RESERVA"].ToString());

            if (row.IsNull("VOLUMEN"))
                throw new Exception($"Fallo al recuperar la variable Logística VOLUMEN en el Sistema GDM para la reserva " + row["RESERVA"].ToString());


            if (row.IsNull("PESO"))
                throw new Exception($"Fallo al recuperar la variable Logística PESO en el Sistema GDM para la reserva " + row["RESERVA"].ToString());


            if (row.IsNull("CANTIDAD"))
                throw new Exception($"Falta cargar el campo CANTIDAD en el Sistema SIGE para la reserva " + row["RESERVA"].ToString());

            if (row.Table.Columns.Contains("CANTIDAD_COMPONENTE"))
            {
                if (row.IsNull("CANTIDAD_COMPONENTE") || Convert.ToInt32(row["CANTIDAD_COMPONENTE"].ToString()) == 0)
                {
                    if (row.Table.Columns.Contains("RESERVA"))
                    {
                        throw new Exception($"Falta cargar el campo CANTIDAD_COMPONENTES en el Sistema SIGE para la reserva " + row["RESERVA"].ToString());
                    }
                    else
                    {
                        throw new Exception($"Falta cargar el campo CANTIDAD_COMPONENTES en el Sistema TRACK3 para la reserva " + row["NRO_RESERVA"].ToString());
                    }
                }
            }

        }

    }
}
