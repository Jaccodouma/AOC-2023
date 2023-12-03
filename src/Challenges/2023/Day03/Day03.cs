using System.Text.RegularExpressions;

namespace Jacco.AOC.Challenges_2023
{
    public class Day03 : IChallenge
    {
        public string Title { get; set; } = "Gear Ratios";
        public int Day { get; set; } = 3;
        public int Year { get; set; } = 2023;

        public int Part1(string[] input)
        {
            List<int> sums = new();

            for (int i = 0; i < input.Length; i++)
            {
                string line = input[i];

                // Get index of number 
                Regex.Matches(line, "[0-9]+").ToList().ForEach(match =>
                {
                    int index = match.Index;
                    int number = int.Parse(match.Value);
                    int length = match.Length;

                    int beforePosition = index - 1;
                    int afterPosition = index + length;

                    // Loop through lines 
                    for (int line = i - 1; line <= i + 1; line++)
                    {
                        if (line < 0 || line >= input.Length) continue;
                        // Loop through characters
                        for (int character = index - 1; character < index + length + 1; character++)
                        {
                            if (character < 0 || character >= input[line].Length) continue;

                            string c = input[line][character].ToString();

                            // If character is not a period or number
                            bool counts = Regex.IsMatch(c, "[^0-9.]");

                            if (counts)
                            {
                                sums.Add(number);
                                return;
                            }
                        }
                    }
                });
            }

            return sums.Sum();
        }

        public int Part2(string[] input)
        {
            List<int> sums = new();

            for (int lineNr = 0; lineNr < input.Length; lineNr++)
            {
                string lineStr = input[lineNr];

                // Get index of gear 
                Regex.Matches(lineStr, "\\*").ToList().ForEach(match =>
                {
                    int index = match.Index;
                    List<int> adjacentNumbers = new();

                    for (int lineToCheck = lineNr-1; lineToCheck <= lineNr+1; lineToCheck++)
                    {
                        if (lineToCheck < 0 || lineToCheck >= input.Length) continue;
                        Regex.Matches(input[lineToCheck], "[0-9]+").ToList().ForEach(match => {
                            // Add if index is around index of gear
                            if (match.Index >= index - match.Length && match.Index <= index + 1)
                            {
                                // Add number if it doesn't exist yet
                                if (!adjacentNumbers.Contains(int.Parse(match.Value)))
                                    adjacentNumbers.Add(int.Parse(match.Value));
                            }
                        });
                    }

                    // Add product of adjacent numbers
                    if (adjacentNumbers.Count == 2)
                        sums.Add(adjacentNumbers.Aggregate((a, b) => a * b));
                });
            }

            return sums.Sum();
        }
    }
}