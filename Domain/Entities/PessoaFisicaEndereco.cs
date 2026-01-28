namespace Domain.Entities
{
    public class PessoaFisicaEndereco : Entity
    {
        public Guid PessoaFisicaId { get; private set; }
        public PessoaFisica PessoaFisica { get; private set; }

        public Guid EnderecoId { get; private set; }
        public Endereco Endereco { get; private set; }

        protected PessoaFisicaEndereco() { }

        public PessoaFisicaEndereco(Guid pessoaFisicaId, Guid enderecoId)
        {
            PessoaFisicaId = pessoaFisicaId;
            EnderecoId = enderecoId;
        }
    }
}
