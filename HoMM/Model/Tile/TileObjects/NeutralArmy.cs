namespace HoMM
{
    public class NeutralArmy : TileObject
    {
        public readonly Unit Unit;
        public CapturableObject GuardedObject { get; private set; }
        public int Quantity { get; private set; }

        public NeutralArmy(Unit unit, int quantity, Vector2i location) : base(location)
        {
            Unit = unit;
            Quantity = quantity;
        }

        public void GuardObject(CapturableObject obj)
        {
            GuardedObject = obj;
        }

        public void KillMonsters(int amount)
        {
            if (amount > Quantity)
            {
                Quantity = 0;
                OnRemove();
            }
            else
                Quantity -= amount;
        }
    }
}
