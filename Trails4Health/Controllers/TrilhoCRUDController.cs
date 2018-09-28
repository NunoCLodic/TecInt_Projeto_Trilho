using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trails4Health.Models;
using Trails4Health.Models.ViewModels;
using System.IO;
using System.Net.Http.Headers;
using System.Threading;
using Microsoft.AspNetCore.Authorization;

namespace Trails4Health.Controllers
{
    public class TrilhoCRUDController : Controller
    {
        // para listar EstadoTrilhos em Detalhes
        private IEnumerable<EstadoTrilho> ListaEstadoTrilhos;
        
        // para pesquisar Nome em dbo.Trilho: msg ErroNomeTrilho em Criar
        private IEnumerable<Trilho> ListaTrilhosBD;        

        private readonly ApplicationDbContext _context;
        private ITrails4HealthRepository repository;  // para Listar Trilhos em BackOffice

        // ORIG: TrilhoCRUDController(ApplicationDbContext context)
        public TrilhoCRUDController(ApplicationDbContext context, ITrails4HealthRepository repository)
        {
            _context = context;
            this.repository = repository; 
        }

        [Authorize(Roles = "Administrador, Professor")]
        public async Task<IActionResult> Index()
        {
            // OrderBy(t => t.Desativado); Coloca desativados em baixo
            var applicationDbContext = _context.Trilhos.Include(t => t.Dificuldade).OrderBy(t => t.Desativado);            
            return View(await applicationDbContext.ToListAsync());
        }

        // paginação
        // Listar Trilhos em BackOffice
        // IMPORTANTE: arg tem de ser "page" como no url Ex: .../TrilhoCRUD/ListaTrilhos?page=2
        public int TamanhoPagina = 3;
        public ViewResult ListaTrilhos(int page = 1) 
        {
            return View(
                new ViewModelListaTrilhos
                {
                    ListaTrilhos = repository.Trilhos
                        .OrderBy(t => t.Desativado)
                        .Skip(TamanhoPagina * (page - 1))
                        .Take(TamanhoPagina),
                        
                    InfoPaginacao = new InfoPaginacao
                    {
                        PaginaAtual = page,
                        ItemsPorPagina = TamanhoPagina,
                        TotalItems = repository.Trilhos.Count()
                    }
                }); // BEFORE ViewModel: return View(repository.Trilhos)
        }

        // GET: Detalhes
        public async Task<IActionResult> Detalhes(int? id)
        {

            if (id == null)
            {
                return NotFound("ID NotFound");
            }

            var trilho = await _context.Trilhos
                .Include(t => t.Dificuldade)
                .SingleOrDefaultAsync(m => m.TrilhoID == id);

            // OrderBy(et => et.DataInicio) | ordena por data 
            var estadoTrilhos = _context.EstadoTrilhos
                .Include(et => et.Estado).OrderBy(et => et.DataInicio)
                .Include(et => et.Trilho);

            // ** fazer com query á BD (se houver tempo)
            ListaEstadoTrilhos = estadoTrilhos.ToListAsync().Result;

            ViewModelTrilho viewModelTrilho = new ViewModelTrilho
            {
                TrilhoID = trilho.TrilhoID,
                TrilhoNome = trilho.Nome,
                TrilhoInicio = trilho.Inicio,
                TrilhoFim = trilho.Fim,
                TrilhoSumario = trilho.Sumario,
                TrilhoDetalhes = trilho.Detalhes,
                //TrilhoFoto = trilho.Foto,
                TrilhoFoto = trilho.Foto,
                TrilhoDistancia = trilho.Distancia,
                TrilhoDesativado = trilho.Desativado,
                Dificuldade = trilho.Dificuldade,
                EstadoTrilhos = ListaEstadoTrilhos
            };

            if (trilho == null)
            {
                return NotFound();
            }
            //ViewData["DificuldadeID"] = new SelectList(_context.Dificuldades, "DificuldadeID", "Nome");
            return View(viewModelTrilho);
        }

