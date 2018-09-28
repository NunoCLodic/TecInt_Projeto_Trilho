using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trails4Health.Models
{
    public interface ITrails4HealthRepository
    {
        // crio 1 class interface para todos modelos(tabelas) com varios IEnumerable<MyModel>
        // por sua vez EF()Repository implementa esta classe e cada 1 destes IEnumerable<MyModel> toma o
        // valor presente na BD atraves de um serviço
        // permite-me criar qualquer tipo de "repositorio" (neste caso de Trilhos) a partir desta interface
        IEnumerable<Trilho> Trilhos { get; }
        IEnumerable<Dificuldade> Dificuldades { get; }
        IEnumerable<EstadoTrilho> EstadoTrilhos { get; }
        IEnumerable<Estado> Estados { get; }

        IEnumerable<Opcao> Opcoes { get; }
        IEnumerable<Questao> Questoes { get; }
        IEnumerable<RespostaAvaliacao> RespostasAvaliacao { get; }
        IEnumerable<Guia> Guias { get; }
        IEnumerable<Turista> Turistas { get; }
        IEnumerable<ReservaGuia> ReservasGuia { get; }
    }
}
