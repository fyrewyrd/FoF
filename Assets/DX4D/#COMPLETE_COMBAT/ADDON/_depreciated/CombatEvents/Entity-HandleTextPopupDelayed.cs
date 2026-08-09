/* //DEPRECIATED
using Mirror;
using UnityEngine;
using System.Collections;

public abstract partial class Entity : NetworkBehaviourNonAlloc
{
    // D E L A Y E D  T E X T  P O P U P
    [Server] IEnumerator HandleTextPopupDelayed(Transform location, string message, float delay, ScriptedTextStyle textStyle)
    {
        if (delay <= 0) delay = 0.1f; //DISALLOW "MACHINE GUN" INSTANT TEXT

        yield return new WaitForSeconds(delay);

        RpcShowSpeechPopup(message);
        //HandleTextPopup(location, message, textStyle);
    }
}
*/
