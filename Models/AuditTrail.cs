using System.ComponentModel.DataAnnotations;

namespace LendingProcessingSystem.Models;

public class AuditTrail
{
    [Key]
    public int Id { get; set; }
    public string? UserId { get; set; }
    public string? UserEmail { get; set; }
    public string EntityName { get; set; } = string.Empty;
    public int? EntityId { get; set; }
    public string ActionType { get; set; } = string.Empty;
    public int? CaseId { get; set; }
    public int? WorkflowStepId { get; set; }
    public string? OldValueJson { get; set; }
    public string? NewValueJson { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }
    public string? Source { get; set; }
    public string? IpAddress { get; set; }
}
