using ProyectoModelado2024.Shared.DTO;

namespace ProyectoModelado2024.Server.Servicios
{
    public interface IPdfService
    {
        Task<byte[]> GenerarPdfPedidoAsync(CrearPedidoDTO pedido);
    }
}