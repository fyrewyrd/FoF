/* //DEPRECIATED
 using UnityEngine;

public class DamageEvent : TimedEvent
{
    [Header("TIMED DAMAGE")]
    public ScriptedDamage damage;

   // S T A R T
    private void OnEnable()
    {
        //Start();
    }

    public override void OnTimerStarted()
    {
        base.OnTimerStarted();
        Debug.Log(name + GetInstanceID() + ":OnTimerStarted()");

        if (!damage) return;

        damage.ApplyStatusOnTarget(damage.statusEffect);
        damage.ApplyDamageMethodMods();
        damage.ApplyElementalMods();

        damage.ApplyHealToTarget();
        damage.ApplyDamageToTarget();

        //damageEvent.ProcessScriptedDamage(); //TODO: Remove this method - Better to be consistent with the other methods
    }

    // O N  T I C K
    public override void OnTick()
    {
        base.OnTick();
        Debug.Log(name + GetInstanceID() + ":OnTick()");

        if (!damage) return;
        
        damage.RefreshTarget();

        damage.ApplyHealToTarget();
        damage.ApplyDamageToTarget();
    }

    // F I N I S H
    public override void OnTimerFired()
    {
        base.OnTimerFired();
        Debug.Log(name + GetInstanceID() + ":OnTimerFired()");

        if (!damage) return;
        
        damage.RefreshTarget();

        //RemoveStatusFromTarget(statusEffect);
        damage.RemoveDamageMethodMods();
        damage.RemoveElementalMods();

        damage.ApplyHealToTarget();
        damage.ApplyDamageToTarget();
    }
}
    */

/* //TODO: U S E  T H I S  L A T E R  -  Maybe it triggers when new Players come near you?
//public override bool OnRebuildObservers(System.Collections.Generic.HashSet<NetworkConnection> observers, bool initialize) { }
*/
