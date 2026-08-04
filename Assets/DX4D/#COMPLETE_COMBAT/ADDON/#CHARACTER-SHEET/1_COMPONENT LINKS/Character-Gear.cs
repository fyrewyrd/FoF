using Mirror;
using UnityEngine;

public partial class CharacterSheet : NetworkBehaviour
{
    //[Header(" [ GEAR ] ")]
    [Tooltip("Equipped Items")]
    [SerializeField] Gear _gear;
    public Gear gear
    {
        get
        {
            if (_gear == null) _gear = gameObject.GetComponent<Gear>();
            if (_gear == null)
            {
                _gear = gameObject.AddComponent<Gear>();
#if UNITY_EDITOR
                Debug.Log(_gear.name + "GEAR" + ": Component added automatically...\nIt might improve performance to add this component manually in the editor.");
#endif
            }
            return _gear;
        }
        set { _gear = value; }
    }
}