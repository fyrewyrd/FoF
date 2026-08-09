using Mirror;
using UnityEngine;

public partial class CharacterSheet : NetworkBehaviour
{
    //[Header(" [ MOVEMENT ] ")]
    [Tooltip("Character Locomotion")]
    [SerializeField] Movement _movement;
    public Movement movement
    {
        get
        {
            if (_movement == null) _movement = gameObject.GetComponent<Movement>();
            if (_movement == null)
            {
                _movement = gameObject.AddComponent<Movement>();
#if UNITY_EDITOR
                Debug.Log(_movement.name + "MOVEMENT" + ": Component added automatically...\nIt might improve performance to add this component manually in the editor.");
#endif
            }
            return _movement;
        }
        set { _movement = value; }
    }
}