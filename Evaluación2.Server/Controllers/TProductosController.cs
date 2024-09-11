using Evaluacion2.BD.Data;
using Evaluacion2.BD.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Evaluacion2.Server.Controllers
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
    }
}
