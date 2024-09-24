using Microsoft.EntityFrameworkCore;
using ProyectoModelado2024.BD.Data;
using ProyectoModelado2024.BD.Data.Entity;

namespace ProyectoModelado2024.Server.Repositorio
{
    public class PedidoRepositorio : Repositorio<Pedido>, IPedidoRepositorio
    {


        private readonly Context context;

        public PedidoRepositorio(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<Pedido>> FullGetAll()
        {
            return await context.Pedidos
                                .ToListAsync();
        }
        public async Task<Pedido> FullGetById(int id)
        {
            return await context.Pedidos
                         .FirstOrDefaultAsync(p => p.Id == id);
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