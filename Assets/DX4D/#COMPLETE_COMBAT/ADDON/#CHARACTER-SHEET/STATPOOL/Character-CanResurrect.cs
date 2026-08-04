//#define ummorpg
using Mirror;
using UnityEngine;

//TODO: Put in PlayerCharacter instead.
public partial class CharacterSheet : NetworkBehaviour
{
    [Header(" [RESURRECT] ")]
    [SerializeField]
    [SyncVar][Range(0, 100)] int _resurrectionLevel = 0;
    public int resurrectionLevel
    {
        get { return _resurrectionLevel; }
        set { _resurrectionLevel = Mathf.Clamp(value, 0, 100); }
    }
    //[SyncVar] bool _canResurrect = false;
    // - - - - - - - - - - - - - -
    // H E L P E R  M E T H O D S
    // - - - - - - - - - - - - - -

    // C A N  R E S U R R E C T
    public virtual bool CanResurrect
    {
        get
        {
            //if (!enabled) { return true; } //If this is not enabled it is not alive
            if (combat.invincible) { _resurrectionLevel = 100; } //If you are invincible you can automatically resurrect at full capacity.

            return (_resurrectionLevel > 0);
        }
    }
}
public partial class Player
{
    [Command]
    public void CmdResurrect() { Resurrect(); }
    [Server]
    public void Resurrect()
    {
        if (character.CanResurrect) { character.Revive( (character.resurrectionLevel * 0.01f) ); }
    }
}