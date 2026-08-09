using System;
using UnityEngine;

/// <summary>
/// Patch Status Info stores info about a patches registration and activation status
/// </summary>
[Serializable] public struct PatchStatusInfo
{
    ///
    public PatchStatusInfo(bool register, bool activate)
    {
        registered = register;
        activated = activate;
    }

    ///
    [SerializeField] public bool registered;

    ///
    [SerializeField] public bool activated;
}
