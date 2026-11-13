using Microsoft.Extensions.DependencyInjection;
using OpinionOlapLoader.Host;
using OpinionOlapLoader.Load.Interfaces.Services;

var host = AppHost.Build(args);

Console.WriteLine("Iniciando carga OLTP -> OLAP...");

using (var scope = host.Services.CreateScope())
{
    var serviceLoadOlap = scope.ServiceProvider.GetRequiredService<IServiceLoadOlap>();

    try
    {
        await serviceLoadOlap.EjecutarCargaAsync();
        Console.WriteLine("Proceso finalizado correctamente.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"El proceso finalizo con errores: {ex.Message}");
        throw;
    }
}