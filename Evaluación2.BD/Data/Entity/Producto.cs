using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoModelado2024.BD.Data.Entity
{
    [Index(nameof(Nombre), Name = "Producto_UQ", IsUnique = true)]
    public class Producto : EntityBase
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100, ErrorMessage = "El nombre debe tener mas de 100 caracteres)")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El stock es obligatorio")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "El tipo de producto es obligatorio")]
        public int TProductoId { get; set; }
        public TProducto TProducto { get; set; } 

    }
}
