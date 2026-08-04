using Mirror;

public partial class CharacterSheet : NetworkBehaviour
{
    // - - - - - - - - - - - - - - - - - - - - - - - - -
    // D E A L  S P E C I A L I Z E D  D A M A G E  A T
    //FLAT DAMAGE
    [Server] public virtual void DealFlatDamage(CharacterSheet defender, DamageInfo.DamageValues dmg)//, StatusEffectList status)
    {
        DamageInfo damage = new DamageInfo();
        damage.damage = dmg;
        damage.properties.directDamage = true;
        damage.properties.penetrateReflect = true;
        DealCombatDamage(defender, damage);//, status);
    }
    //DIRECT DAMAGE
    [Server] public virtual void DealDirectDamage(CharacterSheet defender, DamageInfo initialDamage)//, StatusEffectList status)
    {
        DamageInfo damage = initialDamage.copy();

        damage.properties.directDamage = true;
        DealCombatDamage(defender, damage);//, status);
    }
    [Server] public virtual void DealPenetratingDamage(CharacterSheet defender, DamageInfo initialDamage)//, StatusEffectList status)
    {
        DamageInfo damage = initialDamage.copy();

        damage.properties.penetrateReflect = true;
        DealCombatDamage(defender, damage);//, status);
    }
}
