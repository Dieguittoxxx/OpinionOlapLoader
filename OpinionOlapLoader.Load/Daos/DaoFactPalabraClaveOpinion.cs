using Microsoft.Data.SqlClient.Server;
using Microsoft.Extensions.Configuration;
using OpinionOlapLoader.Common.Data;
using OpinionOlapLoader.Load.Base;
using OpinionOlapLoader.Load.Interfaces.Daos;
using OpinionOlapLoader.Load.Models;
using OpinionOlapLoader.Load.Staging;
using System.Data;

namespace OpinionOlapLoader.Load.Daos;

public sealed class DaoFactPalabraClaveOpinion(IConfiguration configuration)
    : OpinionOlapConnection(configuration), IDaoFactPalabraClaveOpinion
{
    private static readonly SqlMetaData[] ColumnasPalabraClaveTvp =
    [
        new SqlMetaData("Palabra", SqlDbType.VarChar, 100)
    ];

    public async Task<Dictionary<string, int>> ResolverPalabrasClaveAsync(IReadOnlyCollection<string> palabras)
    {
        var resultado = await ExecuteReaderAsync(
            "dbo.SP_ResolverPalabrasClave",
            command => AgregarParametroTvp(
                command,
                "@Palabras",
                "dbo.PalabraClaveTVP",
                TvpBuilder.Construir(
                    ColumnasPalabraClaveTvp,
                    palabras,
                    (registro, palabra) => registro.SetString(0, palabra))),
            reader => new PalabraClaveResuelta
            {
                IdPalabraClave = reader.GetInt32("IdPalabraClave"),
                Palabra = reader.GetString("Palabra")
            });

        return resultado.ToDictionary(x => x.Palabra, x => x.IdPalabraClave, StringComparer.OrdinalIgnoreCase);
    }

    public Task LimpiarStagingAsync()
        => ExecuteNonQueryAsync("dbo.SP_LimpiarStagingFactPalabraClaveOpinion", command => { });

    public Task CargarStagingAsync(DataTable staging)
        => BulkInsertAsync(
            EsquemasStaging.FactPalabraClaveOpinion.NombreTabla,
            staging,
            EsquemasStaging.FactPalabraClaveOpinion.Mapeos);

    public Task CargarDesdeStagingAsync()
        => ExecuteNonQueryAsync("dbo.SP_CargarFactPalabraClaveOpinionDesdeStaging", command => { });
}
