using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoModelado2024.BD.Data;
using ProyectoModelado2024.BD.Data.Entity;
using ProyectoModelado2024.Server.Repositorio;
using ProyectoModelado2024.Shared.DTO;

namespace ProyectoModelado2024.Server.Controllers
{
    [ApiController]
    [Route("api/TProductos")]
    public class TProductosController : ControllerBase
    {
        private readonly ITProductoRepositorio repositorio;
        private readonly IMapper mapper;

        public TProductosController(ITProductoRepositorio repositorio,
                                    IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        #region Peticiones Get

        [HttpGet]
        public async Task<ActionResult<List<TProducto>>> Get()
        {
            return await repositorio.Select();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TProducto>> Get(int id)
        {
            TProducto? sel = await repositorio.SelectById(id);
            if (sel == null)
            {
                return NotFound();
            }
            return sel;
        }

        [HttpGet("GetByCod/{cod}")] //api/TProductos/PAN
        public async Task<ActionResult<TProducto>> GetByCod(string cod)
        {
            TProducto? sel = await repositorio.SelectByCod(cod);
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
        public async Task<ActionResult<int>> Post(CrearTProductoDTO entidadDTO)
        {
            try
            {
                TProducto entidad = mapper.Map<TProducto>(entidadDTO);

                return await repositorio.Insert(entidad);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] TProducto entidad)
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

            sel = mapper.Map<TProducto>(entidad); //pruebo a usar el mapper aqui

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
            if(!existe)
            {
                return NotFound($"El tipo de producto {id} no existe");
            }
            TProducto EntidadABorrar = new TProducto();
            EntidadABorrar.Id = id;

            if(await repositorio.Delete(id))
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
