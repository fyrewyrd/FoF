using System;
using Mirror;
using UnityEngine;

public partial class AI : NetworkBehaviour
{
    [Header("**not implemented** [ DETECTION ] ")]
    public LayerMask detectableLayers = ~0;
    public float sightRange = 20.0f;
    public float hearingRange = 20.0f;
    public float smellRange = 20.0f;

    public float detectionRadius { get { return Mathf.Max(sightRange, smellRange, hearingRange); } }

}