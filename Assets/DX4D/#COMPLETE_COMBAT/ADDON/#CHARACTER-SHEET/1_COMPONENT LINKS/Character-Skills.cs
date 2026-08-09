#define automatic_components
#define add_missing_components
using Mirror;
using UnityEngine;

public partial class CharacterSheet : NetworkBehaviour
{
    //[Header(" [ SKILLS ] ")]
    [SerializeField] Skills _skills;
    public Skills skills
    {
        get
        {
#if automatic_components
            if (_skills == null) _skills = gameObject.GetComponent<Skills>();
#endif

#if add_missing_components
            if (_skills == null)
            {
                _skills = gameObject.AddComponent<Skills>();
                #region DEBUG
#if UNITY_EDITOR
                Debug.Log(_ai.name + "Skills" + ": Component added automatically...\nIt might improve performance to add this component manually in the editor.");
#endif
                #endregion
            }
#endif
            return _skills;
        }
        set { _skills = value; }
    }
}