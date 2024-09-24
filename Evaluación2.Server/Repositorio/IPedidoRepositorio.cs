using ProyectoModelado2024.BD.Data.Entity;

namespace ProyectoModelado2024.Server.Repositorio
{
    public interface IPedidoRepositorio: IRepositorio<Pedido>
    {
        Task<Pedido> AddPedidoConRenglones(Pedido pedido, List<Renglon> renglones);
        Task<List<Pedido>> FullGetAll();
        Task<Pedido> FullGetById(int id);
    }
}