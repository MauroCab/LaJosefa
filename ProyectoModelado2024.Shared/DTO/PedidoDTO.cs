using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoModelado2024.Shared.DTO
{
    public class PedidoDTO
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
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool EsComun { get; set; }
    }

}
