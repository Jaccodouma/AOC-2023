using Jacco.AOC;

ChallengeRunner challengeRunner = new();

// Get year and day from args
int year = 0;
int day = 0;

if (args.Length > 0) {
    year = int.Parse(args[0]);
}

if (args.Length > 1) {
    day = int.Parse(args[1]);
}

// Welcome message
ConsoleWrapper.PrintTitle("Jacco's Advent Of Code attempt!", "Happy Hollidays! <3");

// Run challenge
if (year == 0 && day == 0) {
    Console.WriteLine("Running last challenge!");
    challengeRunner.RunLastChallenge();
} else if (year > 0 && day == 0) {
    Console.WriteLine($"Running all challenges from {year}!");
    challengeRunner.RunYear(year);
} else {
    Console.WriteLine($"Running challenge {year} day {day}!");
    challengeRunner.RunChallenge(year, day);
}