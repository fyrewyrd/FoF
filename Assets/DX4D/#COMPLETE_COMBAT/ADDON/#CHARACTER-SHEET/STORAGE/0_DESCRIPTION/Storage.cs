using UnityEngine;

[DisallowMultipleComponent]
[System.Serializable] public partial class Storage : Mirror.NetworkBehaviour
{
    #region DESCRIPTION
#if UNITY_EDITOR
#pragma warning disable CS0414 //the field is assigned but never used
    [SerializeField][TextArea(1,1)] string componentDescription = 
            "The storage holds valuables securely.";
#endif
    #endregion
}