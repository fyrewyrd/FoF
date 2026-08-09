/* //DEPRECIATED
using Mirror;
using UnityEngine;

public abstract partial class Entity : NetworkBehaviourNonAlloc
{
    [Server] public void ShowChatMessage(string channelName, string message)
    {
        if (!text) text = GetComponent<TextManager>();
        if (!text || message == string.Empty) return;

        //textPopup.Configure(normalPopupTextColor, normalPopupTextSize);

        text.TargetShowChatMessage(channelName, message);
    }
}
*/
