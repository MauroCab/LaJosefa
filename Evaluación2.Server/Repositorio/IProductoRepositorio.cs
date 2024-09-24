using ProyectoModelado2024.BD.Data.Entity;

namespace ProyectoModelado2024.Server.Repositorio
{
    public interface IProductoRepositorio: IRepositorio<Producto>
    {
        Task<List<Producto>> FullGetAll();
        Task<Producto> FullGetById(int id);
    }
}