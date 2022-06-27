using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships.Moves
{
	public class HorizontalMove : Move
	{
		private Coordinates lastChecked;
		public HorizontalMove(Map map, Coordinates lastChecked) : base(map)
		{
			this.lastChecked = lastChecked;
		}

		public override void MakeMove()
		{
			throw new NotImplementedException();
		}

		public override Move NextMove()
		{
			throw new NotImplementedException();
		}
	}
}
