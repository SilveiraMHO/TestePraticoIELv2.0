namespace CadWeb.Entities
{
    public class AppUser
    {
        public Guid Id { get; set; }
        public string Usuario { get; set; }
        public string SenhaHash { get; set; }
        public string Salt { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
