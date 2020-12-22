using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

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

		private readonly List<Tile> _allTiles = new();

		public override string SolvePart1()
		{
			// Step 1: create all tiles
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

				_allTiles.Add(new Tile(key, matrix));
			}

			// Step 2: check which tile has only 2 edges that meet
			// 2/4 edges means corner
			// check all outer edges first
			for (var i = 0; i < _allTiles.Count; i++)
			{
				var outerTile = _allTiles[i];
				// check all inner edges
				foreach (var innerTile in _allTiles)
				{
					if (outerTile.Id == innerTile.Id)
						continue;

					foreach (var outerEdge in outerTile.Edges)
					foreach (var innerEdge in innerTile.Edges)
					{
						if (!outerEdge.SequenceEqual(innerEdge) && !outerEdge.SequenceEqual(innerEdge.Reverse()))
							continue;

						_allTiles[i].MetEdges.Add((thisSeq: outerEdge, thatId: innerTile.Id, thatSeq: innerEdge));
					}
				}
			}

			// Step 3: get the answer
			var ans = 1L;
			foreach (var tile in _allTiles)
				// 2 means only 2 sides had a corresponding edge
				if (tile.MetEdges.Count == 2)
					ans *= tile.Id;

			return ans.ToString();
		}

		private char[,] ConstructImage()
		{
			var lenOfSide = (int) Math.Sqrt(_allTiles.Count);
			var image = new Tile[lenOfSide, lenOfSide];

			var firstCornerTile = _allTiles.First(x => x.MetEdges.Count == 2);
			_allTiles.Remove(firstCornerTile);
			image[0, 0] = firstCornerTile;

			// Rotate the corner tile so we won't go out of bounds 
			var firstNeighboringTile = _allTiles.First(x => x.Id == firstCornerTile.MetEdges[0].thatId);
			var secondNeighboringTile = _allTiles.First(x => x.Id == firstCornerTile.MetEdges[1].thatId);
			var rotationsCompleted = 0;
			var horizFlipsCompleted = 0;
			var vertFlipsCompleted = 0;
			while (true)
			{
				// TODO we need to rotate the neighboring tiles as well...
				if (firstCornerTile.SharesEdge(firstNeighboringTile, Tile.Direction.Right)
				    && firstCornerTile.SharesEdge(secondNeighboringTile, Tile.Direction.Bottom))
					break;

				if (firstCornerTile.SharesEdge(firstNeighboringTile, Tile.Direction.Bottom)
				    && firstCornerTile.SharesEdge(secondNeighboringTile, Tile.Direction.Right))
					break;

				if (rotationsCompleted < 4)
				{
					firstCornerTile.RotateTile();
					rotationsCompleted++;
				}
				else if (horizFlipsCompleted < 4)
				{
					firstCornerTile.RotateTile();
					firstCornerTile.FlipTile(Tile.FlipAcross.HorizontalLine);
					horizFlipsCompleted++;
				}
				else if (vertFlipsCompleted < 4)
				{
					firstCornerTile.RotateTile();
					firstCornerTile.FlipTile(Tile.FlipAcross.VerticalLine);
					vertFlipsCompleted++;
				}
				else
					throw new Exception("Something went wrong...");
			}

			Tile previousTile;
			for (var i = 0; i < image.GetLength(0); i++)
			{
				previousTile = image[i, 0];

				for (var j = 0; j < image.GetLength(1); j++)
				{
					if (i == j && i == 0)
						continue;

					if (j == 0)
					{
						// get the previous tile (left tile) that neighbors w/ the right tile (this tile).
						for (var a = 0; a < previousTile.MetEdges.Count; a++)
						{
							// this is the tile that we plan on putting at this i j position
							var targetTile = _allTiles.First(x => x.Id == previousTile.MetEdges[a].thatId);
							var rCompleted = 0;
							var hFlipsCompleted = 0;
							var vFlipsCompleted = 0;
							var isFound = false;
							while (true)
							{
								if (previousTile.SharesEdge(targetTile, Tile.Direction.Right))
								{
									isFound = true;
									break;
								}

								if (rCompleted < 4)
								{
									targetTile.RotateTile();
									rCompleted++;
									continue;
								}

								if (hFlipsCompleted < 4)
								{
									targetTile.RotateTile();
									targetTile.FlipTile(Tile.FlipAcross.HorizontalLine);
									hFlipsCompleted++;
									continue;
								}

								if (vFlipsCompleted < 4)
								{
									targetTile.RotateTile();
									targetTile.FlipTile(Tile.FlipAcross.VerticalLine);
									vFlipsCompleted++;
									continue;
								}

								break;
							}

							if (!isFound)
								continue;
							image[i, j] = targetTile;
							previousTile = targetTile;
							break;
						}
					}
					else // in between
					{
						// get the tile that is directly above us 
						var topNeighboringTile = _allTiles.First(x => x.Id == image[i, j - 1].Id);
						for (var a = 0; a < previousTile.MetEdges.Count; a++)
						{
							// this is the tile that we plan on putting here
							var targetTile = _allTiles.First(x => x.Id == previousTile.MetEdges[a].thatId);

							var rCompleted = 0;
							var hFlipsCompleted = 0;
							var vFlipsCompleted = 0;
							var isFound = false;
							while (true)
							{
								if (previousTile.SharesEdge(targetTile, Tile.Direction.Right)
								    && topNeighboringTile.SharesEdge(targetTile, Tile.Direction.Top))
								{
									isFound = true;
									break;
								}

								if (rCompleted < 4)
								{
									targetTile.RotateTile();
									rCompleted++;
									continue;
								}

								if (hFlipsCompleted < 4)
								{
									targetTile.RotateTile();
									targetTile.FlipTile(Tile.FlipAcross.HorizontalLine);
									hFlipsCompleted++;
									continue;
								}

								if (vFlipsCompleted < 4)
								{
									targetTile.RotateTile();
									targetTile.FlipTile(Tile.FlipAcross.VerticalLine);
									vFlipsCompleted++;
									continue;
								}

								break;
							}

							if (!isFound)
								continue;

							image[i, j] = targetTile;
							previousTile = targetTile;
							break;
						}
					}
				}
			}

			return new char[1, 1];
		}

		public override string SolvePart2()
		{
			var image = ConstructImage();
			return string.Empty;
		}
	}

	public class Tile
	{
		/// <summary>
		/// How the tile looks.
		/// </summary>
		public char[,] Arrangement { get; private set; }

		/// <summary>
		///  The edges.
		/// </summary>
		public IList<char>[] Edges { get; init; }

		/// <summary>
		/// The tile's ID.
		/// </summary>
		public int Id { get; init; }

		/// <summary>
		/// All the "linked" tiles.
		/// </summary>
		public List<(IList<char> thisSeq, int thatId, IList<char> thatSeq)> MetEdges { get; init; } = new();

		/// <summary>
		/// Creates a new Tile object. 
		/// </summary>
		/// <param name="id">The Tile's ID</param>
		/// <param name="arrangement">The arrangement.</param>
		public Tile(int id, char[,] arrangement)
		{
			Id = id;

			Arrangement = arrangement;

			// get tile info
			var topEdge = new List<char>();
			var bottomEdge = new List<char>();
			var leftEdge = new List<char>();
			var rightEdge = new List<char>();
			for (var i = 0; i < arrangement.GetLength(0); i++)
			{
				topEdge.Add(arrangement[0, i]);
				bottomEdge.Add(arrangement[arrangement.GetLength(0) - 1, i]);
				leftEdge.Add(arrangement[i, 0]);
				rightEdge.Add(arrangement[i, arrangement.GetLength(1) - 1]);
			}

			Debug.Assert(topEdge.Count == 10
			             && bottomEdge.Count == 10
			             && leftEdge.Count == 10
			             && rightEdge.Count == 10);

			Edges = new IList<char>[]
			{
				topEdge,
				rightEdge,
				bottomEdge,
				leftEdge
			};
		}

		/// <summary>
		/// Flips the tile across the horizontal or vertical line.
		/// </summary>
		/// <param name="flipAcross">The direction to flip the tile to.</param>
		public void FlipTile(FlipAcross flipAcross)
		{
			var newMatrix = new char[Arrangement.GetLength(0), Arrangement.GetLength(1)];
			switch (flipAcross)
			{
				case FlipAcross.HorizontalLine:
					for (var i = 0; i < Arrangement.GetLength(0) / 2; i++)
					for (var j = 0; j < Arrangement.GetLength(1); j++)
					{
						newMatrix[Arrangement.GetLength(0) - 1 - i, j] = Arrangement[i, j];
						newMatrix[i, j] = Arrangement[Arrangement.GetLength(0) - 1 - i, j];
					}

					break;
				case FlipAcross.VerticalLine:
					for (var i = 0; i < Arrangement.GetLength(0); i++)
					for (var j = 0; j < Arrangement.GetLength(1) / 2; j++)
					{
						newMatrix[i, Arrangement.GetLength(1) - 1 - j] = Arrangement[i, j];
						newMatrix[i, j] = Arrangement[i, Arrangement.GetLength(1) - 1 - j];
					}

					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(flipAcross), flipAcross, null);
			}

			Arrangement = newMatrix;
		}

		/// <summary>
		/// Rotates the tile 90 degrees clockwise. 
		/// </summary>
		public void RotateTile()
		{
			var newMatrix = new char[Arrangement.GetLength(0), Arrangement.GetLength(1)];
			for (var r = 0; r < Arrangement.GetLength(0); r++)
			for (var c = 0; c < Arrangement.GetLength(1); c++)
				newMatrix[c, Arrangement.GetLength(0) - 1 - r] = Arrangement[r, c];

			Arrangement = newMatrix;
		}

		/// <summary>
		/// Prints the tiles. 
		/// </summary>
		public void PrintTile()
			=> Console.WriteLine(ToString());

		/// <summary>
		/// Returns the string representation of this tile.
		/// </summary>
		/// <returns>The string representation of the tile object.</returns>
		public override string ToString()
		{
			var sb = new StringBuilder();
			for (var i = 0; i < Arrangement.GetLength(0); i++)
			{
				for (var j = 0; j < Arrangement.GetLength(1); j++)
					sb.Append(Arrangement[i, j]);
				sb.AppendLine();
			}

			return sb.ToString();
		}

		/// <summary>
		/// Checks whether the tile shares an edge with another tile. 
		/// </summary>
		/// <param name="tile">The other tile to check.</param>
		/// <param name="dir">The direction to check. Checking the right and bottom tiles will result in the program comparing the right or bottom tiles of this object with the left or top tiles of the given object. Checking the left and top tiles will result in the program comparing the left or top tiles of this object with the right or bottom tiles of the given object.</param>
		/// <returns></returns>
		public bool SharesEdge(Tile tile, Direction dir)
		{
			var thisEdge = new List<char>();
			var thatEdge = new List<char>();

			for (var i = 0; i < tile.Arrangement.GetLength(0); i++)
			{
				switch (dir)
				{
					case Direction.Bottom:
					{
						thisEdge.Add(Arrangement[Arrangement.GetLength(0) - 1, i]);
						thatEdge.Add(tile.Arrangement[0, i]);
						break;
					}
					case Direction.Right:
					{
						thisEdge.Add(Arrangement[i, Arrangement.GetLength(1) - 1]);
						thatEdge.Add(tile.Arrangement[i, 0]);
						break;
					}
					case Direction.Left:
					{
						thisEdge.Add(Arrangement[i, 0]);
						thatEdge.Add(tile.Arrangement[i, tile.Arrangement.GetLength(1) - 1]);
						break;
					}
					case Direction.Top:
					{
						thisEdge.Add(Arrangement[0, i]);
						thatEdge.Add(tile.Arrangement[tile.Arrangement.GetLength(0) - 1, i]);
						break;
					}
					default:
						throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
				}
			}

			return thatEdge.SequenceEqual(thisEdge);
		}


		public enum Direction
		{
			Left,
			Right,
			Top,
			Bottom
		}

		public enum FlipAcross
		{
			HorizontalLine,
			VerticalLine
		}
	}
}