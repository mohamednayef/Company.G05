using System.Reflection;
using Company.G05.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.G05.DAL.Data.Contexts;

public class CompanyDbContext : DbContext
{
    public CompanyDbContext(DbContextOptions<CompanyDbContext> options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     // base.OnConfiguring(optionsBuilder);
    //     string connectionString = "Server=localhost,1433;Database=CompanyG05;User Id=sa;Password=StrongP@ssw0rd;Encrypt=False;TrustServerCertificate=True;";
    //     optionsBuilder.UseSqlServer(connectionString);
    // }
    public DbSet<Department> Departments { get; set; }
}