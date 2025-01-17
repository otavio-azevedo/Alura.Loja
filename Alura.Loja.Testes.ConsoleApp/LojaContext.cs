﻿using Microsoft.EntityFrameworkCore;
using System;

namespace Alura.Loja.Testes.ConsoleApp
{
    internal class LojaContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Promocao> Promocoes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public LojaContext()
        { }

        public LojaContext(DbContextOptions<LojaContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Necessário para criação da "tabela de join" do relacionamento N:N
            modelBuilder
                .Entity<PromocaoProduto>()
                .HasKey(pp => new { pp.PromocaoId, pp.ProdutoId });


            modelBuilder
                .Entity<Endereco>()
                .ToTable("Enderecos");//Define nome da tabela explicitamente, caso contrário pega o que estiver na propriedade
            modelBuilder
                .Entity<Endereco>()
                .Property<int>("ClienteId"); //Shadow proprety/state, propriedade que não existe na classe mas existe no BD
            modelBuilder
                .Entity<Endereco>()
                .HasKey("ClienteId");

            modelBuilder.Entity<Compra>()
                .Property(i => i.Preco)
                .HasColumnType("decimal");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=LojaDB;Trusted_Connection=true;");
            }
        }
    }
}