using System.Data;
using LendingProcessingSystem.Models;
using Microsoft.Data.SqlClient;

namespace LendingProcessingSystem.Services;

public interface IUserManagementService
{
    Task<List<UserProfile>> GetUsersAsync();
    Task CreateUserAsync(string userId, string email, string firstName, string lastName, string phone, string createdBy);
    Task ToggleUserStatusAsync(string userId, bool isActive);
}

public class UserManagementService : IUserManagementService
{
    private readonly IDbStoredProcedureService _db;

    public UserManagementService(IDbStoredProcedureService db)
    {
        _db = db;
    }

    public async Task<List<UserProfile>> GetUsersAsync()
    {
        var table = await _db.ExecuteReaderAsync("usp_UserProfile_List");
        var result = new List<UserProfile>();

        foreach (DataRow row in table.Rows)
        {
            result.Add(new UserProfile
            {
                Id = Convert.ToInt32(row["Id"]),
                UserId = row["UserId"]?.ToString() ?? string.Empty,
                Email = row["Email"]?.ToString() ?? string.Empty,
                FirstName = row["FirstName"]?.ToString(),
                LastName = row["LastName"]?.ToString(),
                Phone = row["Phone"]?.ToString(),
                IsActive = Convert.ToBoolean(row["IsActive"]),
                IsDeleted = Convert.ToBoolean(row["IsDeleted"]),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                CreatedBy = row["CreatedBy"]?.ToString(),
                LastLoginAt = row["LastLoginAt"] != DBNull.Value ? Convert.ToDateTime(row["LastLoginAt"]) : null
            });
        }

        return result;
    }

    public async Task CreateUserAsync(string userId, string email, string firstName, string lastName, string phone, string createdBy)
    {
        await _db.ExecuteNonQueryAsync(
            "usp_UserProfile_Create",
            ("@UserId", userId, SqlDbType.NVarChar),
            ("@Email", email, SqlDbType.NVarChar),
            ("@FirstName", firstName, SqlDbType.NVarChar),
            ("@LastName", lastName, SqlDbType.NVarChar),
            ("@Phone", phone, SqlDbType.NVarChar),
            ("@CreatedBy", createdBy, SqlDbType.NVarChar)
        );
    }

    public async Task ToggleUserStatusAsync(string userId, bool isActive)
    {
        await _db.ExecuteNonQueryAsync(
            "usp_UserProfile_SetActiveState",
            ("@UserId", userId, SqlDbType.NVarChar),
            ("@IsActive", isActive, SqlDbType.Bit)
        );
    }
}
