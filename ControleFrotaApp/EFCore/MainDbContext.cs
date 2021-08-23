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
            };

            mb.Entity<Marca>().HasData(marcasSeed);

            List<Combustível> combustíveeisSeed = new()
            {
                new(1, "Gasolina Comum"),
                new(2, "Gasolina Aditivada"),
                new(3, "Etanol Comum"),
                new(4, "Etanol Adivitado")
            };

            mb.Entity<Combustível>().HasData(combustíveeisSeed);

            base.OnModelCreating(mb);
        }
    }
}
