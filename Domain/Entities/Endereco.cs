namespace Domain.Entities
{
    public class Endereco : Entity
    {
        public string Cep { get; private set; }
        public string Logradouro { get; private set; }
        public string Complemento { get; private set; } = null!;
        public string Unidade { get; private set; } = null!;
        public string Bairro { get; private set; }
        public string Localidade { get; private set; }
        public string Uf { get; private set; }
        public string Estado { get; private set; }
        public string Regiao { get; private set; }
        public string Ddd { get; private set; }

        protected Endereco() { }

        public Endereco(
            string cep,
            string logradouro,
            string complemento,
            string unidade,
            string bairro,
            string localidade,
            string uf,
            string estado,
            string regiao,
            string ddd)
        {
            Cep = cep;
            Logradouro = logradouro;
            Complemento = complemento;
            Unidade = unidade;
            Bairro = bairro;
            Localidade = localidade;
            Uf = uf;
            Estado = estado;
            Regiao = regiao;
            Ddd = ddd;
        }
    }
}
