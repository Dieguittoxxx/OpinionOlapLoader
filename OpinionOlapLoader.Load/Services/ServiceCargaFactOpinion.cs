using OpinionOlapLoader.Extract.Models;
using OpinionOlapLoader.Load.Interfaces.Cache;
using OpinionOlapLoader.Load.Interfaces.Daos;
using OpinionOlapLoader.Load.Interfaces.Services;
using OpinionOlapLoader.Load.Models;
using OpinionOlapLoader.Load.Staging;
using System.Data;

namespace OpinionOlapLoader.Load.Services;

public sealed class ServiceCargaFactOpinion(
    IDaoFactOpinion daoFactOpinion,
    IDimensionKeyCache dimensionKeyCache) : IServiceCargaFactOpinion
{
    private readonly IDaoFactOpinion _daoFactOpinion = daoFactOpinion;
    private readonly IDimensionKeyCache _dimensionKeyCache = dimensionKeyCache;

    public async Task<List<OpinionCargada>> EjecutarAsync(IReadOnlyCollection<OpinionConsolidadaOltp> opiniones)
    {
        var staging = await ConstruirStagingAsync(opiniones);

        await _daoFactOpinion.LimpiarStagingAsync();
        await _daoFactOpinion.CargarStagingAsync(staging);

        return await _daoFactOpinion.CargarDesdeStagingAsync();
    }

    private async Task<DataTable> ConstruirStagingAsync(IReadOnlyCollection<OpinionConsolidadaOltp> opiniones)
    {
        var staging = EsquemasStaging.FactOpinion.CrearTabla();

        foreach (var opinion in opiniones)
        {
            var idFuente = await _dimensionKeyCache.ObtenerIdFuentePorOrigenAsync(opinion.Origen);
            var idClasificacion = await _dimensionKeyCache.ObtenerIdClasificacionAsync(opinion.Clasificacion);
            var idFecha = _dimensionKeyCache.ObtenerIdFecha(opinion.Fecha);

            var fila = staging.NewRow();
            fila["IdFecha"] = idFecha;
            fila["IdCliente"] = opinion.IdCliente;
            fila["IdProducto"] = opinion.IdProducto;
            fila["IdFuente"] = idFuente;
            fila["IdClasificacion"] = (object?)idClasificacion ?? DBNull.Value;
            fila["Puntaje"] = (object?)opinion.Puntaje ?? DBNull.Value;
            fila["IdOpinionOrigen"] = opinion.IdOpinionOrigen;

            staging.Rows.Add(fila);
        }

        return staging;
    }
}
