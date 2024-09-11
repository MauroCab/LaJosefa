using Evaluacion2.BD.Data;
using Microsoft.AspNetCore.Mvc;

namespace Evaluacion2.Server.Controllers
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
    }
}
