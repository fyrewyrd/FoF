using Mirror;
using UnityEngine;

public partial class CharacterSheet : NetworkBehaviour
{
    //[Header(" [ COMBAT ] ")]
    [Tooltip("Damage, Defense, etc")]
    [SerializeField] CombatStats _combat;
    public CombatStats combat
    {
        get
        {
            if (_combat == null) _combat = gameObject.GetComponent<CombatStats>();
            if (_combat == null)
            {
                _combat = gameObject.AddComponent<CombatStats>();
#if UNITY_EDITOR
                Debug.Log(_combat.name + "COMBAT" + ": Component added automatically...\nIt might improve performance to add this component manually in the editor.");
#endif
            }
            return _combat;
        }
        set { _combat = value; }
    }
}