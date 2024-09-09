using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluacion2.BD.Data.Entity
{
    public class Renglon : EntityBase
    {
        [Required(ErrorMessage = "El id de pedido es obligatorio")]
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }

        [Required(ErrorMessage = "El id de producto es obligatorio")]
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        public int Cantidad { get; set; }
    }
}
