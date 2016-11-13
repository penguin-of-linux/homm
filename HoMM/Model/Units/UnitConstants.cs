using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HoMM
{
    public static class UnitConstants
    {
        public static Dictionary<UnitType, int> WeeklyGrowth = new Dictionary<UnitType, int>
        {
            [UnitType.Infantry] = 16,
            [UnitType.Ranged] = 16,
            [UnitType.Cavalry] = 4
        };

        public static Dictionary<UnitType, Dictionary<Resource, int>> UnitCost = new Dictionary<UnitType, Dictionary<Resource, int>>
        {
            [UnitType.Infantry] = new Dictionary<Resource, int> { [Resource.Rubles] = 50, [Resource.Ore] = 1 },
            [UnitType.Ranged] = new Dictionary<Resource, int> { [Resource.Rubles] = 50, [Resource.Wood] = 1 },
            [UnitType.Cavalry] = new Dictionary<Resource, int> { [Resource.Rubles] = 200, [Resource.Crystals] = 2 }
        };

        public static Dictionary<UnitType, int> CombatPower = new Dictionary<UnitType, int>
        {
            [UnitType.Infantry] = 15,
            [UnitType.Ranged] = 15,
            [UnitType.Cavalry] = 60
        };

        public static Dictionary<UnitType, UnitType> CounteredBy = new Dictionary<UnitType, UnitType>
        {
            [UnitType.Infantry] = UnitType.Ranged,
            [UnitType.Ranged] = UnitType.Cavalry,
            [UnitType.Cavalry] = UnitType.Infantry
        };
    }
}
