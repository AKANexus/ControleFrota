// <auto-generated />
using System;
using ControleFrota.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ControleFrota.Migrations
{
    [DbContext(typeof(MainDbContext))]
    [Migration("20210828134420_ManutençãoColunas")]
    partial class ManutençãoColunas
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Peça")
                        .HasColumnType("longtext");

                    b.Property<decimal>("Preço")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("TipoReparo")
                        .HasColumnType("int");

                    b.Property<int?>("VeículoID")
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
                        },
                        new
                        {
                            ID = 7,
                            Nome = "Honda"
                        },
                        new
                        {
                            ID = 8,
                            Nome = "Suzuki"
                        });
                });

            modelBuilder.Entity("ControleFrota.Domain.Modelo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("MarcaID")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.HasKey("ID");

                    b.HasIndex("MarcaID");

                    b.ToTable("Modelos");
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
                        },
                        new
                        {
                            ID = 4,
                            Descrição = "Abastecimento*"
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

            modelBuilder.Entity("ControleFrota.Domain.Modelo", b =>
                {
                    b.HasOne("ControleFrota.Domain.Marca", "Marca")
                        .WithMany()
                        .HasForeignKey("MarcaID");

                    b.Navigation("Marca");
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

                    b.HasOne("ControleFrota.Domain.Veículo", "Veículo")
                        .WithMany("Viagens")
                        .HasForeignKey("VeículoID");

                    b.Navigation("Motorista");

                    b.Navigation("Veículo");
                });

            modelBuilder.Entity("ControleFrota.Domain.Veículo", b =>
                {
                    b.Navigation("Abastecimentos");

                    b.Navigation("Manutenções");

                    b.Navigation("ManutençõesProgramadas");

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
