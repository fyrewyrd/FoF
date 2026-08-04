using Mirror;
using UnityEngine;

public partial class CharacterSheet : NetworkBehaviour
{
    //[Header(" [ FOLLOWERS ] ")]
    [Tooltip("Pets, mounts, mercenaries, etc.")]
    [SerializeField] Followers _followers;
    public Followers follower
    {
        get
        {
            if (_followers == null) _followers = gameObject.GetComponent<Followers>();
            if (_followers == null)
            {
                _followers = gameObject.AddComponent<Followers>();
#if UNITY_EDITOR
                Debug.Log("FOLLOWERS" + ": Component added automatically...\nIt might improve performance to add this component manually in the editor.");
#endif
            }
            return _followers;
        }
        set { _followers = value; }
    }
}