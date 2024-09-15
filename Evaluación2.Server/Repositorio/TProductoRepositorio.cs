using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoModelado2024.BD.Data;
using ProyectoModelado2024.BD.Data.Entity;

namespace ProyectoModelado2024.Server.Repositorio
{
    public class TProductoRepositorio : Repositorio<TProducto>, ITProductoRepositorio
    {
        private readonly Context context;
        public TProductoRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<TProducto> SelectByCod(string cod)
        {
            TProducto? sel = await context.TProductos.FirstOrDefaultAsync(x => x.Codigo == cod);
            return sel;
        }


    }
}
