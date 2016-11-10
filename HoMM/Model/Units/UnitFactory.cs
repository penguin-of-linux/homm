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
            return new Unit("Infantryman", 15, UnitType.Infantry, 
                new Dictionary<Resource, int> { [Resource.Rubles] = 50, [Resource.Ore] = 1 });
        }

        public static Unit CreateCavalry()
        {
            return new Unit("Horseman", 35, UnitType.Cavalry,
                new Dictionary<Resource, int> { [Resource.Rubles] = 200, [Resource.Crystals] = 2 });
        }

        public static Unit CreateRanged()
        {
            return new Unit("Archer", 12, UnitType.Ranged,
                new Dictionary<Resource, int> { [Resource.Rubles] = 75, [Resource.Wood] = 1 });
        }
    }
}
