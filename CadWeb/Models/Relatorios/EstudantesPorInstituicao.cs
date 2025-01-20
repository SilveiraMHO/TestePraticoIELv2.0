using CadWeb.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CadWeb.Models.Relatorios
{
    public class EstudantesPorInstituicaoFiltro
    {
        public int? SelectedInstituicaoEnsinoId { get; set; }
        public List<SelectListItem> InstituicaoEnsinoSelectList { get; set; }
        public List<EstudantesPorInstituicao> Estudantes { get; set; }
    }

    public class EstudantesPorInstituicao
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string NomeCurso { get; set; }
        public string DataConclusao { get; set; }
    }
}
