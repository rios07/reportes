using Infraestructure.track3.Models.hojaruta;
using Infraestructure.track3.Models.movil;
using Infraestructure.track3.Models.pedido;
using Infraestructure.track3.Models.planificacion;
using System.Data;
using track3_api_reportes_core.Middleware.Interfaces;
using track3_api_reportes_core.Middleware.Modelos.Pedidos;
using track3_api_reportes_core.Infraestructura.track3.Models.Servicios.equivalencias;
using track3_api_reportes_core.Infraestructura.Interfaces;
using track3_api_reportes_core.Infraestructura.SecureWay.Models;
using track3_api_reportes_core.Infraestructura.track3.Models.Servicios.Posicion;

namespace track3_api_reportes_core.Middleware.Dto
{
  
    public class ProfileForm: IMappingFormularios   
    {
        private readonly IPedidosRepositorio _PedidoRepositorio;
        private readonly IConfiguration _configuration;

        public ProfileForm(IPedidosRepositorio PedidoRepositorio, IConfiguration configuracion)
        {
            _PedidoRepositorio = PedidoRepositorio;
            _configuration = configuracion; 
            
        }

        public GpsPedido mapPedido(DataTable dt)
        {
            GpsPedido retorno = new GpsPedido();

            foreach (DataRow row in dt.Rows)
            {
                retorno = new GpsPedido(row);

                retorno.Cliente = new GpsCliente(row);
                retorno.Fecha = DateTime.Parse(row["FECHA"].ToString());
                retorno.BandaHoraria = new GpsBandaHoraria(row);
                retorno.items = new List<GpsPedidoDetalle>();
            }

            return retorno;
        }
        public GpsHojaRuta mapHojaRuta(DataTable dt)
        {
            GpsHojaRuta retorno = new GpsHojaRuta();
            foreach (DataRow row in dt.Rows)
            {
                retorno = new GpsHojaRuta(row);
                retorno.Canal = new GpsCanal(row);
                retorno.Estado = new GpsEstado(row);
                if (row["DISTANCIA_TOTAL"].ToString() != "")
                    retorno.DistanciaTotal = decimal.Parse(row["DISTANCIA_TOTAL"].ToString());
                if (row["TIEMPO_ESTIMADO_TOTAL"].ToString() != "")
                    retorno.TiempoEstimadoTotal = decimal.Parse(row["TIEMPO_ESTIMADO_TOTAL"].ToString());
                retorno.Origen = new GpsDireccion()
                {
                    Calle = row["CALLE_ORIGEN"].ToString(),
                    Numero = int.Parse(row["NUMERO_ORIGEN"].ToString()),
                    CodigoPostal = row["CODIGOPOSTAL_ORIGEN"].ToString(),
                    CoordenadaX = row["COORDENADAX_ORIGEN"].ToString(),
                    CoordenadaY = row["COORDENADAY_ORIGEN"].ToString(),
                    Localidad = row["LOCALIDAD_ORIGEN"].ToString(),
                    Piso = row["PISO_ORIGEN"].ToString(),
                    PisoDepto = row["PISO_DEPTO_ORIGEN"].ToString(),
                };
                retorno.Destino = new GpsDireccion()
                {
                    Calle = row["CALLE_DESTINO"].ToString(),
                    Numero = int.Parse(row["NUMERO_DESTINO"].ToString()),
                    CodigoPostal = row["CODIGOPOSTAL_DESTINO"].ToString(),
                    CoordenadaX = row["COORDENADAX_DESTINO"].ToString(),
                    CoordenadaY = row["COORDENADAY_DESTINO"].ToString(),
                    Localidad = row["LOCALIDAD_DESTINO"].ToString(),
                    Piso = row["PISO_DESTINO"].ToString(),
                    PisoDepto = row["PISO_DEPTO_DESTINO"].ToString(),
                };

                if (!string.IsNullOrEmpty(row["PATENTE"].ToString()))
                {
                    retorno.HojaRutaMovil.Id = int.Parse(row["ID_HOJARUTAMOVIL"].ToString());
                    retorno.HojaRutaMovil.Patente = row["PATENTE"].ToString();
                    retorno.HojaRutaMovil.Capacidad = decimal.Parse(row["CAPACIDAD"].ToString());
                    retorno.HojaRutaMovil.Apilabilidad = decimal.Parse(row["APILABILIDAD"].ToString());
                    retorno.HojaRutaMovil.Proveedor = row["PROVEEDOR"].ToString();
                    retorno.HojaRutaMovil.Numero = int.Parse(row["NUMERO"].ToString());

                    var movil = new GpsMovil();
                    movil.Id = int.Parse(row["ID_MOVIL"].ToString());
                    movil.Numero = int.Parse(row["NUMERO"].ToString());
                    movil.Patente = row["PATENTE"].ToString();
                    movil.Id_Moviltipo = int.Parse(row["ID_MOVILTIPO"].ToString());
                    movil.MovilTipo = new GpsMovilTipo
                    {
                        Id = int.Parse(row["ID_MOVILTIPO"].ToString()),
                        Descripcion = row["TIPO"].ToString(),
                        Peso = decimal.Parse(row["PESO"].ToString()),
                        Volumen = decimal.Parse(row["VOLUMEN"].ToString()),
                        UMP = row["UMP"].ToString(),
                        UMV = row["UMV"].ToString()
                    };
                    retorno.HojaRutaMovil.Movil = movil;
                    retorno.IdMovilTipo = movil.Id_Moviltipo;
                    var proveedor = new GpsProveedor();//MetodoProveedor.GetHdr(retorno.id);
                    proveedor.Id = int.Parse(row["PROVEEDOR_ID"].ToString());
                    proveedor.Nombre = row["NOMBRE_PROVEEDOR"].ToString();
                    proveedor.Cuit = row["CUIT"].ToString();

                    if (proveedor != null)
                        retorno.Proveedor = proveedor;
                }
            }

            return retorno;
        }

