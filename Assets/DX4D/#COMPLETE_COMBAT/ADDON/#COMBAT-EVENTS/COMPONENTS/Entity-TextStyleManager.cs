using Mirror;
using UnityEngine;

public partial class CharacterSheet : NetworkBehaviour
{

    [Header("TEXT MANAGER COMPONENT")]
    [SerializeField] TextStyleData _text;
    public TextStyleData text
    {
        get
        {
            if (!_text)
            {
                _text = GetComponent<TextStyleData>();
                if (!_text)
                {
                    _text = GetComponentInChildren<TextStyleData>();
                    if (!_text) { _text = gameObject.AddComponent<TextStyleData>(); }
                }
            }

            return _text;
        }
        set { _text = value; }
    }
}
