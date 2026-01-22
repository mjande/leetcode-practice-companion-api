using LeetCodePracticeCompanion.Api.Data;
using LeetCodePracticeCompanion.Api.Models;

namespace LeetCodePracticeCompanion.Api.Repositories;

public class ProblemRepository(AppDbContext context)
{
    public void AddProblem(Problem problem)
    {
        context.Problems.Add(problem);
        context.SaveChanges();
    }

    public List<Problem> GetAllProblems()
    {
        return context.Problems.OrderBy(p => p.DueDate).ToList();
    }
    
    public Problem? GetProblem(int problemId)
    {
        return context.Problems.Find(problemId);
    }

    public void UpdateProblem(Problem problem)
    {
        context.Problems.Update(problem);
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