        public List<GpsHojaRutaEstado> mapHojaRutaEstado(DataTable dt)
        {
            List<GpsHojaRutaEstado> retorno = new List<GpsHojaRutaEstado>();
            foreach (DataRow row in dt.Rows)
            {
                retorno.Add( new GpsHojaRutaEstado(row));
            }

            return retorno;
        }

        public List<GpsPedidoDetalle> mapItems(DataTable dt)
        {
            List<GpsPedidoDetalle> lista = new List<GpsPedidoDetalle>();

            foreach (DataRow row in dt.Rows)
            {
                GpsPedidoDetalle pedido = new GpsPedidoDetalle(row);

                pedido.NroReserva = row["NRO_RESERVA"].ToString();
                pedido.NroReservaOri = row["NRO_RESERVA_ORI"].ToString();
                pedido.IdEstado = int.Parse(row["ID_ESTADO"].ToString());
                pedido.Operacion = new GpsEstado
                {
                    Id = int.Parse(row["ID_OPERACION"].ToString()),
                    Nombre = row["OPERACION"].ToString()
                };
                pedido.Direccion = new GpsDireccion(row);
                pedido.Cantidad = decimal.Parse(row["CANTIDAD"].ToString());
                lista.Add(pedido);
            }
            return lista;
        }

