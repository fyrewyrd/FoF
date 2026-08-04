using UnityEngine;

public partial class CombatStats// : NetworkBehaviour
{
    [Header("DAMAGE MODIFIERS")]
    [SerializeField] public DamageMultiplier damageMultipliers = new DamageMultiplier(1.0f);
    [SerializeField] public DamageBonus damageBonuses = new DamageBonus(0);
    [SerializeField] public ElementalMultiplier elementalDamageMultipliers = new ElementalMultiplier(1.0f);
    [SerializeField] public ElementalBonus elementalDamageBonuses = new ElementalBonus(0);
}
