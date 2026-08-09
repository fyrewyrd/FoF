using Mirror;
using UnityEngine;
using System.Collections.Generic;

public class FieldEvent : NetworkBehaviour
{
    [Header("FIELD EVENT DATA")]
    [Tooltip("General stats for this field event including damage and positioning information.")]
    [SerializeField] ScriptedFieldEvent field = null;
    [Tooltip("Visual Effects attached to this field event."
        + "\nVisual effects can be any game object you choose.")]
    [SerializeField] ScriptedFieldEventVFX vfx = null;

    [Header("DESTROY BOMBABLE TARGETS")]
    [Tooltip("This field event can destroy bombable targets.")]
    [SerializeField] public bool explosive = false;

    [Header("FIELD EVENT DAMAGE")]
    [Tooltip("This field event will only respond to the layers specified here.")]
    [SerializeField] LayerMask targetableLayers; //NOTE: Set in OnValidate
    [Tooltip("The damage will not effect the owner.")]
    [SerializeField] public bool ownerImmune = false;
    [Tooltip("The damage done each tick by this field event.")]
    [SerializeField] public ScriptedDamage damage = null;

    //TARGET BOMBABLES
    [SerializeField] [HideInInspector] private List<Bombable> _bombableTargets = new List<Bombable>();
    public List<Bombable> targetBombables { get { return _bombableTargets; } set { _bombableTargets = value; } }

    //TARGET CHARACTERS
    [SerializeField] [HideInInspector] private List<CharacterSheet> _currentTargets = new List<CharacterSheet>();
    public List<CharacterSheet> targetCharacters { get { return _currentTargets; } set { _currentTargets = value; } }

    //[SerializeField] [HideInInspector] private GameObject[] _targets;

    //OWNER
    [Tooltip("The owner of this field event\nThe owner is the one accountable for any interactions this event makes (legally etc)")]
    [SerializeField] [HideInInspector] private GameObject _owner = null;
    [SerializeField] [HideInInspector] CharacterSheet _caster;
    public CharacterSheet owner
    {
        get
        {
            if (!_owner) return null; //NO OWNER SET

            //OLD WAY
            //if (_owner.GetComponent<PlayerCharacter>()) return _owner.GetComponent<PlayerCharacter>(); //PLAYER
            //if (_owner.GetComponent<CharacterSheet>()) return _owner.GetComponent<CharacterSheet>(); //NPC PET MONSTER ETC

            if (!_caster) _caster = _owner.GetComponent<PlayerCharacter>(); //PLAYER
            if (!_caster) _caster = _owner.GetComponent<CharacterSheet>(); //NPC PET MONSTER ETC

            return _caster;
        }
    }

    public void SetOwner(GameObject newOwner)
    {
        _owner = newOwner;
    }
    public void SetOwner(CharacterSheet newOwner)
    {
        SetOwner(newOwner.gameObject);
    }
    public void SetOwner(PlayerCharacter newOwner)
    {
        SetOwner(newOwner.gameObject);
    }

    //VALIDATE
    private void OnValidate()
    {
        if (targetableLayers.value == 0)
        {
            targetableLayers = LayerMask.GetMask("Player", "Monster", "Bombable"); //DEFAULT TO MONSTERS & PLAYERS
        }
    }
    //DRAW GIZMOS
    void OnDrawGizmosSelected()
    {
        if (field != null)
        {
            Gizmos.color = new Color(1, 0, 0, 1);
            Gizmos.DrawWireSphere(transform.position, field.range);
            //Gizmos.DrawWireCube(transform.position, new Vector3(field.range * 2, field.range * 2));
        }
    }

    //EFFECT DURATION
    [SerializeField] [HideInInspector] double expirationTime;

    //DEACTIVATE DURATION
    [Tooltip("This field will reactivate at this time (checked against NetworkTime.time)")]
    [SerializeField] [HideInInspector] double reactivationTime;
    [SerializeField] [HideInInspector] bool deactivated = false;

