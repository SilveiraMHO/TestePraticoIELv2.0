namespace CadWeb.Helpers
{
    public static class CpfFormatter
    {
        public static string FormatarCpf(string cpf)
        {
            return $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}.{cpf.Substring(6, 3)}-{cpf.Substring(9, 2)}";
        }

        public static string TirarFormatacaoCpf(string cpfFormatado)
        {
            return cpfFormatado.Replace(".", "").Replace("-", "");
        }
    }
}
