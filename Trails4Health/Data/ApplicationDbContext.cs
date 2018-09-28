using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/* A seguir: 
 * - Criar Scaffolded Item:
 * Rck /controllers > add Scaffolded Item > MVC_controllers_with_views > Model Class: EX: "Trilho.cs"
 *      Notas: 1.Vai criar estrutura para CRUD de Trilho.cs - 2.Pode ser necessario > add Scaffolded Item (2x)
 */

// orig: namespace Trails4Health.Data
namespace Trails4Health.Models
{
    // class DbContext mapea BD
    // instancio esta classe na minha [EFRepository:IRepository] 
    public class ApplicationDbContext : DbContext
    {
        // base(options) mm que super: cria BD com as options
        // permite-me ter acessso ao que está no modelo
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        // config. base dados com os modelos: digo como vou mapear a BD
        public DbSet<Trilho> Trilhos { get; set; }
        public DbSet<Dificuldade> Dificuldades { get; set; }
        public DbSet<EstadoTrilho> EstadoTrilhos { get; set; }
        public DbSet<Estado> Estados { get; set; }

        public DbSet<Questao> Questoes { get; set; }
        public DbSet<Turista> Turistas { get; set; }
        public DbSet<Guia> Guias { get; set; }
        public DbSet<Opcao> Opcoes { get; set; }
        public DbSet<RespostaAvaliacao> RespostasAvaliacao { get; set; }
        public DbSet<ReservaGuia> ReservasGuia { get; set; }
        public DbSet<Trilho2> Trilhos2 { get; set; }

        //uso fluent API para enunciar explicitamente a relação
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // explicitar PK composta
            // EstadoTrilho tem PK constituida por: EstadoID TrilhoID
            // OLD:
            //modelBuilder.Entity<EstadoTrilho>()
            //    .HasKey(et => new { et.EstadoID, et.TrilhoID });

            modelBuilder.Entity<EstadoTrilho>()
                .HasKey(EstadoTrilho => EstadoTrilho.EstadoTrilhoID);

            // relação: EstadoTrilho - Estado
            // 1 EstadoTrilho tem 1 Estado; 1 Estado tem mts EstadoTrilhos; EstadoTrilho tem FK: EstadoID
            modelBuilder.Entity<EstadoTrilho>()
                .HasOne(EstadoTrilho => EstadoTrilho.Estado)
                .WithMany(Estado => Estado.EstadoTrilhos)
                .HasForeignKey(EstadoTrilho => EstadoTrilho.EstadoID);

            // relação: EstadoTrilho - Trilho
            // 1 EstadoTrilho tem 1 Trilho; 1 Trilho tem mts EstadoTrilhos; EstadoTrilho tem FK: TrilhoID
            modelBuilder.Entity<EstadoTrilho>()
                .HasOne(EstadoTrilho => EstadoTrilho.Trilho)
                .WithMany(Trilho => Trilho.EstadoTrilhos)
                .HasForeignKey(EstadoTrilho => EstadoTrilho.TrilhoID);

            // relação: Trilho - Dificuldade 
            // 1 Trilho tem 1 Dificuldade; 1 Dificuldade tem mts Trilhos; Trilho tem FK: DificuldadeID
            modelBuilder.Entity<Trilho>()
                .HasOne(Trilho => Trilho.Dificuldade)
                .WithMany(Dificuldade => Dificuldade.Trilhos)
                .HasForeignKey(Trilho => Trilho.DificuldadeID);

            //------------------------------------------------------
            modelBuilder.Entity<Questao>().HasKey(q => q.QuestaoID);

            modelBuilder.Entity<Opcao>().HasKey(o => o.OpcaoID);

            modelBuilder.Entity<RespostaAvaliacao>().HasKey(ra => ra.RespostaID);
            modelBuilder.Entity<RespostaAvaliacao>()
                .HasOne(ra => ra.Questao)
                .WithMany(q => q.RespostasAvaliacao)
                .HasForeignKey(ra => ra.QuestaoID);
            modelBuilder.Entity<RespostaAvaliacao>()
                .HasOne(ra => ra.Turista)
                .WithMany(t => t.RespostasAvaliacao)
                .HasForeignKey(ra => ra.TuristaID);
            modelBuilder.Entity<RespostaAvaliacao>()
                .HasOne(ra => ra.Guia)
                .WithMany(g => g.RespostasAvaliacao)
                .HasForeignKey(ra => ra.GuiaID);
            modelBuilder.Entity<RespostaAvaliacao>()
                .HasOne(ra => ra.Opcao)
                .WithMany(o => o.RespostasAvaliacao)
                .HasForeignKey(ra => ra.OpcaoID);

            modelBuilder.Entity<Turista>().HasKey(t => t.TuristaID);

            modelBuilder.Entity<Guia>().HasKey(g => g.GuiaID);

            modelBuilder.Entity<ReservaGuia>().HasKey(rg => rg.ReservaID);
            modelBuilder.Entity<ReservaGuia>()
                .HasOne(rg => rg.Turista)
                .WithMany(t => t.ReservasGuia)
                .HasForeignKey(rg => rg.TuristaID);
            modelBuilder.Entity<ReservaGuia>()
                .HasOne(rg => rg.Guia)
                .WithMany(g => g.ReservasGuia)
                .HasForeignKey(rg => rg.GuiaID);
            modelBuilder.Entity<ReservaGuia>()
                .HasOne(rg => rg.Trilho2)
                .WithMany(t2 => t2.ReservasGuia)
                .HasForeignKey(rg => rg.TrilhoID);

            modelBuilder.Entity<Trilho2>().HasKey(t2 => t2.TrilhoID);
        }
    }
}