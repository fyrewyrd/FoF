using Mirror;

public partial class CharacterSheet : NetworkBehaviour
{
    // C H A T  M E S S A G E
    [Client]
    public void ShowChatMessage(string channelName, string message)
    {
        UIChat.singleton.AddMessage(new ChatMessage("", ("(" + channelName + ")"), message, "", null)); //TODO: This still depends on ummorpg
    }
}
