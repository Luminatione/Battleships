using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Battleships.Ships;
using Microsoft.VisualBasic;

namespace Battleships
{
	public class Map
	{
		public enum FieldCheckingResult
		{
			Miss,
			Hit,
			Sink,
			Checked
		}

		private List<bool> fieldsCheckState;
		private int hits = 0;
		private int maxHits = 30;

		public List<Ship> Ships { get; } = new List<Ship>();
		public int SizeX { get; }
		public int SizeY { get; }

		public string MapDisplay => mapDisplayBuilder.ToString();
		private StringBuilder mapDisplayBuilder = new StringBuilder();

		public Map(int sizeX, int sizeY)
		{
			SizeX = sizeX;
			SizeY = sizeY;
			AddShipOnMap(5);
			AddShipOnMap(4);
			AddShipOnMap(4);
			for (int i = 0; i < 3; i++)
			{
				AddShipOnMap(3);
			}
			for (int i = 0; i < 4; i++)
			{
				AddShipOnMap(2);
			}
			fieldsCheckState = new List<bool>(new bool[SizeX * SizeY]);
			Array.Fill(fieldsCheckState.ToArray(), false);
			PrepareMapDisplay(sizeX);
		}

		private void PrepareMapDisplay(int sizeX)
		{
			for (int i = 0; i < SizeY; i++)
			{
				mapDisplayBuilder.Insert(mapDisplayBuilder.Length, "? ", SizeX).Append("\n");
			}
		}

		public FieldCheckingResult CheckField(Coordinates coordinates)
		{
			int fieldIndex = coordinates.Letter - 'a' + coordinates.Number * SizeX;//field with given coordinates
			if (fieldsCheckState[fieldIndex])
			{
				return FieldCheckingResult.Checked;
			}
			fieldsCheckState[fieldIndex] = true;
			return CheckIfShipWasHit(coordinates, fieldIndex);
		}

		private FieldCheckingResult CheckIfShipWasHit(Coordinates coordinates, int fieldIndex)
		{
			int mapDisplayIndex = 2 * fieldIndex + fieldIndex / SizeX;//shift caused by new lines and spaces
			Ship hitShip = Ships.FirstOrDefault(ship => Coordinates.IsInBetween(ship.Begin, ship.End, coordinates));
			if (hitShip == null)
			{
				mapDisplayBuilder[mapDisplayIndex] = '_';
				return FieldCheckingResult.Miss;
			}
			hits++;
			hitShip.Size--;
			mapDisplayBuilder[mapDisplayIndex] = '!';
			return hitShip.Size == 0 ? FieldCheckingResult.Sink : FieldCheckingResult.Hit;
		}

		public bool IsGameEnded()
		{
			return hits == maxHits;
		}

		private bool IsPositionFree(Coordinates begin, Coordinates end)
		{
			//is there point between any ship's end and begin that is between given begin and end?
			return !Ships.Any(ship => Coordinates.GetPointsInBetween(ship.Begin, ship.End).Any(point => Coordinates.IsInBetween(begin, end, point)));
		}

		private void AddShipOnMap(int size)
		{
			Coordinates a, b;
			do
			{
				(a, b) = Coordinates.GetRandomApartCoordinates(size - 1, SizeX, SizeY);//distance between ships' ends gonna be size of ship - 1
			} while (!IsPositionFree(a, b));
			Ships.Add(new Ship(a, b, size));
		}
	}
}
