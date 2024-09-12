using ProyectoModelado2024.BD.Data;
using ProyectoModelado2024.BD.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProyectoModelado2024.Server.Controllers
{
    [ApiController]
    [Route("api/Renglones")]
    public class RenglonesController : ControllerBase
    {
        private readonly Context context;

        public RenglonesController(Context context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Renglon>>> Get()
        {
            return await context.Renglones.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Renglon entidad)
        {
            try
            {
                context.Renglones.Add(entidad);
                await context.SaveChangesAsync();
                return entidad.Id;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Renglon entidad)
        {
            if (id != entidad.Id)
            {
                return BadRequest("Datos incorrectos");
            }
            var sel = await context.Renglones.Where(e => e.Id == id).FirstOrDefaultAsync();
            //sel = Seleccion

            if (sel == null)
            {
                return NotFound("No existe el tipo de documento buscado.");
            }

            sel.PedidoId = entidad.PedidoId;
            sel.Pedido = entidad.Pedido;
            sel.ProductoId = entidad.ProductoId;
            sel.Producto = entidad.Producto;
            sel.Cantidad = entidad.Cantidad;
            

            try
            {
                context.Renglones.Update(sel);
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
