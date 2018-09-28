using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trails4Health.Models.ViewModels
{
    public class InfoPaginacao
    {
        public int TotalItems { get; set; }
        public int ItemsPorPagina { get; set; }
        public int PaginaAtual { get; set; }
        // funciona como get : serve para determinar o nº de paginas por nº de items
        public int TotalPages =>
            // converter um para decimal de modo a que se der por exemplo 2,3 o ceiling encarrega-se de passar o resultado 3 e dps convertemos para inteiro
            (int)Math.Ceiling((decimal)TotalItems / ItemsPorPagina);
    }
}
