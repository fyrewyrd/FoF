using UnityEditor;
using UnityEngine;

public class GameForFunAddons : MonoBehaviour
{
    [MenuItem("GFF Addons/YouTube")]
    static void YouTube()
    {
        Application.OpenURL("https://www.youtube.com/channel/UCIebsFg8bMoXo90Xk1P4yoQ");
    }

    [MenuItem("GFF Addons/Profile in Asset Store")]
    static void assetstore()
    {
        Application.OpenURL("https://assetstore.unity.com/publishers/37837");
    }
}
