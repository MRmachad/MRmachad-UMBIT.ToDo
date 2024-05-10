﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UMBIT.ToDo.Infraestrutura.Contextos;

#nullable disable

namespace UMBIT.ToDo.API.Migrations
{
    [DbContext(typeof(AppContexto))]
    [Migration("20240510020857_other-props")]
    partial class otherprops
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("UMBIT.ToDo.Dominio.Entidades.ToDoItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DataAtualizacao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("IdToDoList")
                        .HasColumnType("char(36)");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdToDoList");

                    b.ToTable("ToDoItem");
                });

            modelBuilder.Entity("UMBIT.ToDo.Dominio.Entidades.ToDoList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DataAtualizacao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("ToDoList");
                });

            modelBuilder.Entity("UMBIT.ToDo.Dominio.Entidades.ToDoItem", b =>
                {
                    b.HasOne("UMBIT.ToDo.Dominio.Entidades.ToDoList", "ToDoList")
                        .WithMany("ToDoItems")
                        .HasForeignKey("IdToDoList")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("ToDoList");
                });

            modelBuilder.Entity("UMBIT.ToDo.Dominio.Entidades.ToDoList", b =>
                {
                    b.Navigation("ToDoItems");
                });
#pragma warning restore 612, 618
        }
    }
}
