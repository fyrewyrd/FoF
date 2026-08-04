using Mirror;
using UnityEngine;
using System;

[RequireComponent(typeof(TextStyleData))] [RequireComponent(typeof(VisualEffectsManager))]
[RequireComponent(typeof(EventTriggerData))]
public class EventTriggerManager : NetworkBehaviour
{
    [HideInInspector] public Vector3 targetLocation;

    [Header("EVENT TRIGGER DATA")]
    [SerializeField] EventTriggerData _data;
    public EventTriggerData data
    {
        get
        {
            if (!_data)
            {
                _data = GetComponent<EventTriggerData>();
                if (!_data)
                {
                    _data = gameObject.GetComponentInChildren<EventTriggerData>(); //NESTED COMPONENTS
                    if (!_data) _data = gameObject.AddComponent<EventTriggerData>();
                }
            }

            return _data;
        }
        set
        {
            _data = value;
        }
    }

    //TRIGGER EVENTS
    //[Server] public void Trigger(Entity self, Entity target, EventTriggerType triggerType)
    //{
    //    if (!data) return;
    //
    //    Trigger(self, target, triggerType, data);
    //}

    [Server]
    public void Trigger(CharacterSheet self, CharacterSheet target, EventTriggerType triggerType)
    {
        if (!data) { Debug.LogWarning(name + " does not have an EventTriggerData component attached with it."); return; }
        if (data.triggers == null || data.triggers.Length == 0) { return; } //NO TRIGGERS TO PROCESS

        foreach (EventTriggerInfo trigger in data.triggers)
        {
            if (trigger.eventTriggerType != EventTriggerType.Never && trigger.eventTriggerType == triggerType)
            {
                Launch(self, target, trigger.spawnLocation, trigger.eventRange, trigger.triggeredEvent);
            }
        }
    }

