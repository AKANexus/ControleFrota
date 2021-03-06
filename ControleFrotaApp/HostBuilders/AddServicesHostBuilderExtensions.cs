using ControleFrota.Services;
using ControleFrota.Services.DataServices;
//using DinkToPdf;
//using DinkToPdf.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ControleFrota.HostBuilders
{
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            host.ConfigureServices((_, serviços) =>
            {
                serviços.AddScoped<EnvioDataService>();

                serviços.AddScoped<VeículoDataService>();
                //serviços.AddScoped<MarcaDataService>();
                serviços.AddScoped<ModeloDataService>();
                serviços.AddScoped<MotoristaDataService>();
                serviços.AddScoped<ViagemDataService>();
                serviços.AddScoped<TipoGastoDataService>();
                serviços.AddScoped<AbastecimentoDataService>();
                serviços.AddScoped<SetorDataService>();
                serviços.AddScoped<ManutençãoDataService>();
                serviços.AddScoped<ManutençãoProgramadaDataService>();
                //serviços.AddSingleton(new SynchronizedConverter(new PdfTools()));
            });
            return host;
        }
    }
}