        // GET: Create
        [Authorize(Roles = "Administrador, Professor")]
        public IActionResult Criar()
        {
            // viewBag recebe valores do tipo ViewData["DificuldadeID"] em runTime
            // SelectList(tabelaBD,valoresColuna,nomeColuna) | nota: valores vao ser recebidos num dropDownList
            ViewData["DificuldadeID"] = new SelectList(_context.Dificuldades, "DificuldadeID", "Nome");
            ViewData["EstadoID"] = new SelectList(_context.Estados, "EstadoID", "Nome");
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Professor")]
        public async Task<IActionResult> Criar([Bind("TrilhoID,TrilhoNome,TrilhoInicio,TrilhoFim,TrilhoDetalhes,TrilhoSumario," +
            "TrilhoDistancia,TrilhoFoto, TrilhoDesativado,DificuldadeID,EstadoID,ImageFile")] ViewModelTrilho trilhoVM)
        {
            // Colocar registos da dbo.Trilhos numa lista
            var trilhos = _context.Trilhos
              .Include(t => t.Dificuldade);

            ListaTrilhosBD = trilhos.ToListAsync().Result;

            // se existir um trilho com o mesmo Nome, mostra msg ErroNomeTrilho e reinsere dados introduzidos na mma View
            // ** fazer com query á B.D. (se houver tempo)
            foreach (var et in ListaTrilhosBD)
            {
                if (et.Nome.Equals(trilhoVM.TrilhoNome))
                {
                    ViewData["ErroNomeTrilho"] = "*Já existe um trilho com esse nome!";
                    ViewData["DificuldadeID"] = new SelectList(_context.Dificuldades, "DificuldadeID", "Nome", trilhoVM.DificuldadeID);
                    ViewData["EstadoID"] = new SelectList(_context.Estados, "EstadoID", "Nome", trilhoVM.EstadoID);
                    return View(trilhoVM);
                }
            }

            if (ModelState.IsValid)
            {
                // crio novo trilho a partir dos valores introduzidos no form (ver Bind)
                Trilho trilho = new Trilho
                {
                    Nome = trilhoVM.TrilhoNome,
                    Inicio = trilhoVM.TrilhoInicio,
                    Fim = trilhoVM.TrilhoFim,
                    Distancia = trilhoVM.TrilhoDistancia,
                    //Foto = trilhoVM.TrilhoFoto,
                    Desativado = trilhoVM.TrilhoDesativado,
                    Detalhes = trilhoVM.TrilhoDetalhes,
                    Sumario = trilhoVM.TrilhoSumario,
                    DificuldadeID = trilhoVM.DificuldadeID
                };

                if (trilhoVM.ImageFile != null) {
                // upload de imagem
                // nota: criado controlador UploadFiles, ver mudanças em ViewModelTrilho e Trilho.cs
                    using (var memoryStream = new MemoryStream())
                    {
                        await trilhoVM.ImageFile.CopyToAsync(memoryStream);
                        trilho.Foto = memoryStream.ToArray();
                    }
                }

                // coloco trilho na tabela dbo.Trilhos
                _context.Add(trilho);
               
                EstadoTrilho estadoTrilho = new EstadoTrilho
                {
                    Trilho = trilho,
                    EstadoID = trilhoVM.EstadoID,
                    DataInicio = DateTime.Now                  
                };

                // coloco estadoTrilho na tabela dbo.EstadoTrilhos
                _context.Add(estadoTrilho);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            // se modelo inválido fica na mma view com os dados introduzidos no form
            ViewData["DificuldadeID"] = new SelectList(_context.Dificuldades, "DificuldadeID", "Nome", trilhoVM.DificuldadeID);
            ViewData["EstadoID"] = new SelectList(_context.Estados, "EstadoID", "Nome", trilhoVM.EstadoID);
            return View(trilhoVM);
        }

        // GET: Editar
        [Authorize(Roles = "Administrador, Professor")]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // registo do trilho seleccionado
            Trilho trilho = await _context.Trilhos.SingleOrDefaultAsync(m => m.TrilhoID == id);

            // registo do ultimo EstadoTrilho do trilho seleccionado
            EstadoTrilho ultimoEstadoTrilho = await _context.EstadoTrilhos.SingleOrDefaultAsync(uet => uet.TrilhoID == id 
                                                                                                    && uet.DataFim == null);
            //
            if (ultimoEstadoTrilho == null)
            {
                return NotFound("GET: ultimoEstadoTrilho == null");
            }          

            //
            if (trilho == null)
            {
                return NotFound("GET: trilho == null");
            }
            // ViewModel com dados para exibir
            ViewModelTrilho VMTrilho = new ViewModelTrilho
            {
                TrilhoID = trilho.TrilhoID,
                TrilhoNome = trilho.Nome,
                TrilhoInicio = trilho.Inicio,
                TrilhoFim = trilho.Fim,
                TrilhoDistancia = trilho.Distancia,
                //TrilhoFoto = trilho.Foto,
                TrilhoFoto = trilho.Foto,
                TrilhoDesativado = trilho.Desativado,
                TrilhoDetalhes = trilho.Detalhes,
                TrilhoSumario = trilho.Sumario,
                DificuldadeID = trilho.DificuldadeID,
                // fica com o Id do ultimo estado inserido para mostrar na view:GET 
                EstadoID = ultimoEstadoTrilho.EstadoID
            };

            // passar campos pretendidos (Trilho, Dificuldade e Estado) para a view
            ViewData["DificuldadeID"] = new SelectList(_context.Dificuldades, "DificuldadeID", "Nome", VMTrilho.DificuldadeID);
            ViewData["EstadoID"] = new SelectList(_context.Estados, "EstadoID", "Nome", VMTrilho.EstadoID);
            return View(VMTrilho);
        }

        // POST: Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Professor")]
        public async Task<IActionResult> Editar(int id, [Bind("TrilhoID,TrilhoNome,TrilhoInicio,TrilhoFim,TrilhoDetalhes," +
            "TrilhoSumario,TrilhoDistancia,TrilhoFoto,TrilhoDesativado,DificuldadeID,EstadoID,TrilhoImagem,ImageFile")] ViewModelTrilho VMTrilho)
        {

            // crio novo trilho a partir dos valores introduzidos no form (ver Bind)
            Trilho trilho = new Trilho
            {
                TrilhoID = VMTrilho.TrilhoID,
                Nome = VMTrilho.TrilhoNome,
                Inicio = VMTrilho.TrilhoInicio,
                Fim = VMTrilho.TrilhoFim,
                Distancia = VMTrilho.TrilhoDistancia,
                //Foto = VMTrilho.TrilhoFoto,
                Foto = VMTrilho.TrilhoFoto,
                Desativado = VMTrilho.TrilhoDesativado,
                Detalhes = VMTrilho.TrilhoDetalhes,
                Sumario = VMTrilho.TrilhoSumario,
                DificuldadeID = VMTrilho.DificuldadeID
            };

            if (VMTrilho.ImageFile != null)
            {
                // upload de imagem
                // nota: criado controlador UploadFiles, ver mudanças em ViewModelTrilho e Trilho.cs
                using (var memoryStream = new MemoryStream())
                {
                    await VMTrilho.ImageFile.CopyToAsync(memoryStream);
                    trilho.Foto = memoryStream.ToArray();
                }
            }
           
            // registo do ultimo EstadoTrilho do trilho seleccionado
            EstadoTrilho ultimoEstadoTrilho = await _context.EstadoTrilhos.SingleOrDefaultAsync(uet => uet.TrilhoID == id
                                                                                                    && uet.DataFim == null);
            // 
            if (ultimoEstadoTrilho == null)
            {
                 return NotFound("POST: ultimoEstadoTrilho = NULL");
            }

            // EstadoTrilho a inserir (se novo Estado)
            EstadoTrilho novoEstadoTrilho = new EstadoTrilho
            {
                Trilho = trilho,
                EstadoID = VMTrilho.EstadoID,
                DataInicio = DateTime.Now
            };

            if (id != trilho.TrilhoID)
            {
                return NotFound("POST: TrilhoID NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Update dbo.trilhos
                    _context.Update(trilho);

                    // Update + insert dbo.EstadoTrilhos
                    if (ultimoEstadoTrilho.EstadoID != VMTrilho.EstadoID)
                    {
                        ultimoEstadoTrilho.DataFim = DateTime.Now;
                        _context.Update(ultimoEstadoTrilho);
                        _context.Add(novoEstadoTrilho);

                        //return NotFound("POST: VMTrilho.EstadoIdAnterior != VMTrilho.EstadoID");
                    }
                    
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrilhoExists(trilho.TrilhoID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            // se modelo inválido fica na mma view com os dados introduzidos no form
            ViewData["DificuldadeID"] = new SelectList(_context.Dificuldades, "DificuldadeID", "Nome", VMTrilho.DificuldadeID);
            ViewData["EstadoID"] = new SelectList(_context.Estados, "EstadoID", "Nome", VMTrilho.EstadoID);
            return View(VMTrilho);
        }

        // GET: Desativar
        [Authorize(Roles = "Administrador, Professor")]
        public async Task<IActionResult> Desativar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trilho = await _context.Trilhos
                .Include(t => t.Dificuldade)
                .SingleOrDefaultAsync(m => m.TrilhoID == id);
            if (trilho == null)
            {
                return NotFound();
            }

            return View(trilho);
        }

        // POST: Desativar
        [HttpPost, ActionName("Desativar")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Professor")]
        public async Task<IActionResult> DesativacaoConfirmada(int id)
        {
            var trilho = await _context.Trilhos.SingleOrDefaultAsync(m => m.TrilhoID == id);
            trilho.Desativado = true;
            //_context.Trilhos.Remove(trilho);
            _context.Trilhos.Update(trilho);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TrilhoExists(int id)
        {
            return _context.Trilhos.Any(e => e.TrilhoID == id);
        }
    }
}
