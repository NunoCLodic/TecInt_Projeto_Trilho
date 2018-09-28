using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// namespace Trails4Health.Data - orig.
namespace Trails4Health.Models
{
    public class EFTrails4HealthRepository : ITrails4HealthRepository
    {

        // (classe baseada em DbContext) - crio um ctx para aceder ás tabelas da BD
        private ApplicationDbContext dbContext;

        // preciso do construtor com contexto da BD para passar Lista Trilhos nos serviços (ver startup.cs)
        public EFTrails4HealthRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        // implement de ITrails4HealthRepository
        // vai buscar os Trilhos á tabela de Trilhos - tenho de criar um serviço no startup.cs
        // aqui aparecem os implement de IRepository (por cada IEnumerable<Mymodels> em IRepository)
        public IEnumerable<Trilho> Trilhos => dbContext.Trilhos;
        public IEnumerable<Dificuldade> Dificuldades => dbContext.Dificuldades;
        public IEnumerable<EstadoTrilho> EstadoTrilhos => dbContext.EstadoTrilhos;
        public IEnumerable<Estado> Estados => dbContext.Estados;

        public IEnumerable<Opcao> Opcoes => dbContext.Opcoes;
        public IEnumerable<Questao> Questoes => dbContext.Questoes;
        public IEnumerable<RespostaAvaliacao> RespostasAvaliacao => dbContext.RespostasAvaliacao;
        public IEnumerable<Guia> Guias => dbContext.Guias;
        public IEnumerable<Turista> Turistas => dbContext.Turistas;
        public IEnumerable<ReservaGuia> ReservasGuia => dbContext.ReservasGuia;
    }
}
