using Mirror;

public partial class CombatStats// : NetworkBehaviour
{
    public SyncListSkill skills = new SyncListSkill();
}

public partial class CharacterSheet : NetworkBehaviour
{
    public SyncListSkill SKILLS {
        get {
            if (combat.skills.Count < 1 && skills.startingSkills.Count < 1) combat.skills.Add(new Skill(UnityEngine.Resources.Load<ScriptedSkillSet>("SKILLSETS/DefaultCombatSkills").skillList[0]) );
            return combat.skills;
        }
        set {
            combat.skills = value;
        }
    }
}