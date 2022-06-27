using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships.Moves
{
	public abstract class Move
	{
		protected Map map;
		protected List<Coordinates> toCheck;
		protected Map.FieldCheckingResult moveResult;

		public Move(Map map, List<Coordinates> toCheck)
		{
			this.map = map;
			this.toCheck = toCheck;
		}

		public abstract Map.FieldCheckingResult MakeMove();
		public abstract Move NextMove();
	}
}
