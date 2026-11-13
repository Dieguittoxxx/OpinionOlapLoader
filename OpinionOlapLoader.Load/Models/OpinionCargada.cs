namespace OpinionOlapLoader.Load.Models;

public sealed class OpinionCargada
{
    public int IdOpinion { get; set; }
    public string IdOpinionOrigen { get; set; } = string.Empty;
    public int IdFuente { get; set; }
}