    //LAUNCH EVENT
    [Server] public void Launch(CharacterSheet self, CharacterSheet target, EventPosition location, float range, ScriptedEvent trigger)// string say, AudioClip sound, string animationName, SpawnData[] minionData, ScriptableSkill skill)
    {
        //LINK MANAGER COMPONENTS
        //if (!text) text = self.GetComponent<TextManager>();

        //TRIGGERED ACTIONS

        //TODO: TIME TRIGGERED

        // S P E A K
        // Deliver Opening Line
        //if(trigger.openingLine != string.Empty)
        //{
        //        switch (location)
        //        {
        //            case EventPosition.Self: { self.RpcShowSpeechPopup(trigger.openingLine); break; }
        //            case EventPosition.Target: { target.RpcShowSpeechPopup(trigger.openingLine); break; }
        //            default: { self.RpcShowSpeechPopup(trigger.openingLine); break; }
        //        }
        //}
        // Deliver Speech
        if (trigger.speech != null && trigger.speech.Length > 0)
        {
            if (trigger.sayARandomLine) //SINGULAR
            {
                string say = trigger.speech[UnityEngine.Random.Range(0, trigger.speech.Length - 1)];

                switch (location)
                {
                    case EventPosition.Self: { self.RpcShowSpeechPopup(say); break; }
                    case EventPosition.Target: { target.RpcShowSpeechPopup(say); break; }
                    default: { self.RpcShowSpeechPopup(say); break; }
                }
            }
            else //MULTIPLE
            {
                string say;

                for (int i = 0; i < trigger.speech.Length; i++)
                {
                    say = trigger.speech[i];
                    switch (location)
                    {
                        case EventPosition.Self: { self.RpcShowSpeechPopupDelayed(say, trigger.delay * i); break; }
                        case EventPosition.Target: { target.RpcShowSpeechPopupDelayed(say, trigger.delay * i); break; }
                        default: { self.RpcShowSpeechPopupDelayed(say, trigger.delay * i); break; }
                    }
                }
            }
        }

        // T R I G G E R  V F X
        if (trigger.triggerVisualEffects != null)
        {
            switch (location)
            {
                case EventPosition.Self: { self.RpcShowVisualEffects(trigger.triggerVisualEffects, trigger.delay); break; }
                case EventPosition.Target: { target.RpcShowVisualEffects(trigger.triggerVisualEffects, trigger.delay); break; }
                default: { self.RpcShowVisualEffects(trigger.triggerVisualEffects, trigger.delay); break; }
            }
        }

        // P L A Y  S O U N D
        if (trigger.playSounds != null)
        {
            float nextPlaytime = 0f;

            for (int i = 0; i < trigger.playSounds.Length; i++)
            {
                switch (location)
                {
                    case EventPosition.Self: { self.audioSource.clip = trigger.playSounds[i]; self.audioSource.PlayDelayed(nextPlaytime); break; }
                    case EventPosition.Target: { target.audioSource.clip = trigger.playSounds[i]; target.audioSource.PlayDelayed(nextPlaytime); break; }
                    default: { self.audioSource.clip = trigger.playSounds[i]; self.audioSource.PlayDelayed(nextPlaytime); break; }
                }

                nextPlaytime += trigger.delay * i;
                nextPlaytime += trigger.playSounds[i].length;
            }
        }

        // P L A Y  A N I M A T I O N
        if (trigger.playAnimationsNamed != null && trigger.playAnimationsNamed.Length > 0)
        {
            foreach (string clip in trigger.playAnimationsNamed)
            {
                switch (location)
                {
                    case EventPosition.Self: { self.animator.Play(clip); break; }
                    case EventPosition.Target: { target.animator.Play(clip); break; }
                    default: { self.animator.Play(clip); break; }
                }
            }
        }

        // C R E A T E  M I N I O N S
        if (trigger.createMinions != null && trigger.createMinions.Length > 0)
        {
            switch (location)
            {
                case EventPosition.Self: targetLocation = self.transform.position; break;
                case EventPosition.Target: targetLocation = target.transform.position; break;
                case EventPosition.Random: targetLocation = (self.transform.position + DX4D.Tools.GetRandom.Vector(-range, range)); break;
                case EventPosition.InFrontOf: targetLocation = ((self.transform.forward * range) + self.transform.position); break;
                case EventPosition.Behind: targetLocation = ((-self.transform.forward * range) + self.transform.position); break;
                case EventPosition.Above: targetLocation = ((self.transform.up * range) + self.transform.position); break;
                case EventPosition.Below: targetLocation = ((-self.transform.up * range) + self.transform.position); break;
                case EventPosition.RightSide: targetLocation = ((self.transform.right * range) + self.transform.position); break;
                case EventPosition.LeftSide: targetLocation = ((-self.transform.right * range) + self.transform.position); break;
                default: targetLocation = self.transform.position; break;
            }

            for (int i = 0; i < trigger.createMinions.Length; i++)
            {
                if (self.spawnedMinions.Count >= self.maxMinions)
                {
#if UNITY_EDITOR
                    Debug.Log(self.name + " summoned too many minions!");
#endif
                    return;
                }

                if (trigger.createMinions[i].toCreate != null && trigger.createMinions[i].toCreate.gameObject != null)
                {
                    if (UnityEngine.Random.Range(0.001f, 1.000f) <= trigger.createMinions[i].probability)
                    {
                        int amount = UnityEngine.Random.Range(trigger.createMinions[i].minAmount, trigger.createMinions[i].maxAmount);
                        for (int x = 0; x < amount; x++)
                        {
                            //GameObject go = Instantiate(spawnMinions[i].gameObject, (transform.position + DX4D.Tools.GetRandomVector(-spawnDistance, spawnDistance)), Quaternion.identity);
                            GameObject go = Instantiate(trigger.createMinions[i].toCreate.gameObject, targetLocation, Quaternion.identity);

                            if (go != null)
                            {
                                go.name = trigger.createMinions[i].toCreate.name; //TODO: This does not work in Server mode for some reason...we might need a ClientRpc or ServerCallback. Normally this would get rid of (Clone)

                                //SUMMON ENTITY
                                CharacterSheet summoned = go.GetComponent<CharacterSheet>();
                                if (summoned != null)
                                {
                                    summoned.name = trigger.createMinions[i].toCreate.name; //TODO: Added for redundancy...still needs to be tested in server mode. One of these can be removed once it works across the network.
                                    self.spawnedMinions.Add(summoned); //Only Entities will count toward your maxAllies quota
                                    summoned.OnAggro(target); //Aggro our target with the newly summoned entity...if it's an Entity
#if UNITY_EDITOR
                                    Debug.Log(self.name + " summoned " + summoned.name);
#endif
                                }

                                //TODO: HANDLE OTHER TYPES

                                NetworkServer.Spawn(go);

                                //RANDOMIZE FOR NEXT SPAWN
                                if (location == EventPosition.Random) { targetLocation = (self.transform.position + DX4D.Tools.GetRandom.Vector(-range, range)); }
                            }
                        }
                    }
                }
            }
        }

        // C A S T  S K I L L
        if (trigger.castSkill != null)
        {
            switch (location)
            {
                case EventPosition.Self: { self.currentSkill = self.GetSkillIndexByName(trigger.castSkill.name); break; }
                case EventPosition.Target: { target.currentSkill = target.GetSkillIndexByName(trigger.castSkill.name); break; }
                default: { self.currentSkill = self.GetSkillIndexByName(trigger.castSkill.name); break; }
            }
        }

        // C R E A T E  I T E M S
        if (trigger.createItems != null && trigger.createItems.Length > 0)
        {

            for (int i = 0; i < trigger.createMinions.Length; i++)
            {
                if (trigger.createItems[i].item != null)
                {
                    if (UnityEngine.Random.Range(0.001f, 1.000f) <= trigger.createItems[i].probability)
                    {
                        switch (location)
                        {
                            case EventPosition.Self: self.InventoryAdd(new Item(trigger.createItems[i].item), 1); break;
                            case EventPosition.Target: target.InventoryAdd(new Item(trigger.createItems[i].item), 1); break;
                            default: self.InventoryAdd(new Item(trigger.createItems[i].item), 1); break;
                        }
                    }
                }
            }
        }
    }
}
