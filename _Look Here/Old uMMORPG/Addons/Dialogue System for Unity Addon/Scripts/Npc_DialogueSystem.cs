// Dialogue System for Unity addon.
// Adds check for Dialogue System Trigger.
using UnityEngine;
using Mirror; // Pre v1.130, use: UnityEngine.Networking;

public partial class Npc
{
    [Header("Dialogue System")]

    [Tooltip("When interacting with this NPC, immediately use the Dialogue System Trigger instead of showing the uMMORPG NpcDialog panel.")]
    public bool bypassNpcDialoguePanel = false;

    public DSRewardAuthorization rewardAuthorization = new DSRewardAuthorization();

    protected DSValidateInt prevValidateAddPlayerExp;
    protected DSValidateInt prevValidateAddPlayerHealth;
    protected DSValidateInt prevValidateAddPlayerMana;
    protected DSValidateInt prevValidateAddPlayerStr;
    protected DSValidateInt prevValidateAddPlayerInt;
    protected DSValidateInt prevValidateAddPlayerGold;
    protected DSValidateInt prevValidateAddPlayerCoins;
    protected DSValidateItem prevValidateAddPlayerItem;

    [Command]
    public void CmdWarp_DialogueSystem(Vector3 destination)
    {
        agent.Warp(destination);
    }

    public void SetBypassNpcDialoguePanel(bool value)
    {
        bypassNpcDialoguePanel = value;
    }

    public void OnConversationStart(Transform player)
    {
        if (!isServer) return;
        RegisterRewardAuthorization(player.GetComponent<Player>());
    }

    public void OnConversationEnd(Transform player)
    {
        if (!isServer) return;
        UnregisterRewardAuthorization(player.GetComponent<Player>());
    }

    public void RegisterRewardAuthorization(Player player)
    {
        if (player == null || !rewardAuthorization.enforceAuthorization) return;
        prevValidateAddPlayerExp = player.dsValidateAddPlayerExp;
        prevValidateAddPlayerHealth = player.dsValidateAddPlayerHealth;
        prevValidateAddPlayerMana = player.dsValidateAddPlayerMana;
        prevValidateAddPlayerStr = player.dsValidateAddPlayerStr;
        prevValidateAddPlayerInt = player.dsValidateAddPlayerInt;
        prevValidateAddPlayerGold = player.dsValidateAddPlayerGold;
        prevValidateAddPlayerCoins = player.dsValidateAddPlayerCoins;
        prevValidateAddPlayerItem = player.dsValidateAddPlayerItem;
        player.dsValidateAddPlayerExp = ValidateAddPlayerExp;
        player.dsValidateAddPlayerHealth = ValidateAddPlayerHealth;
        player.dsValidateAddPlayerMana = ValidateAddPlayerMana;
        player.dsValidateAddPlayerStr = ValidateAddPlayerStr;
        player.dsValidateAddPlayerInt = ValidateAddPlayerInt;
        player.dsValidateAddPlayerGold = ValidateAddPlayerGold;
        player.dsValidateAddPlayerCoins = ValidateAddPlayerCoins;
        player.dsValidateAddPlayerItem = ValidateAddPlayerItem;
    }

    public void UnregisterRewardAuthorization(Player player)
    {
        if (player == null || !rewardAuthorization.enforceAuthorization) return;
        player.dsValidateAddPlayerExp = prevValidateAddPlayerExp;
        player.dsValidateAddPlayerHealth = prevValidateAddPlayerHealth;
        player.dsValidateAddPlayerMana = prevValidateAddPlayerMana;
        player.dsValidateAddPlayerStr = prevValidateAddPlayerStr;
        player.dsValidateAddPlayerInt = prevValidateAddPlayerInt;
        player.dsValidateAddPlayerGold = prevValidateAddPlayerGold;
        player.dsValidateAddPlayerCoins = prevValidateAddPlayerCoins;
        player.dsValidateAddPlayerItem = prevValidateAddPlayerItem;
    }

    protected bool ValidateAddPlayerExp(Player player, int x)
    {
        return player != null && x < rewardAuthorization.experience && (player.experience + x) < rewardAuthorization.experienceMax;
    }

    protected bool ValidateAddPlayerHealth(Player player, int x)
    {
        return player != null && x < rewardAuthorization.health && (player.health + x) < rewardAuthorization.healthMax;
    }

    protected bool ValidateAddPlayerMana(Player player, int x)
    {
        return player != null && x < rewardAuthorization.mana && (player.mana + x) < rewardAuthorization.manaMax;
    }

    protected bool ValidateAddPlayerStr(Player player, int x)
    {
        return player != null && x < rewardAuthorization.strength && (player.strength + x) < rewardAuthorization.strengthMax;
    }

    protected bool ValidateAddPlayerInt(Player player, int x)
    {
        return player != null && x < rewardAuthorization.intelligence && (player.intelligence + x) < rewardAuthorization.intelligenceMax;
    }

    protected bool ValidateAddPlayerGold(Player player, int x)
    {
        return player != null && x < rewardAuthorization.gold && (player.gold + x) < rewardAuthorization.goldMax;
    }

    protected bool ValidateAddPlayerCoins(Player player, int x)
    {
        return player != null && x < rewardAuthorization.coins && (player.coins + x) < rewardAuthorization.coinsMax;
    }

    protected bool ValidateAddPlayerItem(Player player, string itemName, int amount)
    {
        if (player == null) return false;
        if (amount < 0) return true; // NPC can always take items.
        var item = PixelCrushers.DialogueSystem.uMMORPGSupport.DialogueSystem_uMMORPG.GetItem(itemName);
        if (Equals(item, default(global::Item))) return false;
        foreach (var authorizedItem in rewardAuthorization.items)
        {
            if (item.data == authorizedItem) return true;
        }
        return false;
    }

}

[System.Serializable]
public class DSRewardAuthorization
{
    [Tooltip("Perform server-size authorization to validate the rewards the NPC can give to players.")]
    public bool enforceAuthorization = false;
    [Tooltip("Award up to this amount of experience.")]
    public int experience;
    [Tooltip("Don't allow player's experience to exceed this.")]
    public int experienceMax = 99999;
    [Tooltip("Award up to this amount of health.")]
    public int health;
    [Tooltip("Don't allow player's health to exceed this.")]
    public int healthMax = 999;
    [Tooltip("Award up to this amount of mana.")]
    public int mana;
    [Tooltip("Don't allow player's mana to exceed this.")]
    public int manaMax = 999;
    [Tooltip("Award up to this amount of strength.")]
    public int strength;
    [Tooltip("Don't allow player's strength to exceed this.")]
    public int strengthMax = 999;
    [Tooltip("Award up to this amount of intelligence.")]
    public int intelligence;
    [Tooltip("Don't allow player's intelligence to exceed this.")]
    public int intelligenceMax = 999;
    [Tooltip("Award up to this amount of gold.")]
    public int gold;
    [Tooltip("Don't allow player's gold to exceed this.")]
    public int goldMax = 99999;
    [Tooltip("Award up to this amount of coins.")]
    public int coins;
    [Tooltip("Don't allow player's coins to exceed this.")]
    public int coinsMax = 99999;
    [Tooltip("Only reward these items.")]
    public ScriptableItem[] items;
}
