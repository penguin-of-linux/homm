using System.Collections.Generic;
using System.Drawing;

namespace HoMM
{
    public class Garrison : CapturableObject
    {
        public Dictionary<Unit, int> guards;
        public Garrison(Dictionary<Unit, int> guards, Point location) : base(location)
        {
            this.guards = guards;
        }

        public override void InteractWithPlayer(Player p)
        {
            base.InteractWithPlayer(p);
        }
    }
}
