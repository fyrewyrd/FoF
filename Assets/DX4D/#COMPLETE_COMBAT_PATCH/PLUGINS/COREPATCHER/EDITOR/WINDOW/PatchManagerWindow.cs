using UnityEditor;
using UnityEngine;

namespace DX4D
{
    public partial class PatchManagerWindow : EditorWindow
    {
        // U P D A T E  G U I
        void OnGUI()
        {
            GUI.color = patchWindowBackgroundColor;
            GUILayout.BeginVertical(EditorStyles.helpBox);
            {
                // H E A D E R
                GUILayout.Label(headerCompanyLabel + "\n" + headerPatcherNameLabel, headerLabelStyle);

                // P A T C H  L I S T  S C R O L L  V I E W  
                pluginViewScrollPosition = GUILayout.BeginScrollView(pluginViewScrollPosition);
                {
                    if (PatchManager.registeredPatches == null) return;

                    // P O P U L A T E  P A T C H  L I S T
                    foreach (Patch patch in PatchManager.registeredPatches)
                    {
                        GUI.color = patchButtonBorderColor;
                        GUILayout.BeginHorizontal(EditorStyles.helpBox);
                        {

                            // PATCH INFO
                            string patchName = patch.name.Replace(" Patch", "");
                            string patchVersion = " - v" + patch.version.ToString() + (patch.version % 1 == 0 ? ".0" : "");
                            string patchDescription = patch.description;
                            string patchNameAndDescription = patchName + patchVersion + "\n" + patchDescription;

                            // APPLY & REMOVE PATCH BUTTONS
                            bool installed = PatchManager.PatchIsActivated(patch);
                            
                            GUI.color = installed ? patchInstalledButtonColor : patchNotInstalledButtonColor;

                            if (!installed && GUILayout.Button(new GUIContent(patchName, patchNameAndDescription), patchButtonStyle))
                            {
                                //patchDescriptionHotspot = GUILayoutUtility.GetLastRect();
                                //patch.installed = PatchManager.Activate(patch);
                                ;
                                Debug.Log(patchName + " " + (PatchManager.Activate(patch) ? "<color=green><b>INSTALLED</b></color>" : "<color=red><b>NOT INSTALLED</b></color>")); //DEBUG
                            }
                            if (installed && GUILayout.Button(new GUIContent(patchName + " (installed)", patchNameAndDescription), patchButtonStyle))
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
            }
            GUILayout.EndVertical();
        }

        // M E N U
        [MenuItem(menuPath)]
        public static void ShowWindow() { EditorWindow.GetWindow<PatchManagerWindow>(!dockable, windowName, true); }

        // I N I T I A L I Z E  O N  L O A D
        [InitializeOnLoadMethod]
        static void Initialize()
        {
            EditorApplication.delayCall += LoadPatcher;
        }
        static void LoadPatcher()
        {
            //We disable warnings here to prevent an "unreachable code" warning from appearing in the inspector
#pragma warning disable
            if (loadOnStartup) EditorApplication.delayCall += ShowWindow;
#pragma warning restore
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
