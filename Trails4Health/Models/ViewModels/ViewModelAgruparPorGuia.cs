using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trails4Health.Models.ViewModels
{
    public class ViewModelAgruparPorGuia
    {
        public int GuiaID { get; set; }
        public string NomeGuia { get; set; }
        public int OpcaoID { get; set; }

        public int ContarRespostas { get; set; }
        public int SomaAvaliacao { get; set; }
        public double Avaliacao { get; set; }
    }
}
