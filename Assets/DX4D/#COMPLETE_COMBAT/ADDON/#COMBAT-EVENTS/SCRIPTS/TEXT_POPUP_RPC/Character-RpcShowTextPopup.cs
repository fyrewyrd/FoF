using Mirror;

public partial class CharacterSheet : NetworkBehaviour
{
    // TEXT POPUP
    [ClientRpc] public void RpcShowTextPopup(string message)
    {
        HandleTextPopup(transform, message, text.defaultTextStyle);// message, popup.style.color, popup.style.size, new Vector3((v.x * loc.x), (v.y * loc.y), (v.z * loc.z)), popup.style.animation.velocity);
    }
}
