﻿// <auto-generated />
using System;
using ProyectoModelado2024.BD.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ProyectoModelado2024.BD.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20240911183129_codigoUQTProducto")]
    partial class codigoUQTProducto
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProyectoModelado2024.BD.Data.Entity.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("FechaHora")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("ProyectoModelado2024.BD.Data.Entity.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("PrecioUnidad")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<int>("TProductoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TProductoId");

                    b.HasIndex(new[] { "Nombre" }, "Producto_UQ")
                        .IsUnique();

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("ProyectoModelado2024.BD.Data.Entity.Renglon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("PedidoId")
                        .HasColumnType("int");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId");

                    b.HasIndex("ProductoId");

                    b.ToTable("Renglones");
                });

            modelBuilder.Entity("ProyectoModelado2024.BD.Data.Entity.TProducto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Codigo" }, "CodigoTProducto_UQ")
                        .IsUnique();

                    b.HasIndex(new[] { "Codigo", "Nombre" }, "TProducto_UQ")
                        .IsUnique();

                    b.ToTable("TProductos");
                });

            modelBuilder.Entity("ProyectoModelado2024.BD.Data.Entity.Producto", b =>
                {
                    b.HasOne("ProyectoModelado2024.BD.Data.Entity.TProducto", "TProducto")
                        .WithMany()
                        .HasForeignKey("TProductoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("TProducto");
                });

            modelBuilder.Entity("ProyectoModelado2024.BD.Data.Entity.Renglon", b =>
                {
                    b.HasOne("ProyectoModelado2024.BD.Data.Entity.Pedido", "Pedido")
                        .WithMany()
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProyectoModelado2024.BD.Data.Entity.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Pedido");

                    b.Navigation("Producto");
                });
#pragma warning restore 612, 618
        }
    }
}
