using Mirror;

public partial class CharacterSheet : NetworkBehaviour
{
    //HEAL
    [ClientRpc] public void RpcShowHealPopup(int amount)
    {
        HandleTextPopup(transform, ("+" + amount.ToString()), text.healTextStyle);// message, popup.style.color, popup.style.size, new Vector3((v.x * loc.x), (v.y * loc.y), (v.z * loc.z)), popup.style.animation.velocity);
    }
}
