using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http.Json;

namespace Adapters.ViaCep
{
    public class ViaCepService : IViaCepService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ViaCepService> _logger;

        private const int maxRetries = 3;

        public ViaCepService(
            HttpClient httpClient,
            ILogger<ViaCepService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<Endereco?> FindAddressByCep(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
                throw new ArgumentException("Invalid CEP", nameof(cep));

            var cepNumerico = new string(cep.Where(char.IsDigit).ToArray());
            var url = $"https://viacep.com.br/ws/{cepNumerico}/json/";

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    _logger.LogInformation($"ViaCEP request attempt {attempt} for Cep {cepNumerico}");

                    using var response = await _httpClient.GetAsync(url);

                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        _logger.LogWarning($"Not found cep in ViaCEP {cepNumerico} ViaCEP");
                        return null;
                    }

                    response.EnsureSuccessStatusCode();

                    var viacepResponse = await response.Content.ReadFromJsonAsync<ViaCepResponse>();

                    if (viacepResponse == null || !string.IsNullOrEmpty(viacepResponse.Erro))
                    {
                        _logger.LogWarning($"ViaCEP returned an error for CEP: {cepNumerico}");
                        return null;
                    }

                    _logger.LogInformation($"ViaCEP Successful returned from ViaCEP to CEP: {cepNumerico}");

                    return new Endereco(
                        cep: viacepResponse.Cep,
                        logradouro: viacepResponse.Logradouro,
                        complemento: viacepResponse.Complemento,
                        unidade: viacepResponse.Unidade,
                        bairro: viacepResponse.Bairro,
                        localidade: viacepResponse.Localidade,
                        uf: viacepResponse.Uf,
                        estado: viacepResponse.Localidade,
                        regiao: viacepResponse.Regiao,
                        ddd: viacepResponse.Ddd
                    );
                }
                catch (TaskCanceledException ex) when (attempt < maxRetries)
                {
                    _logger.LogWarning($"Timeout ViaCEP attempt {attempt} for CEP {cepNumerico}", ex);
                }
                catch (HttpRequestException ex) when (attempt < maxRetries)
                {
                    _logger.LogWarning($"Fail HTTP ViaCEP attempt {attempt} for CEP {cepNumerico}", ex);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error Unexpected ViaCEP CEP {cepNumerico}",ex);
                    throw;
                }
            }

            _logger.LogError($"ViaCEP fail after {maxRetries} attempts para CEP {cepNumerico}");

            throw new Exception("The ViaCEP service is currently unavailable.");
        }

        private class ViaCepResponse
        {
            public string Cep { get; set; }
            public string Logradouro { get; set; }
            public string Complemento { get; set; }
            public string Bairro { get; set; }
            public string Localidade { get; set; }
            public string Uf { get; set; }
            public string Regiao { get; set; }
            public string Unidade { get; set; }
            public string Ddd { get; set; }
            public string Erro { get; set; }
        }
    }
}