    //TRIGGER
    private bool Trigger()
    {
        FindTargetsInRange(field.range);
        return Trigger(owner, targetBombables) || Trigger(owner, targetCharacters);
    }

    private bool Trigger(CharacterSheet owner, List<Bombable> targets)
    {
        if (explosive)
        {
            if (targets.Count > 0)
            {
                foreach (Bombable target in targets)
                {
                    if (target != null && isServer)
                    {
                        target.Explode();
                    }
                }
            }
        }

        return (targets.Count > 0);
    }
    
    private bool Trigger(CharacterSheet owner, List<CharacterSheet> targets)
    {
        //base.Trigger(owner, targets);

        if (targets.Count > 0)
        {
            //DAMAGE TARGETS
            if (damage != null)
            {
                foreach (CharacterSheet target in targets)
                {
                    if (target != null && isServer)
                    {
                        {
                            if (owner != null)
                            {
                                if (ownerImmune && owner.GetHashCode() == target.GetHashCode()) {  }//owner.gameObject.name == target.gameObject.name) { }
                                else { owner.DealDirectDamage(target, damage.damage); }

                                Debug.Log("<b>" + owner.GetHashCode() + "  " + target.GetHashCode() + "</b>");
                            }
                            else { target.DealDirectDamage(target, damage.damage); }
                        }
                    }
                    //NOTE: Target damages itself if there is no owner.
                    //      Technically jumping into a fire can be considered suicide, so this does make sense.
                }
            }
            #region DEBUG
#if UNITY_EDITOR
            Debug.Log("<b>[FIELD EVENT]</b>\n" + name + " triggered with " + targets.Count + " targets");
#endif
            #endregion
        }
        return (targets.Count > 0);
    }

    //UPDATE TARGETS
    Collider _searchLocation;
    Vector3 _searchStart;
    CharacterSheet _fieldTarget;

    Bombable _bombableTarget;


    Collider[] _targetableColliders;
    public bool FindTargetsInRange(float range)
    {
        if (targetBombables.Count > 0) targetBombables.Clear(); //CLEAR OLD BOMBABLE TARGETS
        if (targetCharacters.Count > 0) targetCharacters.Clear(); //CLEAR OLD CHARACTER TARGETS

        //ASSIGN SEARCH LOCATION
        if (!_searchLocation) _searchLocation = GetComponentInChildren<Collider>(); //GET COLLIDER

        if (!_searchLocation) { _searchStart = transform.position; } //SEARCH FROM FIELD POSITION
        else { _searchStart = _searchLocation.bounds.center; } //SEARCH FROM CENTER OF COLLIDER

        //TODO: If it turns out that CheckSphere performs just as well or better, that would be a better choice.
        //      This should be profiled. This check is meant as an optimization, but it is not necessary to be here.
        //      Remove this check if it's not improving performance. It is only useful if you expect effects to not find targets often.
        //NOTE: This does not work for self-targeted AoE that hits players because it always finds the caster.
        //NOTE: I used a Box here rather than a sphere, this may be less accurate, but this is just an approximation...a box should perform better.
        //if (!Physics.CheckSphere(_searchStart, range, targetableLayers)) { } //ANYBODY AROUND?
        if (!Physics.CheckBox(_searchStart, new Vector3(range, range, range), Quaternion.identity, targetableLayers)) //NOBODY AROUND?
        {
            #region DEBUG
#if UNITY_EDITOR
            Debug.Log("<b>[FIELD EVENT]</b>\n" + name + " did not find any targets...");
#endif
            #endregion
            return false;
        }

        _targetableColliders = Physics.OverlapSphere(_searchStart, range, targetableLayers); //SPHERECAST TARGETS

        //POPULATE TARGETS LIST
        foreach (Collider collision in _targetableColliders)
        {
            if (collision)
            {
                //CHARACTERS
                _fieldTarget = collision.GetComponent<CharacterSheet>();
                if (!_fieldTarget) _fieldTarget = collision.GetComponentInParent<CharacterSheet>();
                if (!_fieldTarget && collision.gameObject.transform.parent != null) _fieldTarget = collision.gameObject.transform.parent.GetComponentInChildren<CharacterSheet>();

                if (_fieldTarget != null)
                {
                    //if (!targets.Contains(_fieldTarget))
                    //{
                    targetCharacters.Add(_fieldTarget);
                    #region DEBUG
#if UNITY_EDITOR
                    Debug.Log("<b>[FIELD EVENT]</b>\n" + name + " interacted with " + _fieldTarget.name);
#endif
                    #endregion
                    //}
                }

                //BOMBABLES
                _bombableTarget = collision.GetComponent<Bombable>();
                if (!_bombableTarget) _bombableTarget = collision.GetComponentInParent<Bombable>();
                if (!_bombableTarget && collision.gameObject.transform.parent != null) _bombableTarget = collision.gameObject.transform.parent.GetComponentInChildren<Bombable>();

                if (_bombableTarget != null)
                {
                    //if (!targets.Contains(_bombableTarget))
                    //{
                    targetBombables.Add(_bombableTarget);
                    #region DEBUG
#if UNITY_EDITOR
                    Debug.Log("<b>[FIELD EVENT]</b>\n" + name + " destroyed " + _bombableTarget.name);
#endif
                    #endregion
                    //}
                }
            }
        }

        return (targetCharacters.Count > 0 || targetBombables.Count > 0);
    }

