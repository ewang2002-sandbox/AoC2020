using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AoC2020.Solutions
{
	/// <summary>
	/// https://adventofcode.com/2020/day/4
	/// </summary>
	public class Day04 : BaseDay
	{
		private readonly IList<string> _input;

		public Day04(string input)
			=> _input = input
				.Split(Environment.NewLine + Environment.NewLine)
				.ToArray();

		// 254
		public override string SolvePart1()
		{
			var valid = 0;
			foreach (var passport in _input)
			{
				var lines = passport.Split("\n");
				var ct = 0;
				foreach (var line in lines)
				{
					var arr = line.Split(" ")
						.Select(x => x.Split(":")[0])
						.ToList();

					arr.Remove("cid");
					ct += arr.Count;
				}

				if (ct == 7) valid++;
			}

			return valid.ToString();
		}

		public override string SolvePart2()
		{
			// define relevant functions
			bool CheckByr(string input)
			{
				var parsedVal = int.Parse(input);
				return 1920 <= parsedVal && parsedVal <= 2002;
			}

			bool CheckIyr(string input)
			{
				var parsedVal = int.Parse(input);
				return 2010 <= parsedVal && parsedVal <= 2020;
			}

			bool CheckEyr(string input)
			{
				var parsedVal = int.Parse(input);
				return 2020 <= parsedVal && parsedVal <= 2030;
			}

			bool CheckHgt(string input)
			{
				if (input.EndsWith("cm"))
				{
					var parsedVal = int.Parse(input.Replace("cm", string.Empty));
					return 150 <= parsedVal && parsedVal <= 193;
				}

				if (input.EndsWith("in"))
				{
					var parsedVal = int.Parse(input.Replace("in", string.Empty));
					return 59 <= parsedVal && parsedVal <= 76;
				}

				return false;
			}

			bool CheckHcl(string input)
			{
				if (input.Length != 7) return false;
				if (input[0] != '#') return false;
				return input[1..].Count(x => 'a' <= x && x <= 'f' || char.IsNumber(x)) == 6;
			}

			bool CheckEcl(string input)
			{
				var poss = new[] {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
				return poss.Contains(input);
			}

			bool CheckPid(string input) => input.Length == 9 && input.Count(char.IsNumber) == 9;

			var validPw = 0;
			foreach (var passport in _input)
			{
				var dict = new Dictionary<string, string>();
				var lines = passport.Split("\n");
				foreach (var line in lines)
				{
					var arr = line.Split(" ")
						.Where(x => !x.StartsWith("cid"))
						.ToList();

					foreach (var a in arr)
					{
						var keyVal = a.Split(":");
						dict.Add(keyVal[0].Trim(), keyVal[1].Trim());
					}
				}

				// not all elements exist
				if (dict.Count != 7)
					continue;

				if (CheckByr(dict["byr"])
				    && CheckIyr(dict["iyr"])
				    && CheckEyr(dict["eyr"])
				    && CheckHgt(dict["hgt"])
				    && CheckHcl(dict["hcl"])
				    && CheckEcl(dict["ecl"])
				    && CheckPid(dict["pid"]))
					validPw++; 
			}

			return validPw.ToString();
		}
	}
}