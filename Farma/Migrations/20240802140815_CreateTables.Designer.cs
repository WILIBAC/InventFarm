﻿// <auto-generated />
using System;
using Farma.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Farma.Migrations
{
    [DbContext(typeof(FarmaciaDbContext))]
    [Migration("20240802140815_CreateTables")]
    partial class CreateTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Farma.Models.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("Farma.Models.FormaFarmaceutica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FormasFarmaceuticas");
                });

            modelBuilder.Entity("Farma.Models.Medicamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaVencimiento")
                        .HasColumnType("datetime2");

                    b.Property<int>("FormaFarmaceuticaId")
                        .HasColumnType("int");

                    b.Property<string>("Lote")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Producto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("FormaFarmaceuticaId");

                    b.ToTable("Medicamentos");
                });

            modelBuilder.Entity("Farma.Models.Medicamento", b =>
                {
                    b.HasOne("Farma.Models.Categoria", "Categoria")
                        .WithMany("Medicamentos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Farma.Models.FormaFarmaceutica", "FormaFarmaceutica")
                        .WithMany("Medicamentos")
                        .HasForeignKey("FormaFarmaceuticaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("FormaFarmaceutica");
                });

            modelBuilder.Entity("Farma.Models.Categoria", b =>
                {
                    b.Navigation("Medicamentos");
                });

            modelBuilder.Entity("Farma.Models.FormaFarmaceutica", b =>
                {
                    b.Navigation("Medicamentos");
                });
#pragma warning restore 612, 618
        }
    }
}