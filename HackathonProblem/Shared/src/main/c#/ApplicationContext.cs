// using Microsoft.EntityFrameworkCore;
// public class ApplicationContext : DbContext
// {
//     public DbSet<EmployeeDTO> Employees { get; set; } = null!;
//     public DbSet<TeamDTO> Teams { get; set; } = null!;
//     public DbSet<WishlistDTO> Wishlists { get; set; } = null!;
//     public DbSet<ExperimentDTO> Experiments { get; set; } = null!;
//     public ApplicationContext()
//     {
//         Database.EnsureCreated();
//     }
//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//     {
//         optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=hackathon;Username=postgres;Password=postgres");
//     }
// }