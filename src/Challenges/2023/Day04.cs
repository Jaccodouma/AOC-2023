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
                id = int.Parse(Regex.Match(line, @"\d+").Value);

                string[] strings = line.Split(':')[1].Split('|');
                winningNumbers = Regex
                    .Split(strings[0].Trim(), @"\s+")
                    .Select(int.Parse)
                    .ToArray();
                numbers = Regex
                    .Split(strings[1].Trim(), @"\s+")
                    .Select(int.Parse)
                    .ToArray();
            }

            public int GetWinningNumberAmount()
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
                int winningNumbersCount = card.GetWinningNumberAmount();
                int instances = instanceNumbers.ContainsKey(card.id) ? instanceNumbers[card.id] : 1;

                for (int instance = 0; instance < instances; instance++)
                {
                    for (int i = 1; i <= winningNumbersCount; i++)
                    {
                        int key = card.id + i;
                        if (instanceNumbers.ContainsKey(key))
                        {
                            instanceNumbers[key]++;
                        }
                        else
                        {
                            instanceNumbers.Add(key, 2);
                        }
                    }
                }

                sums.Add(instances);
            });

            return sums.Sum();
        }
    }
}