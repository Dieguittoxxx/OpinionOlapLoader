using Microsoft.Extensions.Configuration;
using OpinionOlapLoader.Common.Data;
using OpinionOlapLoader.Extract.Base;
using OpinionOlapLoader.Extract.Interfaces.Daos;
using OpinionOlapLoader.Extract.Models;

namespace OpinionOlapLoader.Extract.Daos;

public sealed class DaoExtractOltp(IConfiguration configuration)
    : OpinionOltpExtractConnection(configuration), IDaoExtractOltp
{
    public Task<List<ClienteOltp>> ObtenerClientesAsync()
    {
        return ExecuteReaderAsync(
            "dbo.SP_ObtenerCatalogoClientes",
            command => { },
            reader => new ClienteOltp
            {
                IdCliente = reader.GetInt32("IdCliente"),
                Nombre = reader.GetString("Nombre"),
                Email = reader.GetString("Email")
            });
    }

    public Task<List<ProductoOltp>> ObtenerProductosAsync()
    {
        return ExecuteReaderAsync(
            "dbo.SP_ObtenerCatalogoProductos",
            command => { },
            reader => new ProductoOltp
            {
                IdProducto = reader.GetInt32("IdProducto"),
                Nombre = reader.GetString("Nombre"),
                Categoria = reader.GetString("Categoria")
            });
    }

    public Task<List<OpinionConsolidadaOltp>> ObtenerOpinionesConsolidadasAsync()
    {
        return ExecuteReaderAsync(
            "dbo.SP_ObtenerOpinionesConsolidadas",
            command => { },
            reader => new OpinionConsolidadaOltp
            {
                Origen = reader.GetString("Origen"),
                IdOpinionOrigen = reader.GetString("IdOpinionOrigen"),
                IdCliente = reader.GetInt32("IdCliente"),
                IdProducto = reader.GetInt32("IdProducto"),
                Fecha = reader.GetDateOnly("Fecha"),
                Comentario = reader.GetString("Comentario"),
                Puntaje = reader.GetDecimalOrNull("Puntaje"),
                Clasificacion = reader.GetStringOrNull("Clasificacion")
            });
    }
}
