using Microsoft.EntityFrameworkCore;
using Shared.Model.DTO;

namespace HrDirector.Models.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<EmployeeDto> Employees { get; set; }
    public DbSet<WishlistDto> Wishlists { get; set; }
    public DbSet<TeamDto> Teams { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeDto>().ToTable("Employees");
        modelBuilder.Entity<TeamDto>().ToTable("Teams");
        modelBuilder.Entity<WishlistDto>().ToTable("Wishlists")
            .HasMany(w => w.PreferredEmployees)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "EmployeesInWishLists", // Название таблицы
                right => right.HasOne<EmployeeDto>().WithMany().HasForeignKey("EmployeeId"),
                left => left.HasOne<WishlistDto>().WithMany().HasForeignKey("WishlistId")
            );
    }
}

/*
SandBox:
Experiment: Id, Score
TeamSB: Id, ExperimentId, TeamLead, June <- ExperimentId - class
WishlistSB: Id, ExperimentId, EmployeeId, Array of EmployeeId <- ExperimentID - class
Employee: Id, Name, Role

HrDirector:
Team: Id, ExperimentId (int), TeamLead, June
Wishlist: Id, ExperimentId (int), EmployeeId, Array of EmployeeId
Employee: Id, Name, Role

HrManager:
Wishlist: Id, ExperimentId (int), EmployeeId, Array of EmployeeId
Employee: Id, Name, Role

Messaged:
RunningExperiment - from SandBox to HrDirector
StartingHackathon - from HrDirector to Employees and Sandbox?
GeneratingWishlists - from Employees to HrManager, HrDirector and Sandbox
GeneratingTeams - from HrManager to HrDirector and Sandbox
CalculatingScore - from HrDirector to SandBox
*/