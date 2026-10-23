using System.Data;

namespace LendingProcessingSystem.Services;

public interface IReportService
{
    Task<DataTable> ExecuteReportAsync(string storedProcedureName);
}

public class ReportService : IReportService
{
    private readonly IDbStoredProcedureService _dbStoredProcedureService;

    public ReportService(IDbStoredProcedureService dbStoredProcedureService)
    {
        _dbStoredProcedureService = dbStoredProcedureService;
    }

    public async Task<DataTable> ExecuteReportAsync(string storedProcedureName)
    {
        return await _dbStoredProcedureService.ExecuteReaderAsync(storedProcedureName);
    }
}
