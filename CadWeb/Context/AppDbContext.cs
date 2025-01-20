using CadWeb.Entities;
using Microsoft.EntityFrameworkCore;

namespace CadWeb.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<Estudante> Estudante { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<InstituicaoEnsino> InstituicaoEnsino { get; set; }

        //Método utilizado para a configuração de entidades no EF.
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Garante que qualquer configuração padrão no contexto do DbContext seja preservada.
            base.OnModelCreating(builder);

            //Será lido o Assembly do código (baixo nivel), pegar todas as classes que implementam o "IEntityTypeConfiguration" a aplicar em suas entidades.
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
