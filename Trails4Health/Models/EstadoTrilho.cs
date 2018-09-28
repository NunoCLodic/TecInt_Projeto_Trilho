using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trails4Health.Models
{
    public class EstadoTrilho
    {
        public int EstadoTrilhoID { get; set; }

        // FK Estado
        public int EstadoID { get; set; }
        public Estado Estado { get; set; }
        // FK Trilho
        public int TrilhoID { get; set; }
        public Trilho Trilho { get; set; }

        // ATRIBUTOS
        public DateTime? DataInicio { get; set; } // permitir nulos
        public DateTime? DataFim { get; set; }
    }
}
