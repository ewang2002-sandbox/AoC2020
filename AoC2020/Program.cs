#define GENERAL
using System;
using System.IO;
#if !GENERAL
using AoC2020.RevisedSolutions;
#else
using AoC2020.Solutions;
#endif

var input = await File.ReadAllTextAsync(Path.Join("Inputs", "22.txt"));
#if GENERAL
new Day22(input).Solve();
#else
new Day22Revised(input).Solve();
#endif
Console.ReadLine();