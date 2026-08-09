using Mirror;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(ParticleSystem))] //NOT REALLY REQUIRED...the particle system should be attached in this script, not directly on the object
public class VisualEffectsData : NetworkBehaviour
{
    [SerializeField] public float effectLifetime = 300f; //DESTROY TRIGGERED EFFECTS AFTER 5 MINUTES (This is to prevent memory leaks)

    //ACTIVE VISUAL EFFECTS
    [SerializeField] [HideInInspector] List<string> _activeVisualEffects;
    public List<string> activeVisualEffects
    {
        get
        {
            if (_activeVisualEffects == null) _activeVisualEffects = new List<string>();
            return _activeVisualEffects;
        }
        set
        {
            _activeVisualEffects = value;
        }
    }

    //ACTIVE PARTICLES
    [SerializeField] [HideInInspector] List<GameObject> _activeParticles = new List<GameObject>();
    public List<GameObject> activeParticles
    {
        get
        {
            return _activeParticles;
        }
        set
        {
            _activeParticles = value;
        }
    }

    [SerializeField] [HideInInspector] public GameObject spawnedParticleInstance;
    [SerializeField] [HideInInspector] public ParticleSystem particles;
    [SerializeField] [HideInInspector] public Transform lastTarget;
}
