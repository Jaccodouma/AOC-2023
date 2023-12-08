using System.Linq;
using System.Text.RegularExpressions;

namespace Jacco.AOC.Challenges_2023
{
    public class Day07 : IChallenge
    {
        public string Title { get; set; } = "";
        public int Day { get; set; } = 7;
        public int Year { get; set; } = 2023;

        public static char[] cards = new char[] { 
            'A', 'K', 'Q', 'J', 'T', 
            '9', '8', '7', '6', '5', '4', '3', '2'
        }.Reverse().ToArray();

        public class Hand: IComparable<Hand>
        {
            public int bid;
            public string hand;

            public Hand(int bid, string hand)
            {
                this.bid = bid;
                this.hand = hand;
            }

            private long GetCardScore()
            {
                string score = "";

                foreach (char card in hand)
                {
                    // Cardscore is index in cards array, but always two characters
                    score += Array.IndexOf(cards, card).ToString("00");
                }

                return long.Parse(score);
            }

            private int GetHandScore() {
                // Five of a kind
                if (hand.Distinct().Count() == 1) {
                    return 7;
                }

                // Four of a kind
                if (hand.GroupBy(x => x).Any(g => g.Count() == 4)) {
                    return 6;
                }

                // Full house
                if (hand.GroupBy(x => x).Any(g => g.Count() == 3) && hand.GroupBy(x => x).Any(g => g.Count() == 2)) {
                    return 5;
                }

                // Three of a kind
                if (hand.GroupBy(x => x).Any(g => g.Count() == 3)) {
                    return 4;
                }

                // Two pair
                if (hand.GroupBy(x => x).Where(g => g.Count() == 2).Count() == 2) {
                    return 3;
                }

                // One pair
                if (hand.GroupBy(x => x).Any(g => g.Count() == 2)) {
                    return 2;
                }

                // High card
                return 1;
            }

            public int CompareTo(Hand? other)
            {
                if (other == null) return 1;

                int thisHandScore = this.GetHandScore();
                int otherHandScore = other.GetHandScore();

                if (thisHandScore > otherHandScore) return 1;
                if (thisHandScore < otherHandScore) return -1;
                if (thisHandScore == otherHandScore) {
                    long thisCardScore = this.GetCardScore();
                    long otherCardScore = other.GetCardScore();

                    if (thisCardScore > otherCardScore) return 1;
                    if (thisCardScore < otherCardScore) return -1;
                }

                return 0;
            }
        }

        public int Part1(string[] input)
        {
            List<Hand> hands = new List<Hand>();

            foreach (string line in input)
            {
                string[] vals = line.Split(" ");
                hands.Add(new Hand(int.Parse(vals[1]), vals[0]));
            }

            // Order hands by score
            hands = hands.OrderDescending().ToList();

            // Calculate winnings
            int winnings = 0;

            for (int i = 0; i < hands.Count; i++)
            {
                winnings += hands[i].bid * (i + 1);
            }

            return winnings;
        }

        public int Part2(string[] input)
        {
            return 0;
        }
    }
}