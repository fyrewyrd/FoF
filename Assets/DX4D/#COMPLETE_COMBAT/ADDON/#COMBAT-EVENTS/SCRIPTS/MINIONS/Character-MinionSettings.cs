using Mirror;
using UnityEngine;
using System.Collections.Generic;

public partial class CharacterSheet : NetworkBehaviour
{
    [Header("MINIONS")]
    [SerializeField] public int maxMinions = 99;
    [SerializeField] public bool minionsDieWithMe = true;
    [SerializeField] public bool noMinionCorpses = false;
    [SerializeField] public List<CharacterSheet> spawnedMinions = new List<CharacterSheet>();

    [Server]
    protected void OnDeath_ResetSpawnedAllies()
    {
        if (minionsDieWithMe)
        {
            foreach (CharacterSheet minion in spawnedMinions)
            {
                if (noMinionCorpses) { Destroy(minion.gameObject); }
                else { minion.Die(); } //TODO: Make sure this works in Server mode
            }
        }
        spawnedMinions.Clear();  //TODO: Leaving this here could lead to ungodly amounts of summoned allies if nobody cleans house...
                                 //  Think about adding AI that makes enemies fight when they get bored. ;)
                                 //  A DestroyAfter Component set to around 300 (5 mins) is recommended for all minions to solve this.
    }
}
