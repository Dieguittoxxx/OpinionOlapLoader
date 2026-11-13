using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpinionOlapLoader.Extract.Daos;
using OpinionOlapLoader.Extract.Interfaces.Daos;
using OpinionOlapLoader.Load.Cache;
using OpinionOlapLoader.Load.Daos;
using OpinionOlapLoader.Load.Interfaces.Cache;
using OpinionOlapLoader.Load.Interfaces.Daos;
using OpinionOlapLoader.Load.Interfaces.Nlp;
using OpinionOlapLoader.Load.Interfaces.Services;
using OpinionOlapLoader.Load.Nlp;
using OpinionOlapLoader.Load.Services;

namespace OpinionOlapLoader.Host;

public static class AppHost
{
    public static IHost Build(string[] args)
    {
        var builder = Microsoft.Extensions.Hosting.Host.CreateApplicationBuilder(args);

        builder.Configuration
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

        RegistrarServicios(builder.Services);

        return builder.Build();
    }

    private static void RegistrarServicios(IServiceCollection services)
    {
        services.AddScoped<IDaoExtractOltp, DaoExtractOltp>();

        services.AddScoped<IDaoMergeDimensiones, DaoMergeDimensiones>();
        services.AddScoped<IDaoDimensionKey, DaoDimensionKey>();
        services.AddScoped<IDaoFactOpinion, DaoFactOpinion>();
        services.AddScoped<IDaoFactPalabraClaveOpinion, DaoFactPalabraClaveOpinion>();

        services.AddScoped<IDimensionKeyCache, DimensionKeyCache>();

        services.AddSingleton<ITokenizadorComentarios, TokenizadorComentarios>();

        services.AddScoped<IServiceMergeDimensiones, ServiceMergeDimensiones>();
        services.AddScoped<IServiceCargaFactOpinion, ServiceCargaFactOpinion>();
        services.AddScoped<IServiceCargaFactPalabraClaveOpinion, ServiceCargaFactPalabraClaveOpinion>();
        services.AddScoped<IServiceLoadOlap, ServiceLoadOlap>();
    }
}
