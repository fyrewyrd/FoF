#define TEXTMESHPRO
using Mirror;
using UnityEngine;
using System.Collections;

//TEXT
#if TEXTMESHPRO
using TMPro;
#else
using System.Text;
#endif

public partial class CharacterSheet : NetworkBehaviour
{
    // T E X T  P O P U P
    [Client] IEnumerator HandleTextPopupDelayed(Transform target, string message, ScriptedTextStyle text, float delay)
    {
        if (delay <= 0) delay = 0.1f; //DISALLOW "MACHINE GUN" EFFECT

        yield return new WaitForSeconds(delay);

        HandleTextPopup(target, message, text);
    }
}
