using Mirror;
using UnityEngine;

public partial class CharacterSheet : NetworkBehaviour
{
    [Header("VFX MANAGER COMPONENT")]
    [SerializeField] VisualEffectsManager _vfx;
    public VisualEffectsManager vfx
    {
        get
        {
            if (!_vfx)
            {
                _vfx = GetComponent<VisualEffectsManager>();
                if (!_vfx) _vfx = GetComponentInChildren<VisualEffectsManager>();
                {
                    if (!_vfx) _vfx = gameObject.AddComponent<VisualEffectsManager>();
                }
            }

            return _vfx;
        }
        set { _vfx = value; }
    }
}
