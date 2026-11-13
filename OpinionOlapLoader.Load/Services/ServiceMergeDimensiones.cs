using OpinionOlapLoader.Extract.Interfaces.Daos;
using OpinionOlapLoader.Load.Interfaces.Daos;
using OpinionOlapLoader.Load.Interfaces.Services;

namespace OpinionOlapLoader.Load.Services;

public sealed class ServiceMergeDimensiones(
    IDaoExtractOltp daoExtractOltp,
    IDaoMergeDimensiones daoMergeDimensiones) : IServiceMergeDimensiones
{
    private readonly IDaoExtractOltp _daoExtractOltp = daoExtractOltp;
    private readonly IDaoMergeDimensiones _daoMergeDimensiones = daoMergeDimensiones;

    public async Task EjecutarAsync()
    {
        var clientes = await _daoExtractOltp.ObtenerClientesAsync();
        await _daoMergeDimensiones.MergeClientesAsync(clientes);

        var productos = await _daoExtractOltp.ObtenerProductosAsync();
        await _daoMergeDimensiones.MergeProductosAsync(productos);
    }
}
