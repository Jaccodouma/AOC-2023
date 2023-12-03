using System.Reflection;

namespace Jacco.AOC
{
    public interface IChallenge
    {
        public string Title { get; set; }
        public int Day { get; set; }
        public int Year { get; set; }

        public int Part1(string[] input);
        public int Part2(string[] input);

        public void Run(bool exampleOnly) {
            Console.WriteLine($"Day {Day}: {Title}");
            string dataPath = $"src/Challenges/{Year}/Day{Day.ToString("00")}/data";
            string[] exampleInput = File.ReadAllLines($"{dataPath}/example.txt");
            string[] example2Input = File.ReadAllLines($"{dataPath}/example2.txt");
            string[] input = File.ReadAllLines($"{dataPath}/input.txt");

            Console.WriteLine("  Part 1:");

            int exampleResult = Part1(exampleInput);
            Console.WriteLine($"    Example: {exampleResult}");

            if (!exampleOnly) {
                int result = Part1(input);
                Console.WriteLine($"    Result: {result}");
            }

            Console.WriteLine(" Part 2:");

            int exampleResult2 = Part2(example2Input);
            Console.WriteLine($"    Example: {exampleResult2}");

            if (!exampleOnly) {
                int result2 = Part2(input);
                Console.WriteLine($"    Result: {result2}");
            }
        }
    }

    public class ChallengeRunner
    {
        List<IChallenge> challenges = new();

        bool examplesOnly;

        public ChallengeRunner(bool examplesOnly = false) {
            this.examplesOnly = examplesOnly;

            // Get all challenges
            var challenges = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IChallenge)));

            // Register challenges
            foreach (var challenge in challenges)
            {
                RegisterChallenge((IChallenge)Activator.CreateInstance(challenge)!);
            }
        }

        public void RunYear(int year) {
            // Run all challenges from year
            challenges.Where(c => c.Year == year).ToList().ForEach(c => c.Run(examplesOnly));
        }

        public void RunChallenge(int year, int day) {
            challenges.Where(c => c.Year == year && c.Day == day).ToList().ForEach(c => c.Run(examplesOnly));
        }

        public void RunAll() {
            // Run all challenges
            foreach (var challenge in challenges)
            {
                challenge.Run(examplesOnly);
            }
        }

        public void RunLastChallenge() {
            // Run last challenge
            challenges.Last().Run(examplesOnly);
        }

        public void RegisterChallenge(IChallenge challenge) {
            challenges.Add(challenge);
        }
    }
}