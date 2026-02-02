namespace Domain.Entities
{
    public class PessoaFisicaEndereco : Entity
    {
        public Guid PessoaFisicaId { get; private set; }
        public PessoaFisica PessoaFisica { get; private set; }

        public Guid EnderecoId { get; private set; }
        public Endereco Endereco { get; private set; }

        protected PessoaFisicaEndereco() { }

        public PessoaFisicaEndereco(PessoaFisica pessoaFisica, Endereco endereco)
        {
            PessoaFisica = pessoaFisica;
            PessoaFisicaId = pessoaFisica.Id;

            Endereco = endereco;
            EnderecoId = endereco.Id;
        }
    }
}
