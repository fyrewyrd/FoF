public enum DefensiveState {
    Idle, BeingHealed,
    TakingDamage, TakingSpellDamage,
    TakingWeakToDamage, TakingAchillesHeelDamage,
    ReflectingDamage, AbsorbingDamage, IgnoringDamage, ResistingDamage,
    DodgingAttack, DodgingSpell,
    BlockingAttack, BlockingSpell,
    TakingCriticalDamage, TakingCriticalSpellDamage,
    TakingBackstabDamage, TakingCriticalBackstabDamage,
    TakingFlankDamage, TakingCriticalFlankDamage,
	TakingOverwhelmDamage, TakingCriticalOverwhelmDamage
}
