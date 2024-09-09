using Evaluacion2.BD.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluacion2.BD.Data
{
    public class Context : DbContext
    {
        public DbSet<TProducto> TProductos { get; set; }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<Pedido> Pedidos { get; set; }

        public DbSet<Renglon> Renglones { get; set; }

        public Context(DbContextOptions options) : base(options)
        {

        }
    }
}
