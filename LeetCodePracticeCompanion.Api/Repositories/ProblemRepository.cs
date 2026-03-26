using LeetCodePracticeCompanion.Api.Data;
using LeetCodePracticeCompanion.Api.Domain;
using LeetCodePracticeCompanion.Api.Models;

namespace LeetCodePracticeCompanion.Api.Repositories;

public interface IProblemRepository
{
    public void AddProblem(Problem problem);
    public List<Problem> GetAllProblems();
    public Problem? GetProblem(int problemId);
    public void SolveProblem(Problem problem, bool solvedWithoutHelp, bool solvedWithCorrectComplexity);
    public void UpdateProblem(Problem problem);
    public void DeleteProblem(int problemId);
}



public class ProblemRepository(AppDbContext context) : IProblemRepository
{
    public void AddProblem(Problem problem)
    {
        context.Problems.Add(problem);
        context.SaveChanges();
    }

    public List<Problem> GetAllProblems()
    {
        return context.Problems
            .ToList()
            .OrderBy(p => p.LastSolveDate?
                .AddMonths(p.IntervalMonths)
                .AddDays(p.IntervalDays))
            .ToList();
    }
    
    public Problem? GetProblem(int problemId)
    {
        return context.Problems.Find(problemId);
    }

    public void SolveProblem(Problem problem, bool solvedWithoutHelp, bool solvedWithCorrectComplexity)
    {
        problem.LastSolveDate = DateOnly.FromDateTime(DateTime.Today);

        var currentInterval = new Interval(problem.IntervalDays, problem.IntervalMonths);
        Interval? updatedInterval;
        if (!solvedWithoutHelp)
            updatedInterval = ProblemIntervals.GetPreviousInterval(problem.IntervalDays, problem.IntervalMonths);
        else if (solvedWithoutHelp && solvedWithCorrectComplexity)
            updatedInterval = ProblemIntervals.GetNextInterval(problem.IntervalDays, problem.IntervalMonths);
        else 
            updatedInterval = currentInterval;

        problem.IntervalDays = updatedInterval.Days;
        problem.IntervalMonths = updatedInterval.Months;
        
        context.Problems.Update(problem);
        context.SaveChanges();
    }

    public void UpdateProblem(Problem problem)
    {
        var existingProblem = context.Problems.Find(problem.Id);
        if (existingProblem == null)
            throw new KeyNotFoundException("Problem not found");
        
        existingProblem.Number = problem.Number;
        existingProblem.Name = problem.Name;
        existingProblem.Difficulty = problem.Difficulty;
        existingProblem.IntervalDays = problem.IntervalDays;
        existingProblem.IntervalMonths = problem.IntervalMonths;
        existingProblem.LastSolveDate = problem.LastSolveDate;
        existingProblem.Url = problem.Url;
        
        context.SaveChanges();
    }

    public void DeleteProblem(int problemId)
    {
        var problem = context.Problems.Find(problemId);
        if (problem == null)
            return;
        
        context.Problems.Remove(problem);
        context.SaveChanges();
    }
}