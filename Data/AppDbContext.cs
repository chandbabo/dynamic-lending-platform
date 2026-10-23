using LendingProcessingSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LendingProcessingSystem.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<UserResetToken> UserResetTokens { get; set; }
    public DbSet<AuditTrail> AuditTrails { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Workflow> Workflows { get; set; }
    public DbSet<WorkflowStep> WorkflowSteps { get; set; }
    public DbSet<CaseRecord> Cases { get; set; }
    public DbSet<ReportDefinition> ReportDefinitions { get; set; }
    public DbSet<ReportColumn> ReportColumns { get; set; }
    public DbSet<ReportFilter> ReportFilters { get; set; }
}
