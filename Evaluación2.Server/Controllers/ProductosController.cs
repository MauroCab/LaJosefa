using Evaluacion2.BD.Data;
using Evaluacion2.BD.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Evaluacion2.Server.Controllers
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

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> Get()
        {
            return await context.Productos.ToListAsync();
        }

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
            sel.PrecioUnidad = entidad.PrecioUnidad;
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
    }
}
