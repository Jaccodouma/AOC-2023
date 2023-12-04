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
            string dataPath = $"data/{Year}/{Day.ToString("00")}";
            string[] exampleInput = File.ReadAllLines($"{dataPath}/example.txt");
            string[] example2Input = File.ReadAllLines($"{dataPath}/example2.txt");
            string[] input = File.ReadAllLines($"{dataPath}/input.txt");

            for (int part = 1; part <= 2; part++) {
                Console.WriteLine($"  Part {part}:");
                
                var watch = System.Diagnostics.Stopwatch.StartNew();
                int exampleResult = part == 1 ? Part1(exampleInput) : Part2(example2Input);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                Console.WriteLine($"    Example: {exampleResult} ({elapsedMs}ms)");

                if (exampleOnly) continue;

                watch = System.Diagnostics.Stopwatch.StartNew();
                int result = part == 1 ? Part1(input) : Part2(input);
                watch.Stop();
                elapsedMs = watch.ElapsedMilliseconds;
                Console.WriteLine($"    Result: {result} ({elapsedMs}ms)");
            }
        }

        private void RunPart(string[] input, int part) {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            int result = part == 1 ? Part1(input) : Part2(input);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"    Result: {result} ({elapsedMs}ms)");
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