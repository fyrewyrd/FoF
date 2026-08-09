using System.Collections;
using UnityEngine;

public partial class PlayerCharacter : CharacterSheet
{
    public IEnumerator FollowupAttack(int slotNumber, float delay)
    {
        yield return new WaitForSeconds((delay + 0.25f));

        TryUseSkill(slotNumber, true);
    }
}
