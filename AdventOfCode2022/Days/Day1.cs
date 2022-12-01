using AdventOfCode2022.Utilities;

namespace AdventOfCode2022.Days;

public class Day1 : Day
{
    public override void PerformCalculations(IEnumerable<string> input)
    {
        IEnumerable<int?> calories = input.ToNullableInt();
        calories = calories.Append(null);

        CalculateAndLogTime(() =>
        {
            int result = CalculateResult1(calories);
            Console.WriteLine();
            Console.WriteLine($"Result 1: {result}");
        });

        CalculateAndLogTime(() =>
        {
            int result = CalculateResult2(calories);
            Console.WriteLine();
            Console.WriteLine($"Result 2: {result}");
        });

        Console.WriteLine();
    }

    private int CalculateResult1(IEnumerable<int?> input)
    {
        int maxCalories = 0;
        int currentCalories = 0;

        foreach (int? calories in input)
        {
            if (calories.HasValue)
            {
                currentCalories += calories.Value;
                continue;
            }

            maxCalories = maxCalories < currentCalories
                ? currentCalories
                : maxCalories;

            currentCalories = 0;
        }

        return maxCalories;
    }

    private int CalculateResult2(IEnumerable<int?> input)
    {
        const int numberOfTopElves = 3;
        IEnumerable<int> topCalories = new List<int>();
        int currentCalories = 0;

        foreach (int? calories in input)
        {
            if (calories.HasValue)
            {
                currentCalories += calories.Value;
                continue;
            }

            if (numberOfTopElves > topCalories.Count())
            {
                topCalories = topCalories.Append(currentCalories);
                topCalories = topCalories.OrderBy(c => c);
            }
            else
            {
                if (topCalories.First() < currentCalories)
                {
                    topCalories = topCalories.Skip(1);
                    topCalories = topCalories.Append(currentCalories);
                    topCalories = topCalories.OrderBy(c => c);
                }
            }

            currentCalories = 0;
        }

        return topCalories.Sum();
    }
}
