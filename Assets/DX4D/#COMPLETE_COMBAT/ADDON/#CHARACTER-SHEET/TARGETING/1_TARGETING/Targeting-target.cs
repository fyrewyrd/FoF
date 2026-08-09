using Mirror;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public partial class Targeting// : NetworkBehaviour
{
    // T A R G E T I N G

    [Header(" [ TARGETING ] ")]
    //TARGET
    [SyncVar] public GameObject _target;
    public CharacterSheet target
    {
        get
        {
            return (_target) ? _target.GetComponent<CharacterSheet>() : null;
        }
        set { _target = value.gameObject; }
    }
    public void SetTarget(GameObject newTarget)
    {
        _target = newTarget;
    }

    //NEXT TARGET
    [SyncVar] public GameObject _nextTarget;
    public CharacterSheet nextTarget
    {
        get { return (_nextTarget) ? _nextTarget.GetComponent<CharacterSheet>() : null; }
        set { _nextTarget = value.gameObject; }
    }

    [Client] void AutoTarget()
    {
        // find all monsters that are alive, sort by distance
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Monster");
        List<Monster> monsters = objects.Select(go => go.GetComponent<Monster>()).Where(m => m.health > 0).ToList();
        List<Monster> sorted = monsters.OrderBy(m => Vector3.Distance(transform.position, m.transform.position)).ToList();

        // target nearest one
        if (sorted.Count > 0)
        {
            //SetIndicatorViaParent(sorted[0].transform);
            CmdSetTarget(sorted[0].netIdentity);
        }
    }

    [Command]
    public void CmdSetTarget(NetworkIdentity identity)
    {
        // validate
        if (identity != null)
        {
            // can directly change it, or change it after casting?
            if (my.state == ActiveState.IDLE || my.state == ActiveState.MOVING || my.state == ActiveState.STUNNED)
                target = identity.GetComponent<CharacterSheet>();
            else if (my.state == ActiveState.CASTING)
                nextTarget = identity.GetComponent<CharacterSheet>();
        }
    }

    //public void SetIndicatorViaParent(Transform parent)
    //{
        //if (!indicator) indicator = Instantiate(indicatorPrefab);
        //indicator.transform.SetParent(parent, true);
        //indicator.transform.position = parent.position;
    //}
}