using ProyectoModelado2024.BD.Data;
using ProyectoModelado2024.BD.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet] 
        public async Task<ActionResult<List<TProducto>>> Get()
        {
            return await context.TProductos.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(TProducto entidad)
        {
            try
            {
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
    }
}
