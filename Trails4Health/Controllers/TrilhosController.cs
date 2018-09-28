using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trails4Health.Models;
using Microsoft.AspNetCore.Builder;
using Trails4Health.Models.ViewModels;


// CAMINHO DADOS:
//   .[serviços]: ITrails4HealthRepository recebeu dados de EFTrails4HealthRepository>() (ver startup.cs)
//   .[EFTrails4HealthRepository:ITrails4HealthRepository]: recebeu dados da BD usando ApplicationDbContext (:DbContext)
//   .[ApplicationDbContext:DbContext]: mapeou BD com a classe Trilho (DbSet<Trilho> Trilhos { get; set; })
//      .Nota: A BD foi populada usando SeedData.cs (ver startup.cs) que usa ApplicationDbContext 
//   .[view List]: é do tipo IEnumerable<Trilho> e exibe campos de Trilho com: foreach (Trilho p in Model)

namespace Trails4Health.Controllers
{
    public class TrilhosController : Controller
    {

        private ITrails4HealthRepository repository;

        // Controlador vai ver se existe um serviço para ITrails4HealthRepository
        // dependency injection
        public TrilhosController(ITrails4HealthRepository repository) // construtor
        {
            this.repository = repository;
        }

        // paginação
        // IMPORTANTE: arg tem de ser "page" como no url Ex: .../TrilhoCRUD/ListaTrilhos?page=2
        public int TamanhoPagina = 3;
        public ViewResult Index(int page = 1)
        {
            return View(
                new ViewModelListaTrilhos
                {
                    ListaTrilhos = repository.Trilhos
                        .Skip(TamanhoPagina * (page - 1))
                        .Take(TamanhoPagina),
                    InfoPaginacao = new InfoPaginacao
                    {
                        PaginaAtual = page,
                        ItemsPorPagina = TamanhoPagina,
                        TotalItems = repository.Trilhos.Count()
                    }
                }); // BEFORE VIEW_MODEL:  return View(repository.Trilhos)
        }           // passa trilhos para view: @model IEnumerable<Trilho>



        // devolve o trilho selecionado (de acordo com o id: botão saber_mais - ver taghelper Trilhos\Index.cshtml)
        public ViewResult Detalhes(int? id)
        {
            if (id == null)
            {
                return View("../Shared/Error");
            }
            // trilho selecionado de acordo com TrilhoID == id
            var trilho = repository.Trilhos.SingleOrDefault(t => t.TrilhoID == id);

            if (trilho == null)
            {
                return View("../Shared/Error");
            }

            return View(trilho);
        }
    }
}

