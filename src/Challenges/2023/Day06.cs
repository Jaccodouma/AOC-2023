using System.Linq;
using System.Text.RegularExpressions;

namespace Jacco.AOC.Challenges_2023
{
    public class Day06 : IChallenge
    {
        public string Title { get; set; } = "Wait For It";
        public int Day { get; set; } = 6;
        public int Year { get; set; } = 2023;

        private static int WaysToBeatTime(long distance, long time)
        {
            int ways = 0;

            for (long speed = 1; speed <= time; speed++)
            {
                long travelledDistance = speed * (time - speed);
                if (travelledDistance > distance)
                {
                    ways++;
                }
            }

            return ways;
        }

        public int Part1(string[] input)
        {
            List<int> margins = new List<int>();

            List<int> times = Regex.Matches(input[0], "\\d+")
                .Select(m => int.Parse(m.Value))
                .ToList();
            List<int> distances = Regex.Matches(input[1], "\\d+")
                .Select(m => int.Parse(m.Value))
                .ToList();

            for (int i = 0; i < times.Count; i++)
            {
                margins.Add(WaysToBeatTime(distances[i], times[i]));
            }

            // Return product of margins
            return margins.Aggregate((a, b) => a * b);
        }

        public int Part2(string[] input)
        {
            long time = long.Parse(
                Regex.Matches(input[0], "\\d+")
                    .Select(m => m.Value)
                    .ToList()
                    .Aggregate((a, b) => a + b)
            );
            
            long distance = long.Parse(
                Regex.Matches(input[1], "\\d+")
                    .Select(m => m.Value)
                    .ToList()
                    .Aggregate((a, b) => a + b)
            );

            return WaysToBeatTime(distance, time);
        }
    }
}