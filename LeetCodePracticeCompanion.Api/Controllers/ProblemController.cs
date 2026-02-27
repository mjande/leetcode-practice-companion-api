using LeetCodePracticeCompanion.Api.Models;
using LeetCodePracticeCompanion.Api.Models.Requests;
using LeetCodePracticeCompanion.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LeetCodePracticeCompanion.Api.Controllers;



[ApiController]
[Route("api/problems")]
public class ProblemController(IProblemRepository repository) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<Problem>> GetAll()
    {
        return repository.GetAllProblems();
    }

    [HttpGet("{id:int}")]
    public ActionResult<Problem> Get(int id)
    {
        var problem = repository.GetProblem(id);
        if (problem == null)
            return NotFound();
        return problem;
    }

    [HttpPost]
    public ActionResult<Problem> Create(Problem problem)
    {
        repository.AddProblem(problem);
        return CreatedAtAction(nameof(Get), new { id = problem.Id }, problem);
    }

    [HttpPost("{id:int}/solve")]
    public ActionResult Solve(int id, [FromBody] SolveProblemRequest request)
    {
        var problem = repository.GetProblem(id);
        if (problem == null)
            return NotFound();

        repository.SolveProblem(problem, request.SolvedWithoutHelp, request.SolvedWithCorrectComplexity);
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, Problem problem)
    {
        if (id != problem.Id)
            return BadRequest();

        try
        {
            repository.UpdateProblem(problem);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var problem = repository.GetProblem(id);
        if (problem == null)
            return NotFound();

        repository.DeleteProblem(id);
        return NoContent();
    }
}