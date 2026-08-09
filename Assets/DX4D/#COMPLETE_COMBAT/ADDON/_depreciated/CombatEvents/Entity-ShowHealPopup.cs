/* //DEPRECIATED
using Mirror;
using UnityEngine;

public abstract partial class Entity : NetworkBehaviourNonAlloc
{
    [Header("HEAL TEXT POPUP CONFIG")]
    [SerializeField] Color healPopupTextColor = Color.green;
    [SerializeField] int healPopupTextSize = 4;

    [Server] public void ShowHealPopup(Entity target, string message)
    {
        if (!text) text = GetComponent<TextManager>();
        if (!text || message == string.Empty) return;
        
        RpcShowTextPopup(message, healPopupTextColor, healPopupTextSize);
    }
}
*/
