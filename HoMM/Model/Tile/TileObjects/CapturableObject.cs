using System;
using System.Drawing;

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

        protected CapturableObject(Point location) : base(location)
        {
            Owner = null;
        }
    }
}
