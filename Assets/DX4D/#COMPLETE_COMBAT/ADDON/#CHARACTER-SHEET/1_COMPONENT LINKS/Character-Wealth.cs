using Mirror;
using UnityEngine;

public partial class CharacterSheet : NetworkBehaviour
{
    //[Header(" [ WEALTH ] ")]
    [Tooltip("Collections of things such as Gold and Gems that can be used as a resource.")]
    [SerializeField] Wealth _wealth;
    public Wealth wealth
    {
        get
        {
            if (_wealth == null) _wealth = gameObject.GetComponent<Wealth>();
            if (_wealth == null)
            {
                _wealth = gameObject.AddComponent<Wealth>();
#if UNITY_EDITOR
                Debug.Log(_wealth.name + ":WEALTH" + ": Component added automatically...\nIt might improve performance to add this component manually in the editor.");
#endif
            }
            return _wealth;
        }
        set { _wealth = value; }
    }
}