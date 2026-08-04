///* //TODO TODO TODO
using Mirror;

public partial class PlayerCharacter : CharacterSheet
{
    [Server] public void DealCombatDamage_TriggerAggro(CharacterSheet defender, DamageInfo damage)
    {
        // D R A W  A G G R O //TODO: Improve Aggro System
        if (!damage.properties.nonAggressive)
        {
            if (defender != null && defender.player != null)
            {
                //PlayerCharacter targetPlayer = defender.player;//.GetComponent<PlayerCharacter>();
                //if (this is PlayerCharacter) ((PlayerCharacter)this).OnDamageDealtToPlayer(targetPlayer);
                defender.player.ai.OnDamagedByOpponent(this, damage.total);
                defender.player.OnAggro(this); //We do this in every case to prevent exploits with archery and spells
            }
            else if (defender != null)
            {
                //CharacterSheet pet = ((CharacterSheet)defender);
                //if (this is CharacterSheet) ((CharacterSheet)this).OnDamageDealtToPet(pet);
                defender.ai.OnDamagedByOpponent(this, damage.total);
                defender.OnAggro(this); //We do this in every case to prevent exploits with archery and spells
            }
            /*else if (defender is CharacterSheet)
            {
                Pet pet = ((Pet)defender);
                if (this is PlayerCharacter) ((PlayerCharacter)this).OnDamageDealtToPet(pet);
                pet.OnDamagedByOpponent(this, damage.total);
                pet.OnAggro(this); //We do this in every case to prevent exploits with archery and spells
            }
            else if (defender.GetComponent<NaturalResource>())
            {
                NaturalResource resource = defender.GetComponent<NaturalResource>();
                //if (this is Player) (this as Player).OnDamageDealtToResource(resource); //Add to Player Skill and Experience
                resource.OnDamagedByOpponent(this, damage.total);
            }
            else if (defender is Monster)
            {
                Monster monster = ((Monster)defender);
                OnDamageDealtToMonster(monster);
                monster.OnDamagedByOpponent(this, damage.total);
                monster.OnAggro(this); //We do this in every case to prevent exploits with archery and spells
            }*/
            else
            {
                defender.ai.OnDamagedByOpponent(this, damage.total); //FALLBACK DAMAGE TRIGGER
                defender.OnAggro(this); //FALLBACK AGGRO
            }

            //PET AGGRO //TODO
            //if (activePet != null && activePet.autoAttack) activePet.OnAggro(defender);

            //PET AGGRO
            //if (this is Player)
            //{
            //    Player player = (this as Player);
            //    if (player.activePet != null && player.activePet.autoAttack) player.activePet.OnAggro(defender);
            //}
        }
    }
}
//*/