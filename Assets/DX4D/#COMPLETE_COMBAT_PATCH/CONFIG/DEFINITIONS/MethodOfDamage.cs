using System;

[Flags] public enum MethodOfDamage
{
    Physical = (1 << 0),
    Magic = (1 << 1),
    NoDamage = (1 << 2),
    Blood = (1 << 3),
    Spirit = (1 << 4),
    Poison = (1 << 5),
    Mana = (1 << 6),
    Fury = (1 << 7),
    Stamina = (1 << 8)
};
