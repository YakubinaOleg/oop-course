using ClinicApp.Enums;
using ClinicApp.Utils;

namespace ClinicApp.Models;

public struct WorkSchedule
{
    public int Start { get; }
    public int End { get; }

    public int HoursPerDay => End - Start;

    public string Display => $"{Start:D2}:00–{End:D2}:00";

    public bool IsNow => Contains(DateTime.Now.Hour);

    public WorkSchedule(int start, int end)
    {
        if (start < 0 || start > 23)
        {
            throw new ArgumentOutOfRangeException(nameof(start), "Час початку роботи повинен бути між 0 і 23.");
        }
        if (end < 1 || end > 24)
        {
            throw new ArgumentOutOfRangeException(nameof(end), "Час кінця роботи повинен бути між 1 і 24.");
        }
        if (start >= end)
        {
            throw new ArgumentException("Час кінця роботи повинен бути пізніше часу початку.", nameof(end));
        }

        Start = start;
        End = end;
    }

    public bool Contains(int hour)
    {
        return hour >= Start && hour < End;
    }

    public override string ToString()
    {
        return $"{Display} ({HoursPerDay} год)";
    }
}