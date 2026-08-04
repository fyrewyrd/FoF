using UnityEngine;

[DisallowMultipleComponent]
[System.Serializable] public partial class StatPool : Mirror.NetworkBehaviour
{
    #region DESCRIPTION
#if UNITY_EDITOR
#pragma warning disable CS0414 //the field is assigned but never used
    [SerializeField]
    [TextArea(1, 4)]
    string componentDescription =
            "Each stat pool can be used as a resource to cast spells or activate abilities. A character will die if a stat pool like life blood or spirit reaches zero. Damage Shield is a special pool that absorbs damage directed at a life pool...it will break for a certain amount of time if it reaches zero, during that time the shield pool will not regenerate.";
#endif
    #endregion
}