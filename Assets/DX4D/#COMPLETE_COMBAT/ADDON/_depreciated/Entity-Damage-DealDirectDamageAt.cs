/* //DEPRECIATED
using Mirror;

public abstract partial class Entity : NetworkBehaviourNonAlloc
{
    // - - - - - - - - - - - - -
    // D E A L  D I R E C T  D A M A G E  A T
    [Server] public virtual void DealDirectDamageAt(Entity defender, int amount, float stunChance = 0, float stunTime = 0)
    {
        DealDirectDamageAt(defender, new DamageInfo(amount, dealsDamageType, dealsElementalDamageType, 0, 1.0f), new StatusInfo(stunChance, stunTime));
    }

    /// <summary>
    /// Use this function to deal direct damage regardless of the attacker and target's stats/gear/etc
    /// Direct damage does not trigger aggro
    /// </summary>
    [Server] public virtual void DealDirectDamageAt(Entity defender, DamageInfo damage, StatusInfo status)
    {
        // V A L I D A T E  C O M B A T A N T S
        if (IsDead || !defender || defender.invincible || defender.IsDead) return;

        // S Y N C  G E A R
        //if (this is Player) (this as Player).SyncMyEquipment();
        //if (defender is Player) (defender as Player).SyncMyEquipment();

        // S Y N C  C O M B A T  S T A T E
        //SyncCombatState(defender, damage.method, damage.element);

        // C A L C U L A T E  D A M A G E
        //damage = CalculateDamage(defender, damage, status);

        // A P P L Y  D A M A G E
        ApplyDamage(defender, damage.total, damage.method);

        // S H O W  D A M A G E  P O P U P
        //if(damage.total > 0)
        defender.RpcOnDamageReceived(damage.total, defender.ActiveDefensiveState, damage.method, damage.element);// dealsDamageType, dealsElementalDamageType);

        // A P P L Y  S T A T U S  E F F E C T S
        ApplyStatus(defender, status);

        // D R A W  A G G R O
        //defender.OnAggro(this); //We do this in every case to prevent exploits with archery and spells

        // ummorpg addon system hooks
        if (this is Player)
        {
            Utils.InvokeMany(typeof(Player), (this as Player), "DealDirectDamageAt_", defender, damage.total);
        }
        else
        {
            Utils.InvokeMany(typeof(Entity), this, "DealDirectDamageAt_", defender, damage.total);
        }
    }
}
*/
