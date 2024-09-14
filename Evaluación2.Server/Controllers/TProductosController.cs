using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoModelado2024.BD.Data;
using ProyectoModelado2024.BD.Data.Entity;
using ProyectoModelado2024.Shared.DTO;

namespace ProyectoModelado2024.Server.Controllers
{
    [ApiController]
    [Route("api/TProductos")]
    public class TProductosController : ControllerBase
    {
        private readonly Context context;

        public TProductosController(Context context)
        {
            this.context = context;
        }

        #region Peticiones Get

        [HttpGet]
        public async Task<ActionResult<List<TProducto>>> Get()
        {
            return await context.TProductos.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TProducto>> Get(int id)
        {
            TProducto? sel = await context.TProductos.FirstOrDefaultAsync(x => x.Id == id);
            if (sel == null)
            {
                return NotFound();
            }
            return sel;
        }

        [HttpGet("GetByCod/{cod}")] //api/TProductos/PAN
        public async Task<ActionResult<TProducto>> GetByCod(string cod)
        {
            TProducto? sel = await context.TProductos.FirstOrDefaultAsync(x => x.Codigo == cod);
            if (sel == null)
            {
                return NotFound();
            }
            return sel;
        }

        [HttpGet("existe/{id:int}")]
        public async Task<ActionResult<bool>> Existe(int id)
        {
            var existe = await context.TProductos.AnyAsync(x => x.Id == id);
            return existe;
            
        }

        #endregion

        [HttpPost]
        public async Task<ActionResult<int>> Post(CrearTProductoDTO entidadDTO)
        {
            try
            {
                TProducto entidad = new TProducto();
                entidad.Codigo = entidadDTO.Codigo;
                entidad.Nombre = entidadDTO.Nombre;

                context.TProductos.Add(entidad);
                await context.SaveChangesAsync();
                return entidad.Id;
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
            var sel = await context.TProductos.Where(e => e.Id == id).FirstOrDefaultAsync();
            //sel = Seleccion

            if (sel == null)
            {
                return NotFound("No existe el tipo de documento buscado.");
            }

            sel.Codigo = entidad.Codigo;
            sel.Nombre = entidad.Nombre;

            try
            {
                context.TProductos.Update(sel);
                await context.SaveChangesAsync();
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
            var existe = await context.TProductos.AnyAsync(x => x.Id == id);
            if(!existe)
            {
                return NotFound($"El tipo de producto {id} no existe");
            }
            TProducto EntidadABorrar = new TProducto();
            EntidadABorrar.Id = id;

            context.Remove(EntidadABorrar);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
