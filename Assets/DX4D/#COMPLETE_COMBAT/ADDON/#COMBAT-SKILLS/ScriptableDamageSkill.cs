using UnityEngine;

[CreateAssetMenu(menuName="DX4D/SKILLS/Combat Skill", order=21)]
public class ScriptableDamageSkill : TargetDamageSkill
{
    /*
    public override bool CheckTarget(Entity caster)
    {
        // target exists, alive, not self, oktype?
        return caster.target != null && base.CheckTarget(caster) && caster.CanAttack(caster.target);
    }

    public override bool CheckDistance(Entity caster, int skillLevel, out Vector3 destination)
    {
        // target still around?
        if (caster.target != null)
        {
            destination = caster.target.collider.ClosestPointOnBounds(caster.transform.position);
            return Utils.ClosestDistance(caster.collider, caster.target.collider) <= castRange.Get(skillLevel);
        }
        destination = caster.transform.position;
        return false;
    }

    public override void Apply(Entity caster, int skillLevel)
    {
        // deal damage directly with base damage + skill damage
        base.Apply(caster, skillLevel);
    }
    */
}
