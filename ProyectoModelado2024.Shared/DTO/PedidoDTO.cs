using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoModelado2024.Shared.DTO
{
    public class PedidoDTO
    {
        public DateTime Fecha { get; set; }
        public List<RenglonDTO> Renglones { get; set; }
    }

    public class RenglonDTO
    {
        public int Cantidad { get; set; }
        public string ProductoNombre { get; set; }
    }
}
