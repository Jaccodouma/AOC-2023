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

        // Function to return
        // gcd of a and b
        static int __gcd(int a, int b)
        {
            if (a == 0)
                return b;
            return __gcd(b % a, a);
        }

        //recursive implementation
        static int LcmOfArray(int[] arr, int idx = 0)
        {
            // lcm(a,b) = (a*b/gcd(a,b))
            if (idx == arr.Length - 1)
            {
                return arr[idx];
            }
            int a = arr[idx];
            int b = LcmOfArray(arr, idx + 1);
            return (a * b / __gcd(a, b)); // __gcd(a,b) is inbuilt library function
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
                char instruction = instructions[steps % instructions.Length];

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

            // Fill currentLocations with all maps ending with A
            string[] currentLocations = map.Keys.Where(k => k.EndsWith("A")).ToArray();
            int[] pathLengths = new int[currentLocations.Length];

            #region brute force
            // int steps = 0;

            // // While currentlocations contains any map not ending with Z 
            // while (currentLocations.Any(k => !k.EndsWith("Z")))
            // {
            //     char instruction = instructions[steps%instructions.Length];

            //     currentLocations = currentLocations.Select(location => {
            //         return instruction switch
            //         {
            //             'L' => map[location].Left,
            //             'R' => map[location].Right,
            //             _ => throw new Exception("Invalid instruction")
            //         };
            //     }).ToArray();

            //     steps++;
            // }
            #endregion

            for (int i = 0; i < currentLocations.Length; i++)
            {
                string currentLocation = currentLocations[i];
                pathLengths[i] = 0;

                while (!currentLocation.EndsWith("Z"))
                {
                    char instruction = instructions[pathLengths[i] % instructions.Length];
                    currentLocation = instruction switch
                    {
                        'L' => map[currentLocation].Left,
                        'R' => map[currentLocation].Right,
                        _ => throw new Exception("Invalid instruction")
                    };
                    pathLengths[i]++;
                }
            }

            Console.WriteLine(string.Join(", ", pathLengths));

            return LcmOfArray(pathLengths);
        }
    }
}