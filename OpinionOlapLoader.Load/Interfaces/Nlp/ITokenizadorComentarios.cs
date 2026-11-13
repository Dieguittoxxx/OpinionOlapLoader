namespace OpinionOlapLoader.Load.Interfaces.Nlp;

public interface ITokenizadorComentarios
{
    Task InicializarAsync();
    Dictionary<string, int> TokenizarComentario(string comentario);
}
