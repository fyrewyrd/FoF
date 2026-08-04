using Mirror;
using UnityEngine;
using System.Text;

public partial class CharacterSheet : NetworkBehaviour
{
    // - - - - - - - - - - - - - - - - - -
    // C A L C U L A T E  D A M A G E
    [Server]
    public DamageInfo CalculateDamage(CharacterSheet defender, DamageInfo initialDamage)//, StatusEffectList status)
    {
        // C R E A T E  D A M A G E  P A C K E T
        DamageInfo damage = initialDamage.copy();

        #region START DEBUG
        // D E B U G - DAMAGE REPORT - initial setup
#if UNITY_EDITOR
        StringBuilder damageReport = new StringBuilder("  <b>" + damage.ToString() + "</b>  ");
        damageReport.Append("\n<b>VALUES:</b> (" + combat.attack + " - " + defender.combat.armor + ") + [" + (damage.damage.fixedDamage ? "" : (damage.min + " to ")) + damage.max + "] * " + damage.multiplier + ") + " + damage.bonus + ")");
#endif
        #endregion
        if (!damage.properties.directDamage)
        {
            // C A L C U L A T E  A N D  A P P L Y  B A S E  D A M A G E  B O N U S E S
            damage.bonus += CalculateBaseDamageBonus(defender, damage.method, damage.element);
            #region DEBUG - base damage
#if UNITY_EDITOR
            damageReport.Append("\n<b>MODIFIERS:</b> " + "(base: " + damage.total + ")"); //+ Base Damage
#endif
            #endregion
            // C A L C U L A T E  A N D  A P P L Y  F L A T  D A M A G E  B O N U S E S
            damage.bonus += CalculateDamageModifiers(defender, damage.method, damage.element);
            #region DEBUG - bonus damage
#if UNITY_EDITOR
            damageReport.Append(" " + "(bonus: " + damage.total + ")"); //+ Bonus Damage
#endif
            #endregion
            // C A L C U L A T E  A N D  A P P L Y  D A M A G E  M U L T I P L I E R  B O N U S E S
            damage.bonus += CalculateDamageMultipliers(damage.amount, defender, damage.method, damage.element);
            #region DEBUG - modified damage
#if UNITY_EDITOR
            damageReport.Append(" " + "(multiplier: " + damage.total + ")"); //* Damage Modifiers
#endif
            #endregion
        }

        // C A L C U L A T E  A N D  A P P L Y  A C T I V E  S T A T E  B O N U S E S
        damage.bonus += CalculateStateBasedDamageBonuses(defender, damage.damage, damage.properties);//, status);
        #region DEBUG - combat state damage
#if UNITY_EDITOR
        damageReport.Append(" " + "(state: " + damage.total + ")"); //Block, Dodge, Critical, Reflect, Absorb etc
#endif
        #endregion

        // N O R M A L I Z E  D A M A G E  &&  A P P L Y  D A M A G E  C A P S
        //TODO: This might be wrong math-wise update(I fixed the math, but still double check it)
        damage.amount = Mathf.RoundToInt ((Mathf.Clamp(damage.total, combat.minimumDamage, combat.maximumDamage) - damage.bonus) / damage.multiplier);
        

        #region END DEBUG
#if UNITY_EDITOR
        damageReport.Append("\n(total: " + damage.total + "/" + (int)combat.damageCap + ")"); //The actual damage that will be dealt

        // D E B U G  L O G G I N G
        StringBuilder log = new StringBuilder("<b>| <color=red>D A M A G E  L O G</color> |</b>");

        log.Append("\n<b>" + name.ToUpper() + "</b>:" + ActiveOffensiveState.ToString() //attacker
                                                                                        //+ "\n" + "<" + damage.method + " " + damage.element + ">" + " Added Effect: " + damage.status //status effect, element, and method
            + " <<i>VS</i>> <b>" + defender.name.ToUpper() + "</b>:" + defender.ActiveDefensiveState.ToString()); //defender

        //log.Append("\n" + "" + name.ToUpper() + " (vs) " + defender.name.ToUpper() + ""); //header

        log.Append("\n" + damageReport.ToString()
            //+ "\n" + "[]" + damage.method + " " + damage.element + "[]"
            ); //damage

        if (this is PlayerCharacter)
        {
            PlayerCharacter player = (this as PlayerCharacter);
            if (player.HasSiegeWeapon)
            {
                log.Append("\n" + "<b>SIEGE WEAPON:</b> " + player.siegeWeapon.name);
            }
            else
            {
                log.Append("\n" + "<b>WEAPONS:</b> " + ((player.HasMainWeapon) ? player.mainWeapon.name : "no main weapon") + ((player.IsUnarmed && player.unarmedWeapon != null) ? player.unarmedWeapon.name : " and ") + ((player.HasOffhandWeapon) ? player.offhandWeapon.name : "no offhand weapon"));
            }
        }

        //DAMAGE FORMULA
        log.Append("\n<b>FORMULA:</b> ((attack - armor) + [damage roll]) * multiplier + bonus)");

        Debug.Log(log.ToString()); //DEBUG
#endif
        #endregion
        //damage.amount = (maximumDamage > 0) ? Mathf.Clamp(damage.amount, minimumDamage, maximumDamage - ((damage.bonus > 0) ? damage.bonus : 0)) : damage.amount;

        // R E T U R N  D A M A G E
        return damage;
    }
}
