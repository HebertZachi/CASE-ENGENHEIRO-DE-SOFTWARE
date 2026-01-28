using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Persistence.Configurations
{
    public class PessoaJuridicaConfiguration : IEntityTypeConfiguration<PessoaJuridica>
    {
        public void Configure(EntityTypeBuilder<PessoaJuridica> builder)
        {
            builder.ToTable("PessoaJuridica");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.RazaoSocial).IsRequired().HasMaxLength(100);
            builder.Property(e => e.NomeFantasia).HasMaxLength(100);
            builder.Property(e => e.Cnpj).IsRequired().HasMaxLength(14);

            builder.HasOne(e => e.Endereco)
                   .WithMany()
                   .HasForeignKey(e => e.EnderecoId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(e => e.Cnpj).IsUnique();
            builder.HasIndex(e => e.RazaoSocial);
            builder.HasIndex(e => e.NomeFantasia);
        }
    }
}
