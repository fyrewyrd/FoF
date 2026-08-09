using UnityEngine;

public partial class AI// : NetworkBehaviour
{
    /// <summary>
    /// The amount of excess hate required to break the enemy's focus on a target.
    /// Increase this to prevent mobs from getting confused and "rubberbanding" between targets.
    /// </summary>
    [Header(" [AGGRO] ")]
    [Tooltip("The amount of hate required to take aggro from the top damager." +
        "\n1.0 here would mean you would have to do twice as much damage to take aggro..." +
        "something closer to 0.2 is more ideal")]
    [SerializeField] public float aggroFocus = 0.1f;
    
    [Tooltip("When this character is attacked by an enemy, aggro will build against that attacker." +
        "\nMultiply this number by the amount you were attacked for to find the amount of aggro.")]
    public float aggroScale = 1.0f;
    
    [Tooltip("When this character's enemy gets healed, aggro will build against that healer." +
        "\nMultiply this number by the amount the enemy was healed for to find the amount of aggro.")]
    public float healerAggroScale = 1.0f;


}