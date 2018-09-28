using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trails4Health.Models
{
    public class Turista
    {
        public int TuristaID { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Morada { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Range(100000000, 999999999)]
        public int Nif { get; set; }

        public ICollection<RespostaAvaliacao> RespostasAvaliacao { get; set; }
        public ICollection<ReservaGuia> ReservasGuia { get; set; }
    }
}
