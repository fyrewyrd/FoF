using Mirror;
using UnityEngine;

public partial class CharacterSheet : NetworkBehaviour
{
    [Header("EVENT TRIGGER MANAGER COMPONENT")]
    [SerializeField] EventTriggerManager _eventManager;
    public EventTriggerManager eventManager
    {
        get
        {
            if (!_eventManager)
            {
                _eventManager = GetComponent<EventTriggerManager>();
                if (!_eventManager) _eventManager = GetComponentInChildren<EventTriggerManager>();
                {
                    if (!_eventManager) _eventManager = gameObject.AddComponent<EventTriggerManager>();
                }
            }

            return _eventManager;
        }
        set { _eventManager = value; }
    }
}
