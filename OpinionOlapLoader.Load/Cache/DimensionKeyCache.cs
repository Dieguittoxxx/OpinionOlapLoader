using OpinionOlapLoader.Load.Interfaces.Cache;
using OpinionOlapLoader.Load.Interfaces.Daos;

namespace OpinionOlapLoader.Load.Cache;

public sealed class DimensionKeyCache(IDaoDimensionKey daoDimensionKey) : IDimensionKeyCache
{
    private static readonly Dictionary<string, string> MapaOrigenATipoFuente = new(StringComparer.OrdinalIgnoreCase)
    {
        ["WebReview"] = "Web",
        ["SocialComment"] = "Red Social",
        ["Survey"] = "CSV"
    };

    private readonly IDaoDimensionKey _daoDimensionKey = daoDimensionKey;

    private Dictionary<string, int>? _fuentes;
    private Dictionary<string, int>? _clasificaciones;

    public async Task<int> ObtenerIdFuenteAsync(string tipoFuente)
    {
        _fuentes ??= await _daoDimensionKey.ObtenerFuentesAsync();

        if (!_fuentes.TryGetValue(tipoFuente, out var idFuente))
            throw new InvalidOperationException($"TipoFuente '{tipoFuente}' no existe en DimFuenteDatos.");

        return idFuente;
    }

    public Task<int> ObtenerIdFuentePorOrigenAsync(string origen)
    {
        if (!MapaOrigenATipoFuente.TryGetValue(origen, out var tipoFuente))
            throw new InvalidOperationException($"Origen '{origen}' no tiene mapeo a TipoFuente definido.");

        return ObtenerIdFuenteAsync(tipoFuente);
    }

    public async Task<int?> ObtenerIdClasificacionAsync(string? descripcion)
    {
        if (string.IsNullOrWhiteSpace(descripcion))
            return null;

        _clasificaciones ??= await _daoDimensionKey.ObtenerClasificacionesAsync();

        return _clasificaciones.TryGetValue(descripcion, out var idClasificacion)
            ? idClasificacion
            : null;
    }

    public int ObtenerIdFecha(DateOnly fecha)
        => fecha.Year * 10000 + fecha.Month * 100 + fecha.Day;
}
