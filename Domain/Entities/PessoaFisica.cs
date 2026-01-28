namespace Domain.Entities
{
    public class PessoaFisica : Entity
    {
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public string Cpf { get; private set; }
        public DateTime DataNascimento { get; private set; }

        public ICollection<PessoaFisicaEndereco> Enderecos { get; private set; }
            = new List<PessoaFisicaEndereco>();

        protected PessoaFisica() { }

        public PessoaFisica(
            string nome,
            string sobrenome,
            string cpf,
            DateTime dataNascimento)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
        }
    }
}
