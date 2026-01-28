namespace Domain.Entities
{
    public class PessoaJuridica : Entity
    {
        public string RazaoSocial { get; private set; }
        public string NomeFantasia { get; private set; }
        public string Cnpj { get; private set; }

        public Guid EnderecoId { get; private set; }
        public Endereco Endereco { get; private set; }

        protected PessoaJuridica() { }

        public PessoaJuridica(
            string razaoSocial,
            string nomeFantasia,
            string cnpj,
            Guid enderecoId)
        {
            RazaoSocial = razaoSocial;
            NomeFantasia = nomeFantasia;
            Cnpj = cnpj;
            EnderecoId = enderecoId;
        }
    }
}
