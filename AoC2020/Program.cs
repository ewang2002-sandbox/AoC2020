#define GENERAL
using System;
using System.IO;
using System.Text;
#if !GENERAL
using AoC2020.RevisedSolutions;
#else
using AoC2020.Solutions;
#endif

var input = await File.ReadAllTextAsync(Path.Join("Inputs", "23.txt"));
#if GENERAL
new Day23(input).Solve();
#else
new Day23Revised(input).Solve();
#endif
Console.ReadLine();