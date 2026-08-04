/*using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(NetworkIdentity))]
public class NetworkProximityGridChecker : NetworkBehaviourNonAlloc
{
    [Header("Visibility Settings")]
    [Tooltip("How far this object can be seen (in meters). Higher = more objects visible at once.")]
    public int visRange = 80;

    [Tooltip("How often (in seconds) this object updates who can see it. 0.5 is recommended.")]
    public float visUpdateInterval = 0.5f;

    [Tooltip("Force this object to be hidden from all players.")]
    public bool forceHidden = false;

    // Static grid shared by all objects
    static Grid2D<NetworkConnectionToClient> grid = new Grid2D<NetworkConnectionToClient>();

    Vector2Int previous = new Vector2Int(int.MaxValue, int.MaxValue);
    float lastUpdateTime;

    Vector2Int ProjectToGrid(Vector3 position)
    {
        return Vector2Int.RoundToInt(new Vector2(position.x, position.z) / (visRange / 3f));
    }

    void Update()
    {
        if (!isServer) return;

        if (connectionToClient != null && connectionToClient.identity == netIdentity)
        {
            Vector2Int current = ProjectToGrid(transform.position);

            if (current != previous)
            {
                if (previous.x != int.MaxValue)
                    grid.Remove(previous, connectionToClient);

                grid.Add(current, connectionToClient);
                previous = current;
            }

            if (Time.time - lastUpdateTime >= visUpdateInterval)
            {
                netIdentity.RebuildObservers(false);
                lastUpdateTime = Time.time;
            }
        }
    }

    void OnDestroy()
    {
        if (isServer && connectionToClient != null && connectionToClient.identity == netIdentity)
        {
            grid.Remove(ProjectToGrid(transform.position), connectionToClient);
        }
    }

    public override bool OnRebuildObservers(HashSet<NetworkConnectionToClient> observers, bool initial)
    {
        if (forceHidden)
            return true;

        Vector2Int current = ProjectToGrid(transform.position);
        grid.GetWithNeighbours(current, observers);

        return true;
    }

    // Host visibility - NO 'override' needed in Mirror 96+
    public void OnSetHostVisibility(bool visible)
    {
        foreach (Renderer rend in GetComponentsInChildren<Renderer>(true))
        {
            rend.enabled = visible;
        }
    }
}
*/