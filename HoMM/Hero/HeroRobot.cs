using CVARC.V2;
using HoMM.Rules;
using HoMM.Sensors;
using HoMM.Units.HexagonalMovementUnit;
using HoMM.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HoMM.Hero
{
    class HeroRobot : Robot<HommWorld, HommSensorData, HommCommand, HommRules>,
        IHexMovRobot
    {
        public override IEnumerable<IUnit> Units { get; }

        public Vector2i Location { get; set; }
        public double VelocityModifier { get; }

        public HeroRobot()
        {
            Units = new List<IUnit>
            {
                new HexMovUnit(this),
            };
        }
    }
}
