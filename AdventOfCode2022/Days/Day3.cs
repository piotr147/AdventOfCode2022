using System.Linq;

namespace AdventOfCode2022.Days
{
    public class Day3 : Day
    {
        public override void PerformCalculations(IEnumerable<string> input)
        {
            CalculateAndLogTime(() =>
            {
                int result = CalculateResult1(input);
                Console.WriteLine();
                Console.WriteLine($"Result 1: {result}");
            });

            CalculateAndLogTime(() =>
            {
                int result = CalculateResult2(input);
                Console.WriteLine();
                Console.WriteLine($"Result 2: {result}");
            });

            Console.WriteLine();
        }

        private static int CalculateResult1(IEnumerable<string> input) =>
            input.Select(FindCommonItemPriority).Sum();

        private static int FindCommonItemPriority(string rucksak)
        {
            IEnumerable<char> compartment1 = rucksak[..(rucksak.Length / 2)].Distinct();
            IEnumerable<char> compartment2 = rucksak[(rucksak.Length / 2)..].Distinct();

            foreach (char item in compartment1)
            {
                foreach (char item2 in compartment2)
                {
                    if (item == item2)
                    {
                        return CalculateItemPriority(item);
                    }
                }
            }

            throw new ArgumentException("Compartment do not have common item");
        }

        private int CalculateResult2(IEnumerable<string> input) =>
            DivideIntoGroups(input).Select(FindBadgePriority).Sum();

        private IEnumerable<IEnumerable<string>> DivideIntoGroups(IEnumerable<string> input)
        {
            IEnumerable<IEnumerable<string>> groups = new List<IEnumerable<string>>();
            List<string> newGroup = new ();
            int counter = 0;

            foreach (string elf in input)
            {
                newGroup.Add(elf);

                if(++counter >= 3)
                {
                    counter = 0;
                    groups = groups.Append(newGroup);
                    newGroup = new List<string>();
                }
            }

            return groups;
        }

        private static int FindBadgePriority(IEnumerable<string> group)
        {
            foreach (char item in group.First())
            {
                char? badge = FindBadgeRecursively(group.Skip(1), item);

                if (badge.HasValue)
                {
                    return CalculateItemPriority(badge.Value);
                }
            }

            throw new ArgumentException("Elves do not have common badge");
        }

        private static char? FindBadgeRecursively(IEnumerable<string> elves, char candidate)
        {
            foreach (char item in elves.First())
            {
                if (candidate == item)
                {
                    return elves.Count() > 1
                        ? FindBadgeRecursively(elves.Skip(1), candidate)
                        : item;
                }
            }

            return null;
        }

        private static int CalculateItemPriority(char item)
        {
            int converted = (int)item;

            return converted >= 97
                ? converted - 96
                : converted - 38;
        }
    }
}
