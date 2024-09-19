using ProyectoModelado2024.BD.Data;
using ProyectoModelado2024.BD.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProyectoModelado2024.Server.Controllers
{
    [ApiController]
    [Route("api/Productos")]
    public class ProductosController : ControllerBase
    {
        private readonly Context context;

        public ProductosController(Context context)
        {
            this.context = context;
        }

        #region Peticiones Get

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> Get()
        {
            return await context.Productos.ToListAsync();
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Producto>> Get(int id)
        {
            Producto? sel = await context.Productos.FirstOrDefaultAsync(x => x.Id == id);
            if (sel == null)
            {
                return NotFound();
            }
            return sel;
        }

        [HttpGet("existe/{id:int}")]
        public async Task<ActionResult<bool>> Existe(int id)
        {
            var existe = await context.Productos.AnyAsync(x => x.Id == id);
            return existe;

        }

        #endregion

        [HttpPost]
        public async Task<ActionResult<int>> Post(Producto entidad)
        {
            try
            {
                context.Productos.Add(entidad);
                await context.SaveChangesAsync();
                return entidad.Id;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Producto entidad)
        {
            if (id != entidad.Id)
            {
                return BadRequest("Datos incorrectos");
            }
            var sel = await context.Productos.Where(e => e.Id == id).FirstOrDefaultAsync();
            //sel = Seleccion

            if (sel == null)
            {
                return NotFound("No existe el tipo de documento buscado.");
            }

            sel.Nombre = entidad.Nombre;
            sel.Stock = entidad.Stock;
            sel.TProductoId = entidad.TProductoId;
            sel.TProducto = entidad.TProducto;

            try
            {
                context.Productos.Update(sel);
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
            var existe = await context.Productos.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound($"El producto {id} no existe");
            }
            Producto EntidadABorrar = new Producto();
            EntidadABorrar.Id = id;

            context.Remove(EntidadABorrar);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
