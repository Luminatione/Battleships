using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Battleships;

namespace BattleshipsTest
{
	[TestClass]
	public class CoordinatesTest
	{
		[TestMethod]
		public void ConstructorTest()
		{
			Coordinates a = new Coordinates();
			Coordinates b = new Coordinates('a', 4);
			Coordinates c = new Coordinates('b', 999);
			Coordinates d = new Coordinates(b);
			Assert.IsTrue(a.Letter == 'a' && a.Number == 0);
			Assert.IsTrue(b.Letter == 'a' && b.Number == 4);
			Assert.IsTrue(c.Letter == 'b' && c.Number == 999);
			Assert.IsTrue(d.Letter == b.Letter && d.Number == b.Number);
		}

		[TestMethod]
		public void EqualityTest()
		{
			Coordinates a = new Coordinates();
			Coordinates b = new Coordinates('a', 4);
			Coordinates c = new Coordinates('b', 999);
			Coordinates d = new Coordinates(b);
			Assert.AreNotEqual(a, b);
			Assert.AreNotEqual(c, d);
			Assert.AreEqual(d, b);
			Assert.AreEqual(new Coordinates(), new Coordinates());
		}

		[TestMethod]
		public void IsInBetweenTest()
		{
			Coordinates a = new Coordinates();
			Coordinates b = new Coordinates('a', 4);
			Coordinates c = new Coordinates('b', 999);
			Coordinates d = new Coordinates('a', 9);
			Assert.IsTrue(Coordinates.IsInBetween(a, b, new Coordinates()));

			Assert.IsFalse(Coordinates.IsInBetween(a, c, new Coordinates()));
			Assert.IsFalse(Coordinates.IsInBetween(a, c, new Coordinates('d', 0)));
			Assert.IsFalse(Coordinates.IsInBetween(a, c, new Coordinates('a', 0)));
			Assert.IsFalse(Coordinates.IsInBetween(a, c, new Coordinates('a', 999)));
			Assert.IsFalse(Coordinates.IsInBetween(a, c, new Coordinates('b', 0)));
			Assert.IsFalse(Coordinates.IsInBetween(a, c, new Coordinates('b', 999)));

			Assert.IsTrue(Coordinates.IsInBetween(b, d, new Coordinates('a', 8)));
			Assert.IsTrue(Coordinates.IsInBetween(d, b, new Coordinates('a', 8)));
			Assert.IsTrue(Coordinates.IsInBetween(b, b, new Coordinates('a', 4)));
		}

		[TestMethod]
		public void GetRandomApartCoordinatesTest()
		{
			(Coordinates a, Coordinates b) = Coordinates.GetRandomApartCoordinates(2, 3, 1);
			Assert.IsTrue(a == new Coordinates('a', 0) && b == new Coordinates('a', 2));

			(Coordinates c, Coordinates d) = Coordinates.GetRandomApartCoordinates(1000, 1, 5000);
			Assert.IsTrue(Math.Abs(c.Letter - d.Letter) == 1000);


			(Coordinates e, Coordinates f) = Coordinates.GetRandomApartCoordinates(1000, 5000, 1);
			Assert.IsTrue(Math.Abs(e.Number - f.Number) == 1000);

			Assert.ThrowsException<ArgumentException>(() => Coordinates.GetRandomApartCoordinates(1000, 999, 1));

		}
		[TestMethod]
		public void GetPointsInBetweenTest()
		{
			CollectionAssert.AreEquivalent(new List<Coordinates> {new Coordinates('a', 0)},
				Coordinates.GetPointsInBetween(new Coordinates('a', 0), new Coordinates('a', 0)).ToList());
			CollectionAssert.AreEquivalent(
				new List<Coordinates> {new Coordinates('b', 1), new Coordinates('b', 2), new Coordinates('b', 3)},
				Coordinates.GetPointsInBetween(new Coordinates('b', 1), new Coordinates('b', 3)).ToList());
			CollectionAssert.AreEquivalent(
				new List<Coordinates> { new Coordinates('b', 1), new Coordinates('c', 1), new Coordinates('d', 1) },
				Coordinates.GetPointsInBetween(new Coordinates('b', 1), new Coordinates('d', 1)).ToList());
			CollectionAssert.AreEquivalent(
				new List<Coordinates> { new Coordinates('b', 1), new Coordinates('c', 1), new Coordinates('d', 1) },
				Coordinates.GetPointsInBetween(new Coordinates('d', 1), new Coordinates('b', 1)).ToList());
			CollectionAssert.AreEquivalent(
				new List<Coordinates>(),
				Coordinates.GetPointsInBetween(new Coordinates('d', 1), new Coordinates('b', 3)).ToList());

		}
	}
}
