/* //DEPRECIATED
using Mirror;

public partial class Player : Entity
{
    [Server] string UpdateServer_STATUSEFFECT()
    {
        //TODO: ADD TO PATCH
        //if (state == "STUNNED")  return UpdateServer_STUNNED(); if (state == "AFFLICTED")  return UpdateServer_STATUSEFFECT();
        //else if (state == "AFFLICTED") { } else if (state == "STUNNED")
        //else if (state == "STUNNED")

        // events sorted by priority (e.g. target doesn't matter if we died)
        if (EventDied())
        {
            // we died.
            OnDeath();
            return "DEAD";
        }

        if (HasStatus(StatusEffect.None)) return "IDLE"; //If NONE is flagged we don't process status effects.
        if (HasStatus(StatusEffect.Poison)) return "POISONED";
        if (HasStatus(StatusEffect.Bleed)) return "BLEEDING";
        if (HasStatus(StatusEffect.Burn)) return "BURNING";
        if (HasStatus(StatusEffect.Blind)) return "BLINDED";
        if (HasStatus(StatusEffect.Sleep)) return "SLEEPING";
        if (HasStatus(StatusEffect.Silence)) return "SILENCED";
        if (HasStatus(StatusEffect.Stun)) { return "STUNNED"; }
        if (HasStatus(StatusEffect.Doom)) return "DYING";
        if (HasStatus(StatusEffect.Drench)) return "DRENCHED";
        if (HasStatus(StatusEffect.Freeze)) return "FROZEN";
        if (HasStatus(StatusEffect.Tangle)) return "TANGLED";
        if (HasStatus(StatusEffect.Swift)) return "SWIFT";
        if (HasStatus(StatusEffect.Slow)) return "SLOWED";
        if (HasStatus(StatusEffect.Haste)) return "HASTED";
        if (HasStatus(StatusEffect.Stop)) return "STOPPED";
        if (HasStatus(StatusEffect.Confuse)) return "CONFUSED";
        if (HasStatus(StatusEffect.Protect)) return "PROTECTED";
        if (HasStatus(StatusEffect.Shell)) return "SHELLED";
        if (HasStatus(StatusEffect.Float)) return "FLOATING";
        if (HasStatus(StatusEffect.Reflect)) return "REFLECTIVE";
        if (HasStatus(StatusEffect.Regen)) return "REGENERATING";

        // no status effects = IDLE
        return "IDLE";
    }
}
*/
