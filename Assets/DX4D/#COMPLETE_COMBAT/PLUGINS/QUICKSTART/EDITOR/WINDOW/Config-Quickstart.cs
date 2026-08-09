using UnityEditor;
using UnityEngine;

namespace DX4D
{
    // C O N F I G U R A T I O N
    public partial class QuickstartWindow : EditorWindow
    {
        // loading options
        const bool loadOnStartup = false; //load whenever the editor is reloaded?
        const bool dockable = false; //can it be docked to other elements in the editor?
        const string menuPath = "Tools/DX4D/QUICKSTART";
        const string windowName = "DX4D - Complete Combat - Automatic Scene Configuration";
        private void OnEnable()
        {
            AssetDatabase.Refresh(); // Make sure we are using the latest assets

            // header config
            //headerCompanyLabel = "DX4D";
            headerApplicationLabel = "QUICKSTART";
            headerLabelStyle = new GUIStyle(EditorStyles.largeLabel);
            headerLabelStyle.alignment = TextAnchor.MiddleCenter;
            headerLabelStyle.fontSize = 24;
            headerLabelStyle.stretchWidth = true;
            // button config
            buttonStyle = new GUIStyle(EditorStyles.miniButton);
            buttonStyle.alignment = TextAnchor.MiddleCenter;
            buttonStyle.fontSize = 18;
            buttonStyle.stretchWidth = true;
            // widget color config
            windowBackgroundColor = Color.black;
            buttonBorderColor = Color.black;
            installedButtonColor = Color.green;
            notInstalledButtonColor = Color.red;

            // H O O K  F O R  P A T C H  R E G I S T R A T I O N
            Invoker.InvokeMany(typeof(QuickstartWindow), this, "OnEnable_");
        }
        
        #region V A R I A B L E  D E C L A R A T I O N S
        // scroll view
        Vector2 scrollViewPosition;
        // labels
        //static string headerCompanyLabel = "";
        static string headerApplicationLabel = "Patch Manager";
        // widget colors
        static Color windowBackgroundColor = Color.white;
        static Color buttonBorderColor = Color.white;
        static Color installedButtonColor = Color.green;
        static Color notInstalledButtonColor = Color.red;
        // styles
        GUIStyle headerLabelStyle = new GUIStyle();// EditorStyles.largeLabel);
        GUIStyle buttonStyle = new GUIStyle();// EditorStyles.miniButton);
        #endregion
    }
}
