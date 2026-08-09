using Mirror;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Bombable : NetworkBehaviour
{
    [SyncVar] public int durability = 1;

    [SerializeField] public GameObject brokenItem;

    private void OnEnable()
    {
        if (brokenItem != null) GameObject.Destroy(brokenItem);
    }

    [Server]
    public void Explode()
    {
        RpcExplode();
        Disable();
    }

    [ClientRpc]
    public void RpcExplode()
    {
        SpawnBrokenItem();
    }

    [Server]
    private void Disable()
    {
        durability--;
        if (durability <= 0) gameObject.SetActive(false);
        //Collider collider = GetComponent<Collider>();
        //if (collider != null) collider.enabled = false;
        //gameObject.SetActive(false);
    }

    [Client]
    private void SpawnBrokenItem()
    {
        if (brokenItem != null) Instantiate<GameObject>(brokenItem);// GameObject.Destroy(gameObject);
    }
    
    [Server]
    void DenyPassage(CharacterSheet character)
    {
        //character.ResetMovement();
        character.Warp(character.transform.position - (character.transform.forward * 0.3f));
    }

    void OnTriggerEnter(Collider co)
    {
        if (durability > 0)
        {
            CharacterSheet player = co.GetComponentInParent<CharacterSheet>();

            if (player != null && player.isServer) DenyPassage(player);
        }
    }
}
/*
[RequireComponent(typeof(Collider))]
public class Portal : MonoBehaviour
{
    public int requiredLevel = 1;
    public Transform destination;

    void OnPortal(Player player)
    {
        if (destination != null)
            player.Warp(destination.position);
    }

    void OnTriggerEnter(Collider co)
    {
        // collider might be in player's bone structure. look in parents.
        Player player = co.GetComponentInParent<Player>();
        if (player != null)
        {
            // required level?
            if (player.level >= requiredLevel)
            {
                // server? then enter the portal
                if (player.isServer)
                    OnPortal(player);
            }
            else
            {
                // client? then show info message directly. no need to send it
                // from the server to the client via TargetRpc.
                if (player.isClient)
                    player.chat.AddMsgInfo("Portal requires level " + requiredLevel);
            }
        }
    }
}
 */
