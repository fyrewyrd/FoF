using Mirror;

public partial class Player : Entity
{
    // - - - - - - - - - - - - -
    // D E A L  D A M A G E  A T
    /*
    [Server] public virtual void DealDamageAt(Entity defender, int amount, float stunChance, float stunDuration, bool simulatedDamage = false)
    {
        DamageInfo info = new DamageInfo(amount, amount, true, character.combat.damageMethod, character.combat.damageElement);
        if (stunTime > 0) info.status.statusEffects.Add(new ScriptedStatusEffect(StatusEffect.Stun, stunChance, stunTime));
        character.DealCombatDamage(defender.character, info);//, info);
    }*/

    [Server] public override void DealDamageAt(Entity defender, int amount, float stunChance = 0, float stunTime = 0)
    {
        //ADDON HOOK - For Quest Machine and other addon integrations
        Utils.InvokeMany(typeof(Player), this, "DealDamageAt_", defender, amount);
        //Utils.InvokeMany(typeof(Player), this, "DealDamageAt_", defender, amount);//, stunChance, stunTime );
    }
}
