using System.Text.RegularExpressions;

namespace Jacco.AOC.Challenges
{
    public class Day02: IChallenge
    {
        public string title { get; set; } = "Cube Conundrum";
        public int day { get; set; } = 2;

        private class Game 
        {
            public int Id { get; set; }
            public List<Dictionary<string, int>> Hands { get; set; } = new();

            public Game(string line) {
                // Get game Id
                Id = int.Parse(Regex.Match(line, "[0-9]*(?=:)").Value);

                // Fill Dictionary
                var matches = Regex.Matches(line, "([0-9]+ [a-z]+(, )*)+");
                matches.ToList().ForEach(handLine => {
                    // handline example: 6 red, 1 blue, 3 green
                    var handDict = new Dictionary<string, int>();
                    var hand = handLine.Value.Split(", "); // ["6 red", "1 blue", "3 green"]
                    hand.ToList().ForEach(card => {
                        var cardSplit = card.Split(" ");
                        string key = cardSplit[1];
                        int value = int.Parse(cardSplit[0]);
                        
                        // Add to dictionary
                        handDict.Add(key, value);
                    });
                    Hands.Add(handDict);
                });
            }

            public bool isPossible() 
            {
                // Loop through hands
                foreach (var hand in Hands)
                {
                    if (hand.ContainsKey("red") && hand["red"] > 12) return false;
                    if (hand.ContainsKey("green") && hand["green"] > 13) return false;
                    if (hand.ContainsKey("blue") && hand["blue"] > 14) return false;
                }

                return true;
            }

            public int calculatePower() 
            {
                // Get max values
                int R = Hands.Max(hand => hand.ContainsKey("red") ? hand["red"] : 0);
                int G = Hands.Max(hand => hand.ContainsKey("green") ? hand["green"] : 0);
                int B = Hands.Max(hand => hand.ContainsKey("blue") ? hand["blue"] : 0);

                // Calculate power
                return R * G * B;
            }
        }

        public int Part1(string[] input)
        {
            List<int> sums = new();

            foreach (string line in input)
            {
                Game game = new(line);

                if (game.isPossible()) {
                    sums.Add(game.Id);
                }
            }

            return sums.Sum();
        }

        public int Part2(string[] input) {
            List<int> sums = new();

            foreach (string line in input)
            {
                Game game = new(line);

                sums.Add(game.calculatePower());
            }

            return sums.Sum();
        }
    }
}