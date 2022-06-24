using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships.Ships
{
	public class Ship
	{
		public Coordinates Begin { get; }
		public Coordinates End { get; }
		public int Size { get; set; }

		public Ship(Coordinates begin, Coordinates end, int size)
		{
			Begin = begin;
			End = end;
			Size = size;
		}
	}
}
