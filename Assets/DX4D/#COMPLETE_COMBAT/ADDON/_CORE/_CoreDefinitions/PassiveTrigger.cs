using System;

[Serializable][Flags] public enum PassiveTrigger // might have to use ulong here to let us have up to 62 values - uint would give us 31 values
{
    //IDLE
    /// <summary>Each second when not in motion or performing an action</summary>
    NotMoving = (1 << 0), /* 0b0000000000000001 */

    //PHYSICAL
    /// <summary>When dealing physical damage</summary>
    DamageDealt = (1 << 1),
    /// <summary>When taking physical damage</summary>
    DamageTaken = (1 << 2),

    //MAGIC
    /// <summary>When dealing magic damage</summary>
    MagicDMGDealt = (1 << 3),
    /// <summary>When taking magic damage</summary>
    MagicDMGTaken = (1 << 4),

    //SPELL
    /// <summary>When dealing spell damage</summary>
    SpellDMGDealt = (1 << 5),
    /// <summary>When taking spell damage</summary>
    SpellDMGTaken = (1 << 6),

    //CRITICAL
    /// <summary>When dealing a critical hit with non-spell damage</summary>
    CriticalHit = (1 << 7),
    /// <summary>When taking a critical hit from non-spell damage</summary>
    CritTaken = (1 << 8),
    /// <summary>When dealing a critical hit with spell damage</summary>
    SpellCrit = (1 << 9),
    /// <summary>When taking a critical hit from spell damage</summary>
    SpellCritTaken = (1 << 10),

    //BACKSTAB
    /// <summary>When I deal backstab damage</summary>
    Backstab = (1 << 11),
    /// <summary>When I take backstab damage</summary>
    BackstabTaken = (1 << 12),
    /// <summary>When I deal critical backstab damage</summary>
    BackstabCrit = (1 << 13),
    /// <summary>When I take critical backstab damage</summary>
    BackstabCritTaken = (1 << 14),

    //BLOCK
    /// <summary>When I block non-spell damage</summary>
    BlockAttack = (1 << 15), /* 0b1000000000000000 */
    /// <summary>When my non-spell damage is blocked</summary>
    MyAttackBlocked = (1 << 16),
    /// <summary>When I block spell damage</summary>
    BlockSpell = (1 << 17),
    /// <summary>When my spell damage is blocked</summary>
    MySpellBlocked = (1 << 18),

    //DODGE
    /// <summary>When I dodge an attack</summary>
    DodgeAttack = (1 << 19),
    /// <summary>When my attack is dodged</summary>
    MyAttackDodged = (1 << 20),
    /// <summary>When I dodge a spell</summary>
    DodgeSpell = (1 << 21),
    /// <summary>When my spell is dodged</summary>
    MySpellDodged = (1 << 22),

    //HEALING
    /// <summary>When I heal my target</summary>
    HealTarget = (1 << 23),
    /// <summary>When I am being healed</summary>
    BeingHealed = (1 << 24),

    //MOVEMENT & ACTIONS
    /// <summary>When I am in motion</summary>
    Moving = (1 << 25),
    /// <summary>When I am activating an action</summary>
    Casting = (1 << 26),
    /// <summary>When I have been involved in combat recently</summary>
    InCombat = (1 << 27),

    //NONE
    /// <summary>No triggers</summary>
    None = (1 << 28),
    /// <summary>When I have not been in combat recently</summary>
    NonCombat = (1 << 29)

}