        public List<GpsHojaRutaDetalle> mapHRDetallePedidos(DataTable dt,ref string pedidos, Boolean DetallePedido)
        {
            List<GpsHojaRutaDetalle> retorno = new List<GpsHojaRutaDetalle>();
            foreach (DataRow row in dt.Rows)
            {
                GpsHojaRutaDetalle detalle = new GpsHojaRutaDetalle(row);
                detalle.Pedido = new GpsPedido
                {
                    Id = long.Parse(row["ID_PEDIDO"].ToString()),
                    //Fecha = DateTime.Parse(row["FECHA"].ToString
                    //FechaEvento = DateTime.Parse(row["FECHA_EVENTO"].ToString()),
                    Observacion = row["OBSERVACION"].ToString(),
                    cantidadItems = int.Parse(row["ITEMS"].ToString()),
                    nroPedidoRef = row["NRO_PEDIDO_REF"].ToString(),
                    Terminal = int.Parse(row["TERMINAL"].ToString()),
                    Transaccion = int.Parse(row["TRANSACCION"].ToString()),
                    ImporteTotal = decimal.Parse(row["IMPORTE_TOTAL"].ToString()),
                    tipoOperacion = new GpsEstado { Id = long.Parse(row["TIPO_OPERACION"].ToString()) },
                    Tipo = new GpsPedidoTipo(row),
                    listToken = new List<GpsPedidoRef>()
                };

                detalle.Pedido.listToken.Add(new GpsPedidoRef
                {
                    id = long.Parse(row["ID_PEDIDO_REF"].ToString()),
                    idPedido = long.Parse(row["ID_PEDIDO"].ToString()),
                    Cae = row["CRE"].ToString(),
                    Cre = row["CAE"].ToString(),
                    Estado = new GpsEstado { Id = long.Parse(row["ID_ESTADO_REF"].ToString()), Nombre = row["ESTADO_REF"].ToString() },
                    Operacion = new GpsEstado { Id = long.Parse(row["TIPO_OPERACION_REF"].ToString()), Nombre = row["OPERACION"].ToString() },
                    NroReferencia = row["NRO_REFERENCIA"].ToString(),
                    FechaEvento= DateTime.Parse(row["FECHA_EVENTO"].ToString()),

                });

                detalle.ListPedidosRef.Add(detalle.Pedido.nroPedidoRef);
                pedidos = pedidos == "" ? detalle.Pedido.Id.ToString() : pedidos + ',' + detalle.Pedido.Id;
                detalle.Pedido.Cliente = new GpsCliente(row);
                detalle.Pedido.BandaHoraria = new GpsBandaHoraria(row);
                detalle.Pedido.FastLine = new GpsPedidoFastLine(row);
                if (!string.IsNullOrEmpty(row["ID_EQUIVALENCIAHRDETALLE"].ToString()))
                {
                    detalle.EquivalentePago = new GpsEquivalenciaHojaRutaDetalle(row);
                }
                detalle.Pedido.DireccionEntrega = new GpsDireccion(row);
                detalle.Pedido.Estado = new GpsEstado(row);

                //Detalle de Pedido si requerido con DetallePedido=true
                if (DetallePedido)  
                {
                    detalle.Pedido.items = new List<GpsPedidoDetalle>();
                    DataTable data = _PedidoRepositorio.getPedidoDetalle(detalle.Pedido.Id);

                    detalle.Pedido.items = mapItems(data);

                    var groups = detalle.Pedido.items.GroupBy(x => new { id = x.TipoOperacion }).Select(group => new { cantidad = group.Count(), tipoOperacion = group.Key }).ToList();
                    foreach (var group in groups)
                    {
                        if (groups.Count > 1)
                            detalle.Pedido.tipoOperacion.Id = int.Parse(_configuration.GetSection("ENTREGA").ToString());
                        else
                            detalle.Pedido.tipoOperacion.Id = group.tipoOperacion.id ;
                    }

                 
                }

                retorno.Add(detalle);
            }

            return retorno;
        
        }





        public List<GpsHojaRutaPersonal> mapListPersonal(DataTable dt)
        {
            List<GpsHojaRutaPersonal> listHojaRutaPersonal = new List<GpsHojaRutaPersonal>();
            foreach (DataRow row in dt.Rows)
            {
                listHojaRutaPersonal.Add(new GpsHojaRutaPersonal(row));
            }

            return listHojaRutaPersonal;

        }

        public List<GpsPedidoRefAudit> mapListpPedidosRefAudit(DataTable dt)
        {
            List < GpsPedidoRefAudit > retorno = new List<GpsPedidoRefAudit>();
            foreach (DataRow row in dt.Rows)
            {
                retorno.Add(new GpsPedidoRefAudit(row));

            }
            return retorno;
        }

        public List<GpsBandaHoraria> mapBandaHoraria(DataTable dt)
        {
            List<GpsBandaHoraria> retorno = new List<GpsBandaHoraria>();
            foreach (DataRow row in dt.Rows)
            {
                GpsBandaHoraria obj = new GpsBandaHoraria(row);
                obj.Canal = new GpsCanal(row);
                retorno.Add(obj);
            }

            return retorno;
        }

        public List<GpsContenedor> mapPedidosContenedor(DataTable dt)
        {
            List<GpsContenedor> retorno = new List<GpsContenedor>();
            foreach (DataRow row in dt.Rows)
            {
                retorno.Add(new GpsContenedor
                {
                    IdPedido = long.Parse(row["ID_PEDIDO"].ToString()),
                    NroContenedorRef = int.Parse(row["NRO_CONTENEDOR_REF"].ToString()),
                    IdContenedor = long.Parse(row["ID_CONTENEDOR"].ToString())
                });
            }
            return retorno;
        }

        public List<GpsHojaRutaElemento> ElementosViajes(DataTable dt)
        {
            List<GpsHojaRutaElemento> retorno = new List<GpsHojaRutaElemento>();
            foreach (DataRow row in dt.Rows)
            {
                retorno.Add(new GpsHojaRutaElemento(row));
            }

            return retorno;
        }

        public List<GpsHojaRuta> mapPanelHojaRuta(DataTable dt, ref string idHojaRutas)
        {
            List<GpsHojaRuta> list = new List<GpsHojaRuta>();
            foreach (DataRow row in dt.Rows)
            {
                GpsHojaRuta retorno  = new GpsHojaRuta(row);
                idHojaRutas = idHojaRutas == "" ? retorno.Id.ToString() : idHojaRutas + ',' + retorno.Id;
                retorno.Canal = new GpsCanal(row);
                retorno.Estado = new GpsEstado(row);
                if (row["DISTANCIA_TOTAL"].ToString() != "")
                    retorno.DistanciaTotal = decimal.Parse(row["DISTANCIA_TOTAL"].ToString());
                //if (row["TIEMPO_ESTIMADO_TOTAL"].ToString() != "")
                //    retorno.TiempoEstimadoTotal = decimal.Parse(row["TIEMPO_ESTIMADO_TOTAL"].ToString());

                retorno.EMC = row["EMC"].ToString().Equals("0") ? false : true;
                retorno.cantidadPedidos = int.Parse(row["CANTIDADPEDIDOS"].ToString());
                if (!string.IsNullOrEmpty(row["PATENTE"].ToString()))
                {
                    retorno.HojaRutaMovil.Id = int.Parse(row["ID_HOJARUTAMOVIL"].ToString());
                    retorno.HojaRutaMovil.Patente = row["PATENTE"].ToString();
                    retorno.HojaRutaMovil.Capacidad = decimal.Parse(row["CAPACIDAD"].ToString());
                    retorno.HojaRutaMovil.Apilabilidad = decimal.Parse(row["APILABILIDAD"].ToString());
                    retorno.HojaRutaMovil.Proveedor = row["PROVEEDOR"].ToString();
                    retorno.HojaRutaMovil.Numero = int.Parse(row["NUMERO"].ToString());
                    var movil = new GpsMovil();
                    movil.Id = int.Parse(row["ID_MOVIL"].ToString());
                    movil.Numero = int.Parse(row["NUMERO"].ToString());
                    movil.Patente = row["PATENTE"].ToString();
                    movil.Id_Moviltipo = int.Parse(row["ID_MOVILTIPO"].ToString());
                    retorno.HojaRutaMovil.Movil = movil;
                    retorno.IdMovilTipo = movil.Id_Moviltipo;
                    var proveedor = new GpsProveedor();//MetodoProveedor.GetHdr(retorno.id);
                    proveedor.Id = int.Parse(row["PROVEEDOR_ID"].ToString());
                    proveedor.Nombre = row["PROVEEDOR"].ToString();
                    proveedor.Cuit = row["CUIT"].ToString();

                    if (proveedor != null)
                        retorno.Proveedor = proveedor;

                    
                }

                EstadosPedidos obj = new EstadosPedidos();
                List<EstadosPedidos> listaEstadosPedidosList = new List<EstadosPedidos>();
                obj.Nombre = row["NOMBRE_ESTADO"].ToString();
                if (retorno.WayBillId != "" && obj.Nombre != "")
                {

                    obj.Cantidad = int.Parse(row["CANTIDAD_ENTREGADOS"].ToString());
                    listaEstadosPedidosList.Add(obj);
                }
              

               
                retorno.EstadosPedidos = listaEstadosPedidosList;
                

                list.Add(retorno);
            }

            return list;
        }
    
