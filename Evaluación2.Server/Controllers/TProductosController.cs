using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoModelado2024.BD.Data;
using ProyectoModelado2024.BD.Data.Entity;
using ProyectoModelado2024.Server.Repositorio;
using ProyectoModelado2024.Shared.DTO;

namespace ProyectoModelado2024.Server.Controllers
{
    [ApiController]
    [Route("api/TProductos")]
    public class TProductosController : ControllerBase
    {
        private readonly ITProductoRepositorio repositorio;
        private readonly IMapper mapper;

        public TProductosController(ITProductoRepositorio repositorio,
                                    IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<TProducto>>> Get()
        {
            return await repositorio.Select();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TProducto>> Get(int id)
        {
            TProducto? sel = await repositorio.SelectById(id);
            if (sel == null)
            {
                return NotFound();
            }
            return sel;
        }


        [HttpPost]
        public async Task<ActionResult<int>> Post(CrearTProductoDTO entidadDTO)
        {
            try
            {
                TProducto entidad = mapper.Map<TProducto>(entidadDTO);

                return await repositorio.Insert(entidad);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        
        

        
    }
}
