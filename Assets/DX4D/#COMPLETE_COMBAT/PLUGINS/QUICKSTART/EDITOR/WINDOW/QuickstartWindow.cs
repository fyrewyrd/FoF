using UnityEditor;
using UnityEngine;

namespace DX4D
{
    public partial class QuickstartWindow : EditorWindow
    {
        //bool modifyPrefabs = true;

        // U P D A T E  G U I
        void OnGUI()
        {
            //BACKGROUND
            GUI.color = windowBackgroundColor;

            // H E A D E R
            GUI.color = Color.gray;
            GUILayout.BeginVertical("box");
            {
                //if (headerCompanyLabel != string.Empty) GUILayout.Label(headerCompanyLabel, headerLabelStyle);
                if (headerApplicationLabel != string.Empty) GUILayout.Label(headerApplicationLabel, headerLabelStyle);
            }
            GUILayout.EndVertical();
            GUI.color = windowBackgroundColor;

            GUILayout.BeginVertical(EditorStyles.helpBox);
            {
                //SETUP
                GUILayout.BeginHorizontal();
                {
                    GUI.color = Color.white;
                    Quickstart.modifyPrefabs = EditorGUILayout.Toggle("Modify Prefabs", Quickstart.modifyPrefabs);
                    Quickstart.modifyScene = EditorGUILayout.Toggle("Modify Scene", Quickstart.modifyScene);
                    GUI.color = windowBackgroundColor;

                    //if (modifyPrefabs)
                    //    if (GUILayout.Button("Close"))
                    //        this.Close();
                }
                GUILayout.EndHorizontal();
                //BUTTON
                GUILayout.BeginVertical();
                {
                    GUI.color = Color.grey;
                    if (GUILayout.Button(new GUIContent("COMPLETE COMBAT QUICKSTART", "Searches through the scene and adds Character Sheets where appropriate."), buttonStyle))
                    {
                        Quickstart.SetupScene();
#if UNITY_EDITOR
                        Debug.Log("<color=green><b>QUICKSTART COMPLETE</b></color>"); //DEBUG
#endif
                    }
                    GUI.color = windowBackgroundColor;
                }
                GUILayout.EndVertical();


                /*
                // P A T C H  L I S T  S C R O L L  V I E W  
                scrollViewPosition = GUILayout.BeginScrollView(scrollViewPosition);
                {
                    if (PatchManager.registeredPatches == null) return;

                    // P O P U L A T E  P A T C H  L I S T
                    foreach (Patch patch in PatchManager.registeredPatches)
                    {
                        GUI.color = buttonBorderColor;
                        GUILayout.BeginHorizontal(EditorStyles.helpBox);
                        {

                            // PATCH INFO
                            string patchName = patch.name.Replace(" Patch", "");
                            string patchVersion = " - v" + patch.version.ToString() + (patch.version % 1 == 0 ? ".0" : "");
                            string patchDescription = patch.description;
                            string patchNameAndDescription = patchName + patchVersion + "\n" + patchDescription;

                            // APPLY & REMOVE PATCH BUTTONS
                            bool installed = PatchManager.PatchIsActivated(patch);

                            GUI.color = installed ? installedButtonColor : notInstalledButtonColor;

                            if (!installed && GUILayout.Button(new GUIContent(patchName, patchNameAndDescription), buttonStyle))
                            {
                                //patchDescriptionHotspot = GUILayoutUtility.GetLastRect();
                                //patch.installed = PatchManager.Activate(patch);
                                ;
                                Debug.Log(patchName + " " + (PatchManager.Activate(patch) ? "<color=green><b>INSTALLED</b></color>" : "<color=red><b>NOT INSTALLED</b></color>")); //DEBUG
                            }
                            if (installed && GUILayout.Button(new GUIContent(patchName + " (installed)", patchNameAndDescription), buttonStyle))
                            {
                                //patchDescriptionHotspot = GUILayoutUtility.GetLastRect();
                                //patch.installed = !PatchManager.Deactivate(patch);
                                ;
                                Debug.Log(patchName + " " + (PatchManager.Deactivate(patch) ? "<color=green><b>UNINSTALLED</b></color>" : "<color=red><b>NOT UNINSTALLED</b></color>")); //DEBUG
                            }
                        }
                        GUILayout.EndHorizontal();
                    }
                }
                GUILayout.EndScrollView();
                */
            }
            GUILayout.EndVertical();
        }

        // M E N U
        [MenuItem(menuPath)]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow<QuickstartWindow>(!dockable, windowName, true);
        }

        // I N I T I A L I Z E  O N  L O A D
        [InitializeOnLoadMethod]
        static void Initialize()
        {
            EditorApplication.delayCall += LoadWindow;
        }
        static void LoadWindow()
        {
            //We disable warnings here to prevent an "unreachable code" warning from appearing in the inspector
            if (loadOnStartup)
            {
#pragma warning disable
                EditorApplication.delayCall += ShowWindow;
#pragma warning restore
            }
            //EditorApplication.ExecuteMenuItem(menuPath);
        }
    }
}
/*
 private static Texture2D TextureField(string name, Texture2D texture)
 {
     GUILayout.BeginVertical();
     var style = new GUIStyle(GUI.skin.label);
     style.alignment = TextAnchor.UpperCenter;
     style.fixedWidth = 70;
     GUILayout.Label(name, style);
     var result = (Texture2D)EditorGUILayout.ObjectField(texture, typeof(Texture2D), false, GUILayout.Width(70), GUILayout.Height(70));
     GUILayout.EndVertical();
     return result;
 }
 */
/* 
                //if (patchInstalled) GUILayout.Label("INSTALLED SUCCESSFULLY");

                //OLD
                //patchInstalled = EditorGUILayout.Toggle("Installed", PatchManager.PatchIsActivated(patch));
                //if (patchInstalled && !PatchManager.PatchIsActivated(patch)) { PatchManager.Activate(patch); }
                //else if (!patchInstalled && PatchManager.PatchIsActivated(patch)) { PatchManager.Deactivate(patch); }
//EXAMPLES
myString = EditorGUILayout.TextField("Text Field", myString);

groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
myBool = EditorGUILayout.Toggle("Toggle", myBool);
myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
EditorGUILayout.EndToggleGroup();
*/
