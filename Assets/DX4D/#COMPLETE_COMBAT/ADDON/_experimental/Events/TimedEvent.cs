using Mirror;
using System;
using UnityEngine;

//[CreateAssetMenu(menuName = "DX4D/DAMAGE/TimedEvent", order = 999)]
public abstract partial class TimedEvent : NetworkBehaviour
{
    // C O N F I G U R A T I O N
    private bool _running = false;
    public bool running { get { return _running; } }

    [Tooltip("The number of times this Timer will be fired before it Stops. A value of 0 means it will run until stopped manually.")]
    public uint timesToFire = 1;
    //[HideInInspector]
    [SerializeField] public uint timesFired = 0;
    public float tickFrequency = 1.0f; // (in seconds)

    public float duration = 1.0f; // (in seconds)
    [HideInInspector] private DateTime startTime;

    public TimeSpan timeElapsed { get { return DateTime.Now - startTime; } }
    public bool durationReached { get { return (timeElapsed >= TimeSpan.FromSeconds(duration)); } }

    //public bool autoStart = false;
    //public float activateTime = 1;
    //public float cooldown = 1;
    //public TimedEventTrigger startTrigger = TimedEventTrigger.OnEnabled;
    //public string OnFinishedMethodName = "OnTimerFinished";
    //public string OnTickMethodName = "OnTick";
    //public string OnStartMethodName = "OnTimerStarted";

    #region C O N S T R U C T O R S
    /*
    //autostart
    public TimedEvent(bool automaticStart)
    {
        autoStart = automaticStart;
    }
    public TimedEvent(bool automaticStart, int eventDuration) : this(automaticStart)
    {
        duration = eventDuration;
    }
    public TimedEvent(bool automaticStart, int eventDuration, int secondsBetweenTicks) : this(automaticStart, eventDuration)
    {
        tickFrequency = secondsBetweenTicks;
    }
    public TimedEvent(bool automaticStart, int eventDuration, int secondsBetweenTicks, bool repeatAfterComplete) : this(automaticStart, eventDuration, secondsBetweenTicks)
    {
        repeating = repeatAfterComplete;
    }
    public TimedEvent(bool automaticStart, int eventDuration, int secondsBetweenTicks, bool repeatAfterComplete, TimedEventTrigger eventTrigger) : this(automaticStart, eventDuration, secondsBetweenTicks, repeatAfterComplete)
    {
        startTrigger = eventTrigger;
    }
    //manualstart
    public TimedEvent() : this(false) { }
    public TimedEvent(int eventDuration) : this(false)
    {
        duration = eventDuration;
    }
    public TimedEvent(int eventDuration, int secondsBetweenTicks) : this(eventDuration)
    {
        tickFrequency = secondsBetweenTicks;
    }
    public TimedEvent(int eventDuration, int secondsBetweenTicks, bool repeatAfterComplete) : this(eventDuration, secondsBetweenTicks)
    {
        repeating = repeatAfterComplete;
    }
    public TimedEvent(int eventDuration, int secondsBetweenTicks, bool repeatAfterComplete, TimedEventTrigger eventTrigger) : this(eventDuration, secondsBetweenTicks, repeatAfterComplete)
    {
        startTrigger = eventTrigger;
    }
    */
    #endregion

    #region T I M E R  C O N T R O L S
    // U P D A T E
    private void Update() { if (running && !IsInvoking("Tick")) InvokeRepeating("Tick", tickFrequency, tickFrequency); }

    // S T A R T
    public bool Start()
    {
        if (!running)
        {
            startTime = DateTime.Now;
            _running = true;

            OnTimerStarted();
            if(!IsInvoking("Tick")) InvokeRepeating("Tick", tickFrequency, tickFrequency);

#if UNITY_EDITOR
            Debug.Log(name + GetInstanceID() + ":TimedEvent:Start()-TimerStarted"); //DEBUG
#endif
        }

#if UNITY_EDITOR
        Debug.Log(name.ToString() + ":Start()"); //DEBUG
#endif

        return _running;
    }

    // R E S E T
    bool Reset()
    {
        Stop();

#if UNITY_EDITOR
        Debug.Log(name.ToString() + ":Reset()"); //DEBUG
#endif

        return Start();
    }
    //public void Pause() { Debug.Log(name.ToString() + ":Pause()"); }

    // S T O P
    public void Stop()
    {
        if (IsInvoking("Tick")) CancelInvoke("Tick");
        _running = false;

#if UNITY_EDITOR
        Debug.Log(name + GetInstanceID() + ":TimedEvent:Stop()"); //DEBUG
#endif
    }

    // T I C K
    /// <summary>
    /// This method should be called every x seconds depending on the value of tickFrequency.
    /// returns bool: No More Events To Fire!!!
    /// </summary>
    public void Tick()
    {
        if (!running) return;

        if (durationReached)
        {
            FireEvent();
#if UNITY_EDITOR
            Debug.Log(name + GetInstanceID() + ":TimedEvent:Tick()-EventFired"); //DEBUG
#endif
        }
        else
        {
            OnTick();
#if UNITY_EDITOR
            Debug.Log(name + GetInstanceID() + ":TimedEvent:Tick()-EventUpdated"); //DEBUG
#endif
        }
    }

    //F I R E  E V E N T
    public void FireEvent()
    {
        timesFired++;
        OnTimerFired();

        //Repeat?
        if (timesFired < timesToFire) {
            Reset();
#if UNITY_EDITOR
            Debug.Log(name + GetInstanceID() + ":TimedEvent:Tick()-Repeated" + " Times Remaining: " + (timesToFire - timesFired).ToString() );
#endif
        }
        else {
            Stop();
#if UNITY_EDITOR
            Debug.Log(name + GetInstanceID() + ":TimedEvent:Tick()-Completed");
#endif
        }
    }
    #endregion

    #region T I M E R  E V E N T  M E T H O D S
    public virtual void OnTimerStarted()
    {
#if UNITY_EDITOR
        Debug.Log(name + GetInstanceID() + ":TimedEvent:OnTimerStarted()");
#endif
    }
    public virtual void OnTick()
    {
        //DoTs etc
#if UNITY_EDITOR
        Debug.Log(name + GetInstanceID() + ":TimedEvent:OnTick()");
#endif
    }
    public virtual void OnTimerFired()
    {
#if UNITY_EDITOR
        Debug.Log(name + GetInstanceID() + ":TimedEvent:OnTimerFired()");
#endif
    }
    #endregion
}
