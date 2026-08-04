using UnityEngine;

public partial class CombatStats// : Mirror.NetworkBehaviour
{
    [Header("DAMAGE CAP")][Tooltip("Lowest = 99, Low = 255, Mid = 999, High = 9999, Highest = 99999")]
    [SerializeField] public DamageCap damageCap = DamageCap.High;
    int minDamage = 0; //NOTE: Setting this to more than zero might mess up damage avoidance moves like dodge, damage reflect, and absorb

    public bool damageCapBroken = false;
    public int minimumDamage { get { return minDamage; } }
    public int maximumDamage { get { return (int)damageCap; } }
}
