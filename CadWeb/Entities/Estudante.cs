namespace CadWeb.Entities
{
    public class Estudante (string cpf, string nome, string endereco, int estadoId, string cidade, string nomeCurso, DateTime dataConclusao, int instituicaoEnsinoId)
    {
        public string Cpf { get; set; } = cpf;
        public string Nome { get; set; } = nome;
        public string Endereco { get; set; } = endereco;
        public int EstadoId { get; set; } = estadoId;
        public string Cidade { get; set; } = cidade;
        public string NomeCurso { get; set; } = nomeCurso;
        public DateTime DataConclusao { get; set; } = dataConclusao;
        public int InstituicaoEnsinoId { get; set; } = instituicaoEnsinoId;
        public virtual Estado Estado { get; set; }
        public virtual InstituicaoEnsino InstituicaoEnsino { get; set; }
    }
}
