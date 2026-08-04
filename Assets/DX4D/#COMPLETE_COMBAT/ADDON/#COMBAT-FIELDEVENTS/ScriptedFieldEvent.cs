using UnityEngine;
//using System.Collections.Generic;

public abstract class ScriptedFieldEvent : ScriptableObject
{
    [Header("FIELD EVENT CONFIG")]
    //RANGE
    [Tooltip("The range is equal to the radius of the effect field")]
    [SerializeField] public float range = 1.0f;
    //TICK FREQUENCY
    [Tooltip("Triggers are processed once every x seconds. Where x is the tickFrequency.")]
    [SerializeField] public float tickFrequency = 1.0f;
    //EFFECT DURATION
    [Tooltip("If this effect is not destroyed otherwise (by being triggered etc), it will be destroyed after this duration.")]
    [SerializeField] public float effectDuration = 300.0f;


    [Header("FIELD EVENT TRIGGERS")]
    //DESTROY EVENTS
    [Tooltip("The first time this ticks it is destroyed."
        + "\nUseful for grenades and other timed single action effects")]
    [SerializeField] public bool destroyOnTick = false;

    [Tooltip("The first time this has an effect on anything, it is destroyed."
        + "\nUseful for booby traps or pressure plates that can only trigger once")]
    [SerializeField] public bool destroyOnTriggered = false;


    //REACTIVATION
    [Tooltip("Instead of being destroyed, the field will return after a period of time passes."
        + "\nUseful for resetting traps in a dungeon etc.")]
    [SerializeField] public bool reactivates = false;
    [Tooltip("This field will deactivate for a period of time when certain conditions are met.\nIt will reactivate after this amount of seconds.")]
    [SerializeField] public float reactivateDuration;

}

    /// <summary> With each tick, the Trigger method is called by an EffectField </summary>
    //public abstract bool Trigger(CharacterSheet owner, List<CharacterSheet> targets);
    //DIRECTION
    //[Tooltip("The direction is the line that the effect angle is centered on\nThis field only interacts with targets within that angle")]
    //[SerializeField] public Vector3 direction = Vector3.forward;
    //DAMAGE ANGLE
    //[Tooltip("Represents the angle within which this field will have an effect"
    //    + "\n360 will do damage all the way around the field"
    //    + "\n180 will do damage on one side of the field"
    //    + "\n90 will do damage in the direction of this effect")]
    //[SerializeField][Range(0,360)] public int effectAngle = 360;