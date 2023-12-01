using System.Text.RegularExpressions;

namespace Jacco.AOC.Challenges
{
    public class Day01: IChallenge
    {
        public string title { get; set; } = "Trebuchet?!";
        public int day { get; set; } = 1;

        private int GetCalibrationValue(string input) {
            // Remove all non-numbers
            string nums = Regex.Replace(input, "[^0-9]", "");

            // Construct number
            string first = nums.Substring(0, 1);
            string last = nums.Substring(nums.Length - 1);
            return int.Parse(first + last);
        }

        public int Part1(string[] input)
        {
            List<int> sums = new();

            foreach (string line in input)
            {
                // Add to sums
                sums.Add(GetCalibrationValue(line));
            }

            return sums.Sum();
        }

        public int Part2(string[] input) {
            List<int> sums = new();

            foreach (string line in input)
            {
                string nums = line;

                // Map digit string to int
                List<string> digitStrings = new List<string>() { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

                // Check if digitstrings exist in line, add them to nums
                foreach (var digitString in digitStrings)
                {
                    // Get all indexes of digit
                    var matches = Regex.Matches(line, digitString);

                    matches.ToList().ForEach(m => {
                        // Replace char at index with integer
                        nums = nums.Remove(m.Index, 1).Insert(m.Index, digitStrings.IndexOf(digitString).ToString());
                    });
                }

                // Add to sums
                sums.Add(GetCalibrationValue(nums));
            }

            return sums.Sum();
        }
    }
}