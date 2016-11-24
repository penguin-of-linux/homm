using System.Collections.Generic;

namespace HoMM
{
    public class Garrison : CapturableObject
    {
        public Dictionary<Unit, int> guards;
        public Garrison(Dictionary<Unit, int> guards, Vector2i location) : base(location)
        {
            this.guards = guards;
        }

        public override void InteractWithPlayer(Player p)
        {
            base.InteractWithPlayer(p);
        }
    }
}
