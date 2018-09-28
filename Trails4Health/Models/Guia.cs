using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trails4Health.Models
{
    public class Guia
    {
        public int GuiaID { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
        //public float Preco { get; set; }
        public float Avaliacao { get; set; }

        public ICollection<ReservaGuia> ReservasGuia { get; set; }
        public ICollection<RespostaAvaliacao> RespostasAvaliacao { get; set; }
    }
}
