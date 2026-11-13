namespace OpinionOlapLoader.Extract.Models;

public sealed class ClienteOltp
{
    public int IdCliente { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}