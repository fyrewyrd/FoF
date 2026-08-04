using Mirror;
using UnityEngine;

public class EventTriggerData : NetworkBehaviour
{
    [Header("EVENT TRIGGERS")]
    [SerializeField] public EventTriggerInfo[] triggers;
}
