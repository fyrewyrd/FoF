using Mirror;

public partial class CharacterSheet : NetworkBehaviour
{
    //SPEECH
    [ClientRpc] public void RpcShowSpeechPopup(string message)
    {
        HandleTextPopup(transform, message, text.speechTextStyle);// message, popup.style.color, popup.style.size, new Vector3((v.x * loc.x), (v.y * loc.y), (v.z * loc.z)), popup.style.animation.velocity);
    }
}
