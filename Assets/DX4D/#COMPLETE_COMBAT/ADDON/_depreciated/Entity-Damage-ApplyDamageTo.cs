/* //DEPRECIATED
using Mirror;

public partial class Entity : NetworkBehaviourNonAlloc
{
    // D A M A G E  M Y  T A R G E T
    //[Server] public virtual void DamageMyTarget(int amount, MethodOfDamage damageMethod, Element damageElement) { DealDamageAt(target, amount, damageMethod, damageElement); }
    
    // D A M A G E  T A R G E T
    //[Server] public virtual void ApplyDamageToTarget(int amount, MethodOfDamage damageMethod, Element damageElement) { ApplyDamageTo(target, amount, damageMethod, damageElement); }
    
    /// <summary>
    /// Applies Damage without drawing aggro or synchronizing the player
    /// </summary>
    [Server] public virtual void ApplyDamageTo(Entity defender, DamageInfo damage)
    {
        // A P P L Y  D A M A G E
        ApplyDamage(defender, damage.total, damage.method);//dealsDamageType);

        // S H O W  D A M A G E  P O P U P
        //if(damage.total > 0)
        defender.RpcOnDamageReceived(damage.total, defender.ActiveDefensiveState, damage.method, damage.element);// dealsDamageType, dealsElementalDamageType);

        // H A N D L E  P O S T  A T T A C K  E V E N T S
        HandleDamageTriggers(defender, damage.method, damage.element);
        HandlePassiveTriggers(defender, ActiveOffensiveState);

        // H A N D L E  P O S T  C O M B A T  E V E N T S


    }
}
*/
