//using Mirror;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Entity// : NetworkBehaviour
    
{/*
    [Header(" [ STARTING SKILLS ] ")]
    [SerializeField] List<ScriptableSkillSet> startingSkills = new List<ScriptableSkillSet>();
    [SerializeField] List<ScriptableSkillSet> equipmentSkills = new List<ScriptableSkillSet>();

    internal ScriptableSkill[] skillTemplates
    {
        get
        {
            List<ScriptableSkill> _skills = new List<ScriptableSkill>();

            if (startingSkills.Count > 0)
            {
                //STARTING SKILLS
                foreach (ScriptableSkillSet skillSet in startingSkills)
                {
                    if (skillSet._skills.Count > 0) _skills.AddRange(skillSet._skills);
#region DEBUG
#if UNITY_EDITOR
                    Debug.Log(name + " loading starting skills " + skillSet._skills.ToString());
#endif
#endregion
                }
            }
            else
            {
                _skills.Add(Resources.Load<ScriptableSkill>("skills/default/Attack"));
                //_skills.Add(Resources.Load<ScriptableSkill>("Skills, Buffs, Status Effects/"+name+"/Normal Skill ("+name+")"));
#region DEBUG
#if UNITY_EDITOR
                UnityEngine.Debug.Log(name + " loading default starting skills " + _skills.ToString());
#endif
#endregion
            }

            if (equipmentSkills.Count > 0)
            {
                //EQUIPMENT SKILLS
                foreach (ScriptableSkillSet skillSet in equipmentSkills)
                {
                    if (skillSet._skills.Count > 0) _skills.AddRange(skillSet._skills);
#region DEBUG
#if UNITY_EDITOR
                    Debug.Log(name + " loading equipment skills " + skillSet._skills.ToString());
#endif
#endregion
                }
            }

            return _skills.ToArray();
        }
    }

    [SerializeField] internal SyncListSkill skills = new SyncListSkill();
*/}
