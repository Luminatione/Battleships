using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships.Moves
{
	public class BlindShot : Move
	{
		private List<Coordinates> toCheck;
		private Map.FieldCheckingResult moveResult;
		private Random random = new Random();
		private Coordinates checkedCoordinates;
		public BlindShot(Map map, List<Coordinates> toCheck) : base(map)
		{
			this.toCheck = toCheck;
		}

		public override void MakeMove()
		{
			int coordinatesIndex = random.Next(0, toCheck.Count);
			checkedCoordinates = toCheck[coordinatesIndex];
			moveResult = map.CheckField(checkedCoordinates);
			toCheck.RemoveAt(coordinatesIndex);
		}

		public override Move NextMove()
		{
			if (moveResult == Map.FieldCheckingResult.Hit)
			{
				return new HorizontalMove(map, checkedCoordinates);
			}
			return new BlindShot(map, toCheck);
		}
	}
}
