//#define SHOW_NETWORK_INFO //NOTE: Enable this to show what side of the client/server line this code is running on.
using Mirror;
using UnityEngine;
using System.Text;


public partial class PlayerCharacter : CharacterSheet
{
    // O R G A N I Z E  E Q U I P P E D  G E A R
    [Server]
    public void OrganizeEquippedGear()
    {
        //RESET GEAR
        ResetWeapons();
        ResetArmor();
        ResetAccessories();

#if UNITY_EDITOR
        StringBuilder log = new StringBuilder("<b>| <color=blue>E Q U I P M E N T  S Y N C</color> |</b>"); //DEBUG
        log.Append(" - [" + netId.ToString() + "]\n <b>" + name.ToUpper() + "</b>"); //DEBUG
#endif

        //EXTRACT COMBAT GEAR FROM REGULAR EQUIPMENT
        Item tempItem;
        foreach (ItemSlot slot in EQUIPMENT)
        {
            if (slot.amount > 0)
            {
                tempItem = slot.item;
                if (tempItem.data != null)// && (tempItem.data is CombatGear))
                {
#if UNITY_EDITOR
                    log.Append("\n    <b>|" + tempItem.data.name + "|</b>"); //DEBUG
#endif

                    //NOTE: Accessory should come first because every Accessory is also CombatArmor.
                    //      We do Armor next because you wear more armor than weapons...saves on type checks that way
                    //      If it ever matters
                    if (tempItem.data is CombatAccessory)
                    {
                        equippedAccessories.Add(tempItem.data as CombatAccessory);

#if UNITY_EDITOR
                        log.Append(" - (accessory)"); //DEBUG
#endif
                    }
                    else if (tempItem.data is CombatArmor)
                    {
                        equippedArmors.Add(tempItem.data as CombatArmor);

#if UNITY_EDITOR
                        log.Append(" - (armor)"); //DEBUG
#endif
                    }
                    else if (tempItem.data is CombatWeapon)
                    {
                        CombatWeapon weapon = (tempItem.data as CombatWeapon);

                        switch (weapon.weaponHand)
                        {
                            /*case WeaponHand.Siege:
                                {
                                    if (!_siegeWeapon)
                                    {
                                        siegeWeapon = weapon;
#if UNITY_EDITOR
                                        log.Append(" - (siege weapon)"); //DEBUG
#endif
                                    }
                                    break;
                                }*/
                            case WeaponHand.Unarmed:
                                {
                                    //if (!_mainWeapon && !_offhandWeapon)
                                    if (!_unarmedWeapon)
                                    {
                                        unarmedWeapon = weapon;

                                        //castRangeMultiplier = unarmedWeapon.attackRange;
                                        //if (isClient) CmdSetCastRange(unarmedWeapon.attackRange);

#if UNITY_EDITOR
                                        log.Append(" - (unarmed weapon)"); //DEBUG
#endif
                                    }
                                    break;
                                }
                            /* case WeaponHand.OneHanded:
                                {
                                    if (!HasMainWeapon)
                                    {
                                        mainWeapon = weapon;

                                        //castRangeMultiplier = mainWeapon.attackRange;
                                        //if (isClient) CmdSetCastRange(mainWeapon.attackRange);

#if UNITY_EDITOR
                                        log.Append(" - (main weapon)"); //DEBUG
#endif
                                    }
                                    else if (combat.CanDualWield && !HasOffhandWeapon) //TODO: Getting rid of the else here makes 1h weapons equip on two hands...might be useful for a skill
                                    {
                                        offhandWeapon = weapon;

                                        //castRangeMultiplier = offhandWeapon.attackRange;
                                        //if (isClient) CmdSetCastRange(offhandWeapon.attackRange);

#if UNITY_EDITOR
                                        log.Append(" - (offhand weapon)"); //DEBUG
#endif
                                    }

                                    break;   
                                
                                }*/
                            case WeaponHand.LeftHanded:
                                {
                                    if (!HasMainWeapon)
                                    {
                                        mainWeapon = weapon;

                                        //castRangeMultiplier = mainWeapon.attackRange;
                                        //if (isClient) CmdSetCastRange(mainWeapon.attackRange);

#if UNITY_EDITOR
                                        log.Append(" - (main weapon)"); //DEBUG
#endif
                                    }
                                    else if (combat.CanDualWield && !HasOffhandWeapon) //TODO: Getting rid of the else here makes 1h weapons equip on two hands...might be useful for a skill
                                    {
                                        offhandWeapon = weapon;

                                        //castRangeMultiplier = offhandWeapon.attackRange;
                                        //if (isClient) CmdSetCastRange(offhandWeapon.attackRange);

#if UNITY_EDITOR
                                        log.Append(" - (offhand weapon)"); //DEBUG
#endif
                                    }

                                    break;

                                }
                            case WeaponHand.RightHanded:
                                {
                                    if (!HasMainWeapon)
                                    {
                                        mainWeapon = weapon;

                                        //castRangeMultiplier = mainWeapon.attackRange;
                                        //if (isClient) CmdSetCastRange(mainWeapon.attackRange);

#if UNITY_EDITOR
                                        log.Append(" - (main weapon)"); //DEBUG
#endif
                                    }
                                    else if (combat.CanDualWield && !HasOffhandWeapon) //TODO: Getting rid of the else here makes 1h weapons equip on two hands...might be useful for a skill
                                    {
                                        offhandWeapon = weapon;

                                        //castRangeMultiplier = offhandWeapon.attackRange;
                                        //if (isClient) CmdSetCastRange(offhandWeapon.attackRange);

#if UNITY_EDITOR
                                        log.Append(" - (offhand weapon)"); //DEBUG
#endif
                                    }
                                    break;
                                }
                            case WeaponHand.TwoHanded:
                                {
                                    if (!HasMainWeapon)
                                    {
                                        mainWeapon = weapon;

                                        //castRangeMultiplier = mainWeapon.attackRange;
                                        //if (isClient) CmdSetCastRange(mainWeapon.attackRange);

#if UNITY_EDITOR
                                        log.Append(" - (two-handed main weapon)"); //DEBUG
#endif
                                    }
                                    else if (combat.CanDualWieldLargeWeapons && !HasOffhandWeapon)
                                    {
                                        offhandWeapon = weapon;

                                        //castRangeMultiplier = offhandWeapon.attackRange;
                                        //if (isClient) CmdSetCastRange(offhandWeapon.attackRange);

#if UNITY_EDITOR
                                        log.Append(" - (two-handed offhand weapon)"); //DEBUG
#endif
                                    }
                                    break;
                                }
                        }
                    }
                }
            }
        }

#if UNITY_EDITOR
#if SHOW_NETWORK_INFO
        string networkSide = string.Empty;
        if (isClientOnly) networkSide = "[CLIENT]\n";
        else if (isServerOnly) networkSide = "[SERVER]\n";
        else networkSide = "[EDITOR]\n";
        Debug.Log(networkSide + log.ToString()); //DEBUG
#else
        Debug.Log(log.ToString()); //DEBUG
#endif
#endif
    }
}
