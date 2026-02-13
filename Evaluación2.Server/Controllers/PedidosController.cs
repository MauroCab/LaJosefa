
  using ProyectoModelado2024.BD.Data;
using ProyectoModelado2024.BD.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ProyectoModelado2024.Server.Repositorio;
using ProyectoModelado2024.Shared.DTO;

namespace ProyectoModelado2024.Server.Controllers
{
    [ApiController]
    [Route("api/Pedidos")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoRepositorio repositorio;
        private readonly IMapper mapper;

        public PedidosController(IPedidoRepositorio repositorio,
                                    IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        #region Get y GetById

        [HttpGet]
        public async Task<ActionResult<List<PedidoDTO>>> Get()
        {
            return await repositorio.FullGetAll();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PedidoDTO>> Get(int id)
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
                    ProductoId = r.ProductoId,
                    Cantidad = r.Cantidad
                }).ToList();

                
                var pedidoCreado = await repositorio.AddPedidoConRenglones(nuevoPedido, renglones);

                return pedidoCreado.Id;  
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el pedido: {ex.Message}");
            }
        }

        


    }
}

