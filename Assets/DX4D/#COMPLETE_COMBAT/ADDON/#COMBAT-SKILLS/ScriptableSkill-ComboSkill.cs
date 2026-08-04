using UnityEngine;

public abstract partial class ScriptableSkill// : ScriptableObject
{
    // [Client]
    public virtual void OnCastStarted(CharacterSheet caster)
    {
        if (caster.audioSource != null && castSound != null)
            caster.audioSource.PlayOneShot(castSound);
    }

    // [Client]
    public virtual void OnCastFinished(CharacterSheet caster) { }
}
public abstract partial class ScriptableSkill// : ScriptableObject
{
    //public Sprite image;

    [Header("COMBO SKILL")]
    [SerializeField] public ScriptableSkill nextSkill; // this skill will be cast next (if this skill can Chain)

    /// <summary>
    /// Override the get method to set up conditions for chains to happen.
    /// This will allow us to have conditions to chain attacks, like checking if certain keys are pressed, etc
    /// </summary>
    public virtual bool canChain {
        get { return (nextSkill != null); }
        //set { _canChain = value; }
    }
    public bool isChainSkill { get { return canChain; } }

    public void ChainNextSkill() {
        if (isChainSkill) {
            Player p = Local.player;
            if (!p) return;
            p.StartCastSkill(new Skill(nextSkill));
        }
    }
}
