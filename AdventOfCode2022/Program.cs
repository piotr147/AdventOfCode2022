// See https://aka.ms/new-console-template for more information

using AdventOfCode2022.Days;
using AdventOfCode2022.Utilities;

string inputsPath = args.Length > 0
    ? args[0]
    : "./Inputs";

Dictionary<int, Day> days = InitializeDays();

while (true)
{
    Console.WriteLine();
    Console.Write("Enter day number or 0 to exit: ");

    if (!int.TryParse(Console.ReadLine(), out int day))
    {
        Console.WriteLine("Valid number is expected");
        continue;
    }

    if (day == 0)
    {
        return;
    }

    if (!days.ContainsKey(day))
    {
        Console.WriteLine("Day number is not correct or has not been resolved yet");
        continue;
    }

    await TrySolveDay(day);
}

async Task TrySolveDay(int day)
{
    try
    {
        IEnumerable<string> input = await Helper.GetStringListFromFile($"{inputsPath}/Day{day}.txt");
        days[day].PerformCalculations(input);
    }
    catch (FormatException e)
    {
        Console.WriteLine(e.Message);
    }
    catch (Exception e) when (e is DirectoryNotFoundException || e is FileNotFoundException)
    {
        Console.WriteLine(e.Message);
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        throw;
    }
}

Dictionary<int, Day> InitializeDays() =>
    new()
    {
        [1] = new Day1(),
        [2] = new Day2(),
        [3] = new Day3(),
        [4] = new Day4(),
        [5] = new Day5(),
    };
