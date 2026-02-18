using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoModelado2024.Shared.DTO
{
    public class GetPedidoDTO
    {
        public DateTime Fecha { get; set; }
        public List<GetRenglonDTO> Renglones { get; set; }
    }

    public class GetRenglonDTO
    {
        public int Cantidad { get; set; }
        public string ProductoNombre { get; set; }
    }
}
