using System.ComponentModel.DataAnnotations;

namespace LendingProcessingSystem.Models;

public class WorkflowStep
{
    [Key]
    public int Id { get; set; }
    public int WorkflowId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int StepType { get; set; } = 1;
    public string Description { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public string? ConditionExpression { get; set; }
    public int? ApiDefinitionId { get; set; }
    public string? RequestTemplate { get; set; }
    public string? ResponseMapping { get; set; }
    public int TimeoutSeconds { get; set; } = 60;
    public bool IsRequired { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
