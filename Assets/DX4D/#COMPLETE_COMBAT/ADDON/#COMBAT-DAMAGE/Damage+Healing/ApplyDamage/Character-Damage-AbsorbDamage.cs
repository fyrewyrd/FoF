using Mirror;

public partial class CharacterSheet : NetworkBehaviour
{
    // - - - - - - - - - - - - -
    // A B S O R B  D A M A G E - S H I E L D
    [Server] public int AbsorbDamage(CharacterSheet defender, MethodOfDamage method, int damageToDeal)
    {
        if (damageToDeal < 1 || defender.SHIELDMAX < 1 || defender.ShieldIsBroken()) return damageToDeal;

        //DAMAGE SHIELD
        if (defender.TriggerIsActive(defender.stats.shieldAbsorbs, method))
        {
            //if (damageToDeal >= defender.SHIELD)
            if (defender.SHIELD > damageToDeal)
            {
                defender.SHIELD = (defender.SHIELD - damageToDeal);
                damageToDeal = 0;
            }
            else
            {
                damageToDeal = (damageToDeal - defender.SHIELD);
                defender.SHIELD -= defender.SHIELD;
            }
        }

		if (damageToDeal < 1 || defender.BARRIERMAX < 1 || defender.BarrierIsBroken()) return damageToDeal;

        //DAMAGE BARRIER
        if (defender.TriggerIsActive(defender.stats.barrierAbsorbs, method))
        {
            //if (damageToDeal >= defender.BARRIER)
            if (defender.BARRIER > damageToDeal)
            {
                defender.BARRIER = (defender.BARRIER - damageToDeal);
                damageToDeal = 0;
            }
            else
            {
                damageToDeal = (damageToDeal - defender.BARRIER);
                defender.BARRIER -= defender.BARRIER;
            }
        }
		
        //BREAK SHIELD
        if (defender.SHIELD < 1)//defender.SHIELDMAX > 0 && 
        {
            defender.BreakShield();
            UnityEngine.Debug.Log("<b><color=yellow>" +defender.name + " SHIELD BROKEN</color></b>" + "\nTook " + damageToDeal + " damage");
        }

        return damageToDeal;
		
		 //BREAK BARRIER
        if (defender.BARRIER < 1)//defender.BARRIERMAX > 0 && 
        {
            defender.BreakBarrier();
            UnityEngine.Debug.Log("<b><color=yellow>" +defender.name + " BARRIER BROKEN</color></b>" + "\nTook " + damageToDeal + " damage");
        }

        return damageToDeal;
    }
}
