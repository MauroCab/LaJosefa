using ProyectoModelado2024.BD.Data;
using ProyectoModelado2024.BD.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProyectoModelado2024.Server.Controllers
{
    [ApiController]
    [Route("api/Pedidos")]
    public class PedidosController : ControllerBase
    {
        private readonly Context context;

        public PedidosController(Context context)
        {
            this.context = context;
        }

        #region Peticiones Get

        [HttpGet] 
        public async Task<ActionResult<List<Pedido>>> Get()
        {
            return await context.Pedidos.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Pedido>> Get(int id)
        {
            Pedido? sel = await context.Pedidos.FirstOrDefaultAsync(x => x.Id == id);
            if (sel == null)
            {
                return NotFound();
            }
            return sel;
        }

        [HttpGet("existe/{id:int}")]
        public async Task<ActionResult<bool>> Existe(int id)
        {
            var existe = await context.Pedidos.AnyAsync(x => x.Id == id);
            return existe;

        }

        #endregion

        [HttpPost]
        public async Task<ActionResult<int>> Post(Pedido entidad)
        {
            try
            {
                context.Pedidos.Add(entidad);
                await context.SaveChangesAsync();
                return entidad.Id;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Pedido entidad)
        {
            if (id != entidad.Id)
            {
                return BadRequest("Datos incorrectos");
            }
            var sel = await context.Pedidos.Where(e => e.Id == id).FirstOrDefaultAsync();
            //sel = Seleccion

            if (sel == null)
            {
                return NotFound("No existe el tipo de documento buscado.");
            }

            sel.FechaHora = entidad.FechaHora;
            

            try
            {
                context.Pedidos.Update(sel);
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
            var existe = await context.Pedidos.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound($"El tipo de producto {id} no existe");
            }
            Pedido EntidadABorrar = new Pedido();
            EntidadABorrar.Id = id;

            context.Remove(EntidadABorrar);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
