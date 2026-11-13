namespace OpinionOlapLoader.Load.Interfaces.Daos;

public interface IDaoDimensionKey
{
    Task<Dictionary<string, int>> ObtenerFuentesAsync();
    Task<Dictionary<string, int>> ObtenerClasificacionesAsync();
}