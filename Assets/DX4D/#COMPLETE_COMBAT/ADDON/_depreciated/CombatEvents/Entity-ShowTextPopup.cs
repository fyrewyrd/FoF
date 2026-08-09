/* //DEPRECIATED
using Mirror;
using UnityEngine;

public abstract partial class Entity : NetworkBehaviourNonAlloc
{
    [Header("STANDARD TEXT POPUP CONFIG")]
    [SerializeField] Color normalPopupTextColor = Color.white;
    [SerializeField] int normalPopupTextSize = 4;

    [Server] public void ShowTextPopup(string message)
    {
        ShowTextPopup(message);
    }
    [Server] public void ShowTextPopup(Entity target, string message)
    {
        if (!text) text = GetComponent<TextManager>();
        if (!text || message == string.Empty) return;

        Debug.Log("[Entity-ShowTextPopup] <" + name.ToUpper() + "> triggered popup text on <" + target.name.ToUpper() + ">\n" + message);

        text.RpcShowTextPopup(target.transform, message, normalPopupTextColor, normalPopupTextSize);
    }
}
*/
