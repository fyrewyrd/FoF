using Mirror;
using UnityEngine;

public partial class CharacterSheet : NetworkBehaviour
{
    //[Header(" [ STAT POOL ] ")]
    [Tooltip("Stat pools such as health and mana that can be used as resources.")]
    [SerializeField] StatPool _stats;
    public StatPool stats
    {
        get
        {
            if (_stats == null) _stats = gameObject.GetComponent<StatPool>();
            if (_stats == null)
            {
                _stats = gameObject.AddComponent<StatPool>();
#if UNITY_EDITOR
                Debug.Log(_stats.name + "STATPOOL" + ": Component added automatically...\nIt might improve performance to add this component manually in the editor.");
#endif
            }
            return _stats;
        }
        set { _stats = value; }
    }
}