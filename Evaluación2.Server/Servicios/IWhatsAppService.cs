
using ProyectoModelado2024.Shared.DTO;

namespace ProyectoModelado2024.Server.Servicios
{
    public interface IWhatsAppService
    {
        Task<bool> EnviarPedidoAsync(CrearPedidoDTO pedidoDTO);

        Task<bool> EnviarMensajeTextoAsync(string numero, string mensaje);
    }
}