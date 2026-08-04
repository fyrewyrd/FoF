using System.Text;
using UnityEngine;
using Mirror;

[CreateAssetMenu(menuName="DX4D/ITEM/Field Item", order=0)]
public class FieldItem : UsableItem
{
    [Header("OBJECT TO PLACE")]
    [SyncVar] public GameObject placeInWorld;

    [Header("CARRY THE OBJECT")]
    [SerializeField] public bool heldInHands;
    [SerializeField] public bool attachToUser;
    [SerializeField] public bool flipObject;
    //[SerializeField] public bool aimUpwards = true;

    [Header("USAGE COSTS")]
    [SerializeField] public CastingCost costs;
    [SerializeField] public bool consumeOnUse = true;

    [Header("TOOLTIPS")]
    [SerializeField] public bool automaticTooltip = true;
    [SerializeField, TextArea(1, 30)] public string description;

    // usage
    ///[Server]
    public override void Use(Player player, int inventoryIndex)
    {
        Use(player.GetComponent<PlayerCharacter>(), inventoryIndex);
    }
    public void Use(PlayerCharacter player, int inventoryIndex)
    {
        if (inventoryIndex < 0)
        {
            player.TargetShowTextPopup("[you have no "+ name.ToLower() +"]");
            return;
        }

        if (placeInWorld != null)
        {
            //player.RpcShowTextPopup(placeInWorld.name.ToUpper() + "!!!");
            //player.RpcShowVisualEffect(creates);

#if UNITY_EDITOR
            Debug.Log("<b>[ITEM CREATED]</b> " + player.name + " created " + placeInWorld.name); //DEBUG
#endif
            GameObject go = GameObject.Instantiate<GameObject>(placeInWorld);
            //NetworkManagerMMO.Instantiate<GameObject>(go);
            if (go != null)
            {
                FieldEvent field = go.GetComponent<FieldEvent>();
                if (field)
                {
                    field.SetOwner(player);
                }


                //PLACE AND HOLD THE OBJECT
                if (heldInHands)
                {
                    //if (player.leftHanded && player.leftHandEffectMount)
                    //{
                    //    go.transform.position = player.leftHandEffectMount.position; //POSITION
                    //    go.transform.rotation = player.leftHandEffectMount.rotation; //ROTATE
                    //    if (attachToUser) { go.transform.SetParent(player.leftHandEffectMount); } //HOLD
                    //}
                    //else if (player.rightHandEffectMount)
                    //{
                    //    go.transform.position = player.rightHandEffectMount.position; //POSITION
                    //    go.transform.rotation = player.rightHandEffectMount.rotation; //ROTATE
                    //    if (attachToUser) { go.transform.SetParent(player.rightHandEffectMount); } //HOLD
                    //}
                    if (player.launchOrigin != null)
                    {
                        go.transform.position = player.launchOrigin.position; //POSITION
                        go.transform.rotation = player.launchOrigin.rotation; //ROTATE
                        //go.transform.forward = player.launchOrigin.forward; //FORWARD
                        //go.transform.right = player.launchOrigin.right; //RIGHT
                        //go.transform.up = player.launchOrigin.up; //UP
                        if (attachToUser) { go.transform.SetParent(player.launchOrigin); } //HOLD
                    }
                    else
                    {
                        go.transform.position = player.transform.position; //POSITION
                        go.transform.rotation = player.transform.rotation; //ROTATION
                        //go.transform.forward = player.transform.forward; //FORWARD
                        //go.transform.right = player.transform.right; //RIGHT
                        //go.transform.up = player.transform.up; //UP
                        if (attachToUser) { go.transform.SetParent(player.transform); } //HOLD
                    }
                }
                //else if (player.collider) //TODO: Do we ever need this?
                //{
                //    go.transform.position = player.collider.bounds.center; //PLACE
                //    go.transform.rotation = player.collider.transform.rotation; //PLACE
                //    if (attachToUser) { go.transform.SetParent(player.collider.transform); } //HOLD
                //}
                else
                {
                    go.transform.position = player.transform.position; //POSITION
                    go.transform.rotation = player.transform.rotation; //ROTATION
                    //go.transform.forward = player.transform.forward; //FORWARD
                    //go.transform.right = player.transform.right; //RIGHT
                    //go.transform.up = player.transform.up; //UP
                    if (attachToUser) { go.transform.SetParent(player.transform); } //HOLD
                }

                if (flipObject) go.transform.Rotate(0, 180, 0);//FLIP

                NetworkServer.Spawn(go, player.gameObject); //SPAWN ON NETWORK
            }
            /*
            int skillNumber = player.GetSkillIndexByName(creates.name);

            if (skillNumber > -1)
            {
                player.currentSkill = skillNumber; //NOTE: One of these is probably redundant
            }*/
        }

        if (consumeOnUse)
        {
            ItemSlot inventorySlot = player.INVENTORY[inventoryIndex];
            inventorySlot.DecreaseAmount(1);
            player.INVENTORY[inventoryIndex] = inventorySlot;
        }
    }
    
