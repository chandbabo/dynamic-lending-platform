using System.Data;
using Microsoft.Data.SqlClient;

namespace LendingProcessingSystem.Services;

public interface IDbStoredProcedureService
{
    Task<int> ExecuteNonQueryAsync(string procedureName, params (string Name, object? Value, SqlDbType Type)[] parameters);
    Task<DataTable> ExecuteReaderAsync(string procedureName, params (string Name, object? Value, SqlDbType Type)[] parameters);
}

public class DbStoredProcedureService : IDbStoredProcedureService
{
    private readonly string _connectionString;

    public DbStoredProcedureService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public async Task<int> ExecuteNonQueryAsync(string procedureName, params (string Name, object? Value, SqlDbType Type)[] parameters)
    {
        await using var connection = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(procedureName, connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        foreach (var (name, value, type) in parameters)
        {
            command.Parameters.Add(name, type).Value = value ?? DBNull.Value;
        }

        await connection.OpenAsync();
        return await command.ExecuteNonQueryAsync();
    }

    public async Task<DataTable> ExecuteReaderAsync(string procedureName, params (string Name, object? Value, SqlDbType Type)[] parameters)
    {
        await using var connection = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(procedureName, connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        foreach (var (name, value, type) in parameters)
        {
            command.Parameters.Add(name, type).Value = value ?? DBNull.Value;
        }

        await connection.OpenAsync();

        using var reader = await command.ExecuteReaderAsync();
        var table = new DataTable();
        table.Load(reader);
        return table;
    }
}
