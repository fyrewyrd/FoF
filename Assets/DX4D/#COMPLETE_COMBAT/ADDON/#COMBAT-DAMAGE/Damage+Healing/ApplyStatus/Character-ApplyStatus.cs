//using System.Collections.Generic;
using Mirror;
using UnityEngine;

public partial class CharacterSheet : NetworkBehaviour
{

    // - - - - - - - - - - - -
    // A P P L Y  S T A T U S
    [Server]
    public void ApplyStatusEffects(CharacterSheet defender, StatusEffectList status)
    {
        if (status == null || status.statusEffects == null || status.statusEffects.Count < 1) return;

        for (int i = 0; i < status.statusEffects.Count; i++)
        {
            if (status.statusEffects[i].probability.chance > 0 && Random.value < status.statusEffects[i].probability.chance)
            {
                status.ActivateAll(status.statusEffects[i].statusEffect, this, defender);
                //status.statusEffects[i].expiration = System.Math.Max(NetworkTime.time + status.statusEffects[i].duration.time, status.statusEffects[i].expiration);
            }
        //}

        //for (int i = 0; i < status.Length; i++)
        //{

            // ummorpg stun?
            switch (status.statusEffects[i].statusEffect)
            {
                case StatusEffect.None:
                    break;
                case StatusEffect.Poison:
                    break;
                case StatusEffect.Bleed:
                    break;
                case StatusEffect.Burn:
                    break;
                case StatusEffect.Blind:
                    break;
                case StatusEffect.Sleep:
                    break;
                case StatusEffect.Silence:
                    break;
                case StatusEffect.Stun:
                    {
                        defender.stunTimeEnd = System.Math.Max(status.statusEffects[i].expiration, stunTimeEnd);
                        break;
                    }
                case StatusEffect.Doom:
                    break;
                case StatusEffect.Drench:
                    break;
                case StatusEffect.Freeze:
                    break;
                case StatusEffect.Tangle:
                    break;
                case StatusEffect.Swift:
                    break;
                case StatusEffect.Slow:
                    break;
                case StatusEffect.Haste:
                    break;
                case StatusEffect.Stop:
                    break;
                case StatusEffect.Confuse:
                    break;
                case StatusEffect.Protect:
                    break;
                case StatusEffect.Shell:
                    break;
                case StatusEffect.Float:
                    break;
                case StatusEffect.Reflect:
                    break;
                case StatusEffect.Regen:
                    break;
                default:
                    break;
            }
        }
        //defender.StunExpiration = NetworkTime.time + status.duration.stun; //DEPRECIATED
    }

    /*
    if (status != 0 && status != StatusEffect.None)
    {
        //TODO: Strip out None
        status &= (~StatusEffect.None);

        defender.activeStatusEffects |= status;
        Debug.Log("STATUS - " + status.ToString() + " applied to " + "<" + defender.name + "> at " + NetworkTime.time.ToString()); //DEBUG

        //if(status != StatusEffect.None) defender.RpcShowStatusAppliedPopup(defender.activeStatusEffects.ToString().ToLower());

        //EXAMPLES
        //if ((status & StatusEffect.Stun) == StatusEffect.Stun) { RpcShowStatusAppliedPopup("stunned opponent"); } //TODO: This is just a test
        //else { RpcShowStatusAppliedPopup("did not stun opponent"); } //TODO: This is just a test

        //if(defender.HasStatus(StatusEffect.Stun)) { defender.RpcShowStatusAppliedPopup("stunned"); }
        //if(defender.HasStatus(StatusEffect.Poison)) { defender.RpcShowStatusAppliedPopup("poisoned"); }
        //if(defender.HasStatus(StatusEffect.Bleed)) { defender.RpcShowStatusAppliedPopup("bleeding"); }
        //if(defender.HasStatus(StatusEffect.Burn)) { defender.RpcShowStatusAppliedPopup("burning"); }
    }

    if (HasStatus(StatusEffect.Poison)) PoisonExpiration = NetworkTime.time + PoisonDuration;
    if (HasStatus(StatusEffect.Bleed)) BleedExpiration = NetworkTime.time + BleedDuration;
    if (HasStatus(StatusEffect.Burn)) BurnExpiration = NetworkTime.time + BurnDuration;
    if (HasStatus(StatusEffect.Blind)) BlindExpiration = NetworkTime.time + BlindDuration;
    if (HasStatus(StatusEffect.Sleep)) SleepExpiration = NetworkTime.time + SleepDuration;
    if (HasStatus(StatusEffect.Silence)) SilenceExpiration = NetworkTime.time + SilenceDuration;
    if (HasStatus(StatusEffect.Stun)) StunExpiration = NetworkTime.time + StunDuration;
    if (HasStatus(StatusEffect.Doom)) DoomExpiration = NetworkTime.time + DoomDuration;
    if (HasStatus(StatusEffect.Drench)) DrenchExpiration = NetworkTime.time + DrenchDuration;
    if (HasStatus(StatusEffect.Freeze)) FreezeExpiration = NetworkTime.time + FreezeDuration;
    if (HasStatus(StatusEffect.Tangle)) TangleExpiration = NetworkTime.time + TangleDuration;
    if (HasStatus(StatusEffect.Swift)) SwiftExpiration = NetworkTime.time + SwiftDuration;
    if (HasStatus(StatusEffect.Slow)) SlowExpiration = NetworkTime.time + SlowDuration;
    if (HasStatus(StatusEffect.Haste)) HasteExpiration = NetworkTime.time + HasteDuration;
    if (HasStatus(StatusEffect.Stop)) StopExpiration = NetworkTime.time + StopDuration;
    if (HasStatus(StatusEffect.Confuse)) ConfuseExpiration = NetworkTime.time + ConfuseDuration;
    if (HasStatus(StatusEffect.Protect)) ProtectExpiration = NetworkTime.time + ProtectDuration;
    if (HasStatus(StatusEffect.Shell)) ShellExpiration = NetworkTime.time + ShellDuration;
    if (HasStatus(StatusEffect.Float)) FloatExpiration = NetworkTime.time + FloatDuration;
    if (HasStatus(StatusEffect.Reflect)) ReflectExpiration = NetworkTime.time + ReflectDuration;
    if (HasStatus(StatusEffect.Regen)) RegenExpiration = NetworkTime.time + RegenDuration;
    */
}
