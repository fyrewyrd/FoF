using Mirror;
using SQLite; // from https://github.com/praeclarum/sqlite-net
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public enum itemType { none, Armor, Weapon, Potion, Ammo, Resources, Jewelry, SpecialTool, Certificate, MountsArmor, MountsWeapon, DungeonKey, Gems, Rune, Bag }
public enum itemSubType {
    none,
    //armor
    Head, Shoulders, Chest, Hands, Legs, Feet,

    //weapon
    Shield, Sword, Bow, Spear, Axe, Crossbow, Staff, Wand,

    //potion
    health, mana, stamina, mounts,

    //ammo

    //resources

    //jewelry
    Amulet, Ring,

    //specialTool
    pickaxe, rock, ore,

    //certificate

    //mountsArmor
    Blinkers, Saddle, Stirrup, Horseshoes, MountArmor

    //mountsWeapon

    //dungeonKey

    //gems

    //rune
}
public enum SoundsSystem { none, viaIndividualSounds, viaAddonMenu }
public enum MonsterType { normal, expert, champions, elite, hero, pitBosses, securityMobs }
public enum BuffsType { Normal, Doping, Single, Double, Totem, Exp, Health, Mana, Stamina, Damage, Defense, Crit, Block, Dodge, Accuracy }
public enum LootSystem { original, viaLoots }
public enum resourceType : byte { none, wood, ore, meat, plants, water, blood, fish, fur }
public enum specialTool : byte { none, Axe, Hoe, Butchering_Knife, Tanning_Knife, Pickaxe, Syringe, Shovel, Fishing_Rod }
public enum LoginError { none, serverNotReady, ipAddressIsBlock, outdated, invalidAccountOrPassword, banned, alreadyLoggedIn };


public partial class ScriptableItem
{
    //for Upgrade, Auction, GFF ToolTips, Gathering
    [Header("Item Category")]
    public itemType itemType;
    public itemSubType subType;
}

public static class GFFUtils
{
    public static void BalancePrefabss(GameObject prefab, int amount, Transform parent)
    {
        // instantiate until amount
        for (int i = 0; i < amount; ++i)
        {
            var go = GameObject.Instantiate(prefab);
            go.transform.SetParent(parent.GetChild(i).transform, false);
        }

        // delete everything that's too much
        // (backwards loop because Destroy changes childCount)
        for (int i = 0; i < amount; ++i)
            if (parent.GetChild(i).transform.childCount > 1)
                GameObject.Destroy(parent.GetChild(i).transform.GetChild(1).gameObject);
    }
    
    public static Player FindPlayerByName(string name)
    {
        if (Player.onlinePlayers.ContainsKey(name))
        {
            return Player.onlinePlayers[name].GetComponent<Player>();
        }
        else return null;
    }

    //colors
    static string DecToHex(int value)
    {
        return value.ToString("X2");
    }
    static string FloatNormToHex(float value)
    {
        return DecToHex(Mathf.RoundToInt(value * 255f));
    }
    public static string GetStringFromColor(Color color)
    {
        string red = FloatNormToHex(color.r);
        string green = FloatNormToHex(color.g);
        string blue = FloatNormToHex(color.b);

        return red + green + blue;       
    }


    // invoke multiple functions by prefix via reflection.
    // -> works for static classes too if object = null
    // -> cache it so it's fast enough for Update calls
    static Dictionary<KeyValuePair<Type, string>, MethodInfo[]> check = new Dictionary<KeyValuePair<Type, string>, MethodInfo[]>();
    public static MethodInfo[] GetMethodsByPrefix(Type type, string methodPrefix)
    {
        KeyValuePair<Type, string> key = new KeyValuePair<Type, string>(type, methodPrefix);
        if (!check.ContainsKey(key))
        {
            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)
                                       .Where(m => m.Name.StartsWith(methodPrefix))
                                       .ToArray();
            check[key] = methods;
        }
        return check[key];
    }

    public static bool InvokeBool(Type type, object onObject, string methodPrefix, params object[] args)
    {
        foreach (MethodInfo method in GetMethodsByPrefix(type, methodPrefix))
        {
            return (bool)method.Invoke(onObject, args);
        }

        return false;
    }
    public static int InvokeManyInt(Type type, object onObject, string methodPrefix, params object[] args)
    {
        int bonus = 0;
        foreach (MethodInfo method in GetMethodsByPrefix(type, methodPrefix))
        {
            bonus += (int)method.Invoke(onObject, args);
        }
        return bonus;
    }
    public static int InvokeManyIntWithValue(Type type, object onObject, string methodPrefix, params object[] args)
    {
        int bonus = (int)args[0];
        foreach (MethodInfo method in GetMethodsByPrefix(type, methodPrefix))
        {
            bonus += (int)method.Invoke(onObject, args);
        }
        return bonus;
    }
    public static float InvokeManyFloat(Type type, object onObject, string methodPrefix, params object[] args)
    {
        float bonus = 0;
        foreach (MethodInfo method in GetMethodsByPrefix(type, methodPrefix))
        {
            bonus += (float)method.Invoke(onObject, args);
        }

        return bonus;
    }
    public static float InvokeManyFloatWithValue(Type type, object onObject, string methodPrefix, params object[] args)
    {
        float value = (float)args[0];
        float temp = value;
        foreach (MethodInfo method in GetMethodsByPrefix(type, methodPrefix))
        {
            if ((float)method.Invoke(onObject, args) < temp) temp = (float)method.Invoke(onObject, args);
        }

        return temp;
    }
    public static Item InvokeManyItem(Type type, object onObject, string methodPrefix, params object[] args)
    {
        Item item = (Item)args[0];
        foreach (MethodInfo method in GetMethodsByPrefix(type, methodPrefix))
        {
            item = (Item)method.Invoke(onObject, args);
            args[0] = item;
        }

        return item;
    }

    public static LootSystem lootSystem = LootSystem.original;

    public static int ToInt(this bool Value)
    {
        return Value ? 1 : 0;
    }
    public static bool ToBool(this int Value)
    {
        return Value == 1 ? true : false;
    }

    public static void AddEventTrigger(this EventTrigger eventTrigger, UnityAction<BaseEventData> action, EventTriggerType triggerType)
    {
        EventTrigger.TriggerEvent trigger = new EventTrigger.TriggerEvent();
        trigger.AddListener(action);

        EventTrigger.Entry entry = new EventTrigger.Entry { callback = trigger, eventID = (UnityEngine.EventSystems.EventTriggerType)triggerType };
        eventTrigger.triggers.Add(entry);
    }
}

