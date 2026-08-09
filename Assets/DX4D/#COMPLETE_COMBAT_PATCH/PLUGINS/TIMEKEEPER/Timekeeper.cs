using Mirror;

public class Timekeeper : NetworkBehaviour
{
    // All Times are In Seconds
    public static float syncStartDelay = 0.0f;
    public static float syncTimeInterval = 1.0f;

    public static double currentTime;
    public static double GetTime()
    {
        return currentTime;
    }

    //SYNC TIME ON INTERVAL
    private void OnServerInitialized()
    {
        InvokeRepeating("SyncTime", syncStartDelay, syncTimeInterval);
    }
    //SYNC TIME
    [ClientRpc]
    void RpcSyncTime(double newTime)
    {
        currentTime = newTime;
    }
    [Server]
    void SyncTime()
    {
        RpcSyncTime(GetServerUpTime());
    }
    //GET SERVER UPTIME
    [Server]
    public double GetServerUpTime()
    {
        return NetworkTime.time;
    }
}
