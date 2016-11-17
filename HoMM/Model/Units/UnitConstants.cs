using System.Collections.Generic;

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
            [UnitType.Cavalry] = new Dictionary<Resource, int> { [Resource.Rubles] = 200, [Resource.Crystals] = 2, [Resource.Gems] = 2 }
        };

        public static Dictionary<UnitType, int> CombatPower = new Dictionary<UnitType, int>
        {
            [UnitType.Infantry] = 15,
            [UnitType.Ranged] = 15,
            [UnitType.Cavalry] = 60
        };

        public static Dictionary<UnitType, Dictionary<UnitType, double>> CombatModAgainst = 
            new Dictionary<UnitType, Dictionary<UnitType, double>>
        {
            [UnitType.Infantry] = new Dictionary<UnitType, double> { [UnitType.Ranged] = 0.75, [UnitType.Cavalry] = 1.25 },
            [UnitType.Ranged] = new Dictionary<UnitType, double> { [UnitType.Cavalry] = 0.75, [UnitType.Infantry] = 1.25 },
            [UnitType.Cavalry] = new Dictionary<UnitType, double> { [UnitType.Infantry] = 0.75, [UnitType.Ranged] = 1.25 }
        };
    }
}
