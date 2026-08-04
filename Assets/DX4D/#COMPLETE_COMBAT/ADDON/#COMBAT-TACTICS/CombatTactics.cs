using System;
using UnityEngine;

//[CreateAssetMenu(menuName = "DX4D/DAMAGE/Tactical Damage", order = 31)]
[Serializable] public class CombatTactics// : ScriptedDamage
{
    //[Header("TACTICS")]
    [Tooltip("Enable this to apply tactics when damage is dealt. Can be used to switch tactics off and on ")]
    [SerializeField] public bool useTactics;

    [Header("- k n o c k a r o u n d -")]
    [Tooltip("Knock the target this distance away from you. (positive=away negative=toward)")]
    [SerializeField] public float knockbackDistance;
    [Tooltip("Knock the target this distance to the side. (positive=right negative=left)")]
    [SerializeField] public float knockasideDistance;
    [Tooltip("Knock the target this distance up into the air. (positive=up negative=down)")]
    [SerializeField] public float knockupDistance;
    [Header("- m a n e u v e r s -")]
    [Tooltip("Charge toward the target until you are this distance away.")]
    [SerializeField] public float chargeToDistanceFrom;
    [Tooltip("Pulls the target toward the caster until they are this distance away.")]
    [SerializeField] public float pullToDistanceFrom;

    public CombatTactics copy()
    {
        CombatTactics info = (CombatTactics)this.MemberwiseClone();

        info.useTactics = useTactics;
        info.knockbackDistance = knockbackDistance;
        info.knockasideDistance = knockasideDistance;
        info.knockupDistance = knockupDistance;
        info.chargeToDistanceFrom = chargeToDistanceFrom;
        info.pullToDistanceFrom = pullToDistanceFrom;

        return info;
    }
    public virtual void Apply(CharacterSheet attacker, CharacterSheet defender)
    {
        if (!useTactics) return;
        //if (caster == null)
        //caster = attacker;
        //if (target == null)
        //target = defender;

        if (attacker == null || defender == null) return; //Somebody died

        //NOTE: Knockback + Charge = Swap Positions
        //KNOCKBACK + KNOCKUP + KNOCKASIDE
        if (knockbackDistance > 0 || knockupDistance > 0 || knockasideDistance > 0)
        {
            //defender.transform.position = 
            defender.Warp(defender.agent.transform.position
                + (attacker.agent.transform.forward * knockbackDistance)
                + (attacker.agent.transform.up * knockupDistance)
                + (attacker.agent.transform.right * knockasideDistance)
                );
        }
        //if (knockbackDistance > 0 || knockupDistance > 0) target.agent.Warp(target.agent.transform.position + (caster.agent.transform.forward * knockbackDistance) + (caster.agent.transform.up * knockupDistance));
        //CHARGE
        if (chargeToDistanceFrom > 0)
        {
            //caster.transform.position = (target.transform.position + (target.transform.forward * chargeToDistanceFrom));
            //attacker.transform.position = (defender.transform.position + (defender.transform.forward * chargeToDistanceFrom));
//#if RPG2D
//            attacker.GetComponent<NetworkNavMeshAgentRubberbanding2D>().agent.destination = (defender.transform.position + (defender.transform.forward * chargeToDistanceFrom));
//#else
            //attacker.player.rubberbanding.agent.destination = 
            attacker.Warp(defender.transform.position + (defender.transform.forward * chargeToDistanceFrom));
            //.GetComponent<NetworkNavMeshAgentRubberbanding>()
//#endif
        }
        //if (chargeToDistanceFrom > 0) caster.agent.Warp(target.agent.transform.position + (target.agent.transform.forward * chargeToDistanceFrom));
        //PULL
        if (pullToDistanceFrom > 0)
        {
            //defender.transform.position = 
            defender.Warp(attacker.transform.position + (attacker.transform.forward * pullToDistanceFrom));
        }
    }
}
    /*
    //APPLIED
    public override void OnApplied()
    {
        base.OnApplied();
    }
    //EXPIRED
    public override void OnExpired()
    {
        base.OnExpired();
    }
    //TICK
    public override void OnTick()
    {
        base.OnTick();
    }
    */
      