/* //DEPRECIATED
using Mirror;
using UnityEngine;
using System.Collections;

public abstract partial class Entity : NetworkBehaviourNonAlloc
{
    int currentTick = 0;
    int maxTicksPerSequence = int.MaxValue; //This must be larger than your maximum duration on status effects...prevents eventual index out of bounds errors
    int lastTickProcessed = 0;

    public void OnStartServer_OnTick() { if(!IsInvoking("OnTick")) InvokeRepeating("OnTick", 1.0f, 1.0f); }
    //public void OnStartLocalPlayer_OnTick()
        private void FixedUpdate()
        {
            if (!IsInvoking("OnTick"))
            {
                currentTick = 0;
                InvokeRepeating("OnTick", 1.0f, 1.0f);
            }
        }

    public void OnStartServer_GlobalUpdate()
    {
        float delay = 1.0f;
        StartCoroutine(GlobalUpdate(this, delay));
    }
    [Server]
    public IEnumerator GlobalUpdate(Entity target, float delay)
    {
        yield return new WaitForSeconds(delay);

        ProcessGlobalUpdates(target);
    }

    [Server] public void ProcessGlobalUpdates(Entity target)
    {
        target.GlobalCharacterUpdate();
    }

    [SerializeField] float globalCharacterUpdateDelay = 1.0f;
    float timeElapsed = 0;

    //public void OnStartServer_OnTick()
    //public void LateUpdate()
    //{
    //        OnTick();
    //}
    [Server]
    public void OnTick()
    {
        //if (lastTickProcessed >= currentTick) return;
        timeElapsed += Time.deltaTime;
        if (timeElapsed > globalCharacterUpdateDelay)
        {
            timeElapsed = 0;
            Debug.Log("T I M E R   T I C K");
            Validate();
            GlobalCharacterUpdate();

            currentTick++;
        }
    }
    [Server] private void Validate()
    {
        if (currentTick >= (maxTicksPerSequence - 1))
        {
            //Debug.Log("Max StatusEffect Ticks Reached...Resetting..."); //DEBUG
            lastTickProcessed = 0;
            currentTick = 1;
        }
    }
    // - - - - - - - - - - - -
    // M A I N  H A N D L E R
    /// <summary>
    /// Happens every second
    /// </summary>
    [Server] internal void GlobalCharacterUpdate()
    {
        UnityEngine.Debug.Log(">HANDLING CHARACTER CHANGES<"); //DEBUG

        //HandleStatusEffects(); //DEPRECIATED
        HandleMovement();
        HandleRegeneration();
        //HandleDeathTriggers(); //depreciated - Moved to ApplyDamage
    }
}
*/
