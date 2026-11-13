using Microsoft.Data.SqlClient;

namespace OpinionOlapLoader.Common.Data;

public static class SqlDataReaderExtensions
{
    public static int GetInt32(this SqlDataReader reader, string columna)
        => reader.GetInt32(reader.GetOrdinal(columna));

    public static string GetString(this SqlDataReader reader, string columna)
        => reader.GetString(reader.GetOrdinal(columna));

    public static DateOnly GetDateOnly(this SqlDataReader reader, string columna)
        => DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal(columna)));

    public static decimal? GetDecimalOrNull(this SqlDataReader reader, string columna)
    {
        var ordinal = reader.GetOrdinal(columna);
        return reader.IsDBNull(ordinal) ? null : reader.GetDecimal(ordinal);
    }

    public static string? GetStringOrNull(this SqlDataReader reader, string columna)
    {
        var ordinal = reader.GetOrdinal(columna);
        return reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);
    }
}
