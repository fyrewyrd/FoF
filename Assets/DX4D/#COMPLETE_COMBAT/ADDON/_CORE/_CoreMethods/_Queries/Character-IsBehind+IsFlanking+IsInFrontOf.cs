using Mirror;
using UnityEngine;

public partial class CharacterSheet : NetworkBehaviour
{
    public bool IsWithinAngle(Transform myLocation, Transform targetLocation, int angle)
    {
        Vector3 forwardDifference = (targetLocation.forward.normalized - myLocation.forward.normalized);
        float differenceBetweenAngles = Mathf.Abs(Vector3.Dot(forwardDifference, myLocation.forward) * 180f);

        //Debug.Log(differenceBetweenAngles.ToString() + " < " + targetArea.ToString()); //DEBUG
        return (differenceBetweenAngles < angle);
    }

    public bool IsBehind(CharacterSheet defender)
    {
        if (!BackstabConfig.backstabEnabled || !combat.canBackstab || !defender.combat.canBeBackstabbed) return false;

        return IsWithinAngle(agent.transform, defender.agent.transform, BackstabConfig.backstabAreaAngle);
    }

    public bool IsFlanking(CharacterSheet defender)
    {
        if (!BackstabConfig.flankingEnabled || !combat.canFlank || !defender.combat.canBeFlanked) return false;

        return IsWithinAngle(agent.transform, defender.agent.transform, BackstabConfig.flankingAreaAngle);
    }

    public bool IsOverwhelming(CharacterSheet defender)
    {
		if (!BackstabConfig.overwhelmingEnabled || !combat.canOverwhelm || !defender.combat.canBeOverwhelmed) return false;
		
		return IsWithinAngle(agent.transform, defender.agent.transform, BackstabConfig.overwhelmingAreaAngle);
        //return (!(IsBehind(defender) || IsFlanking(defender)));
    }

    public bool IsNextTo(CharacterSheet defender)
    {
        return IsFlanking(defender);
    }
	
	
}
