using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleships
{
	class Program
	{
		private static int xSize = 10, ySize = 10;

		private static bool IsValidInput(string input)
		{
			return input.Length == 2 && input[0] < ySize + 'a' && input[0] >= 'a' && input[1] >= '0' && input[1] < xSize + '0';
		}


		static void Main(string[] args)
		{

			AIPlayer firstPlayer = new AIPlayer(new Map(xSize, ySize));
			AIPlayer secondPlayer = new AIPlayer(new Map(xSize, ySize));
			do
			{
				while (firstPlayer.MakeMove() != Map.FieldCheckingResult.Miss) ;
				while (secondPlayer.MakeMove() != Map.FieldCheckingResult.Miss) ;
			} while (!firstPlayer.Map.IsGameEnded() && !secondPlayer.Map.IsGameEnded());
		}
	}
}
