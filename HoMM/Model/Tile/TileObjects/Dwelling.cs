using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace HoMM
{
    public class Dwelling : CapturableObject
    {
        public Unit Recruit { get; private set; }
        public int AvailableUnits { get; private set; }

        public Dwelling(Unit unit, Point location, int availableUnits = 0) : base(location)
        {
            if (availableUnits < 0)
                throw new ArgumentException("Cannot have negative units at dwelling!");

            Recruit = unit;
            AvailableUnits = availableUnits;
        }

        
        public void AddWeeklyGrowth()
        {
            AvailableUnits += UnitConstants.WeeklyGrowth[Recruit.unitType];
        }
        public void RemoveBoughtUnits(int amount)
        {
            AvailableUnits -= amount;
        }

        public override void InteractWithPlayer(Player p)
        {
            Owner = p;
        }
    }
}
