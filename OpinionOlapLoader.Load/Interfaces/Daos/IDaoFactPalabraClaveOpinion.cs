using System.Data;

namespace OpinionOlapLoader.Load.Interfaces.Daos;

public interface IDaoFactPalabraClaveOpinion
{
    Task<Dictionary<string, int>> ResolverPalabrasClaveAsync(IReadOnlyCollection<string> palabras);

    Task LimpiarStagingAsync();
    Task CargarStagingAsync(DataTable staging);
    Task CargarDesdeStagingAsync();
}