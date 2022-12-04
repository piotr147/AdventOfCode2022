namespace AdventOfCode2022.Days
{
    public class Day4 : Day
    {
        public override void PerformCalculations(IEnumerable<string> input)
        {
            CalculateAndLogTime(() =>
            {
                IEnumerable<(SectionsRange firstElf, SectionsRange secodElf)> pairs = input.Select(ReadPair);
                int result = CalculateResult1(pairs);
                Console.WriteLine();
                Console.WriteLine($"Result 1: {result}");
            });

            CalculateAndLogTime(() =>
            {
                IEnumerable<(SectionsRange firstElf, SectionsRange secodElf)> pairs = input.Select(ReadPair);
                int result = CalculateResult2(pairs);
                Console.WriteLine();
                Console.WriteLine($"Result 2: {result}");
            });

            Console.WriteLine();
        }

        private (SectionsRange firstElf, SectionsRange secodElf) ReadPair(string line)
        {
            string[] pairs = line.Split(',');

            return (ReadSectionRange(pairs[0]), ReadSectionRange(pairs[1]));
        }

        private SectionsRange ReadSectionRange(string sectionsString)
        {
            string[] sections = sectionsString.Split('-');
            return new(int.Parse(sections[0]), int.Parse(sections[1]));
        }

        private static int CalculateResult1(IEnumerable<(SectionsRange firstElf, SectionsRange secodElf)> elfPairs) =>
            elfPairs.Count(pair => pair.firstElf.Includes(pair.secodElf) || pair.secodElf.Includes(pair.firstElf));

        private static int CalculateResult2(IEnumerable<(SectionsRange firstElf, SectionsRange secodElf)> elfPairs) =>
            elfPairs.Count(pair => pair.firstElf.OverlapsWith(pair.secodElf));

        private struct SectionsRange
        {
            public int Start;
            public int End;

            public SectionsRange(int start, int end)
            {
                Start = start;
                End = end;
            }

            public bool Includes(SectionsRange sections) =>
                Start <= sections.Start && End >= sections.End;

            public bool OverlapsWith(SectionsRange sections) =>
                End >= sections.Start && End <= sections.End
                || sections.Start >= Start && sections.Start <= End
                || Start >= sections.Start && Start <= sections.End
                ||sections.End >= Start && sections.End <= End;
        }
    }
}
