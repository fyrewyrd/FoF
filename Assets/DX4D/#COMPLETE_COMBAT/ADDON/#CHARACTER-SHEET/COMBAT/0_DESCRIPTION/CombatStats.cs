using UnityEngine;

[DisallowMultipleComponent]
[System.Serializable] public partial class CombatStats : Mirror.NetworkBehaviour
{
    #region DESCRIPTION
#if UNITY_EDITOR
#pragma warning disable CS0414 //the field is assigned but never used
    [SerializeField]
    [TextArea(1, 2)]
    string componentDescription =
            "The combat related stats associated with this character.";
#endif
    #endregion
}