    //NATIVE METHODS + HOOKS
    //NOTE: Inject Hooks into these methods if you use them
    void Start() { Create(); }
    void FixedUpdate() { }
    void Update() { CheckTimer(); }

    //SPAWN
    private List<GameObject> _spawned = new List<GameObject>();
    private GameObject SpawnVFX(GameObject toSpawn, bool parented)
    {
        if (!toSpawn) return null; //NO VFX

        GameObject go = GameObject.Instantiate<GameObject>(toSpawn);
        if (go != null)
        {
            go.transform.position = transform.position;
            //go.transform.rotation = transform.rotation;
            //go.transform.up = Vector3.up;
            //go.transform.forward = transform.forward;
            //go.transform.forward = new Vector3(transform.forward.x, go.transform.forward.y, transform.forward.z);

            if (parented && field && vfx) { go.transform.SetParent(gameObject.transform, true); } //ATTACH TO FIELD (if it has vfx and a field description to specify position)

            //if (field.) go.transform.up = Vector3.up;
            //go.transform.up = transform.right;
            //go.transform.up = transform.forward;

            if (isServer) NetworkServer.Spawn(go);//, Local.player.connectionToServer);

        }
        //NetworkManagerMMO.Instantiate(toSpawn);
        return go;
    }

    //CREATE
    private void Create()
    {
        if (vfx != null)
        {
            //GameObject spawnedObject =  //SPAWN VISUAL EFFECTS - (VFX)
            //spawnedObject.transform.forward = transform.forward;
            //spawnedObject.transform.position = transform.position;
            _spawned.Add(SpawnVFX(vfx.onCreatedVFX, true)); //REMEMBER SPAWNED OBJECTS (so we can delete them on destroy)

//#if UNITY_EDITOR
            //Debug.Log("<b>[EFFECT FIELD]</b> " + name + " created " + spawnedObject.name); //DEBUG
//#endif
        }
    }

