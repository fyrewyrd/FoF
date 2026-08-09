using System.Collections.Generic;
using UnityEngine;

public partial class ScriptableItem// : ScriptableObject
{
    [Header(" [ SKILL SETS ] ")]
    [Tooltip("These skill sets become available to the character when this item is equipped or used.")]
    [SerializeField] List<ScriptableSkillSet> _skillSets = new List<ScriptableSkillSet>();
    public List<ScriptableSkillSet> skillSets { get { return _skillSets; } }
}