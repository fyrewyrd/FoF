using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DX4D/SKILLS/Scriptable Skill Set")]
public class ScriptableSkillSet : ScriptableObject
{
    #region DESCRIPTION
#if UNITY_EDITOR
        public string description { get { return _editorDescription; } }
        [SerializeField][TextArea(2,2)] string _editorDescription =
            "Skillbooks are scriptable objects that can be attached to Players, NPCs, and Equipment" +
        " to give characters additional skills.";
#endif
    #endregion

    [Header("DEFAULT LEVEL")]
    [Tooltip("This is the level that skills in this set begin at.\nSetting this to 0 makes the skill unlearned by default.")]
    [SerializeField] int startingLevel = 99;

    [Header("SKILLBOOK")]
    [SerializeField] public List<ScriptableSkill> _skills;
    public List<Skill> skills
    {
        get
        {
            List<Skill> returnedSkills = new List<Skill>();
            foreach (ScriptableSkill skill in _skills)
            {
                returnedSkills.Add(new Skill(skill) { level = Mathf.Min(startingLevel, skill.maxLevel) } );
            }
            return returnedSkills;
        }
    }
}