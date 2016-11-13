using System.Collections.Generic;

namespace HoMM
{
    public class Unit
    {
        public readonly string UnitName;
        public readonly UnitType UnitType;
        public int CombatPower
        {
            get { return UnitConstants.CombatPower[this.UnitType]; }
        }
        public Dictionary<Resource, int> UnitCost
        {
            get { return UnitConstants.UnitCost[this.UnitType]; }
        }

        public Unit(string unitName, UnitType unitType)
        {
            this.UnitName = unitName;
            this.UnitType = unitType;
        }
    }
}
