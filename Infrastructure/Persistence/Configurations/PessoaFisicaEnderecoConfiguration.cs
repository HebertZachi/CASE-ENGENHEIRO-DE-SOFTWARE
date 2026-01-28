using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Persistence.Configurations
{
    public class PessoaFisicaEnderecoConfiguration : IEntityTypeConfiguration<PessoaFisicaEndereco>
    {
        public void Configure(EntityTypeBuilder<PessoaFisicaEndereco> builder)
        {
            builder.ToTable("PessoaFisicaEndereco");

            builder.HasKey(e => new { e.PessoaFisicaId, e.EnderecoId });

            builder.HasOne(e => e.PessoaFisica)
                   .WithMany(p => p.Enderecos)
                   .HasForeignKey(e => e.PessoaFisicaId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Endereco)
                   .WithMany()
                   .HasForeignKey(e => e.EnderecoId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(e => e.EnderecoId);
        }
    }
}
