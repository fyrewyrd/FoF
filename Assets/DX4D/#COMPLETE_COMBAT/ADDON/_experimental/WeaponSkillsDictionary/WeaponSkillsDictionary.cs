using Mirror;
using UnityEngine;
using System;
using System.Collections.Generic;

[ExecuteInEditMode]
public class WeaponSkillsDictionary : NetworkBehaviour {

    //bool populated = false;
    private void OnValidate()
    {
        string[] categoryNames = Enum.GetNames(typeof(WeaponCategory));
        //for (int i = 0; i < categoryNames.Length; i++) { }
        //if (populated) return;

#if UNITY_EDITOR
        Debug.Log("POPULATING WEAPON SKILLS DICTIONARY");
#endif
        // POPULATE DICTIONARY
        //WeaponSkillsDictionary dict = GameObject.Find("WeaponSkillsManager").GetComponent<WeaponSkillsDictionary>();

        //string[] categoryNames = Enum.GetNames(typeof(WeaponCategory));

        //if (dict.weaponSkills != null)
        //if (attachedWeaponSkills == null)
        //{
        //    attachedWeaponSkills = new Dictionary<WeaponCategory, ScriptedSkillSet>(categoryNames.Length);
        //    Debug.Log(attachedWeaponSkills.ToString() + " recreated");
        //}
        //}

        if (categoryNames.Length > attachedWeaponSkills.Count)
        {
#if UNITY_EDITOR
            Debug.Log("TIME: " + NetworkTime.time);
#endif
            for (int i = 0; i < categoryNames.Length; i++)
            {
                WeaponCategory category = DX4D.Tools.Parser.ParseEnum<WeaponCategory>(categoryNames[i]);

                if (!attachedWeaponSkills.ContainsKey(category))
                {
#if UNITY_EDITOR
                    Debug.Log("...adding " + category.ToString() + " weapon skill...");
#endif
                    attachedWeaponSkills.Add( category, null );
                    if (weaponSkillsDictionary.Count < attachedWeaponSkills.Count)
                    {
                        weaponSkillsDictionary.Add(new WeaponSkillSet() { weaponType = category } );
#if UNITY_EDITOR
                        Debug.Log("...adding " + category + " to the dictionary...");
#endif
                    }
                }

                if(attachedWeaponSkills.ContainsKey(category))
                {
#if UNITY_EDITOR
                    Debug.Log(category.ToString() + " weapon skill added successfully");
#endif
                    //weaponSkillsDictionary.Add(attachedWeaponSkills[category].skillList[0]);
                }

            }
        }
            /*
                    //if (!weaponSkills.ContainsKey(category))
                    //{
                    //    Debug.Log("populating weapon skills...");
                    //    weaponSkills.Add(category, null);
                    //}
                    try
                    {
                        int pos = int.Parse(property.propertyPath.Split('[', ']')[1]);
                        var enum_label = categoryNames.GetValue(pos) as string;
                        // Make names nicer to read (but won't exactly match enum definition).
                        //enum_label = ObjectNames.NicifyVariableName(enum_label.ToLower());
                        label = new GUIContent(enum_label);
                    }
                    catch
                    {
                        // keep default label
                    }
                    EditorGUI.PropertyField(position, property, label, property.isExpanded);
                }
            }
        */
        }

    [Serializable] public class WeaponSkillSet
    {
        [SerializeField]
        public WeaponCategory weaponType;
        [SerializeField]
        public ScriptedSkillSet weaponSkills;
    }

    //public class SyncListSkillSet : SyncListSTRUCT<WeaponSkillSet> { }

    //[SerializeField] [NamedArray(typeof(WeaponCategory))] private List<WeaponSkillSet> weaponSkillsDictionary = new SyncListSkillSet<WeaponSkillSet>();
    [SerializeField] [NamedArray(typeof(WeaponCategory))] private List<WeaponSkillSet> weaponSkillsDictionary = new List<WeaponSkillSet>();

    [SerializeField] public Dictionary<WeaponCategory, ScriptedSkillSet> attachedWeaponSkills = new Dictionary<WeaponCategory, ScriptedSkillSet>();
}
