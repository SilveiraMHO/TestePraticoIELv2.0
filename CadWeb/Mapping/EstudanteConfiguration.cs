using CadWeb.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CadWeb.Mapping
{
    public class EstudanteConfiguration : IEntityTypeConfiguration<Estudante>
    {
        public void Configure(EntityTypeBuilder<Estudante> builder)
        {
            builder.ToTable("cad_estudante");
            builder.HasKey(x => x.Cpf);

            builder.Property(x => x.Cpf).HasColumnName("Cpf")
                .HasColumnType("NCHAR(11)")
                .IsRequired();
            builder.Property(x => x.Nome).HasColumnName("nome")
                .HasColumnType("NVARCHAR(100)")
                .IsRequired();
            builder.Property(x => x.Endereco).HasColumnName("endereco")
                .HasColumnType("NVARCHAR(200)")
                .IsRequired();
            builder.Property(x => x.EstadoId).HasColumnName("id_estado")
                .HasColumnType("INT")
                .IsRequired();
            builder.Property(x => x.Cidade).HasColumnName("cidade")
                .HasColumnType("NVARCHAR(50)")
                .IsRequired();
            builder.Property(x => x.NomeCurso).HasColumnName("nome_curso")
                .HasColumnType("NVARCHAR(100)")
                .IsRequired();
            builder.Property(x => x.DataConclusao).HasColumnName("data_conclusao")
                .HasColumnType("DATETIME")
                .IsRequired();
            builder.Property(x => x.InstituicaoEnsinoId).HasColumnName("id_instituicao_ensino")
                .HasColumnType("INT")
                .IsRequired();

            builder.HasOne(x => x.Estado).WithMany()
                .HasForeignKey(x => x.EstadoId)
                .IsRequired();
            builder.HasOne(x => x.InstituicaoEnsino).WithMany()
                .HasForeignKey(x => x.InstituicaoEnsinoId)
                .IsRequired();
        }
    }
}
