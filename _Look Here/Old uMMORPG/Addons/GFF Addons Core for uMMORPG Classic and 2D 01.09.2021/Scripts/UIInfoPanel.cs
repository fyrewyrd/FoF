using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInfoPanel : MonoBehaviour
{
    public GameObject panel;
    public Text text;

    //singleton
    public static UIInfoPanel singleton;
    public UIInfoPanel() { singleton = this; }

    public void Show(string infoText)
    {
        text.text = infoText;
        panel.SetActive(true);
    }
}
