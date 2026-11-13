using Microsoft.Extensions.Configuration;
using OpinionOlapLoader.Common.Data;
using OpinionOlapLoader.Load.Base;
using OpinionOlapLoader.Load.Interfaces.Daos;

namespace OpinionOlapLoader.Load.Daos;

public sealed class DaoDimensionKey(IConfiguration configuration)
    : OpinionOlapConnection(configuration), IDaoDimensionKey
{
    public async Task<Dictionary<string, int>> ObtenerFuentesAsync()
    {
        var filas = await ExecuteReaderAsync(
            "dbo.SP_ObtenerDimFuenteDatos",
            command => { },
            reader => (
                IdFuente: reader.GetInt32("IdFuente"),
                TipoFuente: reader.GetString("TipoFuente")));

        return filas.ToDictionary(x => x.TipoFuente, x => x.IdFuente, StringComparer.OrdinalIgnoreCase);
    }

    public async Task<Dictionary<string, int>> ObtenerClasificacionesAsync()
    {
        var filas = await ExecuteReaderAsync(
            "dbo.SP_ObtenerDimClasificacion",
            command => { },
            reader => (
                IdClasificacion: reader.GetInt32("IdClasificacion"),
                Descripcion: reader.GetString("Descripcion")));

        return filas.ToDictionary(x => x.Descripcion, x => x.IdClasificacion, StringComparer.OrdinalIgnoreCase);
    }
}
