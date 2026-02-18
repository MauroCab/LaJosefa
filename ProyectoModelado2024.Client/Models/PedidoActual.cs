using ProyectoModelado2024.BD.Data.Entity;
using ProyectoModelado2024.Shared.DTO;

namespace ProyectoModelado2024.Client.Models
{
    public class PedidoActual
    {
        public List<RenglonDTO> Renglones { get; set; } = new();

        public void AgregarProducto(ProductoDTO producto, int cantidad)
        {
            var renglonExistente = Renglones.FirstOrDefault(r => r.Producto.Id == producto.Id); 

            if (renglonExistente != null)
            {
                renglonExistente.Cantidad += cantidad;
            }
            else
            {
                Renglones.Add(new RenglonDTO
                {
                    Producto = producto,
                    Cantidad = cantidad
                });
            }
        }

        public void RemoverProducto(int productoId)
        {
            var renglon = Renglones.FirstOrDefault(r => r.Producto.Id == productoId);
            if (renglon != null)
                Renglones.Remove(renglon);
        }
    }
}
