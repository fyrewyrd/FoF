using UnityEngine;

namespace DX4D.Tools
{
    public partial class GetColor
    {
        public static Color FromRarity(GearRarity rarity)
        {
            switch (rarity)
            {
                case GearRarity.Common:
                    {
                        return Config.Text.Rarity.commonColor;
                    }
                case GearRarity.Uncommon:
                    {
                        return Config.Text.Rarity.uncommonColor;
                    }
                case GearRarity.Rare:
                    {
                        return Config.Text.Rarity.rareColor;
                    }
                case GearRarity.Epic:
                    {
                        return Config.Text.Rarity.epicColor;
                    }
                case GearRarity.Legendary:
                    {
                        return Config.Text.Rarity.legendaryColor;
                    }
                case GearRarity.Unique:
                    {
                        return Config.Text.Rarity.uniqueColor;
                    }
                default:
                    {
                        return Color.white;
                    }
            }
        }
    }
}
