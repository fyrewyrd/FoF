public enum OffensiveState {
    Idle, HealingTarget,
    DealingDamage, DealingSpellDamage,
    DamagingWeakness, DamagingAchillesHeel,
    DamageReflected, DamageAbsorbed, DamageIgnored, DamageResisted,
    AttackDodged, SpellDodged,
    AttackBlocked, SpellBlocked,
    DealingCriticalDamage, DealingCriticalSpellDamage,
    DealingBackstabDamage, DealingCriticalBackstabDamage,
    DealingFlankDamage, DealingCriticalFlankDamage,
	DealingOverwhelmDamage, DealingCriticalOverwhelmDamage
}
