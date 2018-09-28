﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using Trails4Health.Models;

namespace Trails4Health.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Trails4Health.Models.Dificuldade", b =>
                {
                    b.Property<int>("DificuldadeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome");

                    b.Property<string>("Observacao");

                    b.HasKey("DificuldadeID");

                    b.ToTable("Dificuldades");
                });

            modelBuilder.Entity("Trails4Health.Models.Estado", b =>
                {
                    b.Property<int>("EstadoID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome");

                    b.HasKey("EstadoID");

                    b.ToTable("Estados");
                });

            modelBuilder.Entity("Trails4Health.Models.EstadoTrilho", b =>
                {
                    b.Property<int>("EstadoTrilhoID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DataFim");

                    b.Property<DateTime?>("DataInicio");

                    b.Property<int>("EstadoID");

                    b.Property<int>("TrilhoID");

                    b.HasKey("EstadoTrilhoID");

                    b.HasIndex("EstadoID");

                    b.HasIndex("TrilhoID");

                    b.ToTable("EstadoTrilhos");
                });

            modelBuilder.Entity("Trails4Health.Models.Guia", b =>
                {
                    b.Property<int>("GuiaID")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("Avaliacao");

                    b.Property<DateTime>("DataNascimento");

                    b.Property<string>("Email");

                    b.Property<string>("Nome");

                    b.Property<string>("Telefone");

                    b.HasKey("GuiaID");

                    b.ToTable("Guias");
                });

            modelBuilder.Entity("Trails4Health.Models.Opcao", b =>
                {
                    b.Property<int>("OpcaoID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("NumeroOpcao");

                    b.HasKey("OpcaoID");

                    b.ToTable("Opcoes");
                });

            modelBuilder.Entity("Trails4Health.Models.Questao", b =>
                {
                    b.Property<int>("QuestaoID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Desactivada");

                    b.Property<string>("Descricao");

                    b.Property<string>("NomeQuestao");

                    b.HasKey("QuestaoID");

                    b.ToTable("Questoes");
                });

            modelBuilder.Entity("Trails4Health.Models.ReservaGuia", b =>
                {
                    b.Property<int>("ReservaID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GuiaID");

                    b.Property<DateTime>("ReservaParaDia");

                    b.Property<int>("TrilhoID");

                    b.Property<int>("TuristaID");

                    b.HasKey("ReservaID");

                    b.HasIndex("GuiaID");

                    b.HasIndex("TrilhoID");

                    b.HasIndex("TuristaID");

                    b.ToTable("ReservasGuia");
                });

            modelBuilder.Entity("Trails4Health.Models.RespostaAvaliacao", b =>
                {
                    b.Property<int>("RespostaID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Data");

                    b.Property<int>("GuiaID");

                    b.Property<int>("OpcaoID");

                    b.Property<int>("QuestaoID");

                    b.Property<int>("TuristaID");

                    b.HasKey("RespostaID");

                    b.HasIndex("GuiaID");

                    b.HasIndex("OpcaoID");

                    b.HasIndex("QuestaoID");

                    b.HasIndex("TuristaID");

                    b.ToTable("RespostasAvaliacao");
                });

            modelBuilder.Entity("Trails4Health.Models.Trilho", b =>
                {
                    b.Property<int>("TrilhoID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Desativado");

                    b.Property<string>("Detalhes")
                        .IsRequired()
                        .HasMaxLength(700);

                    b.Property<int>("DificuldadeID");

                    b.Property<double>("Distancia");

                    b.Property<string>("Fim")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<byte[]>("Foto");

                    b.Property<string>("Inicio")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Sumario")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("TrilhoID");

                    b.HasIndex("DificuldadeID");

                    b.ToTable("Trilhos");
                });

            modelBuilder.Entity("Trails4Health.Models.Trilho2", b =>
                {
                    b.Property<int>("TrilhoID")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Distancia");

                    b.Property<string>("Nome");

                    b.HasKey("TrilhoID");

                    b.ToTable("Trilhos2");
                });

            modelBuilder.Entity("Trails4Health.Models.Turista", b =>
                {
                    b.Property<int>("TuristaID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataNascimento");

                    b.Property<string>("Email");

                    b.Property<string>("Morada");

                    b.Property<int>("Nif");

                    b.Property<string>("Nome");

                    b.Property<string>("Telefone");

                    b.HasKey("TuristaID");

                    b.ToTable("Turistas");
                });

            modelBuilder.Entity("Trails4Health.Models.EstadoTrilho", b =>
                {
                    b.HasOne("Trails4Health.Models.Estado", "Estado")
                        .WithMany("EstadoTrilhos")
                        .HasForeignKey("EstadoID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Trails4Health.Models.Trilho", "Trilho")
                        .WithMany("EstadoTrilhos")
                        .HasForeignKey("TrilhoID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Trails4Health.Models.ReservaGuia", b =>
                {
                    b.HasOne("Trails4Health.Models.Guia", "Guia")
                        .WithMany("ReservasGuia")
                        .HasForeignKey("GuiaID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Trails4Health.Models.Trilho2", "Trilho2")
                        .WithMany("ReservasGuia")
                        .HasForeignKey("TrilhoID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Trails4Health.Models.Turista", "Turista")
                        .WithMany("ReservasGuia")
                        .HasForeignKey("TuristaID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Trails4Health.Models.RespostaAvaliacao", b =>
                {
                    b.HasOne("Trails4Health.Models.Guia", "Guia")
                        .WithMany("RespostasAvaliacao")
                        .HasForeignKey("GuiaID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Trails4Health.Models.Opcao", "Opcao")
                        .WithMany("RespostasAvaliacao")
                        .HasForeignKey("OpcaoID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Trails4Health.Models.Questao", "Questao")
                        .WithMany("RespostasAvaliacao")
                        .HasForeignKey("QuestaoID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Trails4Health.Models.Turista", "Turista")
                        .WithMany("RespostasAvaliacao")
                        .HasForeignKey("TuristaID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Trails4Health.Models.Trilho", b =>
                {
                    b.HasOne("Trails4Health.Models.Dificuldade", "Dificuldade")
                        .WithMany("Trilhos")
                        .HasForeignKey("DificuldadeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
