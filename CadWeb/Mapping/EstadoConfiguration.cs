using CadWeb.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CadWeb.Mapping
{
    public class EstadoConfiguration : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
            builder.ToTable("dom_estado");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id")
                .HasColumnType("INT")
                .IsRequired();
            builder.Property(x => x.Nome).HasColumnName("nome")
                .HasColumnType("NVARCHAR(50)")
                .IsRequired();
            builder.Property(x => x.UF).HasColumnName("UF")
                .HasColumnType("NCHAR(2)")
                .IsRequired();
        }
    }
}
