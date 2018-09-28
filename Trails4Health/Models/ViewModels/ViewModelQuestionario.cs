using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trails4Health.Models.ViewModels
{
    public class ViewModelQuestionario
    {
        public int RespostaID { get; set; }
        //public int Resposta { get; set; }

        [DataType(DataType.Date)]
        public DateTime Data { get; set; } = DateTime.Now;

        public Questao Questao { get; set; }
        public int QuestaoID { get; set; }
        public string Descricao { get; set; }

        public Turista Turista { get; set; }
        public int TuristaID { get; set; }

        public Guia Guia { get; set; }
        public int GuiaID { get; set; }

        public Opcao Opcao { get; set; }
        public int OpcaoID { get; set; }
    }
}
