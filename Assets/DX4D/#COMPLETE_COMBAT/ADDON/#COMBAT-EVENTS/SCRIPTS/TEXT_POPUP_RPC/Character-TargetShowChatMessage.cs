using Mirror;

public partial class CharacterSheet : NetworkBehaviour
{
    // T A R G E T  C H A T  M E S S A G E
    [TargetRpc]
    public void TargetShowChatMessage(string channelName, string message)
    {
        ShowChatMessage(channelName, message);
    }
}
