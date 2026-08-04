using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class DurabilityManager : NetworkBehaviour
{
    [SerializeField] Dictionary<int, int> breakableItems = new Dictionary<int, int>();

    public int GetDurability(CombatGear gear)
    {
        int hashcode = gear.GetHashCode();
        int returnVariable = 1;
        if(!breakableItems.ContainsKey(hashcode))
        {
            //ADD
            returnVariable = gear.durability;
            breakableItems.Add(hashcode, returnVariable);
        }
        else
        {
            breakableItems.TryGetValue(hashcode, out returnVariable);
        }
        return returnVariable;
    }
    private void Awake()
    {
        //breakableItems.Clear();
    }

    [Header("DURABILITY")]
    [Tooltip("Durability is reduced by decayRate every attack")]
    [SerializeField]
    public int decayRate = 1;

    [SerializeField] public int max = 1000;
    [SyncVar] private int _current = 1000; //TODO: This was effecting all items instead of the individual. We need a way to make it a [SyncVar]
    public int current { get { return _current; } set { _current = Mathf.Clamp(value, 0, max); } }
}
