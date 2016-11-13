using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HoMM
{
    public class Combat
    {
        public void ResolveRound(KeyValuePair<Unit, int> attacker, KeyValuePair<Unit, int> defender)
        {
            var attackerDamage = attacker.Key.combatStrength;
        }
    }
}
