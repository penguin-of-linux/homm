using System;
using System.Collections.Generic;

namespace HoMM
{
    public static class UnitFactory
    {
        public static Unit CreateFromUnitType(UnitType unitType)
        {
            switch (unitType.ToString())
            {
                case "Infantry": return CreateInfantry();
                case "Ranged": return CreateRanged();
                case "Cavalry": return CreateCavalry();
                default: throw new ArgumentException("Unsupported unit type!");
            }
        }
        public static Unit CreateInfantry()
        {
            return new Unit("Infantryman", UnitType.Infantry, 
                UnitConstants.CombatPower[UnitType.Infantry], UnitConstants.UnitCost[UnitType.Infantry]);
        }
        public static Unit CreateRanged()
        {
            return new Unit("Archer", UnitType.Ranged, 
                UnitConstants.CombatPower[UnitType.Ranged], UnitConstants.UnitCost[UnitType.Ranged]);
        }
        public static Unit CreateCavalry()
        {
            return new Unit("Horseman", UnitType.Cavalry, 
                UnitConstants.CombatPower[UnitType.Cavalry], UnitConstants.UnitCost[UnitType.Cavalry]);
        }
    }
}
