namespace LeetCodePracticeCompanion.Api.Domain;

public record Interval(int Days, int Months);

public static class ProblemIntervals
{
    private static readonly List<Interval> Sequence =
    [
        new(1, 0),
        new(2, 0),
        new(4, 0),
        new(7, 0),
        new(14, 0),
        new(0, 1),
        new(0, 2),
        new(0, 4),
        new(0, 6),
        new(0, 12),
    ];

    public static Interval GetNextInterval(int days, int months)
    {
        var index = Sequence.FindIndex((i) => i.Days == days && i.Months == months);
        return index == Sequence.Count - 1 ? new Interval(0, 0) : Sequence[index + 1];
    }

    public static Interval GetPreviousInterval(int days, int months)
    {
        var index = Sequence.FindIndex((i) => i.Days == days && i.Months == months);
        return index == 0 ? Sequence[0] : Sequence[index - 1];
    }
}