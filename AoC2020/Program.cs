#define GENERAL
#undef GENERAL
using System;
using System.IO;
#if !GENERAL
using AoC2020.RevisedSolutions;
#else
using AoC2020.Solutions;
#endif

var input = await File.ReadAllTextAsync(Path.Join("Inputs", "17.txt"));
#if GENERAL
new Day17(input).Solve();
#else
new Day17Revised(input).Solve();
#endif
Console.ReadLine();