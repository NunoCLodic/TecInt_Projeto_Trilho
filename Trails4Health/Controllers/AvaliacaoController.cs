using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Trails4Health.Controllers
{
    public class AvaliacaoController : Controller
    {
        // GET:
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult AvaliacaoGuia()
        {
            return View();
        }

        public ViewResult AvaliacaoTrilho()
        {
            return View();
        }


        public ViewResult QuestoesAvaliacaoGuia()
        {
            return View();
        }


        public ViewResult QuestoesAvaliacaoTrilho()
        {
            return View();
        }

    }
}
