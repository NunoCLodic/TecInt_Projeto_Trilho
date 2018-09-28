using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trails4Health.Models
{
    public class Trilho
    {
        // ATRIBUTOS
        public int TrilhoID { get; set; } // formato para reconhecer pk: nomeID !!

        [Required(ErrorMessage = "Introduza nome do Trilho")] // nao nulo 
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Nome tem entre 2-50 caracteres")] // entre 2-50 caracteres
        [RegularExpression(@"(\w+[^_^0-9]+[^\s]+)", ErrorMessage = "Nome Inválido")] // começa com palavra(sem espaço!) > termina sem espaço  
        public string Nome { get; set; }

        [Required(ErrorMessage = "Introduza inicio do Trilho")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Inicio tem entre 2-50 caracteres")] // entre 2-50 caracteres
        [RegularExpression(@"(\w+[^_^0-9]+[^\s]+)", ErrorMessage = "Inicio Inválido")] // começa com palavra(sem espaço!) > termina sem espaço 
        public string Inicio { get; set; }

        [Required(ErrorMessage = "Introduza fim do Trilho")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Fim tem entre 2-50 caracteres")] // entre 2-50 caracteres
        [RegularExpression(@"(\w+[^_^0-9]+[^\s]+)", ErrorMessage = "Fim Inválido")] // começa com palavra(sem espaço!) > termina sem espaço  
        public string Fim { get; set; }

        [Required(ErrorMessage = "Introduza sumario do Trilho")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Sumario tem entre 5-200 caracteres")] // entre entre 5-200 caracteres
        public string Sumario { get; set; }

        [Required(ErrorMessage = "Introduza detalhes do Trilho")]
        [StringLength(700, MinimumLength = 5, ErrorMessage = "Detalhes tem entre 5-700 caracteres")] // entre 5-700 caracteres
        public string Detalhes { get; set; }

        [Required(ErrorMessage = "Introduza distancia do Trilho")]
        [Range(0, 999.99)]
        public double Distancia { get; set; }

        // [Required(ErrorMessage = "Escolha uma foto")]
        public byte[] Foto { get; set; }

        public bool Desativado { get; set; } = false;

        // FK Dificuldade
        public int DificuldadeID { get; set; }
        public Dificuldade Dificuldade { get; set; }

        // Trilho tem varios EstadoTrilhos (classe intermedia)
        public ICollection<EstadoTrilho> EstadoTrilhos { get; set; }

        //public ICollection<ReservaGuia> ReservasGuia { get; set; }
    }
}
