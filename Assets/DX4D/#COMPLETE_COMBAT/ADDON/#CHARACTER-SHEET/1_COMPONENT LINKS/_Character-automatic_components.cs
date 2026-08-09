#define automatic_components
#define add_missing_components
using Mirror;
using UnityEngine;

public partial class CharacterSheet : NetworkBehaviour
{
#if automatic_components
    private void OnValidate()
    {
        if (!_ai) _ai = GetComponent<AI>(); //AI
        if (!_target) _target = GetComponent<Targeting>(); //TARGETING
        if (!_movement) _movement = GetComponent<Movement>(); //MOVEMENT

        if (!_items) _items = GetComponent<Backpack>(); //ITEMS
        if (!_gear) _gear = GetComponent<Gear>(); //EQUIPMENT
        if (!_storage) _storage = GetComponent<Storage>(); //STORAGE
        if (!_wealth) _wealth = GetComponent<Wealth>(); //WEALTH
        if (!_followers) _followers = GetComponent<Followers>(); //FOLLOWERS
        
        if (!_skills) _skills = GetComponent<Skills>(); //SKILLS
        if (!_combat) _combat = GetComponent<CombatStats>(); //COMBAT
        if (!_resists) _resists = GetComponent<Resistances>(); //RESISTANCES
        if (!_weakness) _weakness = GetComponent<Weaknesses>(); //WEAKNESSES

        if (!_stats) _stats = GetComponent<StatPool>(); //STATS
    }
#endif
}