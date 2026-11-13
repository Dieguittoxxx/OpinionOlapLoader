using OpinionOlapLoader.Extract.Models;

namespace OpinionOlapLoader.Load.Interfaces.Daos;

public interface IDaoMergeDimensiones
{
    Task MergeClientesAsync(IReadOnlyCollection<ClienteOltp> clientes);
    Task MergeProductosAsync(IReadOnlyCollection<ProductoOltp> productos);
}