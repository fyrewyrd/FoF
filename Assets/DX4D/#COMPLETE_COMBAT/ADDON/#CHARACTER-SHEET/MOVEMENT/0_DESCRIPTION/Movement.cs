using UnityEngine;

[DisallowMultipleComponent]
[System.Serializable] public partial class Movement : Mirror.NetworkBehaviour
{
    #region DESCRIPTION
#if UNITY_EDITOR
#pragma warning disable CS0414 //the field is assigned but never used
    [SerializeField]
    [TextArea(1, 2)]
    string componentDescription =
            "A character's movement is controlled by this component.";
#endif
    #endregion
}