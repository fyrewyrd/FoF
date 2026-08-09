using Mirror;
using UnityEngine;

public abstract partial class Entity// : NetworkBehaviour
{
    public Resistances resists { get { return my.resists; } }
}

public partial class CharacterSheet : NetworkBehaviour
{
    //[Header(" [ RESISTANCES ] ")]
    [Tooltip("Resistances to various types of damage.")]
    [SerializeField] public Resistances _resists;
    public Resistances resists
    {
        get
        {
            if (_resists == null) _resists = gameObject.GetComponent<Resistances>();
            if (_resists == null)
            {
                _resists = gameObject.AddComponent<Resistances>();
#if UNITY_EDITOR
                Debug.Log(_weakness.name + ":RESISTANCES" + ": Component added automatically...\nIt might improve performance to add this component manually in the editor.");
#endif
            }
            return _resists;
        }
        set { _resists = value; }
    }
}