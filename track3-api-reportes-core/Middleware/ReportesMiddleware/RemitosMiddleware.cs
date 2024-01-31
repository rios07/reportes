using System.Data;
using Infraestructure.track3.Models.movil;
using Infraestructure.track3.Models.pedido;
using Infraestructure.track3.Models.planificacion;
using track3_api_reportes_core.Infraestructura.Interfaces;
using track3_api_reportes_core.Infraestructura.Repositorios;
using track3_api_reportes_core.Middleware.Dto;
using track3_api_reportes_core.Middleware.Interfaces;

namespace track3_api_reportes_core.Middleware.ReportesMiddleware
{
    public class RemitosMiddleware: IRemitosMiddleware
    {
        private readonly IRemitosRepository _remitosRepository;
        private readonly IConfiguration _configuration;
        public RemitosMiddleware(IRemitosRepository remitosRepository ,IConfiguration configuration)
        {
            _remitosRepository = remitosRepository;
            _configuration  = configuration;
        }
        public RemitoImpresion GetRemitos(string idHojaRuta)
        {
            RemitoImpresion retorno = new RemitoImpresion();
            string remitos="";

            DataTable dt = _remitosRepository.getDatosRemitos(long.Parse(idHojaRuta));
            if (dt.Rows.Count > 0)
            {
                GpsCanal canal = null;
                retorno=mapRemito(dt, ref canal);
                foreach (DataRow row in dt.Rows)
                {
                    remitos = remitos=="" ? "'" + row["REMITO"].ToString() + "'" : remitos + ',' + "'" + row["REMITO"].ToString() + "'";
                    PedidoRemito obj=new PedidoRemito(row);
                    if (!retorno.PedidosRemitos.Exists(x=>x.Remito==obj.Remito))
                        retorno.PedidosRemitos.Add(obj);
                }

                dt = _remitosRepository.getCAI(canal);
                mapCai(dt, ref retorno);

                if (remitos != "")
                {
                    var listpedidos = ObtenerListPedidosParaCOT(remitos, long.Parse(idHojaRuta));
                    retorno.PedidosRemitos.ForEach(x =>
                    {
                        x.Pedidos = listpedidos.Where(s => s.NroRemito == x.Remito).ToList();
                    });
                }

                return retorno;
            }
            else
            {
                return retorno;
            }
            

           
        }

        private List<GpsPedido> ObtenerListPedidosParaCOT(string remitos, long idHojaRuta)
        {
            var wf = int.Parse(_configuration.GetSection("WF").Value);
            List<GpsPedido> retorno = new List<GpsPedido>();
            DataTable dt = _remitosRepository.getPedidosByRef(remitos, idHojaRuta);
            var pedidos = "";
            foreach (DataRow row in dt.Rows)
            {
                GpsPedido pedido=new GpsPedido{Id = long.Parse(row["ID"].ToString()), nroPedidoRef = row["RESERVA"].ToString() };
                pedidos = pedidos == "" ? pedido.Id.ToString() : pedidos + ',' + pedido.Id;
                pedido.Fecha = DateTime.Parse(row["FECHA"].ToString());
                GpsCliente cliente = new GpsCliente(row);
                GpsDireccion direccionEntrega = new GpsDireccion(row);
                GpsPedidoTipo tipo = new GpsPedidoTipo(row);
                pedido.BandaHoraria = new GpsBandaHoraria(row);
                pedido.Canal = new GpsCanal(row);
                pedido.Estado = new GpsEstado(row);
                pedido.Tipo = new GpsPedidoTipo(row);
                if (row["OBSERVACION"].ToString() != null)
                    pedido.Observacion = row["OBSERVACION"].ToString();

                pedido.Cliente = cliente;
                pedido.DireccionEntrega = direccionEntrega;

                //Cantidad de items
                pedido.cantidadItems = int.Parse(row["ITEMS"].ToString());

                pedido.Terminal = int.Parse(row["TERMINAL"].ToString());
                pedido.Transaccion = int.Parse(row["TRANSACCION"].ToString());
                pedido.ImporteTotal = decimal.Parse(row["IMPORTE_TOTAL"].ToString());
                pedido.NroRemito = row["REMITO"].ToString();
                retorno.Add(pedido);
            }
            dt= _remitosRepository.getItemsById(pedidos);
            List<GpsPedidoDetalle> detalles = mapPedidoDetalle(dt);

            UnirPedidosItems(ref retorno, detalles, wf);

            return retorno;
        }

