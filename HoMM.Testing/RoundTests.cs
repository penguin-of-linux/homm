using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using HoMM;
using System.Drawing;

namespace HexModelTesting
{
    [TestFixture]
    public class RoundTests
    {
        Round round;

        [SetUp]
        public void PrepareGoodMap()
        {
            round = new Round("TestMaps\\goodMap.txt", new string[] { "First", "Second" });
            round.UpdateTick(new Point[] { new Point(0, 0), new Point(2, 1) });
        }

        [Test]
        public void TestMineCapturing()
        {
            round.UpdateTick(new Point[] { new Point(1, 0), new Point(2, 1) });
            var mine = (Mine)round.map[1, 0].tileObject;
            Assert.That(mine.Owner == round.players[0]);
            Assert.That(mine.Resource == Resource.Rubles);
            round.DailyTick();
            Assert.AreEqual(round.players[0].CheckResourceAmount(Resource.Rubles), 1000);
        }

        [Test]
        public void TestResGathering()
        {
            round.UpdateTick(new Point[] { new Point(0, 0), new Point(1, 1) });
            Assert.That(round.players[1].CheckResourceAmount(Resource.Rubles) == 100);
            //Assert.That(round.map[1, 1].tileObject == null);
        }

        [Test]
        public void TestObjectRecapture()
        {
            var obj = (CapturableObject)round.map[2, 1].tileObject;
            Assert.That(obj.Owner == round.players[1]);
            round.UpdateTick(new Point[] { new Point(2, 1), new Point(0, 0) });
            Assert.That(obj.Owner == round.players[0]);
        }

        #region player.TryBuyUnits testing
        [Test]
        public void PurchaseFailsWhenNotAtDwelling()
        {
            Assert.False(round.players[0].TryBuyUnits(1));
            Assert.That(round.players[0].Army[UnitType.Ranged] == 0);
        }

        [Test]
        public void PurchaseThrowsWhenAskedForNegativeUnits()
        {
            Assert.Throws<ArgumentException>(() => round.players[0].TryBuyUnits(-1));
        }

        [Test]
        public void PurchaseFailsWhenNoAvailableUnits()
        {
            var p = round.players[0];
            p.GainResources(Resource.Rubles, 50);
            p.GainResources(Resource.Wood, 1);
            Assert.False(p.TryBuyUnits(1));
            Assert.That(round.players[0].Army[UnitType.Ranged] == 0);
        }

        [Test]
        public void PurchaseFailsWhenNotEnoughResources()
        {
            for (int i = 0; i < 7; i++)
                round.DailyTick();
            var dwelling = (Dwelling)round.map[2, 1].tileObject;
            Assert.That(dwelling.AvailableUnits == 16);
            Assert.False(round.players[0].TryBuyUnits(1));
        }
        #endregion
    }
}
