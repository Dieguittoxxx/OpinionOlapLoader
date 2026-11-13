using Microsoft.Extensions.Configuration;
using OpinionOlapLoader.Common.Data;
using OpinionOlapLoader.Load.Base;
using OpinionOlapLoader.Load.Interfaces.Daos;
using OpinionOlapLoader.Load.Models;
using OpinionOlapLoader.Load.Staging;
using System.Data;

namespace OpinionOlapLoader.Load.Daos;

public sealed class DaoFactOpinion(IConfiguration configuration)
    : OpinionOlapConnection(configuration), IDaoFactOpinion
{
    public Task LimpiarStagingAsync()
        => ExecuteNonQueryAsync("dbo.SP_LimpiarStagingFactOpinion", command => { });

    public Task CargarStagingAsync(DataTable staging)
        => BulkInsertAsync(
            EsquemasStaging.FactOpinion.NombreTabla,
            staging,
            EsquemasStaging.FactOpinion.Mapeos);

    public Task<List<OpinionCargada>> CargarDesdeStagingAsync()
    {
        return ExecuteReaderAsync(
            "dbo.SP_CargarFactOpinionDesdeStaging",
            command => { },
            reader => new OpinionCargada
            {
                IdOpinion = reader.GetInt32("IdOpinion"),
                IdOpinionOrigen = reader.GetString("IdOpinionOrigen"),
                IdFuente = reader.GetInt32("IdFuente")
            });
    }
}
