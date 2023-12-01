namespace Jacco.AOC
{
    public interface IChallenge
    {
        public string title { get; set; }
        public int day { get; set; }

        public int Part1(string[] input);
        public int Part2(string[] input);
    }

    public class ChallengeRunner
    {
        List<IChallenge> challenges = new();

        public void Run() {
            // Run all challenges
            foreach (var challenge in challenges)
            {
                Console.WriteLine($"Day {challenge.day}: {challenge.title}");
                string[] exampleInput = Helpers.Input.ReadLines($"src/Challenges/Day{challenge.day.ToString("00")}/data/example.txt");
                string[] example2Input = Helpers.Input.ReadLines($"src/Challenges/Day{challenge.day.ToString("00")}/data/example2.txt");
                string[] input = Helpers.Input.ReadLines($"src/Challenges/Day{challenge.day.ToString("00")}/data/input.txt");

                Console.WriteLine(" Part 1:");

                int exampleResult = challenge.Part1(exampleInput);
                Console.WriteLine($"  Example: {exampleResult}");

                int result = challenge.Part1(input);
                Console.WriteLine($"  Result: {result}");

                Console.WriteLine(" Part 2:");

                int exampleResult2 = challenge.Part2(example2Input);
                Console.WriteLine($"  Example: {exampleResult2}");

                int result2 = challenge.Part2(input);
                Console.WriteLine($"  Result: {result2}");
            }
        }

        public void RegisterChallenge(IChallenge challenge) {
            challenges.Add(challenge);
        }
    }
}