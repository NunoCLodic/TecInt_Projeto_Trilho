using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trails4Health.Models
{
    public class ReservaGuia
    {
        public int ReservaID { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReservaParaDia { get; set; }

        public Guia Guia { get; set; }
        public int GuiaID { get; set; }

        public Turista Turista { get; set; }
        public int TuristaID { get; set; }

        public Trilho2 Trilho2 { get; set; }
        public int TrilhoID { get; set; }
    }
}
