using System.Text.RegularExpressions;

namespace Jacco.AOC.Challenges_2023
{
    public class Day04 : IChallenge
    {
        public string Title { get; set; } = "Scratchcards";
        public int Day { get; set; } = 4;
        public int Year { get; set; } = 2023;

        private class Card
        {
            public int id;
            private int[] winningNumbers;
            private int[] numbers;

            public Card(string line)
            {
                // line: 
                // Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
                var parts = line.Split(new string[] { ":", "|" }, StringSplitOptions.RemoveEmptyEntries);
                id = int.Parse(parts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)[1]);

                winningNumbers = parts[1]
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                numbers = parts[2]
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
            }

            public int GetWinningNumbersCount()
            {
                int i = 0;
                foreach (int number in winningNumbers)
                {
                    if (numbers.Contains(number))
                    {
                        i++;
                    }
                }
                return i;
            }

            public int GetPoints()
            {
                int points = 0;
                foreach (int number in winningNumbers)
                {
                    if (numbers.Contains(number))
                    {
                        if (points == 0)
                        {
                            points = 1;
                        }
                        else
                        {
                            points *= 2;
                        }
                    }
                }
                return points;
            }
        }

        public int Part1(string[] input)
        {
            List<int> sums = new();

            input.ToList().ForEach(line =>
            {
                Card card = new Card(line);
                sums.Add(card.GetPoints());
            });

            return sums.Sum();
        }

        public int Part2(string[] input)
        {
            List<int> sums = new();

            // Key value pairs 
            Dictionary<int, int> instanceNumbers = new();

            input.ToList().ForEach(line =>
            {
                Card card = new Card(line);
                int winningNumbersCount = card.GetWinningNumbersCount();
                int instances = instanceNumbers.ContainsKey(card.id) ? instanceNumbers[card.id] : 1;

                for (int i = 1; i <= winningNumbersCount; i++)
                {
                    int key = card.id + i;
                    if (instanceNumbers.ContainsKey(key))
                    {
                        instanceNumbers[key] += instances;
                    }
                    else
                    {
                        instanceNumbers.Add(key, instances + 1);
                    }
                }

                sums.Add(instances);
            });

            return sums.Sum();
        }
    }
}