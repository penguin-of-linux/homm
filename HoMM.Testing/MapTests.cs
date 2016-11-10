using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using HoMM;
using NUnit.Framework;

namespace HexModelTesting
{
    [TestFixture]
    public class MapTests
    {
        [Test]
        public void Test1TileMap()
        {
            var m = new Map("TestMaps\\map1.txt");
            Assert.AreEqual(m.Height, 1);
            Assert.AreEqual(m.Width, 1);
            Assert.IsEmpty(m.GetNeighbourTiles(0, 0));
        }

        [Test]
        public void InitOfBadTerrainThrows()
        {
            Assert.Throws<ArgumentException>(
                () => new Map("TestMaps\\badTerrain.txt"), 
                "Unknown terrain type!", 
                new object[] { });
        }

        [Test]
        public void InitOfBadObjectThrows()
        {
            Assert.Throws<ArgumentException>(
                () => new Map("TestMaps\\badObject.txt"),
                "Unknown object!",
                new object[] { });
        }

        [Test]
        public void TestGoodMap()
        {
            var m = new Map("TestMaps\\goodMap.txt");
            Assert.AreEqual(m.Height, 6);
            Assert.AreEqual(m.Width, 5);
            var tiles = m.GetNeighbourTiles(4, 5);
            Assert.AreEqual(tiles.Count, 3);
            var expected = new List<Point> { new Point(3, 4), new Point(3, 5), new Point(4, 4) };
            CollectionAssert.AreEquivalent(tiles.Select(t => t.location), expected);
        }

        [Test]
        public void TestMapWithUnits()
        {
            var m = new Map("TestMaps\\mapWithUnits.txt");
            //Assert.True(m[])
        }
    }
}
