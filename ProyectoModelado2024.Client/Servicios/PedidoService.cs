using ProyectoModelado2024.BD.Data.Entity;
using ProyectoModelado2024.Client.Models;
using ProyectoModelado2024.Shared.DTO;

namespace ProyectoModelado2024.Client.Servicios
{
    public class PedidoService
    {
        public PedidoActual Pedido { get; private set; } = new();

        public event Action? OnCambio;

        public void AgregarAlPedido(ProductoDTO producto, int cantidad)
        {
            Pedido.AgregarProducto(producto, cantidad);
            OnCambio?.Invoke();
        }

        public void RemoverDelPedido(int productoId)
        {
            Pedido.RemoverProducto(productoId);
            OnCambio?.Invoke();
        }

        public void LimpiarPedido()
        {
            Pedido.Renglones.Clear();
            OnCambio?.Invoke();
        }
    }
}

