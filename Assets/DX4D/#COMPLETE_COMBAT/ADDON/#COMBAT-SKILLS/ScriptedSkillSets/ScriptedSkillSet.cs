using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(menuName = "DX4D/SKILLS/Skill Set", order = 20)]
public class ScriptedSkillSet : ScriptableObject
{
    [SerializeField] public WeaponCategory weaponTypeRequired;
    [SerializeField] public List<ScriptableSkill> skillList = new List<ScriptableSkill>();
}
