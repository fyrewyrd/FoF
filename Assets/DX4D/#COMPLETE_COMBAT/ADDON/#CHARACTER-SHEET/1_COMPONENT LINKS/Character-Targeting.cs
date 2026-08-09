#define automatic_components
#define add_missing_components
using Mirror;
using UnityEngine;

public partial class CharacterSheet : NetworkBehaviour
{
    //[Header(" [ TARGETING ] ")]
    [SerializeField] Targeting _target;
    public Targeting targeting
    {
        get
        {
#if automatic_components
            if (_target == null) _target = gameObject.GetComponent<Targeting>();
#endif

#if add_missing_components
            if (_target == null)
            {
                _target = gameObject.AddComponent<Targeting>();
                #region DEBUG
#if UNITY_EDITOR
                Debug.Log(_target.name + "Targeting" + ": Component added automatically...\nIt might improve performance to add this component manually in the editor.");
#endif
                #endregion
            }
#endif
            return _target;
        }
        set { _target = value; }
    }
}