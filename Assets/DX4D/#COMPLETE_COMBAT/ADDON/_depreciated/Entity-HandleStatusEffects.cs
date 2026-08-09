/* //DEPRECIATED
using Mirror;
using UnityEngine;

public abstract partial class Entity : NetworkBehaviourNonAlloc
{
    [SerializeField] private double lastTickTime;
    private bool handling = false;
    private const double tickFrequency = 2.0;
    

    [Server] public void HandleStatusEffects()
    {
        if (!handling) { handling = true; } else { return; }

        if ((lastTickTime + tickFrequency) >= NetworkTime.time) { handling = false; return; } //Only process if enough time has elapsed

        lastTickTime = NetworkTime.time; //Track the time

        //Debug.Log(" handling status effects - " + lastTickTime.ToString()); //DEBUG

        HandleDoT(StatusEffect.Poison, PoisonExpiration, Random.Range(1, 15), MethodOfDamage.Poison, Element.Neutral);
        HandleDoT(StatusEffect.Bleed, BleedExpiration, Random.Range(15, 25), MethodOfDamage.Blood, Element.Neutral);
        HandleDoT(StatusEffect.Burn, BurnExpiration, Random.Range(25, 75), MethodOfDamage.Physical, Element.Fire);

        HandleImpairment(StatusEffect.Blind, BlindExpiration, 1.0f);
        HandleImpairment(StatusEffect.Sleep, SleepExpiration, 1.0f);
        HandleImpairment(StatusEffect.Silence, SilenceExpiration, 1.0f);
        HandleImpairment(StatusEffect.Stun, StunExpiration, 1.0f);
        HandleImpairment(StatusEffect.Doom, DoomExpiration, 1.0f);
        HandleImpairment(StatusEffect.Drench, DrenchExpiration, 1.0f);

        HandleImpairment(StatusEffect.Blind, BlindExpiration, 1.0f);
        HandleImpairment(StatusEffect.Sleep, SleepExpiration, 1.0f);
        HandleImpairment(StatusEffect.Silence, BlindExpiration, 1.0f);
        HandleImpairment(StatusEffect.Stun, SleepExpiration, 1.0f);
        HandleImpairment(StatusEffect.Doom, BlindExpiration, 1.0f);
        HandleImpairment(StatusEffect.Drench, SleepExpiration, 1.0f);

        HandleBuff(StatusEffect.Regen, RegenExpiration, 1.0f);
        //TODO: Add the rest of the status effects

        handling = false;


        if (HasStatus(StatusEffect.Bleed)) { RpcShowStatusAppliedPopup(""); DealDamageAt(this, 25, MethodOfDamage.Blood, Element.Neutral, StatusEffect.None); }
        if (HasStatus(StatusEffect.Burn)) { RpcShowStatusAppliedPopup(""); DealDamageAt(this, 55, MethodOfDamage.Physical, Element.Fire, StatusEffect.None ); }
        if (HasStatus(StatusEffect.Blind)) { RpcShowStatusAppliedPopup(""); }
        if (HasStatus(StatusEffect)) { RpcShowStatusAppliedPopup(""); }
        if (HasStatus(StatusEffect)) { RpcShowStatusAppliedPopup(""); }
        if (HasStatus(StatusEffect)) { RpcShowStatusAppliedPopup(""); }
        if (HasStatus(StatusEffect)) { RpcShowStatusAppliedPopup(""); }
        if (HasStatus(StatusEffect)) { RpcShowStatusAppliedPopup(""); }
        if (HasStatus(StatusEffect)) { RpcShowStatusAppliedPopup(""); }
        if (HasStatus(StatusEffect)) { RpcShowStatusAppliedPopup(""); }
        if (HasStatus(StatusEffect)) { RpcShowStatusAppliedPopup(""); }
        if (HasStatus(StatusEffect)) { RpcShowStatusAppliedPopup(""); }
        if (HasStatus(StatusEffect)) { RpcShowStatusAppliedPopup(""); }
        if (HasStatus(StatusEffect)) { RpcShowStatusAppliedPopup(""); }
        if (HasStatus(StatusEffect)) { RpcShowStatusAppliedPopup(""); }
        if (HasStatus(StatusEffect)) { RpcShowStatusAppliedPopup(""); }
        if (HasStatus(StatusEffect)) { RpcShowStatusAppliedPopup(""); }
        if (HasStatus(StatusEffect)) { RpcShowStatusAppliedPopup(""); }
        if (HasStatus(StatusEffect.Stun)) { RpcShowStatusAppliedPopup("stunned"); }
        if(HasStatus(StatusEffect.Stun)) { RpcShowStatusAppliedPopup("stunned"); }

    }
    [Server]
    public void HandleImpairment(StatusEffect statusEffect, double expiration, float multiplier)
    {
        if (HasStatus(statusEffect))
        {
            if (!StatusExpired(statusEffect, expiration))
            {
                //if (statusEffect != StatusEffect.None) RpcShowStatusRemovedPopup(statusEffect.ToString());
                RemoveStatus(this, statusEffect);
            }
            else
            {
                //if(statusEffect != StatusEffect.None) RpcShowStatusAppliedPopup(statusEffect.ToString());
                switch (statusEffect)
                {
                    case StatusEffect.Blind:
                        {
                            RpcShowCountdownPopup("blinded");
                            break;
                        }
                    case StatusEffect.Sleep:
                        {
                            RpcShowCountdownPopup("sleeping");
                            break;
                        }
                    case StatusEffect.Silence:
                        {
                            RpcShowCountdownPopup("silenced");
                            break;
                        }
                    case StatusEffect.Stun:
                        {
                            RpcShowCountdownPopup("stunned");
                            break;
                        }
                    case StatusEffect.Doom:
                        {
                            RpcShowCountdownPopup("doomed");
                            break;
                        }
                    case StatusEffect.Drench:
                        {
                            RpcShowCountdownPopup("drenched");
                            break;
                        }
                    case StatusEffect.Freeze:
                        {
                            RpcShowCountdownPopup("frozen");
                            break;
                        }
                    case StatusEffect.Tangle:
                        {
                            RpcShowCountdownPopup("tangled");
                            break;
                        }
                    case StatusEffect.Slow:
                        {
                            RpcShowCountdownPopup("slowed");
                            break;
                        }
                    case StatusEffect.Stop:
                        {
                            RpcShowCountdownPopup("stopped");
                            break;
                        }
                    case StatusEffect.Confuse:
                        {
                            RpcShowCountdownPopup("confused");
                            break;
                        }
                }
                //TODO: A P P L Y  I M P A I R M E N T
                //DealDamageAt(this, amount, damageMethod, damageElement, StatusEffect.None); //WARNING: Do not deal the status effect back to yourself...it could cause unwanted loops
            }
        }
    }
    [Server]
    public void HandleBuff(StatusEffect statusEffect, double expiration, float multiplier)
    {
        if (HasStatus(statusEffect))
        {
            if (!StatusExpired(statusEffect, expiration))
            {
                //if (statusEffect != StatusEffect.None) RpcShowStatusRemovedPopup(statusEffect.ToString());
                RemoveStatus(this, statusEffect);
            }
            else
            {
                //if(statusEffect != StatusEffect.None) RpcShowStatusAppliedPopup(statusEffect.ToString());
                switch (statusEffect)
                {
                    case StatusEffect.Swift:
                        {
                            RpcShowCountdownPopup("swifted");
                            break;
                        }
                    case StatusEffect.Haste:
                        {
                            RpcShowCountdownPopup("hasted");
                            break;
                        }
                    case StatusEffect.Protect:
                        {
                            RpcShowCountdownPopup("protected");
                            break;
                        }
                    case StatusEffect.Shell:
                        {
                            RpcShowCountdownPopup("shelled");
                            break;
                        }
                    case StatusEffect.Float:
                        {
                            RpcShowCountdownPopup("floating");
                            break;
                        }
                    case StatusEffect.Reflect:
                        {
                            RpcShowCountdownPopup("reflective");
                            break;
                        }
                    case StatusEffect.Regen:
                        {
                            RpcShowCountdownPopup("regenerating");
                            break;
                        }
                }
                //DealDamageAt(this, amount, damageMethod, damageElement, StatusEffect.None); //WARNING: Do not deal the status effect back to yourself...it could cause unwanted loops
            }
        }
    }
    [Server]
    public void HandleDoT(StatusEffect statusEffect, double expiration, int amount, MethodOfDamage damageMethod, Element damageElement)
    {
        if (HasStatus(statusEffect))
        {
            if (!StatusExpired(statusEffect, expiration))
            {
                //if (statusEffect != StatusEffect.None) RpcShowStatusRemovedPopup(statusEffect.ToString());
                RemoveStatus(this, statusEffect);
            }
            else
            {
                //if(statusEffect != StatusEffect.None) RpcShowStatusAppliedPopup(statusEffect.ToString());
                DealDirectDamageAt(this, amount, damageMethod, damageElement, StatusEffect.None); //WARNING: Do not deal the status effect back to yourself...it could cause unwanted loops
            }
        }
    }
}
*/
