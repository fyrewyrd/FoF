using UnityEngine;

public partial class CombatStats// : Mirror.NetworkBehaviour
{
    [Header("INNATE DAMAGE")]
    [Tooltip("Allows innate damage methods and elements to be imbued into all Physical or NonElemental attacks")]
    [SerializeField] public bool imbueAttacks = true;

    [Tooltip("The Method of Damage that is imbued into physical attacks."
        + "\nIf this is set to NoDamage, the entity will not deal physical damage")]
    [SerializeField] public MethodOfDamage innateMethodOfDamage = MethodOfDamage.Physical;
    [Tooltip("The Element of Damage that is imbued into non elemental (Neutral) attacks")]
    [SerializeField] public Element innateDamageElement = Element.Neutral;
}
