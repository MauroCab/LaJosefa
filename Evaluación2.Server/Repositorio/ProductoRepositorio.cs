using Microsoft.EntityFrameworkCore;
using ProyectoModelado2024.BD.Data;
using ProyectoModelado2024.BD.Data.Entity;

namespace ProyectoModelado2024.Server.Repositorio
{
    public class ProductoRepositorio : Repositorio<Producto>, IProductoRepositorio
    {
        private readonly Context context;

        public ProductoRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<Producto>> FullGetAll()
        {
            return await context.Productos
                .Include(p => p.TProducto)
                .ToListAsync();
        }
        public async Task<Producto> FullGetById(int id)
        {
            return await context.Productos
                .Include(p => p.TProducto)
                .FirstOrDefaultAsync(p => p.Id == id);
        }


    }
}
