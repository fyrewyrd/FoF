using Mirror;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public partial class Skills
{
    [Header(" [ STARTING SKILLS ] ")]
    [SerializeField] ScriptedSkillSet _startingSkillSet = null;
    [SerializeField] List<ScriptableSkill> _startingSkills = new List<ScriptableSkill>();
    public List<ScriptableSkill> startingSkills
    {
        get
        {
            List<ScriptableSkill> list = new List<ScriptableSkill>();
            if (_startingSkills != null && _startingSkills.Count > 0) { list.AddRange(_startingSkills); } //Validate and add starting skills
            if (_startingSkillSet != null && _startingSkillSet.skillList != null && _startingSkillSet.skillList.Count > 0) //Validate and add starting skill set
            {
                list.AddRange(_startingSkillSet.skillList);
            }
            if (list.Count < 1) { }
            return list;
        }
        set { _startingSkills = value; }
    }
}