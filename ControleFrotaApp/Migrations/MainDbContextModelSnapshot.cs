﻿// <auto-generated />
using System;
using ControleFrota.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ControleFrota.Migrations
{
    [DbContext(typeof(MainDbContext))]
    partial class MainDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("ControleFrota.Domain.Abastecimento", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Combustível")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataHora")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("FormasPagamento")
                        .HasColumnType("int");

                    b.Property<decimal>("KM")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("Litragem")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("MotoristaID")
                        .HasColumnType("int");

                    b.Property<string>("Posto")
                        .HasColumnType("longtext");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("VeículoID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("MotoristaID");

                    b.HasIndex("VeículoID");

                    b.ToTable("Abastecimentos");
                });

            modelBuilder.Entity("ControleFrota.Domain.Combustível", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.HasKey("ID");

                    b.ToTable("Combustívels");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Nome = "Gasolina Comum"
                        },
                        new
                        {
                            ID = 2,
                            Nome = "Gasolina Aditivada"
                        },
                        new
                        {
                            ID = 3,
                            Nome = "Etanol Comum"
                        },
                        new
                        {
                            ID = 4,
                            Nome = "Etanol Aditivado"
                        });
                });

            modelBuilder.Entity("ControleFrota.Domain.Gasto", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AbastecimentoID")
                        .HasColumnType("int");

                    b.Property<int?>("TipoGastoID")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("ViagemID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("AbastecimentoID");

                    b.HasIndex("TipoGastoID");

                    b.HasIndex("ViagemID");

                    b.ToTable("Gastos");
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

                    b.Property<int?>("MotoristaID")
                        .HasColumnType("int");

                    b.Property<string>("Observações")
                        .HasColumnType("longtext");

                    b.Property<decimal>("Preço")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("TipoManutenção")
                        .HasColumnType("int");

                    b.Property<int>("TipoReparo")
                        .HasColumnType("int");

                    b.Property<int?>("VeículoID")
                        .HasColumnType("int");

                    b.Property<int>("ÁreaManutenção")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("MotoristaID");

                    b.HasIndex("VeículoID");

                    b.ToTable("Manutençãos");
                });

            modelBuilder.Entity("ControleFrota.Domain.ManutençãoProgramada", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("DeltaDias")
                        .HasColumnType("int");

                    b.Property<decimal>("DeltaKM")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("TipoManutenção")
                        .HasColumnType("int");

                    b.Property<int>("TipoVeículo")
                        .HasColumnType("int");

                    b.Property<int>("ÁreaManutenção")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("ManutençãoProgramadas");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            DeltaDias = 0,
                            DeltaKM = 50000m,
                            TipoManutenção = 1,
                            TipoVeículo = 0,
                            ÁreaManutenção = 0
                        },
                        new
                        {
                            ID = 2,
                            DeltaDias = 0,
                            DeltaKM = 10000m,
                            TipoManutenção = 0,
                            TipoVeículo = 0,
                            ÁreaManutenção = 0
                        },
                        new
                        {
                            ID = 3,
                            DeltaDias = 0,
                            DeltaKM = 10000m,
                            TipoManutenção = 0,
                            TipoVeículo = 0,
                            ÁreaManutenção = 1
                        },
                        new
                        {
                            ID = 4,
                            DeltaDias = 0,
                            DeltaKM = 10000m,
                            TipoManutenção = 0,
                            TipoVeículo = 0,
                            ÁreaManutenção = 2
                        },
                        new
                        {
                            ID = 5,
                            DeltaDias = 2555,
                            DeltaKM = 0m,
                            TipoManutenção = 1,
                            TipoVeículo = 0,
                            ÁreaManutenção = 4
                        },
                        new
                        {
                            ID = 7,
                            DeltaDias = 0,
                            DeltaKM = 10000m,
                            TipoManutenção = 0,
                            TipoVeículo = 0,
                            ÁreaManutenção = 5
                        },
                        new
                        {
                            ID = 6,
                            DeltaDias = 0,
                            DeltaKM = 100000m,
                            TipoManutenção = 1,
                            TipoVeículo = 0,
                            ÁreaManutenção = 5
                        },
                        new
                        {
                            ID = 8,
                            DeltaDias = 0,
                            DeltaKM = 40000m,
                            TipoManutenção = 1,
                            TipoVeículo = 0,
                            ÁreaManutenção = 6
                        },
                        new
                        {
                            ID = 9,
                            DeltaDias = 0,
                            DeltaKM = 5000m,
                            TipoManutenção = 0,
                            TipoVeículo = 0,
                            ÁreaManutenção = 6
                        },
                        new
                        {
                            ID = 10,
                            DeltaDias = 0,
                            DeltaKM = 5000m,
                            TipoManutenção = 0,
                            TipoVeículo = 0,
                            ÁreaManutenção = 7
                        },
                        new
                        {
                            ID = 11,
                            DeltaDias = 0,
                            DeltaKM = 100000m,
                            TipoManutenção = 1,
                            TipoVeículo = 0,
                            ÁreaManutenção = 8
                        },
                        new
                        {
                            ID = 12,
                            DeltaDias = 0,
                            DeltaKM = 10000m,
                            TipoManutenção = 0,
                            TipoVeículo = 0,
                            ÁreaManutenção = 8
                        },
                        new
                        {
                            ID = 13,
                            DeltaDias = 0,
                            DeltaKM = 10000m,
                            TipoManutenção = 0,
                            TipoVeículo = 0,
                            ÁreaManutenção = 9
                        },
                        new
                        {
                            ID = 14,
                            DeltaDias = 730,
                            DeltaKM = 0m,
                            TipoManutenção = 1,
                            TipoVeículo = 0,
                            ÁreaManutenção = 10
                        },
                        new
                        {
                            ID = 15,
                            DeltaDias = 0,
                            DeltaKM = 5000m,
                            TipoManutenção = 0,
                            TipoVeículo = 0,
                            ÁreaManutenção = 10
                        },
                        new
                        {
                            ID = 16,
                            DeltaDias = 0,
                            DeltaKM = 50000m,
                            TipoManutenção = 1,
                            TipoVeículo = 0,
                            ÁreaManutenção = 11
                        },
                        new
                        {
                            ID = 17,
                            DeltaDias = 0,
                            DeltaKM = 10000m,
                            TipoManutenção = 0,
                            TipoVeículo = 0,
                            ÁreaManutenção = 11
                        },
                        new
                        {
                            ID = 18,
                            DeltaDias = 0,
                            DeltaKM = 10000m,
                            TipoManutenção = 0,
                            TipoVeículo = 0,
                            ÁreaManutenção = 12
                        },
                        new
                        {
                            ID = 19,
                            DeltaDias = 730,
                            DeltaKM = 0m,
                            TipoManutenção = 1,
                            TipoVeículo = 0,
                            ÁreaManutenção = 13
                        },
                        new
                        {
                            ID = 20,
                            DeltaDias = 0,
                            DeltaKM = 80000m,
                            TipoManutenção = 1,
                            TipoVeículo = 0,
                            ÁreaManutenção = 14
                        },
                        new
                        {
                            ID = 21,
                            DeltaDias = 365,
                            DeltaKM = 0m,
                            TipoManutenção = 1,
                            TipoVeículo = 0,
                            ÁreaManutenção = 15
                        },
                        new
                        {
                            ID = 22,
                            DeltaDias = 0,
                            DeltaKM = 10000m,
                            TipoManutenção = 0,
                            TipoVeículo = 0,
                            ÁreaManutenção = 15
                        },
                        new
                        {
                            ID = 23,
                            DeltaDias = 30,
                            DeltaKM = 0m,
                            TipoManutenção = 0,
                            TipoVeículo = 0,
                            ÁreaManutenção = 16
                        },
                        new
                        {
                            ID = 24,
                            DeltaDias = 0,
                            DeltaKM = 7500m,
                            TipoManutenção = 1,
                            TipoVeículo = 0,
                            ÁreaManutenção = 17
                        },
                        new
                        {
                            ID = 25,
                            DeltaDias = 0,
                            DeltaKM = 5000m,
                            TipoManutenção = 0,
                            TipoVeículo = 0,
                            ÁreaManutenção = 17
                        },
                        new
                        {
                            ID = 26,
                            DeltaDias = 0,
                            DeltaKM = 7500m,
                            TipoManutenção = 1,
                            TipoVeículo = 0,
                            ÁreaManutenção = 18
                        },
                        new
                        {
                            ID = 27,
                            DeltaDias = 0,
                            DeltaKM = 5000m,
                            TipoManutenção = 0,
                            TipoVeículo = 0,
                            ÁreaManutenção = 18
                        },
                        new
                        {
                            ID = 28,
                            DeltaDias = 0,
                            DeltaKM = 20000m,
                            TipoManutenção = 1,
                            TipoVeículo = 0,
                            ÁreaManutenção = 19
                        },
                        new
                        {
                            ID = 29,
                            DeltaDias = 365,
                            DeltaKM = 0m,
                            TipoManutenção = 1,
                            TipoVeículo = 0,
                            ÁreaManutenção = 20
                        },
                        new
                        {
                            ID = 30,
                            DeltaDias = 0,
                            DeltaKM = 10000m,
                            TipoManutenção = 0,
                            TipoVeículo = 0,
                            ÁreaManutenção = 20
                        },
                        new
                        {
                            ID = 31,
                            DeltaDias = 60,
                            DeltaKM = 0m,
                            TipoManutenção = 0,
                            TipoVeículo = 0,
                            ÁreaManutenção = 21
                        },
                        new
                        {
                            ID = 32,
                            DeltaDias = 30,
                            DeltaKM = 0m,
                            TipoManutenção = 0,
                            TipoVeículo = 0,
                            ÁreaManutenção = 22
                        });
                });

            modelBuilder.Entity("ControleFrota.Domain.Modelo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Marca")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.Property<int>("TipoVeículo")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Modelos");
                });

            modelBuilder.Entity("ControleFrota.Domain.ModeloManutenção", b =>
                {
                    b.Property<int>("ModeloId")
                        .HasColumnType("int");

                    b.Property<int>("ManutençãoProgramadaId")
                        .HasColumnType("int");

                    b.HasKey("ModeloId", "ManutençãoProgramadaId");

                    b.HasIndex("ManutençãoProgramadaId");

                    b.ToTable("ModeloManutenção");
                });

            modelBuilder.Entity("ControleFrota.Domain.Motorista", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Ativo")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("CNH")
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.Property<int?>("SetorID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ValidadeCNH")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ID");

                    b.HasIndex("SetorID");

                    b.ToTable("Motoristas");
                });

            modelBuilder.Entity("ControleFrota.Domain.Setor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descrição")
                        .HasColumnType("longtext");

                    b.HasKey("ID");

                    b.ToTable("Setors");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Descrição = "Infraestrutura"
                        },
                        new
                        {
                            ID = 2,
                            Descrição = "Suporte Técnico"
                        },
                        new
                        {
                            ID = 3,
                            Descrição = "Vendas"
                        },
                        new
                        {
                            ID = 4,
                            Descrição = "Administração"
                        });
                });

            modelBuilder.Entity("ControleFrota.Domain.TipoGasto", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descrição")
                        .HasColumnType("longtext");

                    b.HasKey("ID");

                    b.ToTable("TipoGastos");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Descrição = "Pedágio"
                        },
                        new
                        {
                            ID = 2,
                            Descrição = "Hospedagem"
                        },
                        new
                        {
                            ID = 3,
                            Descrição = "Alimentação"
                        });
                });

            modelBuilder.Entity("ControleFrota.Domain.Veículo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Ativo")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Chassis")
                        .HasColumnType("longtext");

                    b.Property<bool>("EmManutenção")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("ModeloID")
                        .HasColumnType("int");

                    b.Property<string>("Placa")
                        .HasColumnType("longtext");

                    b.Property<string>("RENAVAM")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ÚltimoLicenciamento")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ID");

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

                    b.HasOne("ControleFrota.Domain.Veículo", "Veículo")
                        .WithMany("Abastecimentos")
                        .HasForeignKey("VeículoID");

                    b.Navigation("Motorista");

                    b.Navigation("Veículo");
                });

            modelBuilder.Entity("ControleFrota.Domain.Gasto", b =>
                {
                    b.HasOne("ControleFrota.Domain.Abastecimento", "Abastecimento")
                        .WithMany()
                        .HasForeignKey("AbastecimentoID");

                    b.HasOne("ControleFrota.Domain.TipoGasto", "TipoGasto")
                        .WithMany()
                        .HasForeignKey("TipoGastoID");

                    b.HasOne("ControleFrota.Domain.Viagem", null)
                        .WithMany("Gastos")
                        .HasForeignKey("ViagemID");

                    b.Navigation("Abastecimento");

                    b.Navigation("TipoGasto");
                });

            modelBuilder.Entity("ControleFrota.Domain.Manutenção", b =>
                {
                    b.HasOne("ControleFrota.Domain.Motorista", "Motorista")
                        .WithMany()
                        .HasForeignKey("MotoristaID");

                    b.HasOne("ControleFrota.Domain.Veículo", "Veículo")
                        .WithMany("Manutenções")
                        .HasForeignKey("VeículoID");

                    b.Navigation("Motorista");

                    b.Navigation("Veículo");
                });

            modelBuilder.Entity("ControleFrota.Domain.ModeloManutenção", b =>
                {
                    b.HasOne("ControleFrota.Domain.ManutençãoProgramada", "ManutençãoProgramada")
                        .WithMany("ModeloManutenções")
                        .HasForeignKey("ManutençãoProgramadaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ControleFrota.Domain.Modelo", "Modelo")
                        .WithMany("ModeloManutenções")
                        .HasForeignKey("ModeloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ManutençãoProgramada");

                    b.Navigation("Modelo");
                });

            modelBuilder.Entity("ControleFrota.Domain.Motorista", b =>
                {
                    b.HasOne("ControleFrota.Domain.Setor", "Setor")
                        .WithMany()
                        .HasForeignKey("SetorID");

                    b.Navigation("Setor");
                });

            modelBuilder.Entity("ControleFrota.Domain.Veículo", b =>
                {
                    b.HasOne("ControleFrota.Domain.Modelo", "Modelo")
                        .WithMany()
                        .HasForeignKey("ModeloID");

                    b.Navigation("Modelo");
                });

            modelBuilder.Entity("ControleFrota.Domain.Viagem", b =>
                {
                    b.HasOne("ControleFrota.Domain.Motorista", "Motorista")
                        .WithMany()
                        .HasForeignKey("MotoristaID");

                    b.HasOne("ControleFrota.Domain.Veículo", "Veículo")
                        .WithMany("Viagens")
                        .HasForeignKey("VeículoID");

                    b.Navigation("Motorista");

                    b.Navigation("Veículo");
                });

            modelBuilder.Entity("ControleFrota.Domain.ManutençãoProgramada", b =>
                {
                    b.Navigation("ModeloManutenções");
                });

            modelBuilder.Entity("ControleFrota.Domain.Modelo", b =>
                {
                    b.Navigation("ModeloManutenções");
                });

            modelBuilder.Entity("ControleFrota.Domain.Veículo", b =>
                {
                    b.Navigation("Abastecimentos");

                    b.Navigation("Manutenções");

                    b.Navigation("Viagens");
                });

            modelBuilder.Entity("ControleFrota.Domain.Viagem", b =>
                {
                    b.Navigation("Gastos");
                });
#pragma warning restore 612, 618
        }
    }
}
