namespace OpinionOlapLoader.Load.Interfaces.Cache;

public interface IDimensionKeyCache
{
    Task<int> ObtenerIdFuenteAsync(string tipoFuente);

    Task<int> ObtenerIdFuentePorOrigenAsync(string origen);

    Task<int?> ObtenerIdClasificacionAsync(string? descripcion);

    int ObtenerIdFecha(DateOnly fecha);
}