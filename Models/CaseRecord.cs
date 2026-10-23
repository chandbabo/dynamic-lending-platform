using System.ComponentModel.DataAnnotations;

namespace LendingProcessingSystem.Models;

public class CaseRecord
{
    [Key]
    public int Id { get; set; }
    public string CaseNumber { get; set; } = string.Empty;
    public string BorrowerName { get; set; } = string.Empty;
    public string? NationalId { get; set; }
    public int Age { get; set; }
    public decimal LoanAmount { get; set; }
    public int ProductId { get; set; }
    public string CurrentStage { get; set; } = "Received";
    public int Status { get; set; } = 0;
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
}
