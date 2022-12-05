using System.Text;

namespace AdventOfCode2022.Days
{
    public class Day5 : Day
    {
        public override void PerformCalculations(IEnumerable<string> input)
        {
            CalculateAndLogTime(() =>
            {
                IEnumerable<Procedure> procedures = ReadProcedures(input).ToList();
                List<Stack<char>> stacks = ReadStacks(input);

                string result = CalculateResult1(stacks, procedures);
                Console.WriteLine();
                Console.WriteLine($"Result 1: {result}");
            });

            CalculateAndLogTime(() =>
            {
                IEnumerable<Procedure> procedures = ReadProcedures(input).ToList();
                List<Stack<char>> stacks = ReadStacks(input);

                string result = CalculateResult2(stacks, procedures);
                Console.WriteLine();
                Console.WriteLine($"Result 2: {result}");
            });

            Console.WriteLine();
        }

        private static string CalculateResult1(List<Stack<char>> stacks, IEnumerable<Procedure> procedures)
        {
            foreach (Procedure procedure in procedures)
            {
                for (int i = 0; i < procedure.Number; ++i)
                {
                    char toBeMoved = stacks[procedure.From].Pop();
                    stacks[procedure.To].Push(toBeMoved);
                }
            }

            StringBuilder sb = new ();
            stacks.ForEach(stack => sb.Append(stack.Peek()));

            return sb.ToString();
        }

        private static string CalculateResult2(List<Stack<char>> stacks, IEnumerable<Procedure> procedures)
        {
            foreach (Procedure procedure in procedures)
            {
                List<char> toBeMoved = new ();
                for (int i = 0; i < procedure.Number; ++i)
                {
                    toBeMoved.Add(stacks[procedure.From].Pop());
                }
                toBeMoved.Reverse();
                toBeMoved.ForEach(item => stacks[procedure.To].Push(item));
            }

            StringBuilder sb = new();
            stacks.ForEach(stack => sb.Append(stack.Peek()));

            return sb.ToString();
        }

        private static IEnumerable<Procedure> ReadProcedures(IEnumerable<string> input) =>
            input
                .SkipWhile(line => !string.IsNullOrWhiteSpace(line))
                .Skip(1)
                .Select(line => new Procedure(line));

        private static List<Stack<char>> ReadStacks(IEnumerable<string> input)
        {
            IEnumerable<string> stackLines = input.TakeWhile(line => !string.IsNullOrWhiteSpace(line)).SkipLast(1);
            int numberOfStacks = (stackLines.First().Length + 1) / 4;
            List<Stack<char>> stacks = Enumerable.Range(0, numberOfStacks).Select(_ => new Stack<char>()).ToList();
            List<int> crateIndexes = Enumerable.Range(0, numberOfStacks).Select(i => i * 4 + 1).ToList();

            foreach (string line in stackLines.Reverse())
            {
                for (int i = 0; i < crateIndexes.Count; ++i)
                {
                    if (line[crateIndexes[i]] != ' ')
                    {
                        stacks[i].Push(line[crateIndexes[i]]);
                    }
                }
            }

            return stacks;
        }

        private struct Procedure
        {
            public int Number;
            public int From;
            public int To;

            public Procedure(string instructionString)
            {
                string[] words = instructionString.Split(' ');
                Number = int.Parse(words[1]);
                From = int.Parse(words[3]) - 1;
                To = int.Parse(words[5]) - 1;
            }
        }
    }
}
