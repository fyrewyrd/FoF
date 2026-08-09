using UnityEngine;
using UnityEditor;

namespace DX4D
{
    [CustomEditor(typeof(SkillBookItem))]
    public class SkillBookItemEditor : DX4DSkillEditor
    {
        public override string playerPrefs => "SkillBookItem";

        SkillBookItem _editTarget = null;
        SkillBookItem editTarget
        {
            get
            {
                if (!_editTarget) _editTarget = (SkillBookItem)target;
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
                                    if (showtooltips && editTarget.ToolTip() != string.Empty)
                                    {
                                        DrawTooltip(editTarget.teachesSkill.ToolTip(1, false));
                                    }
                                }
                                GUILayout.EndVertical(); //END INFO
                            }
                            GUILayout.EndHorizontal(); //END ENTRY
                        }

                        foreach (ScriptableSkillSet skillset in editTarget.skillSets)// ((SkillBookItem)target).skillSets) //TODO: This cast should be done in OnEnable and cached for performance
                        {
                            foreach (ScriptableSkill skill in skillset._skills)
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
                                        if (showtooltips)
                                        {
                                            DrawTooltip(skill.ToolTip(1, false));
                                        }
                                    }
                                    GUILayout.EndVertical(); //END INFO
                                }
                                GUILayout.EndHorizontal(); //END ENTRY
                            }
                        }
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