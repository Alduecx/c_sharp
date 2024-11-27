using Microsoft.EntityFrameworkCore;
using SandBox.Models.DTO;

namespace SandBox.Models.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<ExperimentSb> Experiments { get; set; }
    public DbSet<EmployeeSb> Employees { get; set; }
    public DbSet<WishlistSb> Wishlists { get; set; }
    public DbSet<TeamSb> Teams { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExperimentSb>().ToTable("Experiments");
        modelBuilder.Entity<EmployeeSb>().ToTable("Employees");
        modelBuilder.Entity<TeamSb>().ToTable("Teams");
        modelBuilder.Entity<WishlistSb>().ToTable("Wishlists")
            .HasMany(w => w.PreferredEmployees)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "EmployeesInWishLists", // Название таблицы
                right => right.HasOne<EmployeeSb>().WithMany().HasForeignKey("EmployeeId"),
                left => left.HasOne<WishlistSb>().WithMany().HasForeignKey("WishlistId")
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