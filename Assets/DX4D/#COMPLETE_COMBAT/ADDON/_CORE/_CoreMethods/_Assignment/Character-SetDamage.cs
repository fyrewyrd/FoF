using Mirror;

public partial class CharacterSheet : NetworkBehaviour
{
    // S E T  E N T I T Y  D A M A G E
    [Server] public void SetDamage(MethodOfDamage method, Element element)
    {
        SetDamageMethod(method);
        SetDamageElement(element);
    }
    [Server] public void SetDamageMethod(MethodOfDamage method)
    {
        combat.damageMethod = method;
    }
    [Server] public void SetDamageElement(Element element)
    {
        combat.damageElement = element;
    }
}
