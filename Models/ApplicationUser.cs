using System.ComponentModel.DataAnnotations;

namespace LendingProcessingSystem.Models;

public class ApplicationUser : Microsoft.AspNetCore.Identity.IdentityUser
{
    [MaxLength(100)]
    public string? FirstName { get; set; }

    [MaxLength(100)]
    public string? LastName { get; set; }
}
