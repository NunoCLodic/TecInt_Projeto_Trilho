using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trails4Health.Models
{
    public class Trilho2 // classe temporária, criada com o objectivo de evitar problemas 
        //relacionados com a perda de reservas de guias da base de dados, quando o servidor IIS pára.
    {
        public int TrilhoID { get; set; }
        public string Nome { get; set; }
        public double Distancia { get; set; }

        public ICollection<ReservaGuia> ReservasGuia { get; set; }
    }
}
