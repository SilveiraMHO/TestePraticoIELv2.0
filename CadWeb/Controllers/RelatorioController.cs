using CadWeb.Context;
using CadWeb.Entities;
using CadWeb.Helpers;
using CadWeb.Models.Relatorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CadWeb.Controllers
{
    public class RelatorioController : Controller
    {
        private readonly AppDbContext _context;

        public RelatorioController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> EstudantesPorInstituicao(int? SelectedInstituicaoEnsinoId)
        {
            try
            {
                var estudantes = await _context.Estudante
                .Where(x => string.IsNullOrEmpty(SelectedInstituicaoEnsinoId.ToString()) || x.InstituicaoEnsinoId == SelectedInstituicaoEnsinoId)
                .ToListAsync();

                EstudantesPorInstituicaoFiltro relatorio = new EstudantesPorInstituicaoFiltro();
                relatorio.Estudantes = new List<EstudantesPorInstituicao>();

                relatorio.InstituicaoEnsinoSelectList = InstituicoesDeEnsinoDropDown();

                foreach (var estudante in estudantes)
                {
                    relatorio.Estudantes.Add(new EstudantesPorInstituicao()
                    {
                        Nome = estudante.Nome,
                        Cpf = CpfFormatter.FormatarCpf(estudante.Cpf),
                        NomeCurso = estudante.NomeCurso,
                        DataConclusao = estudante.DataConclusao.ToString("dd/MM/yyyy")
                    });
                }

                return View(relatorio);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index", "Home"); ;
            }
        }

        private List<SelectListItem> InstituicoesDeEnsinoDropDown()
        {
            List<SelectListItem> instituicoesEnsinoListItems = new List<SelectListItem>();

            instituicoesEnsinoListItems.Add(new SelectListItem()
            {
                Value = "0",
                Text = "Selecione..."
            });

            foreach (var item in _context.InstituicaoEnsino.ToList().OrderBy(x => x.Nome))
            {
                instituicoesEnsinoListItems.Add(new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Nome
                });
            }

            return instituicoesEnsinoListItems;
        }
    }
}
