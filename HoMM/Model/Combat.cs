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
            int attackerDamage = (int)Math.Floor(attacker.Key.CombatPower * attacker.Value 
                * attacker.Key.CombatModAgainst[defender.Key.UnitType]);
            int defenderDamage = (int)Math.Floor(defender.Key.CombatPower * defender.Value 
                * defender.Key.CombatModAgainst[attacker.Key.UnitType]);

            //NOT DONE
        }
    }
}
