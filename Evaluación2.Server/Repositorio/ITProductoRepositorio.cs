using ProyectoModelado2024.BD.Data.Entity;

namespace ProyectoModelado2024.Server.Repositorio
{
    public interface ITProductoRepositorio: IRepositorio<TProducto>
    {
        Task<TProducto> SelectByCod(string cod);
    }
}