    ///[Client]
    public virtual void OnUsed(PlayerCharacter player)
    {
        if (placeInWorld != null)
        {
            //USE SKILL
            //player.currentSkill = skillNumber; //NOTE: One of these is probably redundant
            //if (creates != null) GameObject.Instantiate<GameObject>(creates);
            //GameObject go = GameObject.Instantiate<GameObject>(creates);

            //APPLY COSTS
            player.PayCost(costs.me);
            if (player.ACTIVEPET) player.ACTIVEPET.PayCost(costs.pet);
            if (player.ACTIVEMOUNT) player.ACTIVEMOUNT.PayCost(costs.mount);
            //if (cost.life > 0) player.health -= cost.life;
            //if (cost.mana > 0) player.mana -= cost.mana;
            //if (cost.blood > 0) player.blood -= cost.blood;
            //if (cost.spirit > 0) player.spirit -= cost.spirit;
            //if (cost.stamina > 0) player.stamina -= cost.stamina;
            //if (cost.fury > 0) player.fury -= cost.fury;
            //if (cost.experience > 0) player.experience -= cost.experience;

            //if (player.activePet != null)
            //{
            //    if (allyCost.petLife > 0) player.activePet.health -= cost.petLife;
            //    if (cost.petMana > 0) player.activePet.mana -= cost.petMana;
            //    if (cost.petBlood > 0) player.activePet.blood -= cost.petBlood;
            //    if (cost.petSpirit > 0) player.activePet.spirit -= cost.petSpirit;
            //    if (cost.petStamina > 0) player.activePet.stamina -= cost.petStamina;
            //    if (cost.petFury > 0) player.activePet.fury -= cost.petFury;
            //    if (cost.petExperience > 0) player.activePet.experience -= cost.petExperience;
            //}
        }
        else
        {
            player.TargetShowTextPopup("*fizzled*");
#if UNITY_EDITOR
            Debug.LogError(player.name + " can not use " + name + "...you must add it to the Skill Templates of your Player Prefab for this item to work."); //DEBUG
#endif
        }
    }

    // tooltip
    public override string ToolTip()
    {
        StringBuilder tip = new StringBuilder(base.ToolTip());

        if (automaticTooltip)
        {
            tip.Append("<b>{NAME}</b>");
            tip.Append("<b>{DESCRIPTION}</b>");
            tip.Append("{CASTINGCOST}");
            tip.Append("{CREATES}");
            //if (creates != null) { tip.Append("Creates " + creates.name); }
        }
        tip.Replace("{NAME}", name);
        tip.Replace("{DESCRIPTION}", (description != string.Empty) ? "\n" + description : "");
        tip.Replace("{CREATES}", (placeInWorld != null) ? "\n<b><i>Creates</i></b> - " + placeInWorld.name : "");
        tip.Replace("{CASTINGCOST}", Tooltip.CastingCostToolTip(costs));
            /*//PLAYER
            "{HEALTHCOST}" + "{MANACOST}" +
            "{BLOODCOST}" + "{SPIRITCOST}" +
            "{STAMINACOST}" + "{FURYCOST}" +
            "{EXPCOST}" +
            //PET
            "{PETHEALTHCOST}" + "{PETMANACOST}" +
            "{PETBLOODCOST}" + "{PETSPIRITCOST}" +
            "{PETSTAMINACOST}" + "{PETFURYCOST}" +
            "{PETEXPCOST}"// + "{PETSTAMINACOST}"
            );
        //PLAYER
        tip.Replace("{HEALTHCOST}", (cost.life > 0) ? "\n" + "health cost " + cost.life.ToString() : "");
        tip.Replace("{MANACOST}", (cost.mana > 0) ? "\n" + "mana cost " + cost.mana.ToString() : "");
        tip.Replace("{BLOODCOST}", (cost.blood > 0) ? "\n" + "blood cost " + cost.blood.ToString() : "");
        tip.Replace("{SPIRITCOST}", (cost.spirit > 0) ? "\n" + "spirit cost " + cost.spirit.ToString() : "");
        tip.Replace("{STAMINACOST}", (cost.stamina > 0) ? "\n" + "stamina cost " + cost.stamina.ToString() : "");
        tip.Replace("{FURYCOST}", (cost.fury > 0) ? "\n" + "fury cost " + cost.fury.ToString() : "");
        tip.Replace("{EXPCOST}", (cost.experience > 0) ? "\n" + "exp cost " + cost.experience.ToString() : "");
        //PET
        tip.Replace("{PETHEALTHCOST}", (cost.petLife > 0) ? "\n" + "pet health cost " + cost.petLife.ToString() : "");
        tip.Replace("{PETMANACOST}", (cost.petMana > 0) ? "\n" + "pet mana cost " + cost.petMana.ToString() : "");
        tip.Replace("{PETBLOODCOST}", (cost.petBlood > 0) ? "\n" + "pet blood cost " + cost.petBlood.ToString() : "");
        tip.Replace("{PETSPIRITCOST}", (cost.petSpirit > 0) ? "\n" + "pet spirit cost " + cost.petSpirit.ToString() : "");
        tip.Replace("{PETSTAMINACOST}", (cost.petStamina > 0) ? "\n" + "pet stamina cost " + cost.petStamina.ToString() : "");
        tip.Replace("{PETFURYCOST}", (cost.petFury > 0) ? "\n" + "pet fury cost " + cost.petFury.ToString() : "");
        tip.Replace("{PETEXPCOST}", (cost.petExperience > 0) ? "\n" + "pet exp cost " + cost.petExperience.ToString() : "");
        */
        return tip.ToString();
    }
}
