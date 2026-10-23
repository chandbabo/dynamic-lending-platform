using System.ComponentModel.DataAnnotations;

namespace LendingProcessingSystem.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Status { get; set; } = 1;
    public int? WorkflowId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
