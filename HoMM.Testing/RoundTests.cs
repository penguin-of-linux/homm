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
        [Test]
        public void ExampleRound()
        {
            var round = new Round("TestMaps\\goodMap.txt", new string[] { "First", "Second" });
            round.UpdateTick(new Point[] { new Point(0, 0), new Point(1, 2) });
            round.UpdateTick(new Point[] { new Point(0, 1), new Point(1, 1) });
            var mine = (Mine)round.map[1, 0].tileObject;
            Assert.That(mine.Owner == round.players[0]);
            Assert.That(mine.Resource == Resource.Rubles);
            Assert.AreEqual(round.players[1].CheckResourceAmount(Resource.Rubles), 100);
            round.DailyTick();
            Assert.AreEqual(round.players[0].CheckResourceAmount(Resource.Rubles), 1000);
        }
    }
}
