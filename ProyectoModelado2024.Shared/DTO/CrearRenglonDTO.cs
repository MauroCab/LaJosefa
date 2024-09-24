using ProyectoModelado2024.BD.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoModelado2024.Shared.DTO
{
    public class CrearRenglonDTO
    {
        [Required(ErrorMessage = "El id del producto a pedir es obligatorio")]
        public int ProductoId { get; set; }
        

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        public int Cantidad { get; set; }
    }
}
