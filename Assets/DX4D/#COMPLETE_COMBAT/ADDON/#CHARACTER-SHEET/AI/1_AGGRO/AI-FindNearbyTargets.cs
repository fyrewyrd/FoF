using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public partial class AI// : NetworkBehaviour
{
    /// <summary>Generates a list of characters within a specified distance.</summary>
    /// <param name="detectionRange"></param>
    /// <returns></returns>
    public List<CharacterSheet> FindAggroTargets(float detectionRange)
    {
        List<CharacterSheet> observers = new List<CharacterSheet>();
        // if force hidden then return without adding any observers.
        //if (forceHidden) return true;
        // always return true when overwriting OnRebuildObservers so that
        // Mirror knows not to use the built in rebuild method.


        // find players within range
#if !RPG2D
        // OverlapSphereNonAlloc array to avoid allocations.
        // -> static so we don't create one per component
        // -> this is worth it because proximity checking happens for just about
        //    every entity on the server!
        // -> should be big enough to work in just about all cases

        Collider[] hitsBuffer3D = new Collider[10000];
        // cast without allocating GC for maximum performance
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, detectionRange, hitsBuffer3D, detectableLayers);
        if (hitCount == hitsBuffer3D.Length) Debug.LogWarning("Character's OverlapSphere test for " + name + " has filled the whole buffer(" + hitsBuffer3D.Length + "). Some results might have been omitted. Consider increasing buffer size.");

        for (int i = 0; i < hitCount; i++)
        {
            Collider hit = hitsBuffer3D[i];
            // collider might be on pelvis, often the NetworkIdentity is in a parent
            // (looks in the object itself and then parents)
            CharacterSheet found = hit.GetComponentInParent<CharacterSheet>();
            // (if an object has a connectionToClient, it is a player)
            if (found != null)// && identity.connectionToClient != null)
            {
                observers.Add(found);
            }
        }
#else
        Collider2D[] hitsBuffer2D = new Collider2D[10000];
        // cast without allocating GC for maximum performance
        int hitCount = Physics2D.OverlapCircleNonAlloc(transform.position, detectionRange, hitsBuffer2D, detectableLayers);
        if (hitCount == hitsBuffer2D.Length) Debug.LogWarning("Character's OverlapCircle test for " + name + " has filled the whole buffer(" + hitsBuffer2D.Length + "). Some results might have been omitted. Consider increasing buffer size.");

        for (int i = 0; i < hitCount; i++)
        {
            Collider2D hit = hitsBuffer2D[i];
            CharacterSheet found = hit.GetComponentInParent<CharacterSheet>();
            if (found != null) // && identity.connectionToClient != null)
            {
                observers.Add(found);
            }
        }
#endif
        // always return true when overwriting OnRebuildObservers so that
        // Mirror knows not to use the built in rebuild method.
        return observers;
    }
    /// <summary>Finds the closest character from a list of characters.</summary>
    /// <param name="objects">A list of characters to search through. Methods like FindAggroTargets() can be passed in here.</param>
    /// <param name="seekRank">Which ranked character to choose, 3 for example would get the third closest.\nChoosing 0 will select randomly!</param>
    /// <returns></returns>
    public CharacterSheet FindClosestCharacter(List<CharacterSheet> objects, int seekRank = 1)
    {
        //List<t> enemies = objects.Select(go => ((go as GameObject).GetComponent<t>()) as List<t>)/*).Where(m => !m.IsDead)*/.ToList();
        List<CharacterSheet> sorted = objects.OrderBy(m => Vector3.Distance(transform.position, (m as CharacterSheet).transform.position)).ToList();

        // target nearest one
        if (sorted.Count > 0)
        {
            return sorted[seekRank - 1];
        }

        return null;//default(CharacterSheet);
    }

}