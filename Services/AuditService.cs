using System.Text.Json;
using LendingProcessingSystem.Data;
using LendingProcessingSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace LendingProcessingSystem.Services;

public interface IAuditService
{
    Task LogAsync(
        string entityName,
        int? entityId,
        string actionType,
        int? caseId = null,
        int? workflowStepId = null,
        object? oldValue = null,
        object? newValue = null,
        string? source = null,
        string? ipAddress = null);
}

public class AuditService : IAuditService
{
    private readonly AppDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuditService(AppDbContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task LogAsync(string entityName, int? entityId, string actionType, int? caseId = null, int? workflowStepId = null, object? oldValue = null, object? newValue = null, string? source = null, string? ipAddress = null)
    {
        var user = _httpContextAccessor.HttpContext is null ? null : await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

        var audit = new AuditTrail
        {
            UserId = user?.Id,
            UserEmail = user?.Email,
            EntityName = entityName,
            EntityId = entityId,
            ActionType = actionType,
            CaseId = caseId,
            WorkflowStepId = workflowStepId,
            OldValueJson = oldValue is null ? null : JsonSerializer.Serialize(oldValue),
            NewValueJson = newValue is null ? null : JsonSerializer.Serialize(newValue),
            CreatedBy = user?.Id,
            Source = source ?? "WebApp",
            IpAddress = ipAddress ?? _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString()
        };

        _context.AuditTrails.Add(audit);
        await _context.SaveChangesAsync();
    }
}
