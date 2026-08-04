using Mirror;

public partial class CombatStats// : NetworkBehaviour
{
    public SyncListBuff buffs = new SyncListBuff(); // active buffs
}

public partial class CharacterSheet : NetworkBehaviour
{
    public SyncListBuff BUFFS { get { return combat.buffs; } set { combat.buffs = value; } }
}