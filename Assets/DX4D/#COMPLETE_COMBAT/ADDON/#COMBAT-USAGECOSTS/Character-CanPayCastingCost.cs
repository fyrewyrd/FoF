using Mirror;

public abstract partial class Entity// : NetworkBehaviour
{
    public bool CanPayCastingCost(CastingCost costs)
    {
        return my.CanPayCastingCost(costs);
    }
}

public partial class CharacterSheet : NetworkBehaviour
{
    public bool CanPayCastingCost(CastingCost costs)
    {
        //my cost
        if ( (costs.me.total > 0) && (!CanPayCost(costs.me)) ) return false;

        if (this is PlayerCharacter)
        {
            PlayerCharacter player = (this as PlayerCharacter);
            return
                //pet cost
                (costs.pet.total > 0 && player.ACTIVEPET) ? player.ACTIVEPET.CanPayCost(costs.pet) : true
                //mount cost
                && (costs.mount.total > 0 && player.ACTIVEMOUNT) ? player.ACTIVEMOUNT.CanPayCost(costs.mount) : true;
        }

        return true;
    }

    public bool CanPayCost(PersonalCost cost)
    {
        return (
            //stat pools
            stats.life >= cost.life
            && stats.mana >= cost.mana
            && stats.blood >= cost.blood
            && stats.spirit >= cost.spirit
            && stats.fury >= cost.fury
            && stats.stamina >= cost.stamina
            //resources
            //&& wealth.gold >= costs.wealth.gold
            //&& wealth.gems >= costs.wealth.gems
            //&& stats.experience >= costs.wealth.experience
            );
    }
    //public int lifeCosts { get { return (data is ActiveSkill) ? (data as ActiveSkill).cost.life : 0; } }
    //public int manaCosts { get { return data.manaCosts.Get(level); } } //Already in Skill.cs (ummorpg)

    //public int bloodCosts { get { return (data is ActiveSkill) ? (data as ActiveSkill).cost.blood : 0; } }
    //public int spiritCosts { get { return (data is ActiveSkill) ? (data as ActiveSkill).cost.spirit : 0; } }

    //public int furyCosts { get { return (data is ActiveSkill) ? (data as ActiveSkill).cost.fury : 0; } }
    //public int staminaCosts { get { return (data is ActiveSkill) ? (data as ActiveSkill).cost.stamina : 0; } }
}
