using LeetCodePracticeCompanion.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace LeetCodePracticeCompanion.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Problem> Problems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Problem>()
            .Property(p => p.Difficulty)
            .HasConversion<string>();
    }
}
