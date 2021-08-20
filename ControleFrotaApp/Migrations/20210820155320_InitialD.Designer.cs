﻿// <auto-generated />
using System;
using ControleFrota.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ControleFrota.Migrations
{
    [DbContext(typeof(MainDbContext))]
    [Migration("20210820155320_InitialD")]
    partial class InitialD
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.8");

            modelBuilder.Entity("ControleFrota.Domain.Abastecimento", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Combustível")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataHora")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("KM")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("Litragem")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("MotoristaID")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("VeículoID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("MotoristaID");

                    b.HasIndex("VeículoID");

                    b.ToTable("Abastecimentos");
                });

            modelBuilder.Entity("ControleFrota.Domain.Manutenção", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DataHora")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("KM")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Local")
                        .HasColumnType("longtext");

                    b.Property<string>("Motivo")
                        .HasColumnType("longtext");

                    b.Property<string>("Peça")
                        .HasColumnType("longtext");

                    b.Property<decimal>("Preço")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("VeículoID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("VeículoID");

                    b.ToTable("Manutençãos");
                });

            modelBuilder.Entity("ControleFrota.Domain.ManutençãoProgramada", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("KM")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("ManutençãoID")
                        .HasColumnType("int");

                    b.Property<int?>("VeículoID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ManutençãoID");

                    b.HasIndex("VeículoID");

                    b.ToTable("ManutençãoProgramadas");
                });

            modelBuilder.Entity("ControleFrota.Domain.Marca", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.HasKey("ID");

                    b.ToTable("Marcas");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Nome = "Fiat"
                        },
                        new
                        {
                            ID = 2,
                            Nome = "Ford"
                        },
                        new
                        {
                            ID = 3,
                            Nome = "Jeep"
                        },
                        new
                        {
                            ID = 4,
                            Nome = "Renault"
                        },
                        new
                        {
                            ID = 5,
                            Nome = "Volkswagen"
                        },
                        new
                        {
                            ID = 6,
                            Nome = "Yamaha"
                        });
                });

            modelBuilder.Entity("ControleFrota.Domain.Modelo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.HasKey("ID");

                    b.ToTable("Modelos");
                });

            modelBuilder.Entity("ControleFrota.Domain.Motorista", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CNH")
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.HasKey("ID");

                    b.ToTable("Motoristas");
                });

            modelBuilder.Entity("ControleFrota.Domain.Veículo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("MarcaID")
                        .HasColumnType("int");

                    b.Property<int?>("ModeloID")
                        .HasColumnType("int");

                    b.Property<string>("Placa")
                        .HasColumnType("longtext");

                    b.Property<string>("RENAVAM")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ÚltimoLicenciamento")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ID");

                    b.HasIndex("MarcaID");

                    b.HasIndex("ModeloID");

                    b.ToTable("Veículos");
                });

            modelBuilder.Entity("ControleFrota.Domain.Viagem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("KMFinal")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("KMInicial")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Motivo")
                        .HasColumnType("longtext");

                    b.Property<int?>("MotoristaID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Retorno")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Saída")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("VeículoID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("MotoristaID");

                    b.HasIndex("VeículoID");

                    b.ToTable("Viagems");
                });

            modelBuilder.Entity("ControleFrota.Domain.Abastecimento", b =>
                {
                    b.HasOne("ControleFrota.Domain.Motorista", "Motorista")
                        .WithMany()
                        .HasForeignKey("MotoristaID");

                    b.HasOne("ControleFrota.Domain.Veículo", null)
                        .WithMany("Abastecimentos")
                        .HasForeignKey("VeículoID");

                    b.Navigation("Motorista");
                });

            modelBuilder.Entity("ControleFrota.Domain.Manutenção", b =>
                {
                    b.HasOne("ControleFrota.Domain.Veículo", null)
                        .WithMany("Manutenções")
                        .HasForeignKey("VeículoID");
                });

            modelBuilder.Entity("ControleFrota.Domain.ManutençãoProgramada", b =>
                {
                    b.HasOne("ControleFrota.Domain.Manutenção", "Manutenção")
                        .WithMany()
                        .HasForeignKey("ManutençãoID");

                    b.HasOne("ControleFrota.Domain.Veículo", "Veículo")
                        .WithMany("ManutençõesProgramadas")
                        .HasForeignKey("VeículoID");

                    b.Navigation("Manutenção");

                    b.Navigation("Veículo");
                });

            modelBuilder.Entity("ControleFrota.Domain.Veículo", b =>
                {
                    b.HasOne("ControleFrota.Domain.Marca", "Marca")
                        .WithMany()
                        .HasForeignKey("MarcaID");

                    b.HasOne("ControleFrota.Domain.Modelo", "Modelo")
                        .WithMany()
                        .HasForeignKey("ModeloID");

                    b.Navigation("Marca");

                    b.Navigation("Modelo");
                });

            modelBuilder.Entity("ControleFrota.Domain.Viagem", b =>
                {
                    b.HasOne("ControleFrota.Domain.Motorista", "Motorista")
                        .WithMany()
                        .HasForeignKey("MotoristaID");

                    b.HasOne("ControleFrota.Domain.Veículo", null)
                        .WithMany("Viagens")
                        .HasForeignKey("VeículoID");

                    b.Navigation("Motorista");
                });

            modelBuilder.Entity("ControleFrota.Domain.Veículo", b =>
                {
                    b.Navigation("Abastecimentos");

                    b.Navigation("Manutenções");

                    b.Navigation("ManutençõesProgramadas");

                    b.Navigation("Viagens");
                });
#pragma warning restore 612, 618
        }
    }
}
