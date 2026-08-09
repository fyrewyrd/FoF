#define prefetch // default:enabled - Preload default text styles when this component is loaded
#define hotload // default:enabled - Hotload default text styles when they are accessed
using Mirror;
using UnityEngine;

public class TextStyleData : NetworkBehaviour
{
#if prefetch
    private void OnValidate() //TODO: Eval this - This is the original way that worked but showed a warning
    //private void Start()
    {
        if (!_defaultTextStyle) { _defaultTextStyle = Resources.Load<ScriptedTextStyle>("Text/Style/DefaultTextStyle"); }
        if (!_damageTextStyle) { _damageTextStyle = Resources.Load<ScriptedTextStyle>("Text/Style/DamageTextStyle"); }
        if (!_healTextStyle) { _healTextStyle = Resources.Load<ScriptedTextStyle>("Text/Style/HealTextStyle"); }
        if (!_speechTextStyle) { _speechTextStyle = Resources.Load<ScriptedTextStyle>("Text/Style/SpeechTextStyle"); }
    }
#endif

    [Header("TEXT STYLE PRESETS")]
    //DEFAULT
    [SerializeField] private ScriptedTextStyle _defaultTextStyle;
    public ScriptedTextStyle defaultTextStyle
    {
        get
        {
#if hotload
            if (!_defaultTextStyle) { _defaultTextStyle = Resources.Load<ScriptedTextStyle>("Text/Style/DefaultTextStyle"); }
#endif
            return _defaultTextStyle;
        }
        set
        {
            _defaultTextStyle = value;
        }
    }
    //DAMAGE
    [SerializeField] private ScriptedTextStyle _damageTextStyle;
    public ScriptedTextStyle damageTextStyle
    {
        get
        {
#if hotload
            if (!_damageTextStyle) { _damageTextStyle = Resources.Load<ScriptedTextStyle>("Text/Style/DamageTextStyle"); }
#endif
            return _damageTextStyle;
        }
        set
        {
            _damageTextStyle = value;
        }
    }
    //HEAL
    [SerializeField] private ScriptedTextStyle _healTextStyle;
    public ScriptedTextStyle healTextStyle
    {
        get
        {
#if hotload
            if (!_healTextStyle) { _healTextStyle = Resources.Load<ScriptedTextStyle>("Text/Style/HealTextStyle"); }
#endif
            return _healTextStyle;
        }
        set
        {
            _healTextStyle = value;
        }
    }
    //SPEECH
    [SerializeField] private ScriptedTextStyle _speechTextStyle;
    public ScriptedTextStyle speechTextStyle
    {
        get
        {
#if hotload
            if (!_speechTextStyle) { _speechTextStyle = Resources.Load<ScriptedTextStyle>("Text/Style/SpeechTextStyle"); }
#endif
            return _speechTextStyle;
        }
        set
        {
            _speechTextStyle = value;
        }
    }
    //[Header("SCRIPTED TEXT CONFIG")] [Tooltip("The attached Scripted Text Config will override any default settings specified by this component")]
    //[SerializeField] ScriptedTextPopup defaultPopup;
    //[SerializeField] ScriptedTextConfig _textConfig;
    //ScriptedTextConfig textConfig
    //{
    //    get {
    //        if (!_textConfig) _textConfig = Instantiate(new ScriptedTextConfig());
    //        return _textConfig;
    //    }
    //    set { _textConfig = value; }
    //}

    /*
    public string text
    {
        get { return (textPrefix + textBody + textSuffix); }
        set { textPrefix = string.Empty; textBody = value; textSuffix = string.Empty; }
    }

    [Header("TEXT STRING")]
    [SerializeField] public string textPrefix;
    [SerializeField] public string textBody;
    [SerializeField] public string textSuffix;


    [Header("POPUP TEXT CONFIG")]
    [SerializeField] Color color;
    [SerializeField] int size;

    [Header("DEFAULT POPUP ORIENTATION")]
    [SerializeField] public OrientationInfo orientation;

    [Header("DEFAULT POPUP MOTION")]
    [SerializeField] public TextMotionInfo motion;

    [Header("POPUP VISUAL FX")]
    [SerializeField] public TextVisualInfo visual;
    */


    /*
    public void Configure()
    {
        textConfig.color = textColor;
        textConfig.size = textSize;
    }*/

    //internal Bounds myBounds { get { return GetComponent<Collider>().bounds; } }
    //ENTITY HOTSPOTS
    //public Vector3 overhead { get { return new Vector3(myBounds.center.x, myBounds.max.y, myBounds.center.z); } }
    //public Vector3 underfoot { get { return new Vector3(myBounds.center.x, myBounds.min.y, myBounds.center.z); } }
    //public Vector3 midbody { get { return new Vector3(myBounds.center.x, myBounds.center.y, myBounds.center.z); } }

    // P O P U P  T Y P E S
    // T E X T  P O P U P
    //[ClientRpc] public void RpcShowPopup()
    //{
    //    ShowTextPopup();// message, popup.style.color, popup.style.size, new Vector3((v.x * loc.x), (v.y * loc.y), (v.z * loc.z)), popup.style.animation.velocity);
    //}

    /*
    // A C T I V A T I O N
    [ClientRpc]
    public void RpcShowActivationPopup(string message)
    {
        ShowTextPopup(message, text.style.color.activate, text.style.size.normal, midbody, Vector3.zero);
    }
    // P V P
    [ClientRpc]
    public void RpcShowPvPPopup(string message)
    {
        ShowTextPopup(message, text.style.color.activate, text.style.size.small, midbody, Vector3.down);
    }
    // M O V E M E N T
    [ClientRpc]
    public void RpcShowMovementStatePopup(string message)
    {
        ShowTextPopup(message, text.style.color.activate, text.style.size.normal, underfoot, Vector3.zero);
    }
    // S T A T U S  E F F E C T S
    [ClientRpc]
    public void RpcShowStatusAppliedPopup(string message)
    {
        ShowTextPopup(message, Config.Text.statusAppliedColor, Config.Text.statusAppliedTextSize, midbody, Vector3.right);
    }
    [ClientRpc]
    public void RpcShowStatusRemovedPopup(string message)
    {
        ShowTextPopup(message, Config.Text.statusRemovedColor, Config.Text.statusRemoveedTextSize, midbody, Vector3.left);
    }
    // C O M B A T  S T A T E
    [ClientRpc]
    public void RpcShowCombatStatePopup(string message)
    {
        ShowTextPopup(message, text.style.color.activate, text.style.size.normal, midbody, Vector3.right);
    }
    // O O P S
    [ClientRpc]
    public void RpcShowOopsPopup(string message)
    {
        ShowTextPopup(message, Config.Text.oopsTextColor, text.style.size.normal, midbody, Vector3.down);
    }
    // H E A L
    [ClientRpc]
    void RpcShowHealPopup(int amount)
    {
        ShowTextPopup("+" + amount.ToString(), Config.Text.healTextColor, text.style.size.normal, overhead, Vector3.down);
    }
    // C O U N T
    [ClientRpc]
    public void RpcShowCountdownPopup(string message)
    {
        ShowTextPopup(message, Config.Text.countdownTimerColor, text.style.size.normal, overhead, Vector3.down);
    }
    // S P E N D
    [ClientRpc]
    public void RpcShowSpendPopup(string message)
    {
        ShowTextPopup(message, Config.Text.spendTextColor, text.style.size.normal, overhead, Vector3.up);
    }
    */
}

//public abstract partial class Entity : NetworkBehaviourNonAlloc
//{
/*
//DELAYED POPUPS
[Client] IEnumerator ShowTextPopup(string message, Color color, float delay)
{
    yield return new WaitForSeconds(delay);

    ShowTextPopup(message, color);
}
[Client] IEnumerator ShowTextPopup(string message, float delay)
{
    yield return new WaitForSeconds(delay);

    ShowTextPopup(message);
}
*/
//}
