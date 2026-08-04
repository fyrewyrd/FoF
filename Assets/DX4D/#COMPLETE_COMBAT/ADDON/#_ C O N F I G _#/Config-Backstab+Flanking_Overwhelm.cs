//#define RPG2D //NOTE: Enable for 2d support, or import the 2D_MODE unitypackage from the zip file
using UnityEngine;

public static partial class BackstabConfig
{
    // B A C K S T A B
#if RPG2D
    public static bool backstabEnabled = false;
#else
    public static bool backstabEnabled = true;
#endif
    [Range(1, 360)] public static int backstabAreaAngle = 45;

    // F L A N K I N G
#if RPG2D
    public static bool flankingEnabled = false;
#else
    public static bool flankingEnabled = true;
#endif
    [Range(1, 360)] public static int flankingAreaAngle = 180;


// O V E R W H E L M
#if RPG2D
    public static bool overwhelmingEnabled = false;
#else
	public static bool overwhelmingEnabled = true;
#endif
	[Range(1, 360)] public static int overwhelmingAreaAngle = 1;

}