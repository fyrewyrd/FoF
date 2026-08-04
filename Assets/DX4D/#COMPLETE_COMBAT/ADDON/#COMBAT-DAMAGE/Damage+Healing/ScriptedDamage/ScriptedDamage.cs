//using System.Collections.Generic;
using UnityEngine;
using System.Text;

[CreateAssetMenu(menuName = "DX4D/DAMAGE/Scripted Damage", order = 30)]
public partial class ScriptedDamage : ScriptableObject {

    [Header("DAMAGE CONFIGURATION")]
    [SerializeField] public DamageInfo damage;

    [Header("VISUAL EFFECTS")]
    [SerializeField] public GameObject onHitVisualEffect;

    [Header("TARGET CONFIGURATION")]
    [SerializeField] public bool useOnSelf;
    [HideInInspector] public Entity caster;
    [HideInInspector] public Entity target;

    public override string ToString()
    {
        StringBuilder result = new StringBuilder("{DAMAGECOLOR}{DAMAGE}{ENDDAMAGECOLOR}");

        //COLOR ELEMENTAL DAMAGE
        result.Replace("{DAMAGECOLOR}", " <color=" + DX4D.Tools.GetHex.FromElement(damage.element) + "><b>");
        result.Replace("{ENDDAMAGECOLOR}", "</b></color>");

        //DAMAGE
        if (damage.max > 0)
        {
            if (damage.fixedDamage)
            {
                result.Replace("{DAMAGE}", "[" + damage.max + "{DAMAGEBONUS}]" + "{DAMAGEMETHOD}" + "{DAMAGEELEMENT}" + " damage");
            }
            else
            {
                result.Replace("{DAMAGE}", "[" + damage.min + "<b>-</b>" + damage.max
                    + "{DAMAGEBONUS}]"
                     + "{DAMAGEELEMENT}" + "{DAMAGEMETHOD}"
                + " Damage");
            }
        }
        else
        {
            result.Replace("{DAMAGE}", "");
        }

        //DAMAGE BONUS
        if (damage.bonus > 0)
        {
            result.Replace("{DAMAGEBONUS}", " + " + damage.bonus);
        }
        else
        {
            result.Replace("{DAMAGEBONUS}", "");
        }
        
        //DAMAGE PROPERTIES
        result.Replace("{DAMAGEMETHOD}",
            (damage.method != MethodOfDamage.NoDamage && damage.method != MethodOfDamage.Physical)
            ? (" " + damage.method.ToString() + "") : "");
        result.Replace("{DAMAGEELEMENT}", (damage.element != Element.Neutral) ? (" <color=" + DX4D.Tools.GetHex.FromElement(damage.element) + "><b>" + damage.element.ToString() + "</b></color>") : "");

        //DAMAGE OVER TIME
        if (damage.damageOverTime.Count > 0)
        {
            //result.Append("\n\n<color=white><b>[damage over time]</b></color>");
            foreach (ScriptedDamage dmg in damage.damageOverTime)
            {
                result.Append("\n     " + dmg.ToString());
            }
        }

        return result.ToString();
        //return damage.ToString();

        //StringBuilder result = new StringBuilder();

        //result.Append("{DAMAGE}");
        //result.Replace("{DAMAGE}", "[" + damage.ToString() + "]");
        //result.Append();
        //result.Append();

        //return result.ToString();
    }
}

    //[SerializeField] public MethodOfDamage damageMethod; //DEPRECIATED
    //[SerializeField] public Element damageElement; //DEPRECIATED
    //[SerializeField] [EnumButtons] public StatusEffect damageStatusEffect; //DEPRECIATED

    //[SerializeField] public int healAmount = 0; //DEPRECIATED
    //[SerializeField] public int damageAmount = 0; //DEPRECIATED



/* //TODO: I found this to just be a waste of space //DEPRECIATED
public partial class ScriptedDamage : ScriptableObject
{
    // D E F E N S E S
    // d a m a g e  m e t h o d

    public CombatStatInfo physicalStats = new CombatStatInfo(MethodOfDamage.Physical, Element.Neutral);
    public CombatStatInfo magicStats = new CombatStatInfo(MethodOfDamage.Magic, Element.Neutral);
    public CombatStatInfo bloodStats = new CombatStatInfo(MethodOfDamage.Blood, Element.Neutral);
    public CombatStatInfo spiritStats = new CombatStatInfo(MethodOfDamage.Spirit, Element.Neutral);
    public CombatStatInfo poisonStats = new CombatStatInfo(MethodOfDamage.Poison, Element.Neutral);

    //TODO:
    //public DefenseInfo furyDefense = new DefenseInfo(MethodOfDamage.Fury, 1.0f, 0);
    //public DefenseInfo staminaDefense = new DefenseInfo(MethodOfDamage.Stamina, 1.0f, 0);
    //public DefenseInfo manaDefense = new DefenseInfo(MethodOfDamage.Mana, 1.0f, 0);

    // e l e m e n t a l
    public CombatStatInfo elementalStats = new CombatStatInfo(MethodOfDamage.Magic, Element.Neutral);

    public CombatStatInfo fireStats = new CombatStatInfo(MethodOfDamage.Magic, Element.Fire);
    public CombatStatInfo iceStats = new CombatStatInfo(MethodOfDamage.Magic, Element.Ice);
    public CombatStatInfo lightningStats = new CombatStatInfo(MethodOfDamage.Magic, Element.Lightning);
    public CombatStatInfo waterStats = new CombatStatInfo(MethodOfDamage.Magic, Element.Water);
    public CombatStatInfo airStats = new CombatStatInfo(MethodOfDamage.Magic, Element.Wind);
    public CombatStatInfo earthStats = new CombatStatInfo(MethodOfDamage.Magic, Element.Earth);
    public CombatStatInfo darkStats = new CombatStatInfo(MethodOfDamage.Magic, Element.Dark);
    public CombatStatInfo holyStats = new CombatStatInfo(MethodOfDamage.Magic, Element.Holy);

}
*/
