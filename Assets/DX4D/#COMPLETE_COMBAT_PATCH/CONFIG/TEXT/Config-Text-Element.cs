using UnityEngine;

public partial class Config
{
    // E L E M E N T  C O L O R S
    public static partial class Text
    {
        public static partial class Element
        {
            public static float elementTextAlpha = 1.0f;
            // active   passive
            //    |        |
            // F I R E - I C E                              red  green  blue
            public static Color fireColor = new Color(0.7f, 0.0f, 0.0f, elementTextAlpha); // Color.red
            public static Color iceColor = new Color(0.0f, 1.0f, 1.0f, elementTextAlpha); // Color.cyan
            // L I G H T N I N G - W A T E R
            public static Color lightningColor = new Color(1.0f, 1.0f, 0.0f, elementTextAlpha);// Color.yellow
            public static Color waterColor = new Color(0.0f, 0.4f, 0.7f, elementTextAlpha); // Color.blue
            // A I R - E A R T H
            public static Color windColor = new Color(0.2f, 0.7f, 0.2f, elementTextAlpha); //green
            public static Color earthColor = new Color(0.5f, 0.25f, 0.0f, elementTextAlpha); //brown
            // D A R K - H O L Y
            public static Color arcaneColor = new Color(0.0f, 0.0f, 0.0f, elementTextAlpha); // Color.black
            public static Color holyColor = new Color(1.0f, 1.0f, 1.0f, elementTextAlpha);// Color.white
            public static Color runicColor = new Color(1.0f, 1.0f, 1.0f, elementTextAlpha);// Color.white
            public static Color ancientColor = new Color(1.0f, 1.0f, 1.0f, elementTextAlpha);// Color.white
            public static Color spiritColor = new Color(1.0f, 1.0f, 1.0f, elementTextAlpha);// Color.white
            // N E U T R A L
            public static Color neutralColor = new Color(1.0f, 1.0f, 1.0f, elementTextAlpha); // Color.white
        }
    }
}
