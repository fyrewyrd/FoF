using UnityEngine;

namespace DX4D.Tools
{
    public partial class GetColor
    {
        public static Color FromElement(Element element)
        {
            switch (element)
            {
                case Element.Neutral: return Config.Text.Element.neutralColor;
                case Element.Fire: return Config.Text.Element.fireColor;
                case Element.Ice: return Config.Text.Element.iceColor;
                case Element.Lightning: return Config.Text.Element.lightningColor;
                case Element.Water: return Config.Text.Element.waterColor;
                case Element.Wind: return Config.Text.Element.windColor;
                case Element.Earth: return Config.Text.Element.earthColor;
                case Element.Arcane: return Config.Text.Element.arcaneColor;
                case Element.Holy: return Config.Text.Element.holyColor;
                case Element.Runic: return Config.Text.Element.runicColor;
                case Element.Ancient: return Config.Text.Element.ancientColor;
                case Element.Spirit: return Config.Text.Element.spiritColor;
                default: return Color.white;
            }
        }
    }
}
