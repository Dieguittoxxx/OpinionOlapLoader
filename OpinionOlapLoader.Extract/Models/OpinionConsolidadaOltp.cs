namespace OpinionOlapLoader.Extract.Models;

public sealed class OpinionConsolidadaOltp
{
    public string Origen { get; set; } = string.Empty;
    public string IdOpinionOrigen { get; set; } = string.Empty;
    public int IdCliente { get; set; }
    public int IdProducto { get; set; }
    public DateOnly Fecha { get; set; }
    public string Comentario { get; set; } = string.Empty;
    public decimal? Puntaje { get; set; }
    public string? Clasificacion { get; set; }
}