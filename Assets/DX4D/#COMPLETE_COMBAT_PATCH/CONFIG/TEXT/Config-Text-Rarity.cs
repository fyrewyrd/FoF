using UnityEngine;
using System;

public partial class Config {
    // R A R I T Y  C O L O R S
    public static partial class Text
    {
        public static partial class Rarity
        {
            public static float rarityTextAlpha = 1.0f;
            //                                          red  green  blue
            public static Color commonColor = new Color(0.7f, 0.7f, 0.7f, rarityTextAlpha); //light gray
            public static Color uncommonColor = new Color(1.0f, 1.0f, 1.0f, rarityTextAlpha); //white
            public static Color rareColor = new Color(0.0f, 0.4f, 0.7f, rarityTextAlpha); //blue
            public static Color epicColor = new Color(0.5f, 0.0f, 0.7f, rarityTextAlpha); //purple
            public static Color uniqueColor = new Color(1.0f, 0.9f, 0.1f, rarityTextAlpha); //yellow;
            public static Color legendaryColor = new Color(1.0f, 0.7f, 0.0f, rarityTextAlpha);//orange
        }
    }
}
