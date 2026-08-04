using Mirror;
//using UnityEngine;

public partial class Monster : Entity
{
    // skills //////////////////////////////////////////////////////////////////
    // CanAttack check
    // we use 'is' instead of 'GetType' so that it works for inherited types too
    public override bool CanAttack(Entity entity)
    {
        return base.CanAttack(entity) &&
               (entity is Player ||
                entity is Pet ||
                entity is Mount);
    }
}