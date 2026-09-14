using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class NavMeshServerCheck : NetworkBehaviour
{
    public override void OnStartServer()
    {
        // Check 1: Are any surfaces registered?
        Debug.Log($"[NavMesh] Active surfaces count: {NavMeshSurface.activeSurfaces.Count}");

        // Check 2: Can we sample a position near the origin / spawn?
        NavMeshHit hit;
        bool found = NavMesh.SamplePosition(Vector3.zero, out hit, 50f, NavMesh.AllAreas);
        Debug.Log($"[NavMesh] SamplePosition near origin succeeded: {found}");

        // Check 3: How many triangles does the NavMesh have?
        var triangulation = NavMesh.CalculateTriangulation();
        Debug.Log($"[NavMesh] Triangle count: {triangulation.indices.Length / 3}");
    }
}