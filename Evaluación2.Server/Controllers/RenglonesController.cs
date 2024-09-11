using Evaluacion2.BD.Data;
using Microsoft.AspNetCore.Mvc;

namespace Evaluacion2.Server.Controllers
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
    }
}
