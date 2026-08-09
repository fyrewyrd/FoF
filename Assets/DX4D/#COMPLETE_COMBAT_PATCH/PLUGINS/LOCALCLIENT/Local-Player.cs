//NOTE: UNCOMMENT THE LINE BELOW IF YOU ARE USING A UMMORPG VERSION PREVIOUS TO v148
//#define PRE148

public partial class Local
{
    public static Player player
    {
#if PRE148 //ummorpg v147 or less
        get { return Utils.ClientLocalPlayer(); }
#else //ummorpg v148+
        get { return Player.localPlayer; }
#endif
    }
}
