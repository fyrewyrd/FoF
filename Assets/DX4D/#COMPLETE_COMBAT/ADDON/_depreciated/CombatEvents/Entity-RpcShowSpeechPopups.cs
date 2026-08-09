/* //DEPRECIATED
using Mirror;

public abstract partial class Entity : NetworkBehaviourNonAlloc
{
    //DELIVER A SPEECH
    [Server] public void ShowSpeechPopups(string[] lines, float interval)
    {
        //float currentDelay = interval;

        for (int i = 0; i < lines.Length; i++)
        {
            HandleTextPopupDelayed(transform, lines[i], (interval * i), text.speechTextStyle);
        }
    }
}
*/
