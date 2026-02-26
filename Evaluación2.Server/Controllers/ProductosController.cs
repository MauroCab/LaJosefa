using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProyectoModelado2024.BD.Data.Entity;
using ProyectoModelado2024.Server.Repositorio;
using ProyectoModelado2024.Shared.DTO;

namespace ProyectoModelado2024.Server.Controllers
{
    [ApiController]
    [Route("api/Productos")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoRepositorio repositorio;
        private readonly IMapper mapper;

        public ProductosController(IProductoRepositorio repositorio,
                                    IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        #region Get y GetById

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> Get()
        {
            return await repositorio.FullGetAll();
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Producto>> GetById(int id)
        {
            var producto = await repositorio.FullGetById(id);
            if (producto == null) return NotFound();

            return Ok(producto);
        }

        #endregion

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CrearProductoDTO entidadDTO)
        {
            try
            {
                Producto entidad = mapper.Map<Producto>(entidadDTO);
                await repositorio.Insert(entidad);
                return entidad.Id;
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
            Producto EntidadABorrar = new Producto();
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
