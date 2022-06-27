using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships
{
	public class Coordinates
	{
		public char Letter { get; }
		public int Number { get; }

		public Coordinates()
		{
			Letter = 'a';
			Number = 0;
		}

		public Coordinates(char letter, int number)
		{
			Letter = letter;
			Number = number;
		}

		public Coordinates(Coordinates a)
		{
			Letter = a.Letter;
			Number = a.Number;
		}

		private bool Equals(Coordinates other)
		{
			return Letter == other.Letter && Number == other.Number;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.GetType() == this.GetType() && Equals((Coordinates)obj);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Letter, Number);
		}

		public static bool operator ==(Coordinates a, Coordinates b)
		{
			return a?.Letter == b?.Letter && a?.Number == b?.Number;
		}

		public static bool operator !=(Coordinates a, Coordinates b)
		{
			return !(a == b);
		}

		public static bool IsInBetween(Coordinates a, Coordinates b, Coordinates checking)
		{
			if (a.Letter != b.Letter && a.Number != b.Number)
			{
				return false;
			}
			if (a.Number == b.Number && checking.Number == a.Number)
			{
				return Math.Clamp(checking.Letter, Math.Min(a.Letter, b.Letter), Math.Max(a.Letter, b.Letter)) == checking.Letter;
			}
			if (a.Letter == b.Letter && checking.Letter == a.Letter)
			{
				return Math.Clamp(checking.Number, Math.Min(a.Number, b.Number), Math.Max(a.Number, b.Number)) == checking.Number;
			}
			return false;
		}

		public static (Coordinates a, Coordinates b) GetRandomApartCoordinates(int distance, int sizeX, int sizeY)
		{
			Random random = new Random();
			double t = random.NextDouble();
			bool canPlaceVertically = sizeY - distance - 1 >= 0;
			bool canPlaceHorizontally = sizeX - distance - 1 >= 0;
			//Horizontally
			if ((t >= 0.5 || !canPlaceHorizontally) && canPlaceVertically)
			{
				int x1 = random.Next(0, sizeY - distance - 1);
				//second point should be o the left or right side of first point
				int x2 = x1 + distance;
				int y = random.Next(0, sizeX);
				return (new Coordinates((char)(x1 + 'a'), y), new Coordinates((char)(x2 + 'a'), y));
			}
			//Vertically
			if ((t < 0.5 || !canPlaceVertically) && canPlaceHorizontally)
			{
				int y1 = random.Next(0, sizeX - distance - 1);
				//second point should be above or below of first point
				int y2 = y1 + distance;
				int x = random.Next(0, sizeY);
				return (new Coordinates((char)(x + 'a'), y1), new Coordinates((char)(x + 'a'), y2));

			}
			throw new ArgumentException("SizeX and sizeY are too small to fit coordinates with such distance");
		}

		public static IEnumerable<Coordinates> GetPointsInBetween(Coordinates a, Coordinates b)
		{
			List<Coordinates> result = new List<Coordinates>();
			if (a == b)
			{
				result.Add(new Coordinates(a));
				return result;
			}
			if (a.Letter == b.Letter)
			{
				for (int i = Math.Min(a.Number, b.Number); i <= Math.Max(a.Number, b.Number); i++)
				{
					result.Add(new Coordinates(a.Letter, i));
				}
			}
			if (a.Number == b.Number)
			{
				for (int i = Math.Min(a.Letter, b.Letter); i <= Math.Max(a.Letter, b.Letter); i++)
				{
					result.Add(new Coordinates((char)i, a.Number));
				}
			}
			return result;
		}
	}
}
