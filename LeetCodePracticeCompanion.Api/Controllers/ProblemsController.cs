using LeetCodePracticeCompanion.Api.Data;
using LeetCodePracticeCompanion.Api.Models;
using LeetCodePracticeCompanion.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LeetCodePracticeCompanion.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProblemsController(ProblemRepository repository) : ControllerBase
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

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, Problem problem)
    {
        if (id != problem.Id)
            return BadRequest();

        var existing = repository.GetProblem(id);
        if (existing == null)
            return NotFound();

        repository.UpdateProblem(problem);
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