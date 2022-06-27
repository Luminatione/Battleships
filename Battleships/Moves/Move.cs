using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships.Moves
{
	public abstract class Move
	{
		protected Map map;

		public Move(Map map)
		{
			this.map = map;
		}

		public abstract void MakeMove();
		public abstract Move NextMove();
	}
}
