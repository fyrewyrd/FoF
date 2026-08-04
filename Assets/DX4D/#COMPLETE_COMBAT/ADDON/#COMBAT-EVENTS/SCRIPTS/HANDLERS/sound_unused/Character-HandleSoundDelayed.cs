using Mirror;
using UnityEngine;
using System.Collections;

public partial class CharacterSheet : NetworkBehaviour
{
    // D E L A Y E D  S O U N D  C L I P
    [Client] IEnumerator HandleSoundDelayed(AudioClip soundClip, float delay)
    {
        if (delay <= 0) delay = 0.1f; //DISALLOW "MACHINE GUN" REPETITION

        yield return new WaitForSeconds(delay);

        HandleSoundClip(soundClip);
    }
}
