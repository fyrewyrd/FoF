using UnityEngine;

public partial class CombatStats// : NetworkBehaviour
{
    [Header("DEFENSE MODIFIERS")]
    [SerializeField] public DamageMultiplier defenseMultipliers = new DamageMultiplier(1.0f);
    [SerializeField] public DamageBonus defenseBonuses = new DamageBonus(0);
    [SerializeField] public ElementalMultiplier elementalDefenseMultipliers = new ElementalMultiplier(1.0f);
    [SerializeField] public ElementalBonus elementalDefenseBonuses = new ElementalBonus(0);
}
