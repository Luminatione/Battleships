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

		private Map map;
		private Move moveType;

		public AIPlayer(Map map)
		{
			this.map = map;
			moves = GetAllPossibleCoordinates();
			moveType = new BlindShot(map, moves);
		}

		private List<Coordinates> GetAllPossibleCoordinates()
		{
			List<Coordinates> result = new List<Coordinates>(map.SizeX * map.SizeY);
			for (int i = 0; i < map.SizeX; i++)
			{
				for (int j = 0; j < map.SizeY; j++)
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
