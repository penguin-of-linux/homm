using System;
using System.ComponentModel;
using System.Drawing;

namespace HoMM
{
    public abstract class TileObject: INotifyPropertyChanged
    {
        public string unityID;
        public readonly Point location;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


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
