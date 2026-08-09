/* //DEPRECIATED
using Mirror;

public abstract partial class Entity : NetworkBehaviourNonAlloc
{
    // I S  C A S T I N G  M A I N  S K I L L
    public bool IsCastingSkill(int skillSlotNumber)
    {
        return currentSkill == skillSlotNumber;
        //get { return currentSkill == 0; }
    }
    // - - - - - - - - - - - - - - -
    // I S  N O T  C A S T I N G  M A I N  S K I L L
    public bool IsNotCastingMainSkill
    {
        get { return currentSkill != 0; }
    }

}
*/
