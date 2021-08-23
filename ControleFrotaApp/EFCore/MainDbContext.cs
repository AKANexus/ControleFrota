using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using ControleFrota.Domain;
using Microsoft.EntityFrameworkCore;

namespace ControleFrota.EFCore
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions options) : base(options)
        {

        }


        public MainDbContext()
        {
        }

        public DbSet<Veículo> Veículos { get; set; }
        public DbSet<Abastecimento> Abastecimentos { get; set; }
        public DbSet<Manutenção> Manutençãos { get; set; }
        public DbSet<ManutençãoProgramada> ManutençãoProgramadas { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Modelo> Modelos { get; set; }
        public DbSet<Motorista> Motoristas { get; set; }
        public DbSet<Viagem> Viagems { get; set; }
        public DbSet<TipoGasto> TipoGastos { get; set; }
        public DbSet<Gasto> Gastos { get; set; }
        public DbSet<Combustível> Combustívels { get; set; }
        public DbSet<Setor> Setors { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder mb)
        {
            List<Marca> marcasSeed = new()
            {
                new(1, "Fiat"),
                new(2, "Ford"),
                new(3, "Jeep"),
                new(4, "Renault"),
                new(5, "Volkswagen"),
                new(6, "Yamaha"),
                new(7, "Honda"),
                new(8, "Suzuki")
            };

            mb.Entity<Marca>().HasData(marcasSeed);

            List<Combustível> combustíveeisSeed = new()
            {
                new(1, "Gasolina Comum"),
                new(2, "Gasolina Aditivada"),
                new(3, "Etanol Comum"),
                new(4, "Etanol Aditivado")
            };

            mb.Entity<Combustível>().HasData(combustíveeisSeed);

            List<Setor> setorsSeed = new()
            {
                new(1, "Infraestrutura"),
                new(2, "Suporte Técnico"),
                new(3, "Vendas"),
                new(4, "Administração")
            };

            mb.Entity<Setor>().HasData(setorsSeed);

            List<TipoGasto> gastosSeed = new()
            {
                new(1, "Pedágio"),
                new(2, "Hospedagem"),
                new(3, "Alimentação"),
                new(4, "Abastecimento*")
            };

            mb.Entity<TipoGasto>().HasData(gastosSeed);

            base.OnModelCreating(mb);
        }
    }
}
