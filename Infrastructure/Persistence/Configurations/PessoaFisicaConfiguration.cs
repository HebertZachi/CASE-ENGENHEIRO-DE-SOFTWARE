using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class PessoaFisicaConfiguration : IEntityTypeConfiguration<PessoaFisica>
    {
        public void Configure(EntityTypeBuilder<PessoaFisica> builder)
        {
            builder.ToTable("PessoaFisica");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Nome).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Sobrenome).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Cpf).IsRequired().HasMaxLength(11);
            builder.Property(e => e.DataNascimento).HasColumnType("date");

            builder.HasIndex(e => e.Cpf).IsUnique();
        }
    }
}
