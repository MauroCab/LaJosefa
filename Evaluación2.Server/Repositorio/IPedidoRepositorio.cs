using ProyectoModelado2024.BD.Data.Entity;
using ProyectoModelado2024.Shared.DTO;

namespace ProyectoModelado2024.Server.Repositorio
{
    public interface IPedidoRepositorio : IRepositorio<Pedido>
    {
        Task<Pedido> AddPedidoConRenglones(Pedido pedido, List<Renglon> renglones);
        Task<List<PedidoDTO>> FullGetAll();
        Task<PedidoDTO> FullGetById(int id);
    }
}