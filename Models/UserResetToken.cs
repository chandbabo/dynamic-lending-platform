using System.ComponentModel.DataAnnotations;

namespace LendingProcessingSystem.Models;

public class UserResetToken
{
    [Key]
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiryAt { get; set; }
    public bool IsUsed { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
