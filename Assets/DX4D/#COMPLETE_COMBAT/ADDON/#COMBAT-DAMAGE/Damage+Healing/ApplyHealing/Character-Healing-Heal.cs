//#define ummorpg
using Mirror;

public partial class CharacterSheet : NetworkBehaviour
{
    // - - - - - - - - - - - - -
    // A P P L Y  H E A L I N G
    //[Server] public void ApplyHealing(Entity defender, int amountToHeal)
    //{
        // H E A L T H
    //    Heal(defender, amountToHeal);
    //}

    // H E A L
    [Server] public virtual void Heal(CharacterSheet defender, int amount)
    {
#if ummorpg
        defender.GetComponent<Entity>().health += amount;
#endif
        defender.LIFE += amount;
    }
}
