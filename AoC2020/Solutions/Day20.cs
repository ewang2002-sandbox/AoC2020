using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AoC2020.Solutions
{
	/// <summary>
	/// https://adventofcode.com/2020/day/20
	/// </summary>
	public class Day20 : BaseDay
	{
		private readonly string[] _input;

		public Day20(string input)
			=> _input = input
				.Split(Environment.NewLine + Environment.NewLine)
				.ToArray();

		private readonly Dictionary<int, char[,]> _arrangements = new();

		private void ParseInput()
		{
			foreach (var tile in _input)
			{
				var keyVal = tile.Split(":")
					.Select(x => x.Trim())
					.ToArray();

				var key = int.Parse(keyVal[0].Split("Tile ")[1]);
				var value = keyVal[1]
					.Split(Environment.NewLine)
					.Select(x => x.ToCharArray())
					.ToArray();
				var matrix = new char[10, 10];
				for (var i = 0; i < value.Length; i++)
				for (var j = 0; j < value[i].Length; j++)
					matrix[i, j] = value[i][j];

				_arrangements.Add(key, matrix);
			}
		}

		private char[,] RotateMatrix(char[,] matrix)
		{
			var newMatrix = new char[10, 10];
			for (var i = 0; i < matrix.GetLength(0); i++)
			for (var j = 0; j < matrix.GetLength(1); j++)
				matrix[j, i] = matrix[i, j];

			return newMatrix;
		}

		public override string SolvePart1()
		{
			ParseInput();

			// Step 1: get each tile's edge
			// int = id
			// string = edge
			var allEdges = new Dictionary<int, EdgeContainer>();
			foreach (var (id, tile) in _arrangements)
			{
				var topEdge = new List<char>();
				var bottomEdge = new List<char>();
				var leftEdge = new List<char>();
				var rightEdge = new List<char>();
				for (var i = 0; i < tile.GetLength(0); i++)
				{
					topEdge.Add(tile[0, i]);
					bottomEdge.Add(tile[tile.GetLength(0) - 1, i]);
					leftEdge.Add(tile[i, 0]);
					rightEdge.Add(tile[i, tile.GetLength(1) - 1]);
				}

				Debug.Assert(topEdge.Count == 10
				             && bottomEdge.Count == 10
				             && leftEdge.Count == 10
				             && rightEdge.Count == 10);

				allEdges.Add(id, new EdgeContainer
				{
					Id = id,
					Edges = new IList<char>[]
					{
						topEdge,
						rightEdge,
						bottomEdge,
						leftEdge
					}
				});
			}

			// Step 2: check which tile has only 2 edges that meet
			// 2/4 edges means corner
			// K = id, V = edges met
			var idAndEdges = new Dictionary<int, int>();

			// check all outer edges first
			foreach (var (outerId, outerVals) in allEdges)
			{
				idAndEdges[outerId] = 0;

				// check all inner edges
				foreach (var (innerId, innerVals) in allEdges)
				{
					if (outerId == innerId)
						continue;

					for (var i = 0; i < outerVals.Edges.Length; i++)
					for (var j = 0; j < innerVals.Edges.Length; j++)
					{
						var normalTest = outerVals.Edges[i].SequenceEqual(innerVals.Edges[j]);
						var reverseTest = outerVals.Edges[i].SequenceEqual(innerVals.Edges[j].Reverse());

						if (!normalTest && !reverseTest)
							continue;
						allEdges[outerId].MetEdges.Add((withId: innerId, thisEdgeIndex: i, thatEdgeIndex: j, thatReversed: reverseTest));
						idAndEdges[outerId]++;
					}
				}
			}

			foreach (var (id, val) in allEdges)
				Console.WriteLine($"{id} => {string.Join(", ", val.MetEdges)}");
			// Step 3: get the answer
			var ans = 1L;
			foreach (var (id, key) in idAndEdges)
				if (key == 2)
					ans *= id;

			// Step 4: do what the problem told us to do and reconstruct the image...
			var sideLen = (int) Math.Sqrt(allEdges.Count);
			var reconstructedImage = new string[sideLen, sideLen];
			

			return ans.ToString();
		}

		public override string SolvePart2()
		{
			return string.Empty;
		}
	}

	public class EdgeContainer
	{
		public int Id { get; set; }

		// 0 = top, 1 = right, 2 = down, 3 = left
		public IList<char>[] Edges { get; set; }
		public List<(int withId, int thisEdgeIndex, int thatEdgeIndex, bool thatReversed)> MetEdges { get; set; } = new();
	}
}