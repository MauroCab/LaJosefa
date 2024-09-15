using AutoMapper;
using ProyectoModelado2024.BD.Data.Entity;
using ProyectoModelado2024.Shared.DTO;

namespace ProyectoModelado2024.Server.Util
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CrearTProductoDTO, TProducto>();
        }

    }
}
