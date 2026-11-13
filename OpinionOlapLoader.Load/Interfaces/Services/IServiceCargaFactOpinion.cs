using OpinionOlapLoader.Extract.Models;
using OpinionOlapLoader.Load.Models;

namespace OpinionOlapLoader.Load.Interfaces.Services;

public interface IServiceCargaFactOpinion
{
    Task<List<OpinionCargada>> EjecutarAsync(IReadOnlyCollection<OpinionConsolidadaOltp> opiniones);
}