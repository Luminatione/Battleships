using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships.Moves
{
	public class BlindShot : Move
	{
		private List<Coordinates> toCheck;
		
		private Random random = new Random();
		private Coordinates checkedCoordinates;
		public BlindShot(Map map, List<Coordinates> toCheck) : base(map, toCheck)
		{
			this.toCheck = toCheck;
		}

		public override Map.FieldCheckingResult MakeMove()
		{
			int coordinatesIndex = random.Next(0, toCheck.Count);
			checkedCoordinates = toCheck[coordinatesIndex];
			moveResult = map.CheckField(checkedCoordinates);
			toCheck.RemoveAt(coordinatesIndex);
			return moveResult;
		}

		public override Move NextMove()
		{
			if (moveResult == Map.FieldCheckingResult.Hit)
			{
				return new HorizontalMove(map, toCheck, checkedCoordinates);
			}
			return new BlindShot(map, toCheck);
		}
	}
}
