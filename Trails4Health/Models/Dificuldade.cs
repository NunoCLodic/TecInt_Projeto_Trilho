using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trails4Health.Models
{
    public class Dificuldade
    {
        // ATRIBUTOS
        public int DificuldadeID { get; set; }
        public string Nome { get; set; }
        public string Observacao { get; set; }

        // Dificuldade tem varios Trilhos
        public ICollection<Trilho> Trilhos { get; set; }
    }
}
