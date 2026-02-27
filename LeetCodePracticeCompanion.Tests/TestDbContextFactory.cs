namespace LeetCodePracticeCompanion.Tests;

using LeetCodePracticeCompanion.Api.Data;
using Microsoft.EntityFrameworkCore;

public class TestDbContextFactory
{
    public static AppDbContext Create()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        
        return new AppDbContext(options);
    }
}