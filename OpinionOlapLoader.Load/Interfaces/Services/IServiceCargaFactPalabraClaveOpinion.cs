using OpinionOlapLoader.Extract.Models;
using OpinionOlapLoader.Load.Models;

namespace OpinionOlapLoader.Load.Interfaces.Services;

public interface IServiceCargaFactPalabraClaveOpinion
{
    Task EjecutarAsync(
        IReadOnlyCollection<OpinionConsolidadaOltp> opinionesOrigen,
        IReadOnlyCollection<OpinionCargada> opinionesCargadas);
}