/*public partial class Entity
{
    //for addons (Totem Buff, Chaos Potion)
    [Command]public void CmdRemoveBuff(Buff buff)
    {
        RemoveBuff(buff);
    }
    public void RemoveBuff(Buff buff)
    {
        // reset if already in buffs list, otherwise add
        int index = GetBuffIndexByName(buff.name);
        if (index != -1) buffs.RemoveAt(index);
    }
}*/

public partial class Player
{
    //for addons: Mounts Extended
    public int FindFreeInventorySlot()
    {
        for (int i = 0; i < inventorySize; ++i)
            if (inventory[i].amount == 0) return i;
        return -1;
    }

    //for addons: move speed, stamina, mounts extended, GameMaster
    float updateMoveSpeed()
    {
        float bonus = 0;
        float current = 0;

        //the player is sitting on the mount
        if (activeMount != null && activeMount.health > 0)
        {
            current = activeMount.speed;

            // addon system hooks (decrease if stamina end, end weight is is max)
            current = GFFUtils.InvokeManyFloatWithValue(typeof(Mount), activeMount, "MountSpeedUpdate_", current);
        }
        else
        {
            current = base.speed;

            // addon system hooks (decrease if stamina end, end if weight is max)
            current = GFFUtils.InvokeManyFloatWithValue(typeof(Player), this, "PlayerSpeedUpdate_", current);

            // addon system hooks(stamina, Game Master)
            bonus = GFFUtils.InvokeManyFloat(typeof(Player), this, "PlayerSpeedBonus_");

            current += bonus;
        }

        return current;
    }

    //AutoAction, Gathering, TargetPanel
    [Client] public void GffAutoMoveTo(Vector3 destination)
    {
        agent.stoppingDistance = 1.3f;
        agent.destination = destination;
    }

    [TargetRpc] // only send to one client
    public void TargetSendMessageToChat(string text)
    {
        chat.AddMsgInfo(text);
    }

    [Header("GFF GameMaster Extended")]
    public PlayerGameMasterToolExtended gameMasterToolExtended;
}

public partial class Monster
{
    [Header("GFF Loot addon & Drop Bonuses")]
    float bonusPercent = 0f;

    [SyncVar, HideInInspector] public GameObject _killedBy;
    public Entity killedBy
    {
        get { return _killedBy != null ? _killedBy.GetComponent<Entity>() : null; }
        set { _killedBy = value != null ? value.gameObject : null; }
    }
}

public partial class NetworkManagerMMO
{
    [Header("Settings for GFF Addons")]
    //for addons : LoginExtended, GameControlPanel
    public int accountMinLength = 4;
    public int accountMaxLength = 16;
    public int passwordMinLength = 4;
}

public partial struct BannedAccounts
{
    public string startDate;
    public string endDate;
    public string account;
    public string bannedBy;
    public string reasonBan;
}

public class banned_accounts
{
    [PrimaryKey] // important for performance: O(log n) instead of O(n)
    public string account { get; set; }
    public int failedLogin { get; set; }
    public DateTime startDate { get; set; }
    public DateTime endDate { get; set; }
    public string bannedBy { get; set; }
    public string reason { get; set; }
}
public class banned_ip
{
    [PrimaryKey, AutoIncrement]
    public int id { get; set; }
    public uint ipStart { get; set; }
    public uint ipEnd { get; set; }
    public string description { get; set; }
    public bool isActive { get; set; }
}

public partial class UICharacterCreationExtended
{

}

public partial class Database
{
    character_inventory InvokeManySaveCharacterInventory(Type type, object onObject, string methodPrefix, params object[] args)
    {
        character_inventory temp = (character_inventory)args[0];
        foreach (MethodInfo method in GFFUtils.GetMethodsByPrefix(type, methodPrefix))
        {
            temp = (character_inventory)method.Invoke(onObject, args);
            args[0] = temp;
        }

        return temp;
    }
    character_equipment InvokeManySaveCharacterEquipment(Type type, object onObject, string methodPrefix, params object[] args)
    {
        character_equipment temp = (character_equipment)args[0];
        foreach (MethodInfo method in GFFUtils.GetMethodsByPrefix(type, methodPrefix))
        {
            temp = (character_equipment)method.Invoke(onObject, args);
            args[0] = temp;
        }

        return temp;
    }
}

public partial class PlayerGameMasterToolExtended
{
    [Header("Settings")]
    public bool immortality = false;
    public bool invisibility = false;
    public bool superSpeed = false;
    public bool killingWithOneHit = false;
}

public partial class UILocalizationText { }
