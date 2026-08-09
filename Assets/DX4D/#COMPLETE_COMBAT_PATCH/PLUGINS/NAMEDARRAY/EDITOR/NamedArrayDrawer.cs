using UnityEngine;
using System;
using UnityEditor;

[CustomPropertyDrawer(typeof(NamedArrayAttribute))]
public class NamedArrayDrawer : PropertyDrawer
{
    /*
    private void OnValidate()
    {
    }
    private void Awake()
    {
        if (weaponSkills == null)
        {
            weaponSkills = new Dictionary<WeaponCategory, ScriptedSkillSet>();
        }

        string[] categoryNames = Enum.GetNames(typeof(WeaponCategory));
        
//        categoryNames.
//        Dictionary<WeaponCategory, string> LifeCycleDict = Enum.GetValues(typeof(WeaponCategory)).Cast<int>()
//    .ToDictionary(Key => Key, value => ((LifeCycle)value).ToString());
        

        
//        weaponSkills = new Dictionary<WeaponCategory, ScriptedSkillSet>();
//        foreach (WeaponSkillSet weaponSkillSet in weaponSkillsDictionary)
//        {
//            weaponSkills.Add(weaponSkillSet.weaponType, weaponSkillSet.weaponSkills);
//        }
        
    }
*/

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Properly configure height for expanded contents.
        return EditorGUI.GetPropertyHeight(property, label, property.isExpanded);
    }
    string[] categoryNames;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //POPULATE INSPECTOR LABELS
        // Replace label with enum name if possible.
            var config = attribute as NamedArrayAttribute;
            categoryNames = Enum.GetNames(config.TargetEnum);

        try
        {
            int pos = int.Parse(property.propertyPath.Split('[', ']')[1]);
            var enum_label = categoryNames.GetValue(pos) as string;
            // Make names nicer to read (but won't exactly match enum definition).
            enum_label = ObjectNames.NicifyVariableName(enum_label.ToLower());
            label = new GUIContent(enum_label);
        }
        catch
        {
            // keep default label
        }
        EditorGUI.PropertyField(position, property, label, property.isExpanded);
        
    }
}
