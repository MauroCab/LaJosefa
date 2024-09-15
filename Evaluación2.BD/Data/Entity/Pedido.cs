using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoModelado2024.BD.Data.Entity
{
    public class Pedido : EntityBase
    {
        [Required(ErrorMessage = "La fecha y la hora son obligatorias")]
        public DateTime FechaHora { get; set; }

    }
}
