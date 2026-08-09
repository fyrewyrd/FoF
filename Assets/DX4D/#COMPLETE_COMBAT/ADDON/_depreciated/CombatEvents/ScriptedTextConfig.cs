/* //DEPRECIATED
using UnityEngine;

[CreateAssetMenu(menuName = "DX4D/TEXT/Scripted Text Config", order = 91)]
public class ScriptedTextConfig : ScriptableObject
{
    [Header("TEXT STYLE")]
    //[SerializeField] public TextStyleInfo style;
    [SerializeField] public int size = 4;
    [SerializeField] public Color color = Color.white;

    [Header("POPUP ORIENTATION")]
    [SerializeField] public OrientationInfo orientation;
    //[SerializeField] public TextPopupLocation handlesPopupType;

    [Header("POPUP MOTION")]
    [SerializeField] public TextMotionInfo motion;

    [Header("POPUP VISUAL FX")]
    [SerializeField] public TextVisualInfo visual;
}
*/
