using System;

[Flags] public enum StatusEffect {
    None = (1 << 0),
    //DAMAGE
    Poison = (1 << 1),
    Bleed = (1 << 2),
    Burn = (1 << 3),
    //STATUS
    Blind = (1 << 4),
    Sleep = (1 << 5),
    Silence = (1 << 6),
    Stun = (1 << 7),
    Doom = (1 << 8),
    Drench = (1 << 9),
    Freeze = (1 << 10),
    //TIME
    Tangle = (1 << 11),
    Swift = (1 << 12),
    Slow = (1 << 13),
    Haste = (1 << 14),
    Stop = (1 << 15),
    //TRICKS
    Confuse = (1 << 16),
    //DEFENSIVE
    Protect = (1 << 17),
    Shell = (1 << 18),
    Float = (1 << 19),
    Reflect = (1 << 20),
    //HEALING
    Regen = (1 << 21),
}
