using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Battleships;
using Battleships.Ships;

namespace BattleshipsTest
{
	[TestClass]
	public class MapTest
	{
		[TestMethod]
		public void ConstructorTest()
		{
			Assert.ThrowsException<ArgumentException>(() => new Map(4, 4));
			Map map = new Map(10, 10);
			Assert.IsTrue(map.SizeX == 10 && map.SizeY == 10 && map.Ships.Count == 3);
		}
		[TestMethod]
		public void IsGameEndedTest()
		{
			Map map3 = new Map(10, 10);
			map3.Ships.ForEach( ship => Coordinates.GetPointsInBetween(ship.Begin, ship.End).ToList().ForEach(point =>
			{
				Assert.IsFalse(map3.IsGameEnded());
				map3.CheckField(point);
			}));
			Assert.IsTrue(map3.IsGameEnded());
		}
		[TestMethod]
		public void CheckFieldTest()
		{
			Map map = new Map(10, 10);
			map.Ships.Add(new Ship(new Coordinates('c', 2), new Coordinates('c', 2), 1));
			Coordinates a = map.Ships.First().Begin;
			Assert.AreEqual(Map.FieldCheckingResult.Hit, map.CheckField(a));
			Assert.AreEqual(Map.FieldCheckingResult.Checked, map.CheckField(a));
			Map map1 = new Map(10, 10);
			Assert.AreEqual(87, GetMissesAmount(map1));
		}

		private int GetMissesAmount(Map map)
		{
			int misses = 0;
			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					if (map.CheckField(new Coordinates((char)(i + 'a'), j)) == Map.FieldCheckingResult.Miss)
					{
						misses++;
					}
				}
			}
			return misses;
		}
	}
}
