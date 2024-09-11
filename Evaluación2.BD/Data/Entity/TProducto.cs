using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluacion2.BD.Data.Entity
{
    [Index(nameof(Codigo), nameof(Nombre), Name = "TProducto_UQ", IsUnique = true)]
    [Index(nameof(Codigo), Name = "CodigoTProducto_UQ", IsUnique = true)]
    public class TProducto : EntityBase
    {
        [Required(ErrorMessage = "El codigo es obligatorio")]
        [MaxLength(3, ErrorMessage = "El codigo no debe tener mas de 3 caracteres)")]

        public string Codigo { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100, ErrorMessage = "El nombre debe tener mas de 100 caracteres)")]
        public string Nombre { get; set; }
    }
}
