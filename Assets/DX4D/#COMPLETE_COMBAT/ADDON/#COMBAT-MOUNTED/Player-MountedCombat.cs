//#define RPG2D //NOTE: Enable this define for 2D support...or import the 2D_MODE unity package included with this asset

using Mirror;
using UnityEngine;

public abstract partial class Entity// : NetworkBehaviour
{
    public bool enableMountedCombat
    {
        get
        {
            return character.enableMountedCombat;
        }
        set { character.enableMountedCombat = value; }
    }
    [Server]
    string CastMountedSkill(Skill skill, CharacterSheet defender)
    {
        return character.CastMountedSkill(skill, defender);
    }
}
public partial class CharacterSheet : NetworkBehaviour
{
    [Header("MOUNTED COMBAT")]
    [SerializeField] public bool enableMountedCombat = true;
//}
//public partial class PlayerCharacter : CharacterSheet
//{
    [Server] public string CastMountedSkill(Skill skill, CharacterSheet defender)
    {
        nextTarget = defender; // return to this one after any corrections by CastCheckTarget


        //  ________________________________________________________________
        //  |                > > >  NOTE TO 2D USERS  < < <                 |
        //  |   If you are using 2D just scroll to the top of this script   |
        //  |   and remove the // from in front of #define RPG2D            |
        //  |   Alternatively you can just import the 2D_MODE.unitypackage  |
#if RPG2D
        Vector2 destination; //2D
#else
        Vector3 destination; //3D
#endif


        if (CastCheckSelf(skill) && CastCheckTarget(skill) && CastCheckDistance(skill, out destination)) // F I X - SEE ABOVE
        {
#region DEBUG
#if UNITY_EDITOR
            //Debug.Log(bonusDamage + " DAMAGE FROM WEAPONS"); //DEBUG
            Debug.Log(name.ToUpper() + " CASTING MOUNTED SKILL " + skill.name.ToUpper()); //DEBUG
#endif
#endregion
            // start casting and cancel movement in any case
            ResetMovement();
            StartCastSkill(skill);
            return "CASTING";
        }
        else
        {
            // invalid target. stop trying to cast, but keep moving.
            CancelCastSkill();//currentSkill = -1;
            return "MOVING";
        }
    }
}
