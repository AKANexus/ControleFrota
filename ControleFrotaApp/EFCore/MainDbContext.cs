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
                new(3, "Alimentação")
                //new(4, "Abastecimento*")
            };

            mb.Entity<TipoGasto>().HasData(gastosSeed);

            List<ManutençãoProgramada> manutençãoProgramadasSeed = new()
            {
                new(1, 50000, 0, ÁreaManutenção.PneuPneus, TipoManutenção.Troca, TipoVeículo.Carro),
                new(2, 10000, 0, ÁreaManutenção.PneuPneus, TipoManutenção.Revisão, TipoVeículo.Carro),
                new(3, 10000, 0, ÁreaManutenção.PneuAlinhamento, TipoManutenção.Revisão, TipoVeículo.Carro),
                new(4, 10000, 0, ÁreaManutenção.PneuRodízio, TipoManutenção.Revisão, TipoVeículo.Carro),
                new(5, 0, 7*365, ÁreaManutenção.PneusEstepe, TipoManutenção.Troca, TipoVeículo.Carro),
                new(7, 10000, 0, ÁreaManutenção.SuspensãoSuspensão, TipoManutenção.Revisão, TipoVeículo.Carro),
                new(6, 100000, 0, ÁreaManutenção.SuspensãoSuspensão, TipoManutenção.Troca, TipoVeículo.Carro),
                new(8, 40000, 0, ÁreaManutenção.MotorCorreias, TipoManutenção.Troca, TipoVeículo.Carro),
                new(9, 5000, 0, ÁreaManutenção.MotorCorreias, TipoManutenção.Revisão, TipoVeículo.Carro),
                new(10, 5000, 0, ÁreaManutenção.MotorMangueiras, TipoManutenção.Revisão, TipoVeículo.Carro),
                new(11, 100000, 0, ÁreaManutenção.MotorVelas, TipoManutenção.Troca, TipoVeículo.Carro),
                new(12, 10000, 0, ÁreaManutenção.MotorVelas, TipoManutenção.Revisão, TipoVeículo.Carro),
                new(13, 10000, 0, ÁreaManutenção.MotorInjeção, TipoManutenção.Revisão, TipoVeículo.Carro),
                new(14, 0, 2*365, ÁreaManutenção.MotorRadiador, TipoManutenção.Troca, TipoVeículo.Carro),
                new(15, 5000, 0, ÁreaManutenção.MotorRadiador, TipoManutenção.Revisão, TipoVeículo.Carro),
                new(16, 50000, 0, ÁreaManutenção.FreioPastilha, TipoManutenção.Troca, TipoVeículo.Carro),
                new(17, 10000, 0, ÁreaManutenção.FreioPastilha, TipoManutenção.Revisão, TipoVeículo.Carro),
                new(18, 10000, 0, ÁreaManutenção.FreioDisco, TipoManutenção.Revisão, TipoVeículo.Carro),
                new(19, 0, 2*365, ÁreaManutenção.BateriaBateria, TipoManutenção.Troca, TipoVeículo.Carro),
                new(20, 80000, 0, ÁreaManutenção.FluidoTransmissão, TipoManutenção.Troca, TipoVeículo.Carro),
                new(21, 0, 365, ÁreaManutenção.FluidoFreio, TipoManutenção.Troca, TipoVeículo.Carro),
                new(22, 10000, 0, ÁreaManutenção.FluidoFreio, TipoManutenção.Revisão, TipoVeículo.Carro),
                new(23, 0, 30, ÁreaManutenção.FluidoLimpador, TipoManutenção.Revisão, TipoVeículo.Carro),
                new(24, 7500, 0, ÁreaManutenção.FluidoÓleo, TipoManutenção.Troca, TipoVeículo.Carro),
                new(25, 5000, 0, ÁreaManutenção.FluidoÓleo, TipoManutenção.Revisão, TipoVeículo.Carro),
                new(26, 7500, 0, ÁreaManutenção.FiltroÓleo, TipoManutenção.Troca, TipoVeículo.Carro),
                new(27, 5000, 0, ÁreaManutenção.FiltroÓleo, TipoManutenção.Revisão, TipoVeículo.Carro),
                new(28, 20000, 0, ÁreaManutenção.FiltroCombustível, TipoManutenção.Troca, TipoVeículo.Carro),
                new(29, 0, 365, ÁreaManutenção.FiltroAr, TipoManutenção.Troca, TipoVeículo.Carro),
                new(30, 10000, 0, ÁreaManutenção.FiltroAr, TipoManutenção.Revisão, TipoVeículo.Carro),
                new(31, 0, 60, ÁreaManutenção.Lâmpadas, TipoManutenção.Revisão, TipoVeículo.Carro),
                new(32, 0, 30, ÁreaManutenção.Limpadores, TipoManutenção.Revisão, TipoVeículo.Carro),
            };

            mb.Entity<ManutençãoProgramada>().HasData(manutençãoProgramadasSeed);

            mb.Entity<ModeloManutenção>()
                .HasKey(mm => new { mm.ModeloId, mm.ManutençãoProgramadaId });

            mb.Entity<ModeloManutenção>()
                .HasOne(x => x.Modelo)
                .WithMany(x => x.ModeloManutenções)
                .HasForeignKey(mm => mm.ModeloId);

            mb.Entity<ModeloManutenção>()
                .HasOne(x => x.ManutençãoProgramada)
                .WithMany(x => x.ModeloManutenções)
                .HasForeignKey(mm => mm.ManutençãoProgramadaId);

            base.OnModelCreating(mb);
        }
    }
}
