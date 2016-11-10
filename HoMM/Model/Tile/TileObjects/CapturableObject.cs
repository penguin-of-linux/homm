using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

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
