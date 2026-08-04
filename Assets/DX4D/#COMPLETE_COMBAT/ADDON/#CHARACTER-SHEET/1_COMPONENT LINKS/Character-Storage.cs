using Mirror;
using UnityEngine;

public partial class CharacterSheet : NetworkBehaviour
{
    //[Header(" [ STORAGE ] ")]
    [Tooltip("Stored Items")]
    [SerializeField] Storage _storage;
    public Storage storage
    {
        get
        {
            if (_storage == null) _storage = gameObject.GetComponent<Storage>();
            if (_storage == null)
            {
                _storage = gameObject.AddComponent<Storage>();
#if UNITY_EDITOR
                Debug.Log(_storage.name + "STORAGE" + ": Component added automatically...\nIt might improve performance to add this component manually in the editor.");
#endif
            }
            return _storage;
        }
        set { _storage = value; }
    }
}