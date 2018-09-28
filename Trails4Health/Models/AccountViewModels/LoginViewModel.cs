using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trails4Health.Models.AccountViewModels
{
    public class LoginViewModel
    {
        // 7. (b.d.AUTENTICAÇÃO)
        [Required]
        // [EmailAddress] Email passa a ser admin: para não estar sujeito ás regras de [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Manter sessão?")]
        public bool RememberMe { get; set; }
    }
}// correr aplicação
 // ver BooksController.cs
