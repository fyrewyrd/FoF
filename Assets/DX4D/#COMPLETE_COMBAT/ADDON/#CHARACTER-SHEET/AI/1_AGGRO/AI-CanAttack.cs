#define AUTO_ADD_COMPONENTS //NOTE: It is usually best to add components manually rather than at runtime like this does
using Mirror;
using UnityEngine.AI;

public partial class AI : NetworkBehaviour
{
    public virtual bool CanAttack(CharacterSheet entity)
    {
        return !character.IsDead &&
               !entity.IsDead &&
               entity != this &&
               character.combat.isAttackable && entity.combat.isAttackable &&
               !NavMesh.Raycast(transform.position, entity.transform.position, out NavMeshHit hit, NavMesh.AllAreas);
    }
}