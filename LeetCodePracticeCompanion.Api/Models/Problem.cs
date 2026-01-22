namespace LeetCodePracticeCompanion.Api.Models;

using System.ComponentModel.DataAnnotations;

public class Problem
{
    [Key]
    public int Id { get; init; }
    
    [Required]
    public int Number { get; set; }

    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(20)]
    public string Difficulty { get; set; } = string.Empty;

    [MaxLength(20)] public string? CurrentInterval { get; set; } = "1 day";
    
    public DateOnly? LastSolveDate { get; set; }
    
    public DateOnly? DueDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    [MaxLength(200)]
    public string Url { get; set; } = string.Empty;
    
    public bool IsDone { get; set; }
}