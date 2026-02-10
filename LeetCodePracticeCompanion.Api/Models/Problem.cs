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

    [Required] public int Number { get; init; }

    [MaxLength(200)] [Required] public string Name { get; set; }

    [MaxLength(20)] [Required] public DifficultyOptions Difficulty { get; set; }

    public int CurrentInterval { get; set; } = 1;

    public DateOnly? LastSolveDate { get; set; }

    public DateOnly? DueDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    [MaxLength(200)] [Required] public string Url { get; set; }
}