using Mirror;
using UnityEngine;

public partial class CombatStats// : NetworkBehaviour
{
    [Header("[ C O M B A T  S T A T S ]")]
    public int level = 1;
}

public partial class CharacterSheet : NetworkBehaviour
{
    public int level { get { return combat.level; } set { combat.level = value; } }
}