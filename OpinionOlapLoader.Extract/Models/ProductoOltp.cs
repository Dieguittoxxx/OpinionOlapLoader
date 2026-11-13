namespace OpinionOlapLoader.Extract.Models;

public sealed class ProductoOltp
{
    public int IdProducto { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
}