        public List<GpsMovilesEnViaje> mapMovilesEnViaje(DataTable dt, List<GpsCanal> listaCanal)
        {
            List<GpsMovilesEnViaje> lista = new List<GpsMovilesEnViaje> ();

            foreach (DataRow row in dt.Rows)
            {
                GpsMovilesEnViaje item = new GpsMovilesEnViaje ();
                item.idCanal = int.Parse(row["ID_CANAL"].ToString());
                item.nombreCanal = listaCanal.FirstOrDefault(x => x.Id == item.idCanal).Nombre;
                item.movilesEnViaje = int.Parse(row["MOVILES_EN_VIAJE"].ToString());
                lista.Add(item);
            }

            return lista;
        }
    
        public List<GpsMovilesEnViajeDetalle> mapMovilesEnViajeDetalle(DataTable dt)
        {
            List<GpsMovilesEnViajeDetalle> lista = new List<GpsMovilesEnViajeDetalle> ();

            foreach(DataRow row in dt.Rows)
            {
                GpsMovilesEnViajeDetalle item = new GpsMovilesEnViajeDetalle ();
                item.idCanal = int.Parse(row["ID_CANAL"].ToString());
                item.idHojaruta = long.Parse(row["ID_HOJARUTA"].ToString());
                item.waybillid = row["WAYBILLID"].ToString();
                item.pedidosTotales = int.Parse(row["EN_VIAJE"].ToString());
                item.pedidosEntregados = int.Parse(row["ENTREGADOS"].ToString());
                item.pedidosNoEntregados = int.Parse(row["NO_ENTREGADOS"].ToString());
                //TRK3-1616 (177984) - 02/01/2024: Se descomentan la carga de los 3 líneas de códigos siguientes:
                item.pedidosRetirados = int.Parse(row["RETIRO"].ToString());
                item.pedidosNoRetirados = int.Parse(row["NO_RETIRO"].ToString());
                item.pedidosCancelados = int.Parse(row["CANCELADO"].ToString());
                //----------------------------------------------------------------------------------------------
                item.Movil = row["MOVIL"].ToString();

                lista.Add(item);
            }

            return lista;
        }
    
        public List<GpsMovilesEnViajeDetalle> mapPuntoDeEntrega(ref List<GpsMovilesEnViajeDetalle> listaViaje, DataTable dtPuntosDeEntregas)
        {
            foreach(DataRow row in dtPuntosDeEntregas.Rows)
            {
                if (listaViaje.Exists(x => x.idHojaruta == long.Parse(row["ID_HOJARUTA"].ToString())))
                {
                    listaViaje.Where(r => r.idHojaruta == long.Parse(row["ID_HOJARUTA"].ToString())).FirstOrDefault().puntosDeEntrega =
                        int.Parse(row["PUNTOSDEENTREGA"].ToString());
                    if (!string.IsNullOrEmpty(row["FECHA_CIERRE"].ToString()))
                    {
                        listaViaje.Where(r => r.idHojaruta == long.Parse(row["ID_HOJARUTA"].ToString())).FirstOrDefault().fechaCierre = 
                            DateTime.Parse(row["FECHA_CIERRE"].ToString());
                    }
                }
            }
            return listaViaje;
        }

        public Target mapTargets(DataTable dt)
        {

            if (dt.Rows.Count > 0)
            {
                Target retorno = new Target();
                foreach (DataRow row in dt.Rows)
                {
                    retorno.MovilPlateId = row["MovilPlateId"].ToString();
                    retorno.TargetId = long.Parse(row["TargetId"].ToString());
                    retorno.TrackerId = row["TrackerId"].ToString();
                }

                return retorno;
            }
            else
            {
                return null;
            }
        }

        public Target mapTarget(DataRow row)
        {
            Target retorno = new Target();

            retorno.MovilPlateId = row["MovilPlateId"].ToString();
            retorno.TargetId = long.Parse(row["TargetId"].ToString());
            retorno.TrackerId = row["TrackerId"].ToString();
            retorno.MovilTag = row["MovilTag"].ToString();
            retorno.GPSLatitude = double.Parse(row["GPSLatitude"].ToString());
            retorno.GPSLongitude = double.Parse(row["GPSLongitude"].ToString());
            retorno.MovilId = row["MovilId"].ToString();
            retorno.GPSLastPositionAT = DateTime.Parse(row["GPSLastPositionAT"].ToString());
            retorno.GPSSpeed = decimal.Parse(row["GPSSpeed"].ToString());
        
            return retorno;        
        }

