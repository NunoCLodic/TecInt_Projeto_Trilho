using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Trails4Health.Models.ViewModels
{
    public class ViewModelTrilho
    {
        public int TrilhoID { get; set; }

        [Required(ErrorMessage = "Introduza nome do Trilho")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Nome tem entre 2-50 caracteres")] // entre 2-50 caracteres
        [RegularExpression(@"(\w+[^_^0-9]+[^\s]+)", ErrorMessage = "Nome Inválido")] // começa com palavra(sem espaço!) > termina sem espaço  
        public string TrilhoNome { get; set; }

        [Required(ErrorMessage = "Introduza inicio do Trilho")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Inicio tem entre 2-50 caracteres")] // entre 2-50 caracteres
        [RegularExpression(@"(\w+[^_^0-9]+[^\s]+)", ErrorMessage = "Inicio Inválido")] // excepto "_" ou numeros 
        public string TrilhoInicio { get; set; }

        [Required(ErrorMessage = "Introduza fim do Trilho")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Fim tem entre 2-50 caracteres")] // entre 2-50 caracteres
        [RegularExpression(@"(\w+[^_^0-9]+[^\s]+)", ErrorMessage = "Fim Inválido")] // excepto "_" ou numeros 
        public string TrilhoFim { get; set; }

        [Required(ErrorMessage = "Introduza detalhes do Trilho")]
        [StringLength(700, MinimumLength = 5, ErrorMessage = "Detalhes tem entre 5-700 caracteres")] // entre 5-700 caracteres
        public string TrilhoDetalhes { get; set; }

        [Required(ErrorMessage = "Introduza sumario do Trilho")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Sumario tem entre 5-200 caracteres")] // entre entre 5-200 caracteres
        public string TrilhoSumario { get; set; }

        [Required(ErrorMessage = "Introduza distancia do Trilho")]
        [Range(0, 999.99)]
        public double TrilhoDistancia { get; set; }

        // UPLOAD IMAGEM
        public IFormFile ImageFile { get; set; }
        public byte[] TrilhoFoto { get; set; }

        public bool TrilhoDesativado { get; set; } = false;

        public int DificuldadeID { get; set; }
        public Dificuldade Dificuldade { get; set; }

        public int EstadoID { get; set; }

        // para listar EstadoTrilhos na View /TrilhoCRUD/Detalhes
        public IEnumerable<EstadoTrilho> EstadoTrilhos { get; set; }
    }
}
