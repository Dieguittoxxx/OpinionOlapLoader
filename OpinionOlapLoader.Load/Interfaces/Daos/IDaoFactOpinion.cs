using OpinionOlapLoader.Load.Models;
using System.Data;

namespace OpinionOlapLoader.Load.Interfaces.Daos;

public interface IDaoFactOpinion
{
    Task LimpiarStagingAsync();
    Task CargarStagingAsync(DataTable staging);
    Task<List<OpinionCargada>> CargarDesdeStagingAsync();
}