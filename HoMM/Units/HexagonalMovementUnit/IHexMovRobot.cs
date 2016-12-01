using CVARC.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HoMM.Units.HexagonalMovementUnit
{
    interface IHexMovRobot
    {
        Vector2i Location { get; set; }
        double VelocityModifier { get; }
    }
}
