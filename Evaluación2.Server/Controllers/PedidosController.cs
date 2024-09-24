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

        #region Peticiones Get

        [HttpGet]
        public async Task<ActionResult<List<Pedido>>> Get()
        {
            return await repositorio.Select();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Pedido>> Get(int id)
        {
            Pedido? sel = await repositorio.SelectById(id);
            if (sel == null)
            {
                return NotFound();
            }
            return sel;
        }

        

        [HttpGet("existe/{id:int}")]
        public async Task<ActionResult<bool>> Existe(int id)
        {
            var existe = await repositorio.Existe(id);
            return existe;
        }

        #endregion

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CrearPedidoDTO entidadDTO)
        {
            if (entidadDTO == null || entidadDTO.Renglones == null || !entidadDTO.Renglones.Any())
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

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Pedido entidad)
        {
            if (id != entidad.Id)
            {
                return BadRequest("Datos incorrectos");
            }
            var sel = await repositorio.SelectById(id);
            //sel = Seleccion

            if (sel == null)
            {
                return NotFound("No existe el tipo de documento buscado.");
            }

            //sel.Codigo = entidad.Codigo;
            //sel.Nombre = entidad.Nombre;

            sel = mapper.Map<Pedido>(entidad); //pruebo a usar el mapper aqui

            try
            {
                await repositorio.Update(id, sel);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
            {
                return NotFound($"El tipo de producto {id} no existe");
            }
            Pedido EntidadABorrar = new Pedido();
            EntidadABorrar.Id = id;

            if (await repositorio.Delete(id))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
    }
}

