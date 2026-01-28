using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("Endereco");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Cep).HasMaxLength(10).IsRequired();
            builder.Property(e => e.Logradouro).HasMaxLength(150);
            builder.Property(e => e.Complemento).HasMaxLength(50);
            builder.Property(e => e.Unidade).HasMaxLength(20);
            builder.Property(e => e.Bairro).HasMaxLength(100);
            builder.Property(e => e.Localidade).HasMaxLength(100);
            builder.Property(e => e.Uf).HasMaxLength(2);
            builder.Property(e => e.Estado).HasMaxLength(50);
            builder.Property(e => e.Regiao).HasMaxLength(50);
            builder.Property(e => e.Ddd).HasMaxLength(2);
        }
    }
}
