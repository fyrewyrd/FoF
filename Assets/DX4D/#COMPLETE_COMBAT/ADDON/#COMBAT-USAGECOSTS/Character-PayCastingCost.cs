//#define expanded_currency
using Mirror;

// E N T I T Y
public partial class CharacterSheet : NetworkBehaviour
{
    public void PayCost(Price cost)
    {
        if (cost.gold > 0) GOLD -= cost.gold;
        if (cost.gems > 0) GEMS -= cost.gems;
#if expanded_currency //TODO: Alternative currencies
        if (cost.copper > 0) copperCoins -= cost.copper;
        if (cost.silver > 0) silverCoins -= cost.silver;
        if (cost.platinum > 0) platinumCoins -= cost.platinum;
        if (cost.electrum > 0) electrumCoins -= cost.electrum;
#endif
    }

    public void PayCost(PersonalCost cost)
    {
        if (cost.life > 0) LIFE -= cost.life;
        if (cost.mana > 0) MANA -= cost.mana;
        if (cost.blood > 0) BLOOD -= cost.blood;
        if (cost.spirit > 0) SPIRIT -= cost.spirit;
        if (cost.stamina > 0) STAMINA -= cost.stamina;
        if (cost.fury > 0) FURY -= cost.fury;
        //if (cost.experience > 0) player.experience -= cost.experience;

    }
    //public int lifeCosts { get { return (data is ActiveSkill) ? (data as ActiveSkill).cost.life : 0; } }
    //public int manaCosts { get { return data.manaCosts.Get(level); } } //Already in Skill.cs (ummorpg)

    //public int bloodCosts { get { return (data is ActiveSkill) ? (data as ActiveSkill).cost.blood : 0; } }
    //public int spiritCosts { get { return (data is ActiveSkill) ? (data as ActiveSkill).cost.spirit : 0; } }

    //public int furyCosts { get { return (data is ActiveSkill) ? (data as ActiveSkill).cost.fury : 0; } }
    //public int staminaCosts { get { return (data is ActiveSkill) ? (data as ActiveSkill).cost.stamina : 0; } }
}
