using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships.Moves
{
	public class HorizontalMove : Move
	{
		private Coordinates lastChecked;
		private bool rightSideChecked = false;
		private int offsetFromRoot = 1;
		private int rightSideHits = 0;
		public HorizontalMove(Map map, List<Coordinates> toCheck, Coordinates lastChecked) : base(map, toCheck)
		{
			this.lastChecked = lastChecked;
		}

		public override Map.FieldCheckingResult MakeMove()
		{
			Coordinates checking;
			if (lastChecked.Number < map.SizeX - offsetFromRoot && !rightSideChecked)
			{
				checking = new Coordinates(lastChecked.Letter, lastChecked.Number + offsetFromRoot);
			}
			else if (lastChecked.Number - offsetFromRoot >= 0)
			{
				checking = new Coordinates(lastChecked.Letter, lastChecked.Number - offsetFromRoot);
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
			if (rightSideChecked)
			{
				return ProcessLeftSide();
			}
			return ProcessRightSide();
		}

		private Move ProcessRightSide()
		{
			if (moveResult == Map.FieldCheckingResult.Sink)
			{
				return new BlindShot(map, toCheck);
			}

			if (moveResult == Map.FieldCheckingResult.Hit)
			{
				rightSideHits++;
				offsetFromRoot++;
				return this;
			}

			offsetFromRoot = 1;
			rightSideChecked = true;
			return this;
		}

		private Move ProcessLeftSide()
		{
			if (moveResult == Map.FieldCheckingResult.Hit)
			{
				offsetFromRoot++;
				return this;
			}

			if (rightSideHits == 0 && offsetFromRoot == 1)
			{
				return new VerticalMove(map, toCheck, lastChecked);
			}

			return new BlindShot(map, toCheck);
		}
	}
}
