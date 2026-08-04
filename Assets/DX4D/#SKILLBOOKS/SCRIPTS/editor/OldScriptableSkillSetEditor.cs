/* //DEPRECIATED
using UnityEngine;
using UnityEditor;

public enum ViewTab { VIEW, SETUP }

[CustomEditor(typeof(ScriptableSkillSet))]
public class ScriptableSkillSetEditor : Editor
{
    float topMargin = 10f;

    float optionButtonHeight = 20f;
    float labelWidth = 150f;

    float iconScale = 1f;
    float iconPadding = 2f;

    bool showicons = true;
    bool shownames = true;
    bool showcategories = true;
    bool showtooltips = false;

    Vector2 scrollPos = Vector2.zero;

    ViewTab activeTab = ViewTab.VIEW;
    bool showview = false;
    bool showsetup = false;
    bool showoptions = false;

    Color defaultcolor = Color.white;
    Color activebuttoncolor = Color.green;
    Color inactivebuttoncolor = Color.gray;
    //GUIStyle style = new GUIStyle(GUI.skin.button);

    GUIContent optionsLabel;
    //GUIContent setupLabel;

    //ENABLE
    private void OnEnable()
    {
        defaultcolor = GUI.contentColor;
        //optionsLabel = new GUIContent(Resources.Load<Sprite>("icons/options").texture);
        //setupLabel = new GUIContent(Resources.Load<Sprite>("icons/setup").texture);
        optionsLabel = new GUIContent("OPTIONS");
        //setupLabel = new GUIContent("SETUP");
        activeTab = (ViewTab)PlayerPrefs.GetInt("ScriptableSkillSet:activetab", 0);
        showoptions = (PlayerPrefs.GetInt("ScriptableSkillSet:showoptions", 0) > 0) ? true : false;

        showicons = (PlayerPrefs.GetInt("ScriptableSkillSet:showicons", 1) > 0) ? true : false;
        shownames = (PlayerPrefs.GetInt("ScriptableSkillSet:shownames", 1) > 0) ? true : false;
        showcategories = (PlayerPrefs.GetInt("ScriptableSkillSet:showcategories", 1) > 0) ? true : false;
        showtooltips = (PlayerPrefs.GetInt("ScriptableSkillSet:showtooltips", 1) > 0) ? true : false;
    }
    void ResetTabs()
    {
        showview = false;
        showsetup = false;
    }
    //INSPECTOR GUI
    public override void OnInspectorGUI()
    {
        //TOOLBAR
        GUILayout.BeginHorizontal("box"); //BEGIN TOOLBAR
        activeTab = (ViewTab)GUILayout.Toolbar((int)activeTab, System.Enum.GetNames(typeof(ViewTab))); //TODO: This can be further optimized
        GUILayout.EndHorizontal(); //END TOOLBAR

        ResetTabs(); //RESET ACTIVE TABS
        switch (activeTab)
        {
            case ViewTab.VIEW: { showview = true; break; }
            case ViewTab.SETUP: { showsetup = true; break; }
        }
        
        PlayerPrefs.SetInt("ScriptableSkillSet:activetab", (int)activeTab);
        
        // V I E W
        if (showview)
        {
            // O P T I O N S
            if (showoptions = EditorGUILayout.BeginToggleGroup(optionsLabel, showoptions)) //BEGIN OPTIONS GROUP
            {
                GUILayout.BeginHorizontal("box"); //BEGIN OPTIONS BOX

                //SHOW ICONS BUTTON
                if (!showicons) GUI.backgroundColor = inactivebuttoncolor;
                else GUI.backgroundColor = activebuttoncolor;
                if (GUILayout.Button("show icons",
                //GUILayout.Width(thumbnailWidth),
                GUILayout.Height(optionButtonHeight)))
                {
                    showicons = !showicons;
                    PlayerPrefs.SetInt("ScriptableSkillSet:showicons", (showicons) ? 1 : 0);
                }

                //SHOW NAMES BUTTON
                if (!shownames) GUI.backgroundColor = inactivebuttoncolor;
                else GUI.backgroundColor = activebuttoncolor;
                if (GUILayout.Button("show names",
                //GUILayout.Width(thumbnailWidth),
                GUILayout.Height(optionButtonHeight)))
                {
                    shownames = !shownames;
                    PlayerPrefs.SetInt("ScriptableSkillSet:shownames", (shownames) ? 1 : 0);
                }

                //SHOW CATEGORIES BUTTON
                if (!showcategories) GUI.backgroundColor = inactivebuttoncolor;
                else GUI.backgroundColor = activebuttoncolor;
                if (GUILayout.Button("show categories",
                //GUILayout.Width(thumbnailWidth),
                GUILayout.Height(optionButtonHeight)))
                {
                    showcategories = !showcategories;
                    PlayerPrefs.SetInt("ScriptableSkillSet:showcategories", (showcategories) ? 1 : 0);
                }

                //SHOW TOOLTIPS BUTTON
                if (!showtooltips) GUI.backgroundColor = inactivebuttoncolor;
                else GUI.backgroundColor = activebuttoncolor;
                if (GUILayout.Button("show tooltips",
                //GUILayout.Width(thumbnailWidth),
                GUILayout.Height(optionButtonHeight)))
                {
                    showtooltips = !showtooltips;
                    PlayerPrefs.SetInt("ScriptableSkillSet:showtooltips", (showtooltips) ? 1 : 0);
                }

                GUI.backgroundColor = defaultcolor; //RETURN TO DEFAULT COLOR

                GUILayout.EndHorizontal(); //END OPTIONS BOX
            }
            EditorGUILayout.EndToggleGroup(); //END OPTIONS GROUP

            PlayerPrefs.SetInt("ScriptableSkillSet:showoptions", (showoptions) ? 1 : 0);
            // S K I L L S
            //GUILayout.Space(20f);
            //GUILayout.Label("SKILLS");

            GUILayout.BeginVertical(); //BEGIN SKILLS BOX

            scrollPos = GUILayout.BeginScrollView(scrollPos); //BEGIN SKILLS SCROLL LIST

            foreach (ScriptableSkill skill in ((ScriptableSkillSet)target)._skills)
            {
                GUILayout.BeginHorizontal(); //BEGIN SKILL ENTRY

                //ICON
                if (showicons)
                {
                    GUILayout.BeginVertical("box"); //BEGIN ICON

                    if (skill.image == null) skill.image = Resources.Load<Sprite>("icons/SkillBook");
                    GUILayout.Label(skill.image.texture
                    , GUILayout.Width((iconPadding + skill.image.texture.width) * iconScale)
                    , GUILayout.Height((iconPadding + skill.image.texture.height) * iconScale)
                    );

                    GUILayout.EndVertical(); //END ICON
                }
                
                //GUILayout.BeginVertical(skill.image.texture, "box"); //BEGIN SKILL INFO
                GUILayout.BeginVertical(); //BEGIN SKILL INFO

                //NAME
                if (shownames)
                {
                    GUILayout.Space(topMargin);
                    GUILayout.Label(skill.name,
                      new GUIStyle() { fontStyle = FontStyle.Bold },
                      GUILayout.Width(labelWidth));
                }

                //CATEGORY
                if (showcategories)
                {
                    //category
                    if (skill.requiredWeaponCategory != string.Empty)
                    {
                        GUILayout.Label(skill.requiredWeaponCategory.Trim("Weapon".ToCharArray()),
                            GUILayout.Width(labelWidth));
                    }
                    //level req
                    //if (skill.maxLevel > 0)
                    //{
                    //    GUILayout.Label( "max level " + skill.maxLevel.ToString() ,
                    //        GUILayout.Width(labelWidth));
                    //}
                }

                //TOOLTIP
                if (showtooltips) EditorGUILayout.HelpBox(skill.ToolTip(1, false), MessageType.None);

                GUILayout.EndVertical(); //END SKILL INFO
                GUILayout.EndHorizontal(); //END SKILL ENTRY

            }

            GUILayout.EndScrollView(); //END SKILLS SCROLL LIST
            GUILayout.EndVertical(); //END SKILLS BOX
        }
        

        // S E T U P
        if (showsetup)// = EditorGUILayout.BeginToggleGroup(setupLabel, showsetup)) //BEGIN SETUP GROUP
        {

            GUILayout.BeginVertical("box"); //BEGIN SETUP BOX
            GUILayout.Space(20f);

            DrawDefaultInspector();//base.DrawDefaultInspector();

            GUILayout.Space(20f);
            GUILayout.EndVertical(); //END SETUP BOX
        }
        //EditorGUILayout.EndToggleGroup(); //END SETUP GROUP
    }
}
*/