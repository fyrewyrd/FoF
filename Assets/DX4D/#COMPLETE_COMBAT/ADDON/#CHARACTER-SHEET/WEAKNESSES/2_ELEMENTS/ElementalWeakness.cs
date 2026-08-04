using Mirror;
using UnityEngine;

public partial class Weaknesses// : NetworkBehaviour
{
    [Header("ELEMENTAL WEAKNESS")]
    public Element weakToElement = Element.Neutral;
    public Element veryWeakToElement = Element.Neutral;
}

public partial class CharacterSheet : NetworkBehaviour
{
    public bool WeakTo(Element element)
    {
        return ((weakness.weakToElement & element) != 0);
    }
    public bool VeryWeakTo(Element element)
    {
        return ((weakness.veryWeakToElement & element) != 0);
    }
}