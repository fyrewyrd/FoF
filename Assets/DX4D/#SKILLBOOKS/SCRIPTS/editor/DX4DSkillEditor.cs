using UnityEngine;
using UnityEditor;

namespace DX4D
{
    public enum SkillViewTab { VIEW, SETUP }

    public abstract class DX4DSkillEditor : Editor
    {
        public abstract string playerPrefs { get; }
        public virtual string defaultIconPath { get { return "icons/SkillBook"; } }

        float topMargin = 10f;
        float labelWidth = 150f;

        float iconScale = 1f;
        float iconPadding = 2f;
        //CONTENT
        float optionButtonHeight = 20f;

        //VIEWS
        internal bool showview = false;
        internal bool showsetup = false;
        internal bool showoptions = false;
        internal bool showicons = true;
        internal bool shownames = true;
        internal bool showcategories = true;
        internal bool showtooltips = false;

        //BUTTONS
        internal Color defaultcolor = Color.white;
        internal Color activebuttoncolor = Color.green;
        internal Color inactivebuttoncolor = Color.gray;
        //GUIStyle style = new GUIStyle(GUI.skin.button);

        GUIContent optionsLabel;
        //GUIContent setupLabel;

        //TOOLBAR
        SkillViewTab activeTab = SkillViewTab.VIEW;

        //ENABLE
        private void OnEnable()
        {
            defaultcolor = GUI.contentColor;
            //optionsLabel = new GUIContent(Resources.Load<Sprite>("icons/options").texture);
            //setupLabel = new GUIContent(Resources.Load<Sprite>("icons/setup").texture);
            optionsLabel = new GUIContent("OPTIONS");
            //setupLabel = new GUIContent("SETUP");
            activeTab = (SkillViewTab)PlayerPrefs.GetInt(playerPrefs + ":activetab", 0);
            showoptions = (PlayerPrefs.GetInt(playerPrefs + ":showoptions", 0) > 0) ? true : false;

            showicons = (PlayerPrefs.GetInt(playerPrefs + ":showicons", 1) > 0) ? true : false;
            shownames = (PlayerPrefs.GetInt(playerPrefs + ":shownames", 1) > 0) ? true : false;
            showcategories = (PlayerPrefs.GetInt(playerPrefs + ":showcategories", 1) > 0) ? true : false;
            showtooltips = (PlayerPrefs.GetInt(playerPrefs + ":showtooltips", 1) > 0) ? true : false;
        }
        void ResetTabs()
        {
            showview = false;
            showsetup = false;
        }

        protected virtual void DrawToolbar()
        {
            GUILayout.BeginHorizontal("box"); //BEGIN TOOLBAR
            activeTab = (SkillViewTab)GUILayout.Toolbar((int)activeTab, System.Enum.GetNames(typeof(SkillViewTab))); //TODO: This can be further optimized
            GUILayout.EndHorizontal(); //END TOOLBAR

            ResetTabs(); //RESET ACTIVE TABS
            switch (activeTab)
            {
                case SkillViewTab.VIEW: { showview = true; break; }
                case SkillViewTab.SETUP: { showsetup = true; break; }
            }

            PlayerPrefs.SetInt(playerPrefs + ":activetab", (int)activeTab);
        }
        protected virtual void DrawOptionsButtons()
        {
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
                    PlayerPrefs.SetInt(playerPrefs + ":showicons", (showicons) ? 1 : 0);
                }

                //SHOW NAMES BUTTON
                if (!shownames) GUI.backgroundColor = inactivebuttoncolor;
                else GUI.backgroundColor = activebuttoncolor;
                if (GUILayout.Button("show names",
                //GUILayout.Width(thumbnailWidth),
                GUILayout.Height(optionButtonHeight)))
                {
                    shownames = !shownames;
                    PlayerPrefs.SetInt(playerPrefs + ":shownames", (shownames) ? 1 : 0);
                }

                //SHOW CATEGORIES BUTTON
                if (!showcategories) GUI.backgroundColor = inactivebuttoncolor;
                else GUI.backgroundColor = activebuttoncolor;
                if (GUILayout.Button("show categories",
                //GUILayout.Width(thumbnailWidth),
                GUILayout.Height(optionButtonHeight)))
                {
                    showcategories = !showcategories;
                    PlayerPrefs.SetInt(playerPrefs + ":showcategories", (showcategories) ? 1 : 0);
                }

                //SHOW TOOLTIPS BUTTON
                if (!showtooltips) GUI.backgroundColor = inactivebuttoncolor;
                else GUI.backgroundColor = activebuttoncolor;
                if (GUILayout.Button("show tooltips",
                //GUILayout.Width(thumbnailWidth),
                GUILayout.Height(optionButtonHeight)))
                {
                    showtooltips = !showtooltips;
                    PlayerPrefs.SetInt(playerPrefs + ":showtooltips", (showtooltips) ? 1 : 0);
                }

                GUI.backgroundColor = defaultcolor; //RETURN TO DEFAULT COLOR

                GUILayout.EndHorizontal(); //END OPTIONS BOX
            }
            EditorGUILayout.EndToggleGroup(); //END OPTIONS GROUP

            PlayerPrefs.SetInt(playerPrefs + ":showoptions", (showoptions) ? 1 : 0);
        }
        protected virtual void DrawIcon(Sprite image)
        {
            if (image == null) image = Resources.Load<Sprite>(defaultIconPath);
            GUILayout.Label(image.texture
            , GUILayout.Width((iconPadding + image.texture.width) * iconScale)
            , GUILayout.Height((iconPadding + image.texture.height) * iconScale)
            );
        }
        protected virtual void DrawName(string nameLabel)
        {
            GUILayout.Space(topMargin);
            GUILayout.Label(nameLabel,
              new GUIStyle() { fontStyle = FontStyle.Bold },
              GUILayout.Width(labelWidth));
        }
        protected virtual void DrawCategory(string nameLabel)
        {
            GUILayout.Label(nameLabel,
                GUILayout.Width(labelWidth));
        }
        protected virtual void DrawTooltip(string tooltip)
        {
            EditorGUILayout.HelpBox(tooltip, MessageType.None);
        }
    }
}