using System;

namespace HoMM
{
    public abstract class CapturableObject : TileObject
    {
        Player owner;
        public Player Owner
        {
            get { return owner; }
            set
            {
                if (value == null && owner != null)
                    throw new ArgumentException("Cannot un-own a mine!");
                owner = value;
                OnPropertyChanged("Owner");
            }
        }

        protected CapturableObject(Vector2i location) : base(location)
        {
            Owner = null;
        }
    }
}
