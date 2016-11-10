using System.Drawing;

namespace HoMM
{
    public class NeutralArmy : TileObject
    {
        public readonly Unit unit;
        public readonly Mine guardedMine;
        public int quantity { get; private set; }

        public NeutralArmy(Unit unit, int quantity, Mine guardedMine, Point location) : base(location)
        {
            this.unit = unit;
            this.quantity = quantity;
            this.guardedMine = guardedMine;
        }

        public void KillMonsters(int amount)
        {
            if (amount > quantity)
                quantity = 0;
            else
                quantity -= amount;
        }
    }
}
