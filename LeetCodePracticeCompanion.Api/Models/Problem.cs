namespace LeetCodePracticeCompanion.Api.Models;

using System.ComponentModel.DataAnnotations;

public enum DifficultyOptions
{
    Easy,
    Medium,
    Hard,
}

public class Problem
{
    [Key] public int Id { get; init; }

    [Required] public int Number { get; set; }

    [MaxLength(200)] [Required] public required string Name { get; set; }

    [Required] public DifficultyOptions Difficulty { get; set; }

    [Required] public int IntervalDays { get; set; } = 1;
    
    [Required] public int IntervalMonths { get; set; }

    public DateOnly? LastSolveDate { get; set; }

    [MaxLength(200)] [Required] public required string Url { get; set; }
}