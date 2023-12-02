﻿using Jacco.AOC;
using Jacco.AOC.Challenges;

// Create runner
ChallengeRunner runner = new ChallengeRunner();

// Register challenges
runner.RegisterChallenge(new Day01());
runner.RegisterChallenge(new Day02());

// Run challenges
runner.Run();

