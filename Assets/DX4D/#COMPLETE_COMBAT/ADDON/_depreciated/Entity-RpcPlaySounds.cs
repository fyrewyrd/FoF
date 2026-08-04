/* //DEPRECIATED
using Mirror;
using UnityEngine;

public abstract partial class Entity : NetworkBehaviourNonAlloc
{
    //VFX
    [ClientRpc] public void RpcPlaySounds(AudioC[] soundClips, float interval)
    {
        for (int i = 0; i < soundClips.Length; i++)
        {
            if (soundClips[i] is AudioClip) HandleSoundDelayed((soundClips[i] as AudioClip), (interval * i));
        }
    }
}
*/
