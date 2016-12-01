using CVARC.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HoMM.Units.HexagonalMovementUnit
{
    class HexMovUnit : IUnit
    {
        private IHexMovRobot actor;
        private IHexMovRules rules;

        public HexMovUnit(IHexMovRobot actor)
        {
            this.actor = actor;
            this.rules = Compatibility.Check<IHexMovRules>(this, actor.Rules);
        }

        public UnitResponse ProcessCommand(object _command)
        {
            var movement = Compatibility.Check<IHexMovCommand>(this, _command).Movement;
            if (movement == null) return UnitResponse.Denied();

            var commandDuration = rules.MovementDuration * actor.VelocityModifier;

            actor.World.Clocks.AddTrigger(new OneTimeTrigger(commandDuration, 
                () => actor.Location = movement.Turn(actor.Location)));

            return UnitResponse.Accepted(commandDuration);
        }
    }
}
