using System;
using System.Collections.Generic;
using System.Text;
using Battleships.Moves;

namespace Battleships
{
	public class AIPlayer
	{
		private List<Coordinates> moves;
		private int currentMove = 0;

		public Map Map { get; }
		private Move moveType;

		public AIPlayer(Map map)
		{
			this.Map = map;
			moves = GetAllPossibleCoordinates();
			moveType = new BlindShot(map, moves);
		}

		private List<Coordinates> GetAllPossibleCoordinates()
		{
			List<Coordinates> result = new List<Coordinates>(Map.SizeX * Map.SizeY);
			for (int i = 0; i < Map.SizeX; i++)
			{
				for (int j = 0; j < Map.SizeY; j++)
				{
					result.Add(new Coordinates((char)(j + 'a'), i));
				}
			}

			return result;
		}

		public void MakeMove()
		{
			moveType.MakeMove();
			moveType = moveType.NextMove();
		}
	}
}
