using Mirror;
using UnityEngine;

public partial class CharacterSheet : NetworkBehaviour
{
    //VFX
    [Client] public void HandleSoundClip(AudioClip soundClip)
    {
        AudioSource.PlayClipAtPoint(soundClip, transform.position);
    }
}
