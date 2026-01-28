using Application.Services;
using Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public static class ServiceDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IPessoaFisicaService, PessoaFisicaService>();
            services.AddScoped<IPessoaJuridicaService, PessoaJuridicaService>();
            services.AddScoped<IEnderecoService, EnderecoService>();
            services.AddScoped<IPessoaFisicaEnderecoService, PessoaFisicaEnderecoService>();

            return services;
        }
    }
}
