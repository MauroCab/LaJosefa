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

        #region Peticiones Get

        [HttpGet]
        public async Task<ActionResult<List<Renglon>>> Get()
        {
            return await context.Renglones.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Renglon>> Get(int id)
        {
            Renglon? sel = await context.Renglones.FirstOrDefaultAsync(x => x.Id == id);
            if (sel == null)
            {
                return NotFound();
            }
            return sel;
        }

        [HttpGet("existe/{id:int}")]
        public async Task<ActionResult<bool>> Existe(int id)
        {
            var existe = await context.Renglones.AnyAsync(x => x.Id == id);
            return existe;

        }

        #endregion

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

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Renglones.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound($"El tipo de producto {id} no existe");
            }
            Renglon EntidadABorrar = new Renglon();
            EntidadABorrar.Id = id;

            context.Remove(EntidadABorrar);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
