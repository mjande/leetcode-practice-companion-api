using LeetCodePracticeCompanion.Api.Models.Requests;
using Microsoft.AspNetCore.WebUtilities;

namespace LeetCodePracticeCompanion.Tests.Controllers;

using LeetCodePracticeCompanion.Api.Controllers;
using LeetCodePracticeCompanion.Api.Models;
using LeetCodePracticeCompanion.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class ProblemControllerTests
{
    [Fact]
    public void GetAll_ReturnsListOfProblems()
    {
        var mockRepo = new Mock<IProblemRepository>();
        mockRepo.Setup(r => r.GetAllProblems())
            .Returns([
                new Problem
                {
                    Number = 1,
                    Name = "TestProblem1",
                    Difficulty = DifficultyOptions.Medium,
                    Url = "https://www.leetcode.com",
                },
                new Problem
                {
                    Number = 2,
                    Name = "TestProblem2",
                    Difficulty = DifficultyOptions.Easy,
                    Url = "https://www.leetcode.com",
                }
            ]);
        
        var controller = new ProblemController(mockRepo.Object);
        
        var result = controller.GetAll();
        
        var actionResult = Assert.IsType<ActionResult<List<Problem>>>(result);
        var returnValue = Assert.IsType<List<Problem>>(actionResult.Value);
        
        Assert.Equal(2, returnValue.Count);
        Assert.Equal("TestProblem1", returnValue[0].Name);
    }

    [Fact]
    public void Get_ReturnsProblem_WhenExists()
    {
        var mockRepo = new Mock<IProblemRepository>();
        var problem = new Problem
        {
            Number = 1,
            Name = "TestProblem",
            Difficulty = DifficultyOptions.Medium,
            Url = "https://www.leetcode.com",
        };
        
        mockRepo.Setup(r => r.GetProblem(problem.Id)).Returns(problem);
        
        var controller = new ProblemController(mockRepo.Object);
        var result = controller.Get(problem.Id);
        
        var actionResult = Assert.IsType<ActionResult<Problem>>(result);
        var returnValue = Assert.IsType<Problem>(actionResult.Value);
        
        Assert.Equal(1, returnValue.Number);
        Assert.Equal("TestProblem", returnValue.Name);
    }

    [Fact]
    public void Create_CreatesProblem()
    {
        var mockRepo = new Mock<IProblemRepository>();
        var controller = new ProblemController(mockRepo.Object);

        var newProblem = new Problem
        {
            Number = 1,
            Name = "TestProblem",
            Difficulty = DifficultyOptions.Medium,
            Url = "https://www.leetcode.com",
        };
        
        var result = controller.Create(newProblem);
        
        mockRepo.Verify(r => r.AddProblem(newProblem), Times.Once);
        
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(nameof(controller.Get), createdResult.ActionName);
        
        var returnProblem = Assert.IsType<Problem>(createdResult.Value);
        Assert.Equal(1, returnProblem.Number);
        Assert.Equal("TestProblem", returnProblem.Name);
        Assert.Equal(DifficultyOptions.Medium, returnProblem.Difficulty);
        Assert.Equal("https://www.leetcode.com", returnProblem.Url);
    }
    
    [Fact]
    public void Solve_ReturnsNoContent_WhenProblemExists()
    {
        var mockRepo = new Mock<IProblemRepository>();
        var problem = new Problem
        {
            Number = 1,
            Name = "TestProblem",
            Difficulty = DifficultyOptions.Medium,
            Url = "https://www.leetcode.com",
        };
        
        mockRepo
            .Setup(r => r.GetProblem(problem.Id))
            .Returns(problem);
        
        var controller = new ProblemController(mockRepo.Object);

        var request = new SolveProblemRequest
        {
            SolvedWithoutHelp = true,
            SolvedWithCorrectComplexity = true,
        };

        var result = controller.Solve(problem.Id, request);
        
        Assert.IsType<NoContentResult>(result);
        
        mockRepo.Verify(
            r => 
                r.SolveProblem(problem, true, true), Times.Once);
    }
    
    [Fact]
    public void Solve_ReturnsNotFound_WhenProblemDoesNotExist()
    {
        var mockRepo = new Mock<IProblemRepository>();
        
        mockRepo
            .Setup(r => r.GetProblem(1))
            .Returns((Problem?)null);

        var controller = new ProblemController(mockRepo.Object);
        
        var request = new SolveProblemRequest
        {
            SolvedWithoutHelp = true,
            SolvedWithCorrectComplexity = true,
        };
        
        var result = controller.Solve(1, request);
        
        Assert.IsType<NotFoundResult>(result);
        
        mockRepo.Verify(r =>
            r.SolveProblem(It.IsAny<Problem>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Never);
    }

    [Fact]
    public void Update_ReturnsBadRequest_WhenIdsDoNotMatch()
    {
        var mockRepo = new Mock<IProblemRepository>();
        var controller =  new ProblemController(mockRepo.Object);

        var problem = new Problem
        {
            Id = 2,
            Number = 1,
            Name = "TestProblem",
            Difficulty = DifficultyOptions.Medium,
            Url = "https://www.leetcode.com",
        };

        var result = controller.Update(1, problem);
        
        Assert.IsType<BadRequestResult>(result);
        
        mockRepo.Verify(r => r.UpdateProblem(It.IsAny<Problem>()), Times.Never);
    }
    
    [Fact]
    public void Update_ReturnsNotFound_WhenProblemDoesNotExist()
    {
        var mockRepo = new Mock<IProblemRepository>();
        var controller = new ProblemController(mockRepo.Object);
        var problem = new Problem
        {
            Number = 1,
            Name = "TestProblem",
            Difficulty = DifficultyOptions.Medium,
            Url = "https://www.leetcode.com",
        };

        mockRepo
            .Setup(r => r.UpdateProblem(problem))
            .Throws(new KeyNotFoundException());
        
        var result = controller.Update(problem.Id, problem);
        
        Assert.IsType<NotFoundResult>(result);
        
        mockRepo.Verify(r => r.UpdateProblem(It.IsAny<Problem>()), Times.Once);
    }

    [Fact]
    public void Update_ReturnsNoContent()
    {
        var mockRepo = new Mock<IProblemRepository>();
        var controller = new ProblemController(mockRepo.Object);
        var existing = new Problem
        {
            Number = 1,
            Name = "TestProblem",
            Difficulty = DifficultyOptions.Medium,
            Url = "https://www.leetcode.com",
        };
        
        mockRepo
            .Setup(r => r.GetProblem(existing.Id))
            .Returns(existing);
        
        var updatedProblem = new Problem
        {
            Number = 1,
            Name = "TestProblemUpdated",
            Difficulty = DifficultyOptions.Medium,
            Url = "https://www.leetcode.com",
        };
        
        var result = controller.Update(existing.Id, updatedProblem);
        
        Assert.IsType<NoContentResult>(result);
        
        mockRepo.Verify(r => r.UpdateProblem(updatedProblem), Times.Once);
    }
    
    [Fact]
    public void Delete_ReturnsNotFound_WhenProblemDoesNotExist()
    {
        var mockRepo = new Mock<IProblemRepository>();
        var controller = new ProblemController(mockRepo.Object);
        
        mockRepo
            .Setup(r => r.GetProblem(1))
            .Returns((Problem?)null);
        
        var result = controller.Delete(1);
        
        Assert.IsType<NotFoundResult>(result);
        
        mockRepo.Verify(r => r.DeleteProblem(It.IsAny<int>()), Times.Never);
    }
    
    [Fact]
    public void Delete_ReturnsNoContent()
    {
        var mockRepo = new Mock<IProblemRepository>();
        var controller = new ProblemController(mockRepo.Object);
        var existing = new Problem
        {
            Number = 1,
            Name = "TestProblem",
            Difficulty = DifficultyOptions.Medium,
            Url = "https://www.leetcode.com",
        };
        
        mockRepo
            .Setup(r => r.GetProblem(existing.Id))
            .Returns(existing);
        
        var result = controller.Delete(existing.Id);
        
        Assert.IsType<NoContentResult>(result);
        
        mockRepo.Verify(r => r.DeleteProblem(existing.Id), Times.Once);
    }
}