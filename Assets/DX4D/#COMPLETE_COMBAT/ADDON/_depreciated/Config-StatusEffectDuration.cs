/* //DEPRECIATED
using Mirror;
using UnityEngine;

public abstract partial class Entity : NetworkBehaviourNonAlloc
{
    [Header("ACTIVE STATUS EFFECTS")]
    [SyncVar] public StatusEffect activeStatusEffects = StatusEffect.None;

    protected double PoisonExpiration;
    protected const double PoisonDuration = 24.0f;

    protected double BleedExpiration;
    protected const double BleedDuration = 18.0f;

    protected double BurnExpiration;
    protected const double BurnDuration = 12.0f;
    
    protected double BlindExpiration;
    protected const double BlindDuration = 300.0f;

    protected double SleepExpiration;
    protected const double SleepDuration = 60.0f;

    protected double SilenceExpiration;
    protected const double SilenceDuration = 30.0f;

    protected double StunExpiration;
    protected const double StunDuration = 3.0f;

    protected double DoomExpiration;
    protected const double DoomDuration = 300.0f;

    protected double DrenchExpiration;
    protected const double DrenchDuration = 60.0f;

    protected double FreezeExpiration;
    protected const double FreezeDuration = 6.0f;

    protected double TangleExpiration;
    protected const double TangleDuration = 30.0f;

    protected double SwiftExpiration;
    protected const double SwiftDuration = 300.0f;

    protected double SlowExpiration;
    protected const double SlowDuration = 300.0f;

    protected double HasteExpiration;
    protected const double HasteDuration = 300.0f;

    protected double StopExpiration;
    protected const double StopDuration = 3.0f;

    protected double ConfuseExpiration;
    protected const double ConfuseDuration = 18.0f;

    protected double ProtectExpiration;
    protected const double ProtectDuration = 300.0f;

    protected double ShellExpiration;
    protected const double ShellDuration = 300.0f;

    protected double FloatExpiration;
    protected const double FloatDuration = 300.0f;

    protected double ReflectExpiration;
    protected const double ReflectDuration = 30.0f;

    protected double RegenExpiration;
    protected const double RegenDuration = 300.0f;

}

public abstract partial class Entity : NetworkBehaviourNonAlloc
{
    public bool HasStatus(StatusEffect toCheck)
    {
        return ((activeStatusEffects & toCheck) == toCheck);

        switch (toCheck)
        {
            case StatusEffect.Poison: return StatusExpired(toCheck, PoisonExpiration);
            case StatusEffect.Bleed: return StatusExpired(toCheck, BleedExpiration);
            case StatusEffect.Burn: return StatusExpired(toCheck, BurnExpiration);
            case StatusEffect.Blind: return StatusExpired(toCheck, BlindExpiration);
            case StatusEffect.Sleep: return StatusExpired(toCheck, SleepExpiration);
            case StatusEffect.Silence: return StatusExpired(toCheck, SilenceExpiration);
            case StatusEffect.Stun: return StatusExpired(toCheck, StunExpiration);

            case StatusEffect.Doom: return StatusExpired(toCheck, DoomExpiration);
            case StatusEffect.Drench: return StatusExpired(toCheck, DrenchExpiration);
            case StatusEffect.Freeze: return StatusExpired(toCheck, FreezeExpiration);
            case StatusEffect.Tangle: return StatusExpired(toCheck, TangleExpiration);
            case StatusEffect.Swift: return StatusExpired(toCheck, SwiftExpiration);
            case StatusEffect.Slow: return StatusExpired(toCheck, SlowExpiration);
            case StatusEffect.Haste: return StatusExpired(toCheck, HasteExpiration);
            case StatusEffect.Stop: return StatusExpired(toCheck, StopExpiration);
            case StatusEffect.Confuse: return StatusExpired(toCheck, ConfuseExpiration);
            case StatusEffect.Protect: return StatusExpired(toCheck, ProtectExpiration);
            case StatusEffect.Shell: return StatusExpired(toCheck, ShellExpiration);
            case StatusEffect.Float: return StatusExpired(toCheck, FloatExpiration);
            case StatusEffect.Reflect: return StatusExpired(toCheck, ReflectExpiration);
            case StatusEffect.Regen: return StatusExpired(toCheck, RegenExpiration);
            case StatusEffect.None: return true; //TODO
            default: break;
        }

        //Status not found
        Debug.LogError("DX4D#COMBAT - " + toCheck.ToString() + " was not handled in the HasStatus method of UpdateServer_STATUSEFFECT"); //DEBUG
        return false;

    }

    public bool StatusExpired(StatusEffect statusToCheck, double statusDuration)
    {
        if (NetworkTime.time <= statusDuration) { ApplyStatus(this, statusToCheck); return false; }
        else { RemoveStatus(this, statusToCheck); return true; }
    }
}
*/
