using UnityEngine;

[DisallowMultipleComponent]
[System.Serializable] public partial class Followers : Mirror.NetworkBehaviour
{
    #region DESCRIPTION
#if UNITY_EDITOR
#pragma warning disable CS0414 //the field is assigned but never used
    [SerializeField][TextArea(1,1)] string followersDescription = 
            "Pets and mercenaries under this character's command.";
#endif
    #endregion
}