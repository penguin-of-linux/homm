using System;
using System.Collections.Generic;
using System.Drawing;

namespace HoMM
{
    public class Player
    {
        public string Name { get; private set; }
        private Map map;
        Dictionary<Resource, int> resources;
        Dictionary<Unit, int> army;
        public Point Location { get; set; }

        public Player(string name, Map map)
        {
            Name = name;
            resources = new Dictionary<Resource, int>();
            foreach (Resource res in Enum.GetValues(typeof(Resource)))
                resources.Add(res, 0);
            army = new Dictionary<Unit, int>();
            this.map = map;
        }

        public int CheckResourceAmount(Resource res)
        {
            return resources[res];
        }
        public Dictionary<Resource, int> CheckAllResources()
        {
            return new Dictionary<Resource, int>(resources);
        }

        public void GainResources(Resource res, int amount)
        {
            if (amount < 0)
                throw new ArgumentException("Cannot 'gain' negative resources!");
            resources[res] += amount;
        }

        public void PayResources(Resource res, int amount)
        {
            if (amount < 0)
                throw new ArgumentException("Cannot 'pay' positive resources!");
            if (amount > resources[res])
                throw new ArgumentException("Not enough " + res.ToString() + " to pay " + amount);
            resources[res] -= amount;
        }

        public void AddUnits(Unit unit, int amount)
        {
            if (!army.ContainsKey(unit))
                army.Add(unit, 0);
            army[unit] += amount;
        }

        public bool TryBuyUnits(int unitsToBuy)
        {
            if (!(map[Location.X, Location.Y].tileObject is Dwelling))
                return false;

            var d = (Dwelling)map[Location.X, Location.Y].tileObject;
            if (d.AvailableUnits < unitsToBuy)
                return false;
            foreach (var kvp in d.Recruit.unitCost)
                if (CheckResourceAmount(kvp.Key) < kvp.Value * unitsToBuy)
                    return false;
            foreach (var kvp in d.Recruit.unitCost)
                PayResources(kvp.Key, kvp.Value * unitsToBuy);
            AddUnits(d.Recruit, unitsToBuy);
            return true;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Player;
            if (other == null)
                return false;
            return this.Name == other.Name;
        }

        public override int GetHashCode()
        {
            int hash = 37;
            unchecked
            {
                foreach (var c in Name)
                    hash = hash * 101 + Convert.ToByte(c);
            }
            return hash;
        }

        public override string ToString()
        {
            return "Player " + Name;
        }
    }
}
