using OpinionOlapLoader.Extract.Models;

namespace OpinionOlapLoader.Extract.Interfaces.Daos;

public interface IDaoExtractOltp
{
    Task<List<ClienteOltp>> ObtenerClientesAsync();
    Task<List<ProductoOltp>> ObtenerProductosAsync();
    Task<List<OpinionConsolidadaOltp>> ObtenerOpinionesConsolidadasAsync();
}
