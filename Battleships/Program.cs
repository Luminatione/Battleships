using System;
using System.Collections.Generic;

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

			Map map = new Map(xSize, ySize);

			while (!map.IsGameEnded())
			{
				Console.Write(map.MapDisplay);
				string input = Console.ReadLine();

				if (IsValidInput(input))
				{
					Map.FieldCheckingResult result = map.CheckField(new Coordinates(input[0], input[1] - '0'));
					Console.Clear();
					Console.WriteLine(result.ToString());
				}
				else
				{
					Console.Clear();
					Console.WriteLine("Entered invalid position");
				}
			}
			Console.Clear();
			Console.WriteLine("You won!");
		}
	}
}
