using CadWeb.Context;
using CadWeb.Entities;
using CadWeb.Extensions;
using CadWeb.Helpers;
using CadWeb.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CadWeb.Controllers
{
    public class EstudanteController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IValidator<EstudanteViewModel> _validator;
        private readonly HttpClient _httpClient;

        public EstudanteController(AppDbContext context, IValidator<EstudanteViewModel> validator, HttpClient httpClient)
        {
            _context = context;
            _validator = validator;
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            IEnumerable<Estudante> estudantes = _context.Estudante.ToList();

            List<EstudanteViewModel> viewModel = new List<EstudanteViewModel>();

            foreach (Estudante estudante in estudantes)
            {
                viewModel.Add(new EstudanteViewModel
                {
                    Cpf = CpfFormatter.FormatarCpf(estudante.Cpf),
                    Nome = estudante.Nome,
                    Cidade = estudante.Cidade,
                    UF = estudante.Estado.UF,
                    NomeCurso = estudante.NomeCurso,
                    //InstituicaoEnsino = estudante.InstituicaoEnsino.Nome,
                    DataConclusaoString = estudante.DataConclusao.ToString("dd/MM/yyyy"),
                });
            }

            return View(viewModel);
        }

        public IActionResult Form(string id)
        {
            EstudanteViewModel viewModel = new EstudanteViewModel();
            viewModel.EstadoSelectList = EstadosDropDown();
            viewModel.CidadeSelectList = CidadesDropDown();
            viewModel.InstituicaoEnsinoSelectList = InstituicoesDeEnsinoDropDown();

            if (id == null)
            {
                return View(viewModel);
            }

            id = CpfFormatter.TirarFormatacaoCpf(id);

            Estudante estudante = _context.Estudante.SingleOrDefault(x => x.Cpf == id);

            string estudanteUF = estudante.Estado.UF;

            viewModel.Cpf = estudante.Cpf;
            viewModel.Nome = estudante.Nome;
            viewModel.Endereco = estudante.Endereco;
            viewModel.SelectedEstadoUF = estudanteUF;
            viewModel.SelectedCidadeValue = estudante.Cidade;
            viewModel.NomeCurso = estudante.NomeCurso;
            viewModel.DataConclusao = estudante.DataConclusao.ToString("yyyy-MM-dd");
            viewModel.SelectedInstituicaoEnsinoId = estudante.InstituicaoEnsinoId;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Form(EstudanteViewModel viewModel)
        {
            ValidationResult result = _validator.Validate(viewModel);

            if (!result.IsValid)
            {
                viewModel.EstadoSelectList = EstadosDropDown();
                viewModel.CidadeSelectList = CidadesDropDown();
                viewModel.InstituicaoEnsinoSelectList = InstituicoesDeEnsinoDropDown();

                result.AddToModelState(this.ModelState);
                return View(viewModel);
            }

            int estadoId = _context.Estado.FirstOrDefault(x => x.UF == viewModel.SelectedEstadoUF).Id;

            var estudanteExistente = _context.Estudante.SingleOrDefault(x => x.Cpf == CpfFormatter.TirarFormatacaoCpf(viewModel.Cpf));

            if (_context.Estudante.SingleOrDefault(x => x.Cpf == CpfFormatter.TirarFormatacaoCpf(viewModel.Cpf)) is null)
            {
                var novoEstudante = new Estudante(
                    cpf: CpfFormatter.TirarFormatacaoCpf(viewModel.Cpf),
                    nome: viewModel.Nome,
                    endereco: viewModel.Endereco,
                    estadoId: estadoId,
                    cidade: viewModel.SelectedCidadeValue,
                    nomeCurso: viewModel.NomeCurso,
                    dataConclusao: DateTime.ParseExact(viewModel.DataConclusao, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    instituicaoEnsinoId: viewModel.SelectedInstituicaoEnsinoId.Value
                );

                _context.Estudante.Add(novoEstudante);
            }
            else
            {
                estudanteExistente.Nome = viewModel.Nome;
                estudanteExistente.Endereco = viewModel.Endereco;
                estudanteExistente.EstadoId = estadoId;
                estudanteExistente.Cidade = viewModel.SelectedCidadeValue;
                estudanteExistente.NomeCurso = viewModel.NomeCurso;
                estudanteExistente.DataConclusao = DateTime.ParseExact(viewModel.DataConclusao, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                estudanteExistente.InstituicaoEnsinoId = viewModel.SelectedInstituicaoEnsinoId.Value;

                _context.Update(estudanteExistente);
            }

            _context.SaveChanges();

            TempData["success"] = "Salvo com sucesso.";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string id)
        {
            Estudante estudante = _context.Estudante.Find(CpfFormatter.TirarFormatacaoCpf(id));
            _context.Attach(estudante);
            _context.Remove(estudante);
            _context.SaveChanges();

            TempData["success"] = "Removido com sucesso.";
            return RedirectToAction("Index");
        }

        private IEnumerable<SelectListItem> EstadosDropDown()
        {
            List<SelectListItem> estadosListItems = new List<SelectListItem>();

            estadosListItems.Add(new SelectListItem()
            {
                Value = "0",
                Text = "Selecione..."
            });

            foreach (var item in _context.Estado.ToList())
            {
                estadosListItems.Add(new SelectListItem()
                {
                    Value = item.UF,
                    Text = item.Nome
                });
            }

            return estadosListItems;
        }

        private IEnumerable<SelectListItem> CidadesDropDown()
        {
            List<SelectListItem> cidadesListItems = new List<SelectListItem>();

            cidadesListItems.Add(new SelectListItem()
            {
                Value = "0",
                Text = "Selecione um estado..."
            });

            return cidadesListItems;
        }

        private IEnumerable<SelectListItem> InstituicoesDeEnsinoDropDown()
        {
            List<SelectListItem> instituicoesEnsinoListItems = new List<SelectListItem>();

            instituicoesEnsinoListItems.Add(new SelectListItem()
            {
                Value = "0",
                Text = "Selecione..."
            });

            foreach (var item in _context.InstituicaoEnsino.ToList())
            {
                instituicoesEnsinoListItems.Add(new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Nome
                });
            }

            return instituicoesEnsinoListItems;
        }

        [HttpGet]
        public async Task<IActionResult> GetCidadesPorEstado(string uf)
        {
            if (string.IsNullOrEmpty(uf))
                return BadRequest("UF não fornecida.");

            string url = $"https://servicodados.ibge.gov.br/api/v1/localidades/estados/{uf}/municipios";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, "Erro ao buscar cidades.");

                var responseJson = await response.Content.ReadFromJsonAsync<List<CidadeIbge>>();

                var cidadesSelectList = responseJson.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Nome
                });

                return Json(cidadesSelectList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
