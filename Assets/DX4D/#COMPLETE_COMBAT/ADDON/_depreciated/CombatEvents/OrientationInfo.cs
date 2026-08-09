/* //DEPRECIATED
using UnityEngine;
using System;

[Serializable] public struct OrientationInfo
{
    public HorizontalLocation horizontal;
    public VerticalLocation vertical;
    public AxialLocation axial;

    public Vector3 AsVector { get { return new Vector3((float)horizontal, (float)vertical, (float)axial); } }
}

public enum HorizontalLocation { Center = 0, Left = -1, Right = 1 }
public enum VerticalLocation { Neutral = 0, Below = -1, Above = 1 }
public enum AxialLocation { Middle = 0, Back = -1, Front = 1 }
*/
