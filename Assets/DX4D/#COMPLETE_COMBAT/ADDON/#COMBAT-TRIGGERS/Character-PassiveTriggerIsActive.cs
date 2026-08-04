using Mirror;

// - - - - - - - - - - - - - - -
// T R I G G E R  H E L P E R S
// - - - - - - - - - - - - - - -
public partial class CharacterSheet : NetworkBehaviour
{
    // C O M P A R E  A C T I V E  T R I G G E R S
    [Server] bool TriggerIsActive(PassiveTrigger activeTriggers, PassiveTrigger triggerToCheckFor)
    {
        if ((activeTriggers & PassiveTrigger.None) == PassiveTrigger.None) return false; //NONE TRIGGER TAKES PRIORITY
        return ((activeTriggers & triggerToCheckFor) == triggerToCheckFor);
    }
}
