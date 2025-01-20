using Microsoft.AspNetCore.Mvc.Rendering;

namespace CadWeb.Models
{
    public class EstudanteViewModel
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string NomeCurso { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string InstituicaoEnsino { get; set; }
        public string DataConclusao { get; set; }
        public string DataConclusaoString { get; set; }
        public string SelectedEstadoUF { get; set; }
        public string SelectedCidadeValue { get; set; }
        public int? SelectedInstituicaoEnsinoId { get; set; }
        public IEnumerable<SelectListItem> EstadoSelectList { get; set; }
        public IEnumerable<SelectListItem> CidadeSelectList { get; set; }
        public IEnumerable<SelectListItem> InstituicaoEnsinoSelectList { get; set; }
    }
}
