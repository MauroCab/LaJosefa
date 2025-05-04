using ProyectoModelado2024.BD.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoModelado2024.BD.Data
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var cascadeFKs = modelBuilder.Model.G­etEntityTypes()
                                          .SelectMany(t => t.GetForeignKeys())
                                          .Where(fk => !fk.IsOwnership
                                                       && fk.DeleteBehavior == DeleteBehavior.Casca­de);
            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restr­ict;
            }
    
            base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<Renglon>()
               .HasOne(r => r.Pedido)
               .WithMany(p => p.Renglones)
               .HasForeignKey(r => r.PedidoId);

                 modelBuilder.Entity<Renglon>()
                .HasOne(r => r.Producto)
                .WithMany() // Si Producto no tiene una lista de Renglones
                .HasForeignKey(r => r.ProductoId);
        }
    }
}
