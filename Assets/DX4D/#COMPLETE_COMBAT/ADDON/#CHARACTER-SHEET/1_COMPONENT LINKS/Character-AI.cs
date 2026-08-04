#define automatic_components
#define add_missing_components
using Mirror;
using UnityEngine;

public partial class CharacterSheet : NetworkBehaviour
{
    //[Header(" [ AI ] ")]
    [Header(" [ COMPONENT LINKS ] ")]
    [SerializeField] AI _ai;
    public AI ai
    {
        get
        {
#if automatic_components
            if (_ai == null) _ai = gameObject.GetComponent<AI>();
#endif

#if add_missing_components
            if (_ai == null)
            {
                _ai = gameObject.AddComponent<AI>();
                #region DEBUG
#if UNITY_EDITOR
                Debug.Log(_ai.name + "AI" + ": Component added automatically...\nIt might improve performance to add this component manually in the editor.");
#endif
                #endregion
            }
#endif
            return _ai;
        }
        set { _ai = value; }
    }
}