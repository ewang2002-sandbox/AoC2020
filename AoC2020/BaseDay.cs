using System;
using System.Diagnostics;

namespace AoC2020
{
	public abstract class BaseDay
	{
		/// <summary>
		/// Solution to part 1.
		/// </summary>
		/// <returns>The solution.</returns>
		public abstract string SolvePart1();

		/// <summary>
		/// Solution to part 2.
		/// </summary>
		/// <returns>The solution.</returns>
		public abstract string SolvePart2();

		/// <summary>
		/// The method that will be executed when solving a problem. Your implementation for each part should be private.
		/// </summary>
		public void Solve()
		{
			var sw = new Stopwatch();
			sw.Start();
			var sol1 = SolvePart1();
			sw.Stop();
			var timeTakenSol1 = sw.Elapsed;

			sw.Restart();
			var sol2 = SolvePart2();
			sw.Stop();
			var timeTakenSol2 = sw.Elapsed;

			Console.WriteLine($"Day 1 Solution: {sol1}\n\tTime Taken: {timeTakenSol1.Milliseconds} MS.");
			Console.WriteLine($"Day 2 Solution: {sol2}\n\tTime Taken: {timeTakenSol2.Milliseconds} MS.");
		}
	}
}