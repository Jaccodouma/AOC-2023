using Jacco.AOC;

#region args
// Check if only examples should be run
bool examplesOnly = false;
if (args.Length > 0) {
    examplesOnly = args.Contains("-e");
}

// Remove all flags from args
args = args.Where(a => !a.StartsWith("-")).ToArray();

// Get year and day from args
int year = 0;
int day = 0;

if (args.Length > 0) {
    year = int.Parse(args[0]);
}

if (args.Length > 1) {
    day = int.Parse(args[1]);
}
#endregion

// Welcome message
ConsoleWrapper.Init("Jacco's Advent Of Code attempt!", "Happy Holidays! <3");

// Create challenge runner
ChallengeRunner challengeRunner = new(examplesOnly);

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