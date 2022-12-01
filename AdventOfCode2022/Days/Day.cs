using System.Diagnostics;

namespace AdventOfCode2022.Days;

public abstract class Day
{
    public abstract void PerformCalculations(IEnumerable<string> input);

    protected void CalculateAndLogTime(Action calc)
    {
        Stopwatch sw = Stopwatch.StartNew();
        calc.Invoke();

        sw.Stop();
        Console.WriteLine($"Elapsed miliseconds: {sw.ElapsedMilliseconds}");
    }
}
