using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoModelado2024.Shared.DTO
{
    public class CrearPedidoDTO
    {
        public List<RenglonDTO> Renglones { get; set; }
    }

    public class RenglonDTO
    {
        public int Cantidad { get; set; }

        public ProductoDTO Producto { get; set; }
    }

    public class ProductoDTO
    {
        public required int Id { get; set; }
        public required string Nombre { get; set; }
        public bool EsComun { get; set; }
    }

}
