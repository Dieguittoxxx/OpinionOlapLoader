using Microsoft.Data.SqlClient.Server;
using Microsoft.Extensions.Configuration;
using OpinionOlapLoader.Common.Data;
using OpinionOlapLoader.Extract.Models;
using OpinionOlapLoader.Load.Base;
using OpinionOlapLoader.Load.Interfaces.Daos;
using System.Data;

namespace OpinionOlapLoader.Load.Daos;

public sealed class DaoMergeDimensiones(IConfiguration configuration)
    : OpinionOlapConnection(configuration), IDaoMergeDimensiones
{
    private static readonly SqlMetaData[] ColumnasClienteTvp =
    [
        new SqlMetaData("IdCliente", SqlDbType.Int),
        new SqlMetaData("Nombre", SqlDbType.VarChar, 100),
        new SqlMetaData("Email", SqlDbType.VarChar, 150)
    ];

    private static readonly SqlMetaData[] ColumnasProductoTvp =
    [
        new SqlMetaData("IdProducto", SqlDbType.Int),
        new SqlMetaData("Nombre", SqlDbType.VarChar, 100),
        new SqlMetaData("Categoria", SqlDbType.VarChar, 50)
    ];

    public Task MergeClientesAsync(IReadOnlyCollection<ClienteOltp> clientes)
    {
        return ExecuteNonQueryAsync(
            "dbo.SP_MergeDimCliente",
            command => AgregarParametroTvp(
                command,
                "@Clientes",
                "dbo.ClienteTVP",
                TvpBuilder.Construir(ColumnasClienteTvp, clientes, (registro, cliente) =>
                {
                    registro.SetInt32(0, cliente.IdCliente);
                    registro.SetString(1, cliente.Nombre);
                    registro.SetString(2, cliente.Email);
                })));
    }

    public Task MergeProductosAsync(IReadOnlyCollection<ProductoOltp> productos)
    {
        return ExecuteNonQueryAsync(
            "dbo.SP_MergeDimProducto",
            command => AgregarParametroTvp(
                command,
                "@Productos",
                "dbo.ProductoTVP",
                TvpBuilder.Construir(ColumnasProductoTvp, productos, (registro, producto) =>
                {
                    registro.SetInt32(0, producto.IdProducto);
                    registro.SetString(1, producto.Nombre);
                    registro.SetString(2, producto.Categoria);
                })));
    }
}
