using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProyectoModelado2024.BD.Data.Entity;
using ProyectoModelado2024.Server.Repositorio;
using ProyectoModelado2024.Server.Servicios;
using ProyectoModelado2024.Shared.DTO;

namespace ProyectoModelado2024.Server.Controllers
{
    [ApiController]
    [Route("api/Pedidos")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoRepositorio repositorio;
        private readonly IMapper mapper;
        private readonly IWhatsAppService whatsAppService;
        public PedidosController(IPedidoRepositorio repositorio,
                                    IMapper mapper,
                                    IWhatsAppService IWAS)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
            this.whatsAppService = IWAS;
        }

        #region Get y GetById

        [HttpGet]
        public async Task<ActionResult<List<GetPedidoDTO>>> Get()
        {
            return await repositorio.FullGetAll();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetPedidoDTO>> Get(int id)
        {
            var pedido = await repositorio.FullGetById(id);

            if (pedido == null)
                return NotFound();

            return pedido;
        }

        #endregion

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CrearPedidoDTO entidadDTO)
        {
            bool pedidoEstaVacio = entidadDTO == null || entidadDTO.Renglones == null || !entidadDTO.Renglones.Any();

            if (pedidoEstaVacio)
            {
                return BadRequest("El pedido debe contener al menos un renglón.");
            }

            try
            {

                var nuevoPedido = new Pedido
                {
                    FechaHora = DateTime.Now
                };


                var renglones = entidadDTO.Renglones.Select(r => new Renglon
                {
                    Cantidad = r.Cantidad,
                    ProductoId = r.Producto.Id
                }).ToList();


                var pedidoCreado = await repositorio.AddPedidoConRenglones(nuevoPedido, renglones);

                if (pedidoCreado != null)
                {
                    try
                    {
                        // Usar el método que acepta CrearPedidoDTO
                        var resultado = await whatsAppService.EnviarPedidoAsync(entidadDTO);

                        if (resultado)
                        {
                            return Ok(new
                            {
                                pedidoId = pedidoCreado.Id,  // ← DEVOLVER EL ID
                                mensaje = "Pedido creado y enviado correctamente por WhatsApp"
                            });
                        }
                        else
                        {
                            // El pedido se creó pero no se pudo enviar el WhatsApp
                            return Ok(new
                            {
                                pedidoId = pedidoCreado.Id,
                                mensaje = "Pedido creado pero NO se pudo enviar por WhatsApp",
                                warning = "Revisa la configuración de Twilio"
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        // El pedido se creó pero falló el envío de WhatsApp
                        return Ok(new
                        {
                            pedidoId = pedidoCreado.Id,
                            mensaje = "Pedido creado pero falló el envío por WhatsApp",
                            error = ex.Message
                        });
                    }
                }
                return pedidoCreado.Id;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el pedido: {ex.Message}");
            }
        }




    }
}

