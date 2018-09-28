using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trails4Health.Models;

namespace Trails4Health.Data
{
    // Esta classe tem o mesmo objectivo da classe "SeedData", ou seja, colocar alguns dados de teste na base de dados 
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Opcoes.Any())
            {
                context.Opcoes.AddRange(
                new Opcao { NumeroOpcao = 1 },
                new Opcao { NumeroOpcao = 2 },
                new Opcao { NumeroOpcao = 3 },
                new Opcao { NumeroOpcao = 4 },
                new Opcao { NumeroOpcao = 5 }
                );
                context.SaveChanges();
            }

            if (!context.Questoes.Any())
            {
                context.Questoes.AddRange(
                new Questao { NomeQuestao = "O guia demonstrou conhecimento do trilho?", Desactivada = false, Descricao = "1 - Discordo Em Absoluto | 5 - Concordo Plenamente" },
                new Questao { NomeQuestao = "O guia percorreu o trilho com um ritmo adequado à dificuldade do trilho?", Desactivada = false, Descricao = "1 - Discordo Em Absoluto | 5 - Concordo Plenamente" },
                new Questao { NomeQuestao = "O guia fez pausas nos locais assinalados como sendo de interesse?", Desactivada = false, Descricao = "1 - Discordo Em Absoluto | 5 - Concordo Plenamente" }
                );
                context.SaveChanges();
            }

            if (!context.Guias.Any())
            {
                context.Guias.AddRange(
                new Guia { Nome = "José Esteves", Telefone = "271100100", Email = "jesteves@gmail.com" },
                new Guia { Nome = "Túlio Gonzaga", Telefone = "271100200", Email = "tulio@gmail.com" },
                new Guia { Nome = "António Malaquias", Telefone = "271100300", Email = "amalaquias@gmail.com" }
                );
                context.SaveChanges();
            }

            if (!context.Turistas.Any()) // Nif's válidos gerados aleatoriamente (https://nif.marcosantos.me/)
            {
                context.Turistas.AddRange(
                new Turista { Nome = "Maurício Abraão", Telefone = "271200100", Email = "mauricio@gmail.com", Nif = 275092879 },
                new Turista { Nome = "Jaime Coelho", Telefone = "271200200", Email = "jcoelho@gmail.com", Nif = 274261154 },
                new Turista { Nome = "Pedro Gama", Telefone = "271200300", Email = "mauricio@gmail.com", Nif = 281910430 }
                );
                context.SaveChanges();
            }

            if (!context.Trilhos2.Any())
            {
                context.Trilhos2.AddRange(
                new Trilho2 { Nome = "Covão dos Conchos", Distancia = 8 },
                new Trilho2 { Nome = "Faias", Distancia = 15 }
                );
                context.SaveChanges();
            }

            if (!context.ReservasGuia.Any())
            {
                context.ReservasGuia.AddRange(
                new ReservaGuia { ReservaParaDia = new DateTime(2018, 3, 1, 0, 0, 0), GuiaID = 1, TuristaID = 1, TrilhoID = 1 },
                new ReservaGuia { ReservaParaDia = new DateTime(2018, 3, 5, 0, 0, 0), GuiaID = 2, TuristaID = 2, TrilhoID = 2 },
                new ReservaGuia { ReservaParaDia = new DateTime(2018, 3, 12, 0, 0, 0), GuiaID = 3, TuristaID = 3, TrilhoID = 1 }
                );
                context.SaveChanges();
            }
        }
    }
}
