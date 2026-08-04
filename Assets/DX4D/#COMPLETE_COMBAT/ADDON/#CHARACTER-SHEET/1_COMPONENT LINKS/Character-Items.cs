using Mirror;
using UnityEngine;

public partial class CharacterSheet : NetworkBehaviour
{
    //[Header(" [ INVENTORY ] ")]
    [Tooltip("Carried items.")]
    [SerializeField] Backpack _items;
    public Backpack items
    {
        get
        {
            if (_items == null) _items = gameObject.GetComponent<Backpack>();
            if (_items == null)
            {
                _items = gameObject.AddComponent<Backpack>();
#if UNITY_EDITOR
                Debug.Log(_items.name + "ITEMS" + ": Component added automatically...\nIt might improve performance to add this component manually in the editor.");
#endif
            }
            return _items;
        }
        set { _items = value; }
    }
}