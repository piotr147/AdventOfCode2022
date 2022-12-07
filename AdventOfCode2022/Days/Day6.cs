namespace AdventOfCode2022.Days
{
    public class Day6 : Day
    {
        private const int MARKER_LENGTH = 4;
        private const int MESSAGE_MARKER_LENGTH = 14;

        public override void PerformCalculations(IEnumerable<string> input)
        {
            CalculateAndLogTime(() =>
            {
                string buffer = input.First();

                int result = CalculateResult1(buffer);
                Console.WriteLine();
                Console.WriteLine($"Result 1: {result}");
            });

            CalculateAndLogTime(() =>
            {
                string buffer = input.First();

                int result = CalculateResult2(buffer);
                Console.WriteLine();
                Console.WriteLine($"Result 2: {result}");
            });

            Console.WriteLine();
        }

        private static int CalculateResult1(string buffer) =>
            DetectMarker(buffer, MARKER_LENGTH);

        private static int CalculateResult2(string buffer) =>
            DetectMarker(buffer, MESSAGE_MARKER_LENGTH);

        private static int DetectMarker(string buffer, int markerLength)
        {
            Queue<char> queue = new (buffer.Take(markerLength));
            int counter = markerLength;

            if (queue.Distinct().Count() == markerLength)
            {
                return counter;
            }

            foreach (char c in buffer.Skip(markerLength))
            {
                ++counter;

                queue.Dequeue();
                queue.Enqueue(c);

                if (queue.Distinct().Count() == markerLength)
                {
                    return counter;
                }
            }

            throw new ArgumentException("Invalid signal received");
        }
    }
}
