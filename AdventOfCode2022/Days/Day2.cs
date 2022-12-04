namespace AdventOfCode2022.Days
{
    public class Day2 : Day
    {
        private Dictionary<string, Shape> _round1Translation = new ()
        {
            ["A"] = Shape.Rock,
            ["B"] = Shape.Paper,
            ["C"] = Shape.Scissors,
            ["X"] = Shape.Rock,
            ["Y"] = Shape.Paper,
            ["Z"] = Shape.Scissors,
        };

        private Dictionary<string, Shape> _round2ShapeTranslation = new ()
        {
            ["A"] = Shape.Rock,
            ["B"] = Shape.Paper,
            ["C"] = Shape.Scissors,
        };

        private Dictionary<string, Result> _round2ResultTranslation = new ()
        {
            ["X"] = Result.Lose,
            ["Y"] = Result.Draw,
            ["Z"] = Result.Win,
        };

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

        private int CalculateResult1(IEnumerable<string> input)
        {
            IEnumerable<Round> rounds = input.Select(line =>
            {
                string[] symbols = line.Split(' ');
                return new Round(_round1Translation[symbols[0]], _round1Translation[symbols[1]]);
            });

            return rounds.Sum(r => r.CalculateScore());
        }

        private int CalculateResult2(IEnumerable<string> input)
        {
            IEnumerable<Round> rounds = input.Select(line =>
            {
                string[] symbols = line.Split(' ');
                return new Round(_round2ShapeTranslation[symbols[0]], _round2ResultTranslation[symbols[1]]);
            });

            return rounds.Sum(r => r.CalculateScore());
        }

        private enum Shape
        {
            Rock = 1,
            Paper = 2,
            Scissors = 3,
        }

        private enum Result
        {
            Lose = 0,
            Draw = 3,
            Win = 6,
        }

        private class Round
        {
            public Result? RoundResult { get; set; }
            public Shape Opp { get; set; }
            public Shape? Mine { get; set; }

            public Round(Shape opp, Shape mine)
            {
                Opp = opp;
                Mine = mine;
            }

            public Round(Shape opp, Result result)
            {
                Opp = opp;
                RoundResult = result;
            }

#pragma warning disable CS8629 // Nullable value type may be null.
            public int CalculateScore()
            {
                if (!Mine.HasValue && !RoundResult.HasValue)
                {
                    throw new ArgumentException("Both shape of mine and round result cannot be null in the same time");
                }

                if (!Mine.HasValue)
                {
                    Mine = RoundResult.Value switch
                    {
                        Result.Draw => Opp,
                        Result.Win => (Shape)((int)Opp + 1 <= 3 ? (int)Opp + 1 : 1),
                        Result.Lose => (Shape)((int)Opp - 1 >=1 ? (int)Opp - 1 : 3),
                        _ => throw new ArgumentException("Values in round are invalid."),
                    };
                }

                if (!RoundResult.HasValue)
                {
                    int diff = (Mine.Value - Opp + 3) % 3;
                    RoundResult = diff switch
                    {
                        0 => Result.Draw,
                        1 => Result.Win,
                        2 => Result.Lose,
                        _ => throw new ArgumentException("Values in round are invalid."),
                    };
                }

                return (int)Mine.Value + (int)RoundResult.Value;
            }
#pragma warning restore CS8629 // Nullable value type may be null.
        }
    }
}
