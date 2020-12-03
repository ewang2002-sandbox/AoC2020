using System;
using System.IO;
using System.Threading.Tasks;
using AoC2020.Solutions;

namespace AoC2020
{
	public class Program
	{
		public static async Task Main()
		{
			var input = await File
				.ReadAllLinesAsync(Path.Join("Inputs", "01.txt"));

			new Day01(input).Solve();
			Console.ReadLine();
		}
	}
}
