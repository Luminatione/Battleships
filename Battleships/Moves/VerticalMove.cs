using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships.Moves
{
	public class VerticalMove : Move
	{
		private Coordinates lastChecked;
		private bool aboveChecked = false;
		private int offsetFromRoot = 1;
		private int aboveHits = 0;
		public VerticalMove(Map map, List<Coordinates> toCheck, Coordinates lastChecked) : base(map, toCheck)
		{
			this.lastChecked = lastChecked;
		}

		public override Map.FieldCheckingResult MakeMove()
		{
			Coordinates checking;
			if (lastChecked.Letter - 'a' < map.SizeY - offsetFromRoot && !aboveChecked)
			{
				checking = new Coordinates((char)(lastChecked.Letter + offsetFromRoot), lastChecked.Number);
			}
			else if (lastChecked.Letter - 'a' - offsetFromRoot >= 0)
			{
				checking = new Coordinates((char)(lastChecked.Letter - offsetFromRoot), lastChecked.Number);
			}
			else
			{
				moveResult = Map.FieldCheckingResult.Checked;
				return moveResult;
			}
			toCheck.RemoveAll(e => e == checking);
			moveResult = map.CheckField(checking);
			offsetFromRoot++;
			return moveResult;
		}

		public override Move NextMove()
		{
			if (aboveChecked)
			{
				if (moveResult == Map.FieldCheckingResult.Hit)
				{
					offsetFromRoot++;
					return this;
				}

				if (aboveHits == 0 && offsetFromRoot == 1)
				{
					return new BlindShot(map, toCheck);
				}

				return new BlindShot(map, toCheck);
			}
			if (moveResult == Map.FieldCheckingResult.Sink)
			{
				return new BlindShot(map, toCheck);
			}

			if (moveResult == Map.FieldCheckingResult.Hit)
			{
				aboveHits++;
				offsetFromRoot++;
				return this;
			}

			offsetFromRoot = 1;
			aboveChecked = true;
			return this;
		}
	}
}
