using UnityEngine;

[DisallowMultipleComponent]
[System.Serializable] public partial class Gear : Mirror.NetworkBehaviour
{
    #region DESCRIPTION
#if UNITY_EDITOR
#pragma warning disable CS0414 //the field is assigned but never used
    [SerializeField][TextArea(1,2)] string componentDescription = 
            "Gear represents the equipment that this character is wearing.";
#endif
    #endregion
}