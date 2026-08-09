#define automatic_components
//#define RPG2D //ENABLE FOR 2D MODE...or import the 2D_MODE unitypackage from the zip file
using Mirror;
using UnityEngine;
using UnityEngine.AI;

public partial class CharacterSheet : NetworkBehaviour
{
    [Header(" [ STATUS EFFECTS ] ")]
    public double stunTimeEnd;


    [Header(" [ PLAYER CHARACTER ] ")]
    [Tooltip("The player component of a character.")]
    [SerializeField] PlayerCharacter _player;

    //public PlayerCharacter actor { get { return player; } set { _player = value; } } //alias
    public PlayerCharacter player
    {
        get
        {
            if (_player == null) _player = gameObject.GetComponent<PlayerCharacter>();
            
 #if automatic_components
            if (_player == null && GetComponent<Player>())
            {
                _player = gameObject.AddComponent<PlayerCharacter>();
#if UNITY_EDITOR
                Debug.Log(_player.name + ":PLAYERCHARACTER" + ": Component added automatically...\nIt might improve performance to add this component manually in the editor.");
#endif
            }
#endif

            return _player;
        }
        set { _player = value; }
    }

    // T A R G E T I N G

    //TARGET
    public CharacterSheet target
    {
        get { return targeting.target; }
        set { targeting.target = value; }
    }

    //NEXT TARGET
    public CharacterSheet nextTarget
    {
        get { return targeting.nextTarget; }
        set { targeting.nextTarget = value; }
    }

    // V F X

    [Header(" [ ANIMATION ] ")]
    //ANIMATOR
#pragma warning restore CS0109 // member does not hide accessible member
    [SerializeField] public Animator _animator;
    public Animator animator {
        get { if (!_animator) { _animator = GetComponent<Animator>(); } return _animator; }
        set { _animator = value; }
    }
    [Header(" [ AUDIO ] ")]
    //AUDIO
    [SerializeField] private AudioSource _audioSource;
    public AudioSource audioSource {
        get { if (!_audioSource) { _audioSource = GetComponent<AudioSource>(); } return _audioSource; }
        set { _audioSource = value; }
    }



    // A I
    //AGENT
#if !RPG2D // 3D
    [SerializeField] private NavMeshAgent _agent;
    public NavMeshAgent agent
    {
        get { if (!_agent) { _agent = GetComponent<NavMeshAgent>(); } return _agent; }
    }
#else // 2D
    [Header("components")]
    [SerializeField] private NavMeshAgent2D _agent;
    public NavMeshAgent2D agent
    {
        get { if (!_agent) { _agent = GetComponent<NavMeshAgent2D>(); } return _agent; }
    }
#endif
    //PROXIMITY
    /*[SerializeField] private NetworkProximityGridChecker _proxchecker;
    public NetworkProximityGridChecker proxchecker
    {
        get { if (!_proxchecker) { _proxchecker = GetComponent<NetworkProximityGridChecker>(); } return _proxchecker; }
        //set { _proxchecker = value; }
    }*/
    //NETWORK NAVMESH AGENT
#if RPG2D // 2D
    [SerializeField] private NetworkNavMeshAgent2D _networkNavMeshAgent;
    public NetworkNavMeshAgent2D networkNavMeshAgent
    {
        get
        {
            if (!_networkNavMeshAgent) { _networkNavMeshAgent = GetComponent<NetworkNavMeshAgent2D>(); }
            return _networkNavMeshAgent;
        }
        set { _networkNavMeshAgent = value; }
    }
#else // 3D
    [SerializeField] private NetworkNavMeshAgent _networkNavMeshAgent;
    public NetworkNavMeshAgent networkNavMeshAgent
    {
        get
        {
            if (!_networkNavMeshAgent) { _networkNavMeshAgent = GetComponent<NetworkNavMeshAgent>(); }
            return _networkNavMeshAgent;
        }
        set { _networkNavMeshAgent = value; }
    }
#endif

    //COLLIDER
#if !RPG2D
#pragma warning disable CS0109 // member does not hide accessible member
    [SerializeField] private Collider _collider;
    new public Collider collider
    {
        get
        {
            if (!_collider)
            {
                _collider = GetComponent<Collider>();
                if (!_collider) _collider = GetComponentInChildren<Collider>();
            }
            return _collider;
        }
        set { _collider = value; }
    }
#else
#pragma warning disable CS0109 // member does not hide accessible member
    [SerializeField] private Collider2D _collider;
    new public Collider2D collider
    {
        get { if (!_collider) { _collider = GetComponent<Collider2D>(); } return _collider; }
        set { _collider = value; }
    }
#endif
    //VISIBILITY
    //[Server] public void Hide() { proxchecker.forceHidden = true; }
    //[Server] public void Show() { proxchecker.forceHidden = false; }
    //public bool IsHidden() => proxchecker.forceHidden;
    //public float VisRange() => NetworkProximityGridChecker.visRange;
    //POSITIONING
    public void LookAtY(Vector3 position)
    {
        transform.LookAt(new Vector3(position.x, transform.position.y, position.z));
    }
    public bool IsMoving() =>
        agent.pathPending ||
        agent.remainingDistance > agent.stoppingDistance ||
#if !RPG2D
        agent.velocity != Vector3.zero;
#else
        agent.velocity != Vector2.zero;
#endif


