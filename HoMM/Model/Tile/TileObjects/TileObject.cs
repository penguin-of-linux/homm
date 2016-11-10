using System;
using System.Drawing;

namespace HoMM
{
    public abstract class TileObject
    {
        public string unityID;
        public readonly Point location;

        protected TileObject(Point location)
        {
            this.location = location;
        }

        public virtual void InteractWithPlayer(Player p) { }

        public event Action<TileObject> Remove;

        public void OnRemove()
        {
            if(Remove != null)
                Remove(this);
        }
    }
}