    //EXPIRE
    private void Expire()
    {
        //CLEANUP SPAWNED OBJECTS
        foreach (GameObject spawn in _spawned)
        {
            if (spawn != null) GameObject.Destroy(spawn); //DESTROY SPAWNED
        }
        _spawned.Clear(); //CLEAR SPAWNED LIST

        //SPAWN VFX
        if (vfx != null)
        {
            GameObject spawnedObject = SpawnVFX(vfx.onDestroyedVFX, false); //SPAWN VISUAL EFFECTS - (VFX)
            //_spawned.Add(SpawnVFX(vfx.onDestroyedVFX)); //REMEMBER SPAWNED OBJECTS (so we can delete them on destroy)


            //if (spawnedObject != null) NetworkServer.Spawn(spawnedObject);
            //spawnedObject.transform.forward = transform.forward;
            //spawnedObject.transform.position = transform.position;
        }

        //DEACTIVATE
        if (field.reactivates)
        {
            reactivationTime = (NetworkTime.time + field.reactivateDuration);
            deactivated = true;
            #region DEBUG
#if UNITY_EDITOR
            Debug.Log("<b>[FIELD EVENT]</b>\n" + field.name + " deactivated...will restore in " + field.reactivateDuration + "s");
#endif
            #endregion
        }
        //DESTROY
        else
        {
            GameObject.Destroy(gameObject);
            #region DEBUG
#if UNITY_EDITOR
            Debug.Log("<b>[FIELD EVENT]</b>\n" + name + " destroyed...");
#endif
            #endregion
        }
    }

    //CHECK TIMER
    double nextTickTime;
    private void CheckTimer()
    {
        //if (!Local.player) return; //NOT IN GAME YET

        if (deactivated)
        {
            if (reactivationTime > NetworkTime.time) { return; } //STILL DEACTIVATED
            else { deactivated = false; }; //REACTIVATE
        }

        if (!field) { return; } //NO EFFECT FIELD ASSIGNED //TODO: Debug warning for this

        //INITIALIZE TIMERS
        if (nextTickTime == 0) nextTickTime = (NetworkTime.time + field.tickFrequency);
        if (expirationTime == 0) expirationTime = (NetworkTime.time + field.effectDuration);
        if (reactivationTime == 0) reactivationTime = (NetworkTime.time + field.reactivateDuration);


        if (NetworkTime.time > expirationTime)
        {
            Expire();
        }

        if (NetworkTime.time > nextTickTime)
        {
            if (Trigger())
            {
                if (field.destroyOnTriggered)
                {
                    Expire();
                }
            }

            if (field.destroyOnTick)
            {
                Expire();
            }


            nextTickTime = NetworkTime.time + field.tickFrequency;
        }
    }
}
            /*
#if UNITY_EDITOR
                Debug.Log("<b>[EFFECT FIELD]</b> " + field.name + " expired...");
#endif
            if (field.reactivates)
            {
                reactivationTime = (NetworkTime.time + field.reactivateDuration);
                deactivated = true;
            }
            else
            {
                GameObject.Destroy(gameObject);
            }
            */
        /*
        int i = 0;
        while (i < colliders.Length)
        {
            var character = colliders[i].gameObject.GetComponent<CharacterSheet>();
            if (!character) character = colliders[i].gameObject.GetComponent<PlayerCharacter>();
            if (character != null)
            {
                if (!targets.Contains(character))
                {
                    targets.Add(character);
#if UNITY_EDITOR
                    Debug.Log("<b>[EFFECT FIELD]</b> " + " effected " + character.name);
#endif
                }
            }
            //hitColliders[i].SendMessage("AddDamage");

            i++;
        }

        return (targets.Count > 0);
        */
/*public abstract class DamageField : EffectField
{
    void Start() { }
    void Update() { }
    private void FixedUpdate() { }
}*/
/*

        RaycastHit hit;
        
        if (Physics.SphereCast(owner.gameObject.transform.position, field.range, field.direction, out hit))
        {
            PlayerCharacter player = hit.collider.gameObject.GetComponent<PlayerCharacter>();
            if (player != null)
            {
                if (!targets.Contains(player))
                {
                    targets.Add(player);
                }
            }
            else
            {
                CharacterSheet character = hit.collider.gameObject.GetComponent<CharacterSheet>();
                if (character != null)
                {
                    if (!targets.Contains(character))
                    {
                        targets.Add(character);
#if UNITY_EDITOR
                    Debug.Log("<b>[EFFECT FIELD]</b> " + field.name + " effected " + character.name);
#endif
                    }
                }
            }
        }
*/
