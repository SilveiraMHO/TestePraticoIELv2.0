using CadWeb.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CadWeb.Mapping
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("cad_app_user");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id")
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();
            builder.Property(x => x.Usuario).HasColumnName("usuario")
                .HasColumnType("NVARCHAR(20)")
                .IsRequired();
            builder.Property(x => x.SenhaHash).HasColumnName("senha_hash")
                .HasColumnType("NVARCHAR(100)")
                .IsRequired();
            builder.Property(x => x.Salt).HasColumnName("salt")
                .HasColumnType("NVARCHAR(50)")
                .IsRequired();
            builder.Property(x => x.DataCriacao).HasColumnName("data_criacao")
                .HasColumnType("DATETIME")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
