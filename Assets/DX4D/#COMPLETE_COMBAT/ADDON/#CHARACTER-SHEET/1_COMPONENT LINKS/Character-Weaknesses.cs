using Mirror;
using UnityEngine;

public abstract partial class Entity// : NetworkBehaviour
{
    public Weaknesses weakness {
        get { return my.weakness; } }
}

public partial class CharacterSheet : NetworkBehaviour
{
    //[Header(" [ WEAKNESSES ] ")]
    [Tooltip("Weaknesses to various types of damage.")]
    [SerializeField] public Weaknesses _weakness;
    public Weaknesses weakness
    {
        get
        {
            if (_weakness == null) _weakness = gameObject.GetComponent<Weaknesses>();
            if (_weakness == null)
            {
                _weakness = gameObject.AddComponent<Weaknesses>();
#if UNITY_EDITOR
                Debug.Log(_weakness.name + ":WEAKNESSES" + ": Component added automatically...\nIt might improve performance to add this component manually in the editor.");
#endif
            }
            return _weakness;
        }
        set { _weakness = value; }
    }
}