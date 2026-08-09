using Mirror;
using UnityEngine;

public abstract partial class Entity// : NetworkBehaviour
{
    [Command]
    public void CmdSyncMyEquipment()
    {
        character.player.SyncMyEquipment();
    }
}
public partial class PlayerCharacter : CharacterSheet
{
    // S Y N C  C H A R A C T E R  O N  L O G I N
    //public void OnStartLocalPlayer_SyncMyEquipment()
    public void OnStartServer_SyncMyEquipment()
    {
        SyncGear();
    }

    private void Start()
    {
        SyncGear();
    }
    // S Y N C  C H A R A C T E R

    ///[Client]
    public void SyncEquipmentModels()
    {
        for (int i = 0; i < EQUIPMENT.Count; ++i)
        {
            //if (EQUIPMENT[i].item.hash != 0)
            //{
                RefreshLocation(i);
                SwapSkin(i);
            //}
        }
    }

    public void SyncGear()
    {
        if (isClient)
        {
            SyncEquipmentModels();
            //CmdSyncMyEquipment();//TODO: EVAL THIS - WAS://
            if (Local.player != null) Local.player.CmdSyncMyEquipment();
        }
        if (isServer)
        {
            SyncMyEquipment();
            //SyncEquipmentModels();
        }
    }
    ///[Client]
    public void SwapSkin(int index)
    {
        ItemSlot slot = EQUIPMENT[index];
        //EquipmentInfo info = equipmentInfo[index];

        if (slot.amount > 0)
        {
            EquipmentItem itemData = (EquipmentItem)slot.item.data;
            if (itemData.changeToPlayerSkin != null)
            {
                GetComponent<Renderer>().material = itemData.changeToPlayerSkin;
            }
        }
    }

    [Command] public void CmdSyncMyEquipment()
    {
        SyncMyEquipment();
    }
    [Server]
    public void SyncMyEquipment()
    {
        //TODO: Move this somewhere with Character Sheet
        //ADD CHARACTER SHEET TO PLAYER IF THERE IS NONE
        //CharacterSheet characterSheet = GetComponent<CharacterSheet>();
        //if (!characterSheet) { gameObject.AddComponent<CharacterSheet>(); }

        //TODO: CharacterSheet now adds this automatically...this is redundant if you use it
        //ADD DAMAGE SLOT TO PLAYER IF THERE IS NONE
        //DamageEvent damageEvent = GetComponent<DamageEvent>();
        //if (!damageEvent) { gameObject.AddComponent<DamageEvent>(); }

        // O R G A N I Z E
        OrganizeEquippedGear();

        // R E S E T
        resists.Reset(); //+MethodOfDamage.NoDamage //+Element.Neutral (default)
        weakness.Reset(); //+MethodOfDamage.NoDamage //+Element.Neutral (default)
        ResetDamage();

        // S Y N C H R O N I Z E
        //ARMOR
        SyncArmorToCharacter();

        //ACCESSORIES
        SyncAccessoriesToCharacter();

        //WEAPONS
        SyncWeaponsToCharacter();

        //SKILLS
        SyncCurrentSkillToCharacter();
        SyncWeaponSkillsToCharacter();


        //VFX
        SyncVisualEffectsToCharacter(); //TODO: This should be tested to make sure it works across the network and stays in sync...if it doesn't the declaration of this method needs a [Server] tag and an RPC method to call

        //OFFENSIVE GEAR
        //SyncThrownWeaponToCharacter(); //TODO: We will probably depreciate this
    }
}
