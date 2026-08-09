using UnityEngine;

[DisallowMultipleComponent]
[System.Serializable] public partial class Resistances : Mirror.NetworkBehaviour
{
    #region DESCRIPTION
#if UNITY_EDITOR
#pragma warning disable CS0414 //the field is assigned but never used
    [SerializeField]
    [TextArea(1, 2)]
    string componentDescription =
            "A character's resistances make them less vulnerable to certain elements and damage methods.";
#endif
    #endregion

    public void Reset(MethodOfDamage resetToMethod = MethodOfDamage.NoDamage, Element resetToElement = Element.Neutral)
    {
        ResetDamageMethod(resetToMethod);
        ResetElement(resetToElement);
    }
    public void ResetDamageMethod(MethodOfDamage resetToMethod = MethodOfDamage.NoDamage)
    {
        reflectDamage = resetToMethod;
        negateDamage = resetToMethod;
        absorbDamage = resetToMethod;
        resistDamage = resetToMethod;
    }
    public void ResetElement(Element resetToElement = Element.Neutral)
    {
        reflectElement = resetToElement;
        absorbElement = resetToElement;
        negateElement = resetToElement;
        resistElement = resetToElement;
    }
}