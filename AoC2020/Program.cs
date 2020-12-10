#define GENERAL
using System;
using System.IO;
using System.Threading.Tasks;
#if !GENERAL
using AoC2020.RevisedSolutions;
#else
using AoC2020.Solutions;
#endif

namespace AoC2020
{
	public class Program
	{
		public static async Task Main()
		{
			var input = await File
				.ReadAllTextAsync(Path.Join("Inputs", "10.txt"));

#if GENERAL
			new Day10(input).Solve();
#else
			new Day10Revised(input).Solve();
#endif
			Console.ReadLine();
		}
	}
}