using UnityEngine;

public partial class CombatStats// : Mirror.NetworkBehaviour
{
    //[Header("DAMAGE METHOD")]
    [SerializeField, HideInInspector] public MethodOfDamage damageMethod = MethodOfDamage.Physical;
}
