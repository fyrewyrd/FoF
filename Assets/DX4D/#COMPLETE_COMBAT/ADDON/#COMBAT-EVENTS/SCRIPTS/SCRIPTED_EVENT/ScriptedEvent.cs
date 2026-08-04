using UnityEngine;

[CreateAssetMenu(menuName = "DX4D/EVENT/Scripted Event", order = 51)]
public class ScriptedEvent : ScriptableObject
{
    [Header("EVENT TIMING")]
    [SerializeField][Range(0f, 1f)] public float shortPause;
    [SerializeField][Range(0, 600)] public float longPause;
    public float delay { get { return (shortPause + longPause); } }

    [Header("SAY SOMETHING")]
    //[SerializeField, TextArea(1, 3)] public string openingLine;
    [SerializeField, TextArea(1, 3)] public string[] speech;
    
    [SerializeField] public bool sayARandomLine;

    [Header("CAST A SKILL")]
    [SerializeField] public ScriptableSkill castSkill;

    [Header("PLAY AUDIO")]
    [SerializeField] public AudioClip[] playSounds;

    [Header("TRIGGER VFX")]
    [SerializeField] public GameObject[] triggerVisualEffects;

    [Header("PLAY ANIMATION")]
    [SerializeField] public string[] playAnimationsNamed;

    [Header("CREATE MINIONS")]
    [SerializeField] public CreationInfo[] createMinions;

    [Header("CREATE ITEMS")]
    [SerializeField] public ItemDropChance[] createItems;
}