        public List<GpsPosicion> mapPosiciones(DataTable dt)
        {
            List<GpsPosicion> retorno = new List<GpsPosicion>();
            foreach (DataRow row in dt.Rows)
            {
                retorno.Add(new GpsPosicion
                {
                    GpsLatitude = decimal.Parse(row["GPSLatitude"].ToString()),
                    GpsLongitude = decimal.Parse(row["GPSLongitude"].ToString()),
                    FechaPosicion = DateTime.Parse(row["Date"].ToString())
                });
            }

            return retorno;
        }

        public List<GpsHojaRuta> mapPanelHojaRutaCD(DataTable dt, ref string idHojaRutas)
        {
            List<GpsHojaRuta> list = new List<GpsHojaRuta>();
            foreach (DataRow row in dt.Rows)
            {
                GpsHojaRuta retorno = new GpsHojaRuta(row);
                idHojaRutas = idHojaRutas == "" ? retorno.Id.ToString() : idHojaRutas + ',' + retorno.Id;
                retorno.Canal = new GpsCanal(row);
                retorno.Estado = new GpsEstado(row);
                if (row["DISTANCIA_TOTAL"].ToString() != "")
                    retorno.DistanciaTotal = decimal.Parse(row["DISTANCIA_TOTAL"].ToString());
                //if (row["TIEMPO_ESTIMADO_TOTAL"].ToString() != "")
                //    retorno.TiempoEstimadoTotal = decimal.Parse(row["TIEMPO_ESTIMADO_TOTAL"].ToString());

                retorno.EMC = row["EMC"].ToString().Equals("0") ? false : true;
                retorno.cantidadPedidos = int.Parse(row["CANTIDADPEDIDOS"].ToString());
                if (!string.IsNullOrEmpty(row["PATENTE"].ToString()))
                {
                    retorno.HojaRutaMovil.Id = int.Parse(row["ID_HOJARUTAMOVIL"].ToString());
                    retorno.HojaRutaMovil.Patente = row["PATENTE"].ToString();
                    retorno.HojaRutaMovil.Capacidad = decimal.Parse(row["CAPACIDAD"].ToString());
                    retorno.HojaRutaMovil.Apilabilidad = decimal.Parse(row["APILABILIDAD"].ToString());
                    retorno.HojaRutaMovil.Proveedor = row["PROVEEDOR"].ToString();
                    retorno.HojaRutaMovil.Numero = int.Parse(row["NUMERO"].ToString());
                    var movil = new GpsMovil();
                    movil.Id = int.Parse(row["ID_MOVIL"].ToString());
                    movil.Numero = int.Parse(row["NUMERO"].ToString());
                    movil.Patente = row["PATENTE"].ToString();
                    movil.Id_Moviltipo = int.Parse(row["ID_MOVILTIPO"].ToString());
                    retorno.HojaRutaMovil.Movil = movil;
                    retorno.IdMovilTipo = movil.Id_Moviltipo;
                    var proveedor = new GpsProveedor();//MetodoProveedor.GetHdr(retorno.id);
                    proveedor.Id = int.Parse(row["PROVEEDOR_ID"].ToString());
                    proveedor.Nombre = row["PROVEEDOR"].ToString();
                    proveedor.Cuit = row["CUIT"].ToString();

                    if (proveedor != null)
                        retorno.Proveedor = proveedor;


                }

                if (!string.IsNullOrEmpty(row["ANDEN"].ToString()))
                {
                    GpsHojaRutaAnden anden = new GpsHojaRutaAnden();
                    anden.Anden = row["ANDEN"].ToString();
                }
                EstadosPedidos obj = new EstadosPedidos();
                List<EstadosPedidos> listaEstadosPedidosList = new List<EstadosPedidos>();
                obj.Nombre = row["NOMBRE_ESTADO"].ToString();
                if (retorno.WayBillId != "" && obj.Nombre != "")
                {

                    obj.Cantidad = int.Parse(row["CANTIDAD_ENTREGADOS"].ToString());
                    listaEstadosPedidosList.Add(obj);
                }



                retorno.EstadosPedidos = listaEstadosPedidosList;


                list.Add(retorno);
            }

            return list;
        }
    }
}
