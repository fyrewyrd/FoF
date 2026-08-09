using Mirror;
using UnityEngine;

public partial class Weaknesses// : NetworkBehaviour
{
    [Header("DAMAGE WEAKNESS")]
    public MethodOfDamage weakToDamage = MethodOfDamage.NoDamage;
    public MethodOfDamage veryWeakToDamage = MethodOfDamage.NoDamage;
}

public partial class CharacterSheet : NetworkBehaviour
{
    public bool WeakTo(MethodOfDamage method)
    {
        return ((weakness.weakToDamage & method) != 0);
    }
    public bool VeryWeakTo(MethodOfDamage method)
    {
        return ((weakness.veryWeakToDamage & method) != 0);
    }
}