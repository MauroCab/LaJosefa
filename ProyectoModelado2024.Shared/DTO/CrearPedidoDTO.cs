using ProyectoModelado2024.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoModelado2024.Shared.DTO
{
    public class CrearPedidoDTO
    {
        public List<CrearRenglonDTO> Renglones { get; set; }
    }
}
