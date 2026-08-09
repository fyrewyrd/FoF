using UnityEngine;
using System;

[Serializable] public class EventTriggerInfo
{
    [Header("EVENT TRIGGER INFO")]
    [SerializeField] public EventTriggerType eventTriggerType;

    [Header("EVENT POSITIONING")]
    [SerializeField] public EventPosition spawnLocation;
    [SerializeField] public float eventRange;

    [Header("SCRIPTED EVENT TRIGGERS")]
    [SerializeField] public ScriptedEvent triggeredEvent;
}
