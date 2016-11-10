using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using HoMM;

namespace HexModelTesting
{
    [TestFixture]
    public class TileTerrainTests
    {
        [Test]
        public void AddGoodTerrain()
        {
            TileTerrain t = new TileTerrain(TerrainType.Marsh);
            Assert.AreEqual(t.TravelCost, 1.75, 0.0001);
            Assert.AreEqual(t.TerrainType.ToString(), "Marsh");
        }

        [Test]
        public void EditCorrectlyFixesTravelCost()
        {
            var t = new TileTerrain(TerrainType.Grass);
            Assert.AreEqual(t.TravelCost, 1, 0.0001);
            t.TerrainType = TerrainType.Arid;
            Assert.AreEqual(t.TravelCost, 1.25, 0.0001);
        }
    }
}
