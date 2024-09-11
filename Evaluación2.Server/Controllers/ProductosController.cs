using Evaluacion2.BD.Data;
using Microsoft.AspNetCore.Mvc;

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
    }
}