        private void UnirPedidosItems(ref List<GpsPedido> retorno, List<GpsPedidoDetalle> detalles, int wf)
        {
            retorno.ForEach(x =>
            {
                x.items = new List<GpsPedidoDetalle>();
                x.items=detalles.Where(d => d.IdPedido == x.Id).ToList();
                if (x.Canal.Id == wf)
                {
                    string observaciones = "";
                    foreach (var item in x.items)
                    {
                        if (!string.IsNullOrEmpty(item.Observaciones))
                        {
                            observaciones = observaciones == ""
                                ? item.NroReserva + " - " + item.Observaciones
                                : observaciones + " - " + item.NroReserva + " - " + item.Observaciones;
                        }

                        if (observaciones != "")
                            x.Observacion = observaciones;

                        var groups = x.items.GroupBy(x => new { id = x.Operacion.Id })
                            .Select(group => new { cantidad = group.Count(), tipoOperacion = group.Key })
                            .ToList();
                        foreach (var group in groups)
                        {
                            if (groups.Count > 1)
                                x.tipoOperacion = new GpsEstado()
                                    { Id = long.Parse(_configuration.GetSection("ENTREGA").ToString()) };
                            else
                                x.tipoOperacion = new GpsEstado() { Id = group.tipoOperacion.id };
                        }
                    }
                }
            });
        }

        public List<GpsPedidoDetalle> mapPedidoDetalle(DataTable dt)
        {
            List<GpsPedidoDetalle> lista = new List<GpsPedidoDetalle>();
            foreach (DataRow row in dt.Rows)
            {
                GpsPedidoDetalle d = new GpsPedidoDetalle(row);
                d.IdPedido= long.Parse(row["ID_PEDIDO"].ToString());
                d.NroReserva = row["NRO_RESERVA"].ToString();
                d.NroReservaOri = row["NRO_RESERVA_ORI"].ToString();
                d.IdContenedor = int.Parse(row["ID_CONTENEDOR"].ToString());
                d.IdEstado = int.Parse(row["ID_ESTADO"].ToString());
                d.Operacion = new GpsEstado() { Id = int.Parse(row["ID_OPERACION"].ToString()), Nombre = row["OPERACION"].ToString() };
                lista.Add(d);
            }

            return lista;
        }
        private void mapCai(DataTable dt, ref RemitoImpresion retorno)
        {
            foreach (DataRow item in dt.Rows)
            {
               
                retorno.fechaVencimientoCAI = DateTime.Parse(item["FECHCAIU"].ToString());
                retorno.CAI = (item["NCAICEN1"].ToString());
            }
        }

        private RemitoImpresion mapRemito(DataTable dt, ref GpsCanal canal)
        {
            RemitoImpresion retorno = new RemitoImpresion();
            foreach (DataRow row in dt.Rows)
            {
                retorno.IdHojaruta = long.Parse(row["ID_HOJARUTA"].ToString());
                retorno.Hojaruta = row["HOJARUTA"].ToString();
                retorno.Fecha = DateTime.Parse(row["FECHA"].ToString());
                retorno.DNI = row["DNI"].ToString();
                retorno.Chofer = row["CHOFER"].ToString();
                retorno.Legajo = row["LEGAJO"].ToString();
                retorno.Cadete = row["CADETE"].ToString();
                retorno.Patente = row["PATENTE"].ToString();
                retorno.NumeroMovil = int.Parse(row["NUMERO"].ToString());
                canal = new GpsCanal(row);
                retorno.Sucursal = canal.Sucursal;
                retorno.Proveedor = new GpsProveedor
                {
                    Id = int.Parse(row["ID_PROVEEDOR"].ToString()),
                    Nombre = row["PROVEEDOR"].ToString(),
                    Cuit = row["CUIT"].ToString(),
                    Telefono = row["TELEFONO_PROVEEDOR"].ToString(),
                    Direccion = new GpsDireccion()
                    {
                        Id = int.Parse(row["ID_DIRECCION"].ToString()),
                        Calle = row["CALLE"].ToString(),
                        Numero = int.Parse(row["ALTURA_CALLE"].ToString()),
                        CodigoPostal = row["CODIGO_POSTAL"].ToString(),
                        Localidad = row["LOCALIDAD"].ToString(),
                        Provincia = row["PROVINCIA"].ToString()
                    }
                };
                retorno.PedidosRemitos = new List<PedidoRemito>();
                break;
            }
            return retorno;
        }
    }
}
