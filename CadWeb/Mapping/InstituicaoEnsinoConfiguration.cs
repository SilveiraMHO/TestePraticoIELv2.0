using CadWeb.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CadWeb.Mapping
{
    public class InstituicaoEnsinoConfiguration : IEntityTypeConfiguration<InstituicaoEnsino>
    {
        public void Configure(EntityTypeBuilder<InstituicaoEnsino> builder)
        {
            builder.ToTable("dom_instituicao_ensino");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id")
                .HasColumnType("INT")
                .IsRequired();
            builder.Property(x => x.Nome).HasColumnName("nome")
                .HasColumnType("NVARCHAR(100)")
                .IsRequired();
        }
    }
}
