using UnityEditor;
using UnityEngine;

namespace DX4D
{
    // C O N F I G U R A T I O N
    public partial class PatchManagerWindow : EditorWindow
    {
        // loading options
        const bool loadOnStartup = false; //load whenever the editor is reloaded?
        const bool dockable = false; //can it be docked to other elements in the editor?
        const string menuPath = "Tools/DX4D/PATCHER";
        const string windowName = "DX4D PATCHER";
        private void OnEnable()
        {
            AssetDatabase.Refresh(); // Make sure we are using the latest assets

            // header config
            headerCompanyLabel = " D X 4 D ";
            headerPatcherNameLabel = " P A T C H  M A N A G E R ";
            headerLabelStyle = new GUIStyle(EditorStyles.largeLabel);
            headerLabelStyle.alignment = TextAnchor.MiddleCenter;
            headerLabelStyle.fontSize = 24;
            headerLabelStyle.stretchWidth = true;
            // button config
            patchButtonStyle = new GUIStyle(EditorStyles.miniButton);
            patchButtonStyle.alignment = TextAnchor.MiddleCenter;
            patchButtonStyle.fontSize = 18;
            patchButtonStyle.stretchWidth = true;
            // widget color config
            patchWindowBackgroundColor = Color.black;
            patchButtonBorderColor = Color.black;
            patchInstalledButtonColor = Color.green;
            patchNotInstalledButtonColor = Color.red;

            // H O O K  F O R  P A T C H  R E G I S T R A T I O N
            Invoker.InvokeMany(typeof(PatchManagerWindow), this, "OnEnable_");
        }
        
        #region V A R I A B L E  D E C L A R A T I O N S
        // plugin list
        Vector2 pluginViewScrollPosition;
        // labels
        static string headerCompanyLabel = "";
        static string headerPatcherNameLabel = "Patch Manager";
        // widget colors
        static Color patchWindowBackgroundColor = Color.white;
        static Color patchButtonBorderColor = Color.white;
        static Color patchInstalledButtonColor = Color.green;
        static Color patchNotInstalledButtonColor = Color.red;
        // styles
        GUIStyle headerLabelStyle = new GUIStyle();// EditorStyles.largeLabel);
        GUIStyle patchButtonStyle = new GUIStyle();// EditorStyles.miniButton);
        #endregion
    }
}