    // A I  T R I G G E R S

    public virtual void OnAggro(CharacterSheet aggressor) { } //TODO - May be unnecessary
    // death ///////////////////////////////////////////////////////////////////
    // universal OnDeath function that takes care of all the Entity stuff.
    // should be called by inheriting classes' finite state machine on death.
    [Server] protected virtual void OnDeath()
    {
        stats.life = 0; //SET LIFE TO 0 (Syncs Death to the Database)

        // clear movement/buffs/target/cast
        ResetMovement();
        target = null;
        CancelCastSkill(); //currentSkill = -1;

        // clear buffs that shouldn't remain after death
        for (int i = 0; i < BUFFS.Count; ++i)
        {
            if (!BUFFS[i].remainAfterDeath)
            {
                BUFFS.RemoveAt(i);
                --i;
            }
        }
        // addon system hooks
        Utils.InvokeMany(typeof(CharacterSheet), this, "OnDeath_");
    }


    // C O L L I D E R  T R I G G E R S

    bool usingFieldWeapon = false;
    //ENTER COLLIDER
    protected virtual void OnTriggerEnter(Collider col)
    {
        // check if trigger first to avoid GetComponent tests for environment
        if (col.isTrigger)
        {
            //SAFE ZONE
            if (col.GetComponent<SafeZone>()) combat.isAttackable = false;

            //FIELD WEAPON
            if (!usingFieldWeapon && col.GetComponent<FieldWeapon>())
            {
                FieldWeapon weapon = col.GetComponent<FieldWeapon>();
                AttachFieldWeapon(weapon);
                usingFieldWeapon = true;
                Debug.Log(weapon.name.ToUpper() + " ATTACHED");
            }
        }
    }
    //EXIT COLLIDER
    protected virtual void OnTriggerExit(Collider col)
    {
        if (col.isTrigger)
        {
            //SAFE ZONE
            if (col.GetComponent<SafeZone>()) combat.isAttackable = true;

            //FIELD WEAPON
            if (usingFieldWeapon && col.GetComponent<FieldWeapon>())
            {
                FieldWeapon weapon = col.GetComponent<FieldWeapon>();
                DetachFieldWeapon(weapon);
                usingFieldWeapon = false;
                Debug.Log(weapon.name.ToUpper() + " DETACHED");
            }
        }
    }

    //DETACH WEAPON
    /// <summary>Removes all the field weapons from this character.</summary>
    [Server] private void DetachFieldWeapon(FieldWeapon weapon)
    {
        //player.EquipmentRemove(weapon.weapon, 1); //NOTE: This just unequips, we want to delete the old one instead.

        int siegeWeaponIndex = -1;

        for (int i = 0; i < EQUIPMENT.Count; i++)
        {
            if (CheckSiegeWeaponIndex(i)) { siegeWeaponIndex = i; break; }
        }

        if (siegeWeaponIndex > -1)
        {
            EQUIPMENT[siegeWeaponIndex] = new ItemSlot();
        }
    }

    //ATTACH WEAPON
    /// <summary>Attaches a field weapon to this character.</summary>
    [Server] private void AttachFieldWeapon(FieldWeapon weapon)
    {
        int siegeWeaponIndex = -1;

        for (int i = 0; i < EQUIPMENT.Count; i++)
        {
            if (CheckSiegeWeaponIndex(i)) { siegeWeaponIndex = i; break; }
        }

        if (siegeWeaponIndex > -1)
        {
            EQUIPMENT[siegeWeaponIndex] = new ItemSlot(new Item(weapon.weapon), 1);
        }
    }

    //CHECK IF SIEGE WEAPON SLOT
    /// <summary>Checks if the passed in index is a siege weapon</summary>
    [Server]
    private bool CheckSiegeWeaponIndex(int checkIndex)
    {
        return gear.slots[checkIndex].requiredCategory.ToUpper().Contains("SIEGE");
    }
}
        /*
        for (int i = 0; i < gear.slots.Length; i++)
        {
            if (gear.slots[i].requiredCategory.ToUpper().Contains("SIEGE"))
            {

            }
        }*/
        //if (EQUIPMENT[i].amount <= 0)
        //EQUIPMENT.FindIndex(gear.equipment[checkIndex] = new Item(weapon));
        //return false;