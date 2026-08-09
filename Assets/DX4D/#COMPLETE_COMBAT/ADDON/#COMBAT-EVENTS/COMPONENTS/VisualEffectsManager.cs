using Mirror;
using UnityEngine;

[RequireComponent(typeof(VisualEffectsData))]
public class VisualEffectsManager : NetworkBehaviour
{
    [Header("VISUAL EFFECTS DATA")]
    [SerializeField] public VisualEffectsData _data;
    public VisualEffectsData data
    {
        get
        {
            if (!_data)
            {
                _data = GetComponent<VisualEffectsData>();
                if (!_data)
                {
                    _data = gameObject.GetComponentInChildren<VisualEffectsData>(); //NESTED COMPONENTS
                    if (!_data) _data = gameObject.AddComponent<VisualEffectsData>();
                }
            }

            return _data;
        }
        set
        {
            _data = value;
        }
    }

    //TRIGGERED VFX
    public void TriggerVisualEffect(Transform target, GameObject effect)
    {
        if (!effect)
        {
#if UNITY_EDITOR
            Debug.LogWarning("[VFXManager>TriggerVisualEffect] - Triggered without an attached effect.");
#endif
            return;
        } //&& target.IsDead

        //Debug.Log("{" + name.ToUpper() + "} <" + location.name.ToUpper() + "> INVOKING VISUAL EFFECT: " + effect.name.ToUpper() + "!!!");//DEBUG

        //ADD NETWORK ID TO THE EFFECT
        if (!effect.GetComponent<NetworkIdentity>()) effect.AddComponent<NetworkIdentity>();

        //DESTROY TRIGGERED EFFECTS AFTER 5 MINUTES (This is to prevent memory leaks)
        if (!effect.GetComponent<DestroyAfter>())
        {
#if UNITY_EDITOR
            Debug.Log("VFXLAUNCHER - ADDING DESTROY AFTER");
#endif
            effect.AddComponent<DestroyAfter>();
            effect.GetComponent<DestroyAfter>().time = data.effectLifetime;
        }

        //Center the effect in the collider if possible...otherwise just make it the target's position
        Collider col = target.gameObject.GetComponent<Collider>();
        if (!col) col = target.gameObject.GetComponentInChildren<Collider>();
        Vector3 targetLoc = (col != null) ? (col.bounds.center) : (target.position);

        if (data.activeVisualEffects.Contains(effect.name + target.name))
        {
            //particles.transform.Rotate(new Vector3(agent.transform.rotation.x, agent.transform.rotation.y, agent.transform.rotation.z));
            //particles = spawnedParticleInstance.GetComponent<ParticleSystem>();
            if (!data.particles)
            {
#if UNITY_EDITOR
                Debug.LogWarning("No ParticleSystem component found on " + target.name);
#endif
            }

            //REASSIGN TARGET
            if (data.lastTarget != null && data.lastTarget != target)
            {
                data.activeVisualEffects.Remove(effect.name + target.name);
                data.spawnedParticleInstance.transform.SetParent(target);
            }

            if (!data.particles.isPlaying || !data.particles.isEmitting)
            {
                data.particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                data.particles.Play();
            }
        }

        if (!data.activeVisualEffects.Contains(effect.name + target.name))
        {
            //transform.Rotate(0, col.transform.eulerAngles.y, 0); //LOOK AT THE TARGET BEFORE INSTANTIATING PARTICLES
            data.spawnedParticleInstance = Instantiate(effect, targetLoc, Quaternion.Euler(target.rotation.eulerAngles)); //INSTANTIATE THE PARTICLES

            //spawnedParticleInstance = Instantiate(effect, targetLoc, Quaternion.identity);
            //effect.transform.LookAt(transform.right); //AIM EFFECT FORWARD
            //spawnedParticleInstance.transform.Rotate( new Vector3(agent.transform.rotation.x, agent.transform.rotation.y, agent.transform.rotation.z) );
            /* //TODO: Adds compatibility with ummorpg skill effects
            SkillEffect visualEffect = spawnedInstance.GetComponent<SkillEffect>();
            if (!visualEffect) visualEffect = spawnedInstance.GetComponent<OneTimeTargetSkillEffect>();
            if (!visualEffect) { Debug.LogWarning("No SkillEffect component found on " + name); }

            if (visualEffect != null)
            {
                visualEffect.caster = this;
                visualEffect.target = target;
            }
            */

            data.particles = data.spawnedParticleInstance.GetComponent<ParticleSystem>();
            if (!data.particles)
            {
#if UNITY_EDITOR
                Debug.LogWarning("No ParticleSystem component found on " + target.name);
#endif
            }

                if (data.particles != null)
            {
                if (data.spawnedParticleInstance.transform.parent != target)
                {
                    data.spawnedParticleInstance.transform.SetParent(target);//TODO: add weapon mounts
                }
                data.particles.Play(true);
                NetworkServer.Spawn(data.spawnedParticleInstance);
                //activeParticles.Add(spawnedParticleInstance);
                data.lastTarget = target; //ASSIGN LAST TARGET
                data.activeVisualEffects.Add(effect.name + target.name); //We use the effect, not the spawned instance
            }
        }
    }

    //MOUNTED VFX
    public void MountVisualEffect(Transform targetObject, GameObject effect)
    {
        if (!effect) return;// || (target != null && target.IsDead)) return;

        //Debug.Log("{" + name.ToUpper() + "} <" + location.name.ToUpper() + "> INVOKING VISUAL EFFECT: " + effect.name.ToUpper() + "!!!");//DEBUG

        if (!effect.GetComponent<NetworkIdentity>()) effect.AddComponent<NetworkIdentity>();

        //Center the effect in the collider if possible...otherwise just make it the target's position
        Collider col = targetObject.GetComponent<Collider>();
        Vector3 targetLoc = (col != null) ? (col.bounds.center) : (targetObject.transform.position);

        if (data.activeVisualEffects.Contains(effect.name + targetObject.name))
        {
            //particles.transform.Rotate(new Vector3(agent.transform.rotation.x, agent.transform.rotation.y, agent.transform.rotation.z));
            //particles = spawnedParticleInstance.GetComponent<ParticleSystem>();
            if (!data.particles)
            {
#if UNITY_EDITOR
                Debug.LogWarning("No ParticleSystem component found on " + name);
#endif
            }

            //REASSIGN TARGET
            if (data.lastTarget != null && data.lastTarget != targetObject)
            {
                data.activeVisualEffects.Remove(effect.name + targetObject.name);
                data.spawnedParticleInstance.transform.SetParent(targetObject);
            }

            if (!data.particles.isPlaying || !data.particles.isEmitting)
            {
                data.particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                data.particles.Play();
            }
        }

        if (!data.activeVisualEffects.Contains(effect.name + targetObject.name))
        {
            //transform.Rotate(0, col.transform.eulerAngles.y, 0); //LOOK AT THE TARGET BEFORE INSTANTIATING PARTICLES
            data.spawnedParticleInstance = Instantiate(effect, targetLoc, Quaternion.Euler(transform.rotation.eulerAngles)); //INSTANTIATE THE PARTICLES

            //spawnedParticleInstance = Instantiate(effect, targetLoc, Quaternion.identity);
            //effect.transform.LookAt(transform.right); //AIM EFFECT FORWARD
            //spawnedParticleInstance.transform.Rotate( new Vector3(agent.transform.rotation.x, agent.transform.rotation.y, agent.transform.rotation.z) );

            //TODO: Adds compatibility with ummorpg skill effects
            //SkillEffect visualEffect = spawnedInstance.GetComponent<SkillEffect>();
            //if (!visualEffect) visualEffect = spawnedInstance.GetComponent<OneTimeTargetSkillEffect>();
            //if (!visualEffect) { Debug.LogWarning("No SkillEffect component found on " + name); }
            //
            //if (visualEffect != null)
            //{
            //    visualEffect.caster = this;
            //    visualEffect.target = target;
            //}


            data.particles = data.spawnedParticleInstance.GetComponent<ParticleSystem>();
            if (!data.particles)
            {
#if UNITY_EDITOR
                Debug.LogWarning("No ParticleSystem component found on " + targetObject.name);
#endif
            }

            if (data.particles != null)
            {
                if (data.spawnedParticleInstance.transform.parent != targetObject)
                {
                    data.spawnedParticleInstance.transform.SetParent(targetObject);//TODO: add weapon mounts
                }
                data.particles.Play(true);
                NetworkServer.Spawn(data.spawnedParticleInstance);
                //activeParticles.Add(spawnedParticleInstance);
                data.lastTarget = targetObject; //ASSIGN LAST TARGET
                data.activeVisualEffects.Add(effect.name + targetObject.name); //We use the effect, not the spawned instance
            }
        }
    }

    /*
    public void ShowVisualEffect(Vector3 location, GameObject effect)
    {
        if (!effect || (target != null && target.IsDead) ) return;

        //Debug.Log("{" + name.ToUpper() + "} <" + location.name.ToUpper() + "> INVOKING VISUAL EFFECT: " + effect.name.ToUpper() + "!!!");//DEBUG

        if(!effect.GetComponent<NetworkIdentity>()) effect.AddComponent<NetworkIdentity>();

        GameObject go = Instantiate(effect, location, Quaternion.identity);
        if (!activeVisualEffects.Contains(effect))
        {
            ParticleSystem particles = go.GetComponent<ParticleSystem>();
            SkillEffect visualEffect = go.GetComponent<SkillEffect>();
            if (!visualEffect) { Debug.LogWarning("No SkillEffect component found on " + name); }

            if (!visualEffect) visualEffect = go.GetComponent<OneTimeTargetSkillEffect>();
            else
            {
                visualEffect.caster = this;
                visualEffect.target = target;
            }

            if (!particles) { Debug.LogWarning("No ParticleSystem component found on " + name); }
            else
            {
                go.transform.position = location;
                particles.Play(true);
                NetworkServer.Spawn(go);
                activeParticles.Add(go);
                activeVisualEffects.Add(effect);
            }
        }
        //else //TODO
        int count = activeParticles.Count;
        for (int i = 0; i < count; i++)
        {
            ParticleSystem particles = activeParticles[i].GetComponent<ParticleSystem>();
            if (!particles) { Debug.LogWarning("No ParticleSystem component found on " + name); }
            else if (!particles.isPlaying)
            {
                //activeParticles.RemoveAt(i);
            }
        }
        foreach (GameObject o in activeParticles)
        {
            //o.transform.position = (!target) ? (collider.bounds.center) : (target.collider.bounds.center);
        }
    }
    */
}
