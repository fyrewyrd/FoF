using Mirror;
using UnityEngine;

public partial class AI : NetworkBehaviour
{
    //TODO Add aggro range

    [Header(" [ MOVEMENT ] ")]
    [Tooltip("The chance each second this character will decide to move.\ndefault:0.3f")]
    [Range(0, 1)] public float moveProbability = 0.3f;
    [Tooltip("This character will wander away from home, but they will only go this far away.\ndefault:16")]
    public int wanderDistance = 16;
    [Tooltip("The distance from home that the character will continue to chase an enemy.\ndefault:32")]
    public int chaseDistance = 32;
    [Tooltip("The character will move to the optimal range before attacking or using a skill."
        + "\nExample: 0.5f would mean you move to the halfway point between your target and your max range."
        + "\n\ndefault:0.8f")]
    [Range(0.1f, 1)] public float optimalRange = 0.85f;
}