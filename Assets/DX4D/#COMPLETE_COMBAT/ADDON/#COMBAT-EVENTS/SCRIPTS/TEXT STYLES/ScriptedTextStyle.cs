//#define RPG2D //NOTE: Enable this define for 2D support...or import the 2D_MODE unity package included with this asset
#define TEXTMESHPRO
using UnityEngine;

//TEXT
#if TEXTMESHPRO
using TMPro;
#else
using System.Text;
#endif

[CreateAssetMenu(menuName = "DX4D/TEXT/Scripted Text Style", order = 95)]
public class ScriptedTextStyle : ScriptableObject
{
    [SerializeField] const float forcelimit = 10f;

    [Header("TEXT POPUP PREFAB")]
#if RPG2D
    [SerializeField] GameObject _2dPopupPrefab = null;
#else
    [SerializeField] GameObject _3dPopupPrefab = null;
#endif
    public GameObject popupPrefab
    {
        get
        {
            //  ________________________________________________________________
            //  |                > > >  NOTE TO 2D USERS  < < <                 |
            //  |   If you are using 2D just scroll to the top of this script   |
            //  |   and remove the // from in front of #define RPG2D            |
            //  |   Alternatively you can just import the 2D_MODE.unitypackage  |
#if RPG2D
            if (!_2dPopupPrefab) _2dPopupPrefab = Resources.Load<GameObject>("Text/Popup/2DTextPopupPrefab");
            return _2dPopupPrefab;
#else
            if (!_3dPopupPrefab) _3dPopupPrefab = Resources.Load<GameObject>("Text/Popup/3DTextPopupPrefab");
            return _3dPopupPrefab;
#endif
        }

        set
        {
#if RPG2D
            _2dPopupPrefab = value;
#else
            _3dPopupPrefab = value;
#endif
        }
    }

    [Header("TEXT STYLE")]
    [SerializeField]
    public Color color = Color.white;
    [SerializeField] public int size = 4;


    [Header("TEXT SHADOW")]
    [SerializeField]
    public bool textShadowEnabled = true;

    [Header("POPUP TEXT FONT")]
#if TEXTMESHPRO && !RPG2D
    [SerializeField] public TMP_FontAsset popupTextFont;
#else
    [SerializeField] public Font popupTextFont;
#endif

    [Header("TEXT FADE AWAY")]
    [SerializeField] public float duration = 6.0f;

    [Header("TEXT MOTION")]
    [SerializeField] [Range(-forcelimit, forcelimit)] float gravityMultiplier = 1.0f;
    [SerializeField] [Range(-forcelimit, forcelimit)] float forceMultiplier = 1.0f;
    [SerializeField] [Range(-forcelimit, forcelimit)] float randomization = 0.0f;
    [SerializeField] Vector3 _velocity = Vector3.down;
    public Vector3 velocity
    {
        get
        {
            Vector3 v = _velocity;

            // R A N D O M  T E X T  V E L O C I T Y
            if (randomization > 0)
            {
                v = v + DX4D.Tools.GetRandom.Vector(-randomization, randomization);
            }

            v.y = v.y * gravityMultiplier;

            v = v * forceMultiplier;

            return (v);
        }
    }
    //[SerializeField] public DynamicTextInfo text = new DynamicTextInfo();
    //[SerializeField] public DynamicTextStyle style = new DynamicTextStyle();
    //[SerializeField] public DynamicTextMotion motion = new DynamicTextMotion();
}
