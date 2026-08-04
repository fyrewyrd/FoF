using UnityEngine;

[DisallowMultipleComponent]
[System.Serializable] public partial class Wealth : Mirror.NetworkBehaviour
{
    #region DESCRIPTION
#if UNITY_EDITOR
#pragma warning disable CS0414 //the field is assigned but never used
    [SerializeField][TextArea(1,5)] string componentDescription = 
            "Your wealth includes all of your currencies as well as experience " +
        "Note: Exp is used as currency in some games...it can also be used to \"purchase\" levels," +
        " like in those games where you have to hit a button to level up.";
#endif
    #endregion
}