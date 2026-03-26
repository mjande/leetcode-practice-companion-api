namespace LeetCodePracticeCompanion.Tests.Repositories;

using LeetCodePracticeCompanion.Api.Models;
using LeetCodePracticeCompanion.Api.Repositories;
using Xunit;

public class ProblemRepositoryTests
{
    [Fact]
    public void AddProblem_AddsProblemToDatabase()
    {
        using var context = TestDbContextFactory.Create();
        var repo = new ProblemRepository(context);

        var problem = new Problem
        {
            Number = 1,
            Name = "TestProblem",
            Difficulty = DifficultyOptions.Medium,
            Url = "https://www.leetcode.com",
        };

        repo.AddProblem(problem);

        Assert.Single(context.Problems);
    }

    [Fact]
    public void GetAllProblems_ReturnsAllProblemsOrderedByDueDate()
    {
        using var context = TestDbContextFactory.Create();
        var repo = new ProblemRepository(context);

        context.Problems.AddRange(
            new Problem
            {
                Number = 1,
                Name = "EarlierProblem",
                Difficulty = DifficultyOptions.Medium,
                Url = "https://www.leetcode.com",
                LastSolveDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-1)),
                IntervalDays = 1,
            },
            new Problem
            {
                Number = 1,
                Name = "LaterProblem",
                Difficulty = DifficultyOptions.Medium,
                Url = "https://www.leetcode.com",
                LastSolveDate = DateOnly.FromDateTime(DateTime.Today),
                IntervalDays = 1,
            }
        );
        context.SaveChanges();

        var result = repo.GetAllProblems();

        Assert.Equal(2, result.Count());
        Assert.True(result[0].Name == "EarlierProblem");
        Assert.True(result[1].Name == "LaterProblem");
    }

    [Fact]
    public void GetProblem_ReturnsCorrectProblem_WhenExists()
    {
        using var context = TestDbContextFactory.Create();
        var repo = new ProblemRepository(context);

        var problem = new Problem
        {
            Number = 1,
            Name = "TestProblem",
            Difficulty = DifficultyOptions.Medium,
            Url = "https://www.leetcode.com",
        };

        context.Problems.Add(problem);
        context.SaveChanges();
        
        var fetched = repo.GetProblem(problem.Id);

        Assert.NotNull(fetched);
        Assert.Equal(problem.Name, fetched.Name);
        Assert.Equal(problem.Difficulty, fetched.Difficulty);
        Assert.Equal(problem.Url, fetched.Url);
    }

    [Fact]
    public void SolveProblem_PerfectSolveDoublesIntervalAndUpdatesDates()
    {
        using var context = TestDbContextFactory.Create();
        var repo = new ProblemRepository(context);

        var problem = new Problem
        {
            Number = 1,
            Name = "TestProblem",
            Difficulty = DifficultyOptions.Medium,
            IntervalDays = 2,
            Url = "https://www.leetcode.com",
        };

        context.Problems.Add(problem);
        context.SaveChanges();

        repo.SolveProblem(problem, true, true);

        var updated = context.Problems.Find(problem.Id);

        Assert.NotNull(updated);
        Assert.Equal(4, updated.IntervalDays);
        Assert.Equal(
            DateOnly.FromDateTime(DateTime.Today),
            updated.LastSolveDate
        );
    }

    [Fact]
    public void SolveProblem_SolvedWithHelpHalvesInterval()
    {
        using var context = TestDbContextFactory.Create();
        var repo = new ProblemRepository(context);

        var problem = new Problem
        {
            Number = 1,
            Name = "TestProblem",
            Difficulty = DifficultyOptions.Medium,
            IntervalDays = 2,
            Url = "https://www.leetcode.com",
        };

        context.Problems.Add(problem);
        context.SaveChanges();

        repo.SolveProblem(problem, false, true);

        var updated = context.Problems.Find(problem.Id);

        Assert.NotNull(updated);
        Assert.Equal(1, updated.IntervalDays);
        Assert.Equal(
            DateOnly.FromDateTime(DateTime.Today),
            updated.LastSolveDate
        );
    }

    [Fact]
    public void SolveProblem_SolvedWithIncorrectComplexityKeepsSameInterval()
    {
        using var context = TestDbContextFactory.Create();
        var repo = new ProblemRepository(context);

        var problem = new Problem
        {
            Number = 1,
            Name = "TestProblem",
            Difficulty = DifficultyOptions.Medium,
            IntervalDays = 2,
            Url = "https://www.leetcode.com",
        };

        context.Problems.Add(problem);
        context.SaveChanges();

        repo.SolveProblem(problem, true, false);
        var updated = context.Problems.Find(problem.Id);
        
        Assert.NotNull(updated);
        Assert.Equal(2, updated.IntervalDays);
        Assert.Equal(DateOnly.FromDateTime(DateTime.Today), updated.LastSolveDate);
    }
    
    [Fact]
    public void UpdateProblem_UpdatesProblem_WhenExists()
    {
        using var context = TestDbContextFactory.Create();
        var repo = new ProblemRepository(context);

        var problem = new Problem
        {
            Number = 1,
            Name = "TestProblem",
            Difficulty = DifficultyOptions.Medium,
            Url = "https://www.leetcode.com",
        };
        
        context.Problems.Add(problem);
        context.SaveChanges();
        
        problem.Name = "ChangedTestProblem";
        problem.Difficulty = DifficultyOptions.Easy;
        repo.UpdateProblem(problem);
        
        var updated = context.Problems.Find(problem.Id);
        
        Assert.NotNull(updated);
        Assert.Equal("ChangedTestProblem", updated.Name);
        Assert.Equal(DifficultyOptions.Easy, updated.Difficulty);
    }

    [Fact]
    public void DeleteProblem_DeletesProblem_WhenExists()
    {
        using var context = TestDbContextFactory.Create();
        var repo = new ProblemRepository(context);

        var problem = new Problem
        {
            Number = 1,
            Name = "TestProblem",
            Difficulty = DifficultyOptions.Medium,
            Url = "https://www.leetcode.com",
        };
        
        context.Problems.Add(problem);
        context.SaveChanges();
        
        repo.DeleteProblem(problem.Id);
        
        var deleted = context.Problems.Find(problem.Id);
        Assert.Null(deleted);
    }
}