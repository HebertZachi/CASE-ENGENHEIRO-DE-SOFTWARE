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
            Endereco endereco
            )
        {
            RazaoSocial = razaoSocial;
            NomeFantasia = nomeFantasia;
            Cnpj = cnpj;
            Endereco = endereco;
        }

        public void Update(string razaoSocial, string nomeFantasia, string cnpj)
        {
            RazaoSocial = razaoSocial;
            NomeFantasia = nomeFantasia;
            Cnpj = cnpj;

            SetUpdated();
        }
    }
}
