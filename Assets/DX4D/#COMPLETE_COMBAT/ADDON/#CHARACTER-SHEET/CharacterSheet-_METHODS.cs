using Mirror;

public partial class CharacterSheet : NetworkBehaviour
{
    public override void OnStartServer()
    {
        // health recovery every second
        InvokeRepeating(nameof(Recover), 1, 1);

        if (IsDead) state = ActiveState.DEAD;
        //if (health == 0) _oldstate = "DEAD";

        // addon system hooks
        Utils.InvokeMany(typeof(CharacterSheet), this, "OnStartServer_");
    }
}