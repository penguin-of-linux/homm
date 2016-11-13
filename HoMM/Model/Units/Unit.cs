using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HoMM
{
    public class Unit
    {
        public readonly string unitName;
        public readonly int combatStrength;
        public readonly UnitType unitType;
        public readonly Dictionary<Resource, int> unitCost;

        public Unit(string unitName, UnitType unitType, int combatStrength, Dictionary<Resource, int> unitCost)
        {
            this.unitName = unitName;
            this.combatStrength = combatStrength;
            this.unitType = unitType;
            this.unitCost = unitCost;
        }
    }
}
