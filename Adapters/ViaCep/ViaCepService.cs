using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using System.Net.Http.Json;

namespace Adapters.ViaCep
{
    public class ViaCepService : IViaCepService
    {
        private readonly HttpClient _httpClient;

        public ViaCepService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Endereco?> FindAddressByCep(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
                throw new ArgumentException("CEP inválido.", nameof(cep));

            var cepNumerico = new string(cep.Where(char.IsDigit).ToArray());
            var url = $"https://viacep.com.br/ws/{cepNumerico}/json/";

            try
            {
                var viacepResponse = await _httpClient.GetFromJsonAsync<ViaCepResponse>(url);

                if (viacepResponse == null || !string.IsNullOrEmpty(viacepResponse.Erro))
                {
                    throw new Exception($"CEP not found {cep}.");
                }

                var endereco = new Endereco(
                    cep: viacepResponse.Cep,
                    logradouro: viacepResponse.Logradouro,
                    complemento: viacepResponse.Complemento ?? "",
                    unidade: viacepResponse.Unidade ?? "",
                    bairro: viacepResponse.Bairro ?? "",
                    localidade: viacepResponse.Localidade ?? "",
                    uf: viacepResponse.Uf ?? "",
                    estado: viacepResponse.Localidade ?? "",
                    regiao: "",
                    ddd: viacepResponse.Ddd ?? ""
                );

                return endereco;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Fail to access ViaCEP {cep}. Details: {ex.Message}");
            }
        }

        private class ViaCepResponse
        {
            public string Cep { get; set; } = "";
            public string Logradouro { get; set; } = "";
            public string Complemento { get; set; } = "";
            public string Bairro { get; set; } = "";
            public string Localidade { get; set; } = "";
            public string Uf { get; set; } = "";
            public string Unidade { get; set; } = "";
            public string Ddd { get; set; } = "";
            public string Erro { get; set; } = "";
        }
    }
}
