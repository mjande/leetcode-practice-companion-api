namespace LeetCodePracticeCompanion.Api.Models.Requests;

public record SolveProblemRequest
{
    public bool SolvedWithoutHelp { get; init; }
    public bool SolvedWithCorrectComplexity { get; init;  }
};