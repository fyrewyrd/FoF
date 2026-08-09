using Mirror;
using UnityEngine;

public partial class Resistances// : NetworkBehaviour
{
    [Header("DAMAGE RESISTANCE")]
    public MethodOfDamage resistDamage = MethodOfDamage.NoDamage;
    public MethodOfDamage negateDamage = MethodOfDamage.NoDamage;
    public MethodOfDamage absorbDamage = MethodOfDamage.NoDamage;
    public MethodOfDamage reflectDamage = MethodOfDamage.NoDamage;
}

public partial class CharacterSheet : NetworkBehaviour
{
    public bool Resists(MethodOfDamage method)
    {
        return ((resists.resistDamage & method) != 0);
    }
    public bool Negates(MethodOfDamage method)
    {
        return ((resists.negateDamage & method) != 0);
    }
    public bool Absorbs(MethodOfDamage method)
    {
        return ((resists.absorbDamage & method) != 0);
    }
    public bool Reflects(MethodOfDamage method)
    {
        return ((resists.reflectDamage & method) != 0);
    }
}