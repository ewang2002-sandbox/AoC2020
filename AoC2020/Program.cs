#define GENERAL
using System;
using System.IO;
#if !GENERAL
using AoC2020.RevisedSolutions;
#else
using AoC2020.Solutions;
#endif

var input = await File.ReadAllTextAsync(Path.Join("Inputs", "16.txt"));
#if GENERAL
new Day16(input).Solve();
#else
new Day16Revised(input).Solve();
#endif
Console.ReadLine();