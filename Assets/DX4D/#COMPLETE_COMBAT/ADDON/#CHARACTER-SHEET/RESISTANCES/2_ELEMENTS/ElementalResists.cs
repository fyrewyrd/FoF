using Mirror;
using UnityEngine;

public partial class Resistances// : NetworkBehaviour
{
    [Header("ELEMENTAL RESISTANCE")]
    public Element resistElement = Element.Neutral;
    public Element negateElement = Element.Neutral;
    public Element absorbElement = Element.Neutral;
    public Element reflectElement = Element.Neutral;
}

public partial class CharacterSheet : NetworkBehaviour
{
    public bool Resists(Element element)
    {
        return ((resists.resistElement & element) != 0);
    }
    public bool Negates(Element element)
    {
        return ((resists.negateElement & element) != 0);
    }
    public bool Absorbs(Element element)
    {
        return ((resists.absorbElement & element) != 0);
    }
    public bool Reflects(Element element)
    {
        return ((resists.reflectElement & element) != 0);
    }
}