using OpinionOlapLoader.Extract.Interfaces.Daos;
using OpinionOlapLoader.Load.Interfaces.Services;

namespace OpinionOlapLoader.Load.Services;

public sealed class ServiceLoadOlap(
    IDaoExtractOltp daoExtractOltp,
    IServiceCargaFactOpinion serviceCargaFactOpinion,
    IServiceMergeDimensiones serviceMergeDimensiones,
    IServiceCargaFactPalabraClaveOpinion serviceCargaFactPalabraClaveOpinion) : IServiceLoadOlap
{
    private readonly IDaoExtractOltp _daoExtractOltp = daoExtractOltp;
    private readonly IServiceMergeDimensiones _serviceMergeDimensiones = serviceMergeDimensiones;
    private readonly IServiceCargaFactOpinion _serviceCargaFactOpinion = serviceCargaFactOpinion;
    private readonly IServiceCargaFactPalabraClaveOpinion _serviceCargaFactPalabraClaveOpinion = serviceCargaFactPalabraClaveOpinion;

    public async Task EjecutarCargaAsync()
    {
        await _serviceMergeDimensiones.EjecutarAsync();

        var opiniones = await _daoExtractOltp.ObtenerOpinionesConsolidadasAsync();

        var opinionesCargadas = await _serviceCargaFactOpinion.EjecutarAsync(opiniones);

        await _serviceCargaFactPalabraClaveOpinion.EjecutarAsync(opiniones, opinionesCargadas);
    }
}
