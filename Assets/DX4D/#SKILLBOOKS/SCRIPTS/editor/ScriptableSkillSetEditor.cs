using UnityEngine;
using UnityEditor;

namespace DX4D
{
    [CustomEditor(typeof(ScriptableSkillSet))]
    public class ScriptableSkillSetEditor : DX4DSkillEditor
    {
        public override string playerPrefs => "ScriptableSkillSet";

        ScriptableSkillSet _editTarget = null;
        ScriptableSkillSet editTarget
        {
            get
            {
                if (!_editTarget) _editTarget = (ScriptableSkillSet)target;
                return _editTarget;
            }
        }

        Vector2 scrollPos = Vector2.zero;
        
        //INSPECTOR GUI
        public override void OnInspectorGUI()
        {
            DrawToolbar();

            #region V I E W   T A B
            if (showview)
            {
                DrawOptionsButtons();

                #region S K I L L S   L I S T
                GUILayout.BeginVertical(); //BEGIN SKILLS BOX
                {
                    scrollPos = GUILayout.BeginScrollView(scrollPos); //BEGIN SKILLS SCROLL LIST
                    {
                        /*
                        if (editTarget.teachesSkill)
                        {
                            GUILayout.BeginHorizontal(); //BEGIN ENTRY
                            {
                                //ICON
                                if (showicons)
                                {
                                    GUILayout.BeginVertical("box"); //BEGIN ICON

                                    DrawIcon(editTarget.teachesSkill.image);

                                    GUILayout.EndVertical(); //END ICON
                                }

                                GUILayout.BeginVertical(); //BEGIN INFO
                                {
                                    //NAME
                                    if (shownames)
                                    {
                                        DrawName(editTarget.teachesSkill.name);
                                    }

                                    //CATEGORY
                                    if (showcategories)
                                    {
                                        //category
                                        if (editTarget.teachesSkill.requiredWeaponCategory != string.Empty)
                                        {
                                            DrawCategory(editTarget.teachesSkill.requiredWeaponCategory.Trim("Weapon".ToCharArray()));
                                        }
                                    }

                                    //TOOLTIP
                                    if (showtooltips)
                                    {
                                        DrawTooltip(editTarget.teachesSkill.ToolTip(1, false));
                                    }
                                }
                                GUILayout.EndVertical(); //END INFO
                            }
                            GUILayout.EndHorizontal(); //END ENTRY
                        }
                        */
                        //foreach (ScriptableSkillSet skillset in ((SkillBookItem)target).skillSets) //TODO: This cast should be done in OnEnable and cached for performance
                        //{
                        foreach (ScriptableSkill skill in editTarget._skills)
                        {
                            GUILayout.BeginHorizontal(); //BEGIN ENTRY
                            {
                                //ICON
                                if (showicons)
                                {
                                    GUILayout.BeginVertical("box"); //BEGIN ICON

                                    DrawIcon(skill.image);

                                    GUILayout.EndVertical(); //END ICON
                                }

                                GUILayout.BeginVertical(); //BEGIN INFO
                                {
                                    //NAME
                                    if (shownames)
                                    {
                                        DrawName(skill.name);
                                    }

                                    //CATEGORY
                                    if (showcategories && skill.requiredWeaponCategory != string.Empty)
                                    {
                                        DrawCategory(skill.requiredWeaponCategory.Trim("Weapon".ToCharArray()));
                                    }

                                    //TOOLTIP
                                    if (showtooltips && skill.ToolTip(1, false) != string.Empty)
                                    {
                                        DrawTooltip(skill.ToolTip(1, false));
                                    }
                                }
                                GUILayout.EndVertical(); //END INFO
                            }
                            GUILayout.EndHorizontal(); //END ENTRY
                        }
                        //}
                    }
                    GUILayout.EndScrollView(); //END SKILLS SCROLL LIST
                }
                GUILayout.EndVertical(); //END SKILLS BOX
                #endregion
            }
            #endregion

            #region S E T U P   T A B
            if (showsetup)
            {
                GUILayout.BeginVertical("box"); //BEGIN SETUP BOX
                {
                    GUILayout.Space(20f);

                    DrawDefaultInspector();//base.DrawDefaultInspector();

                    GUILayout.Space(20f);
                }
                GUILayout.EndVertical(); //END SETUP BOX
            }
            #endregion
        }
    }
}