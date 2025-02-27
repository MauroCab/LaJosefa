using Microsoft.EntityFrameworkCore;
using ProyectoModelado2024.BD.Data;
using ProyectoModelado2024.BD.Data.Entity;
using ProyectoModelado2024.Shared.DTO;

namespace ProyectoModelado2024.Server.Repositorio
{
    public class PedidoRepositorio : Repositorio<Pedido>, IPedidoRepositorio
    {
        private readonly Context context;

        public PedidoRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<PedidoDTO>> FullGetAll()
        {
            var pedidos = await context.Pedidos
                                .Include(p => p.Renglones)
                                .ThenInclude(r => r.Producto)
                                .ToListAsync();

            return pedidos.Select(p => new PedidoDTO
            {
                Fecha = p.FechaHora,
                Renglones = p.Renglones.Select(r => new RenglonDTO
                {
                    Cantidad = r.Cantidad,
                    ProductoNombre = r.Producto.Nombre // Aquí se obtiene el nombre del producto
                }).ToList()
            }).ToList();
        }

        public async Task<PedidoDTO> FullGetById(int id)
        {
            var pedido = await context.Pedidos
                           .Include(p => p.Renglones)
                           .ThenInclude(r => r.Producto)
                           .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
                return null;

            return new PedidoDTO
            {
                Fecha = pedido.FechaHora,
                Renglones = pedido.Renglones.Select(r => new RenglonDTO
                {
                    Cantidad = r.Cantidad,
                    ProductoNombre = r.Producto.Nombre
                }).ToList()
            };
        }


        public async Task<Pedido> AddPedidoConRenglones(Pedido pedido, List<Renglon> renglones)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {

                    context.Pedidos.Add(pedido);
                    await context.SaveChangesAsync();


                    foreach (var renglon in renglones)
                    {
                        renglon.PedidoId = pedido.Id;
                        context.Renglones.Add(renglon);
                    }

                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return pedido;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }

        }
    }
}