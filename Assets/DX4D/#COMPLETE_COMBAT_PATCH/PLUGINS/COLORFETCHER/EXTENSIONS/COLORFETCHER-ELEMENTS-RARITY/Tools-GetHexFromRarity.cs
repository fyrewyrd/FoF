namespace DX4D.Tools
{
    public partial class GetHex
    {
        public static string FromRarity(GearRarity rarity)
        {
            return GetHex.FromColor(GetColor.FromRarity(rarity));
        }
    }
}
