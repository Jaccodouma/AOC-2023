using System.Linq;
using System.Text.RegularExpressions;

namespace Jacco.AOC.Challenges_2023
{
    public class Day08 : IChallenge
    {
        public string Title { get; set; } = "data/2023/06";
        public int Day { get; set; } = 8;
        public int Year { get; set; } = 2023;

        public class Instruction
        {
            public required string Left;
            public required string Right;
        }

        public int Part1(string[] input)
        {
            char[] instructions = input[0].ToCharArray();

            // Build up map of instructions 
            Dictionary<string, Instruction> map = new Dictionary<string, Instruction>();

            input[2..].ToList().ForEach(line =>
            {
                string[] vals = Regex.Matches(line, "\\w+").Select(m => m.Value).ToArray();
                map[vals[0]] = new Instruction
                {
                    Left = vals[1],
                    Right = vals[2]
                };
            });

            string currentLocation = "AAA";
            int steps = 0;

            while (currentLocation != "ZZZ")
            {
                char instruction = instructions[steps%instructions.Length];

                currentLocation = instruction switch
                {
                    'L' => map[currentLocation].Left,
                    'R' => map[currentLocation].Right,
                    _ => throw new Exception("Invalid instruction")
                };

                steps++;
            }

            return steps;
        }

        public int Part2(string[] input)
        {
            return 0;
        }
    }
}