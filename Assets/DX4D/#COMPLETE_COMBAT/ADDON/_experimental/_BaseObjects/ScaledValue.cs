[System.Serializable]
public struct ScaledValue
{
    public int baseValue;
    public int baseBonus;
    public int scaledBonus;
    public int scale;
    public int total { get { return (baseValue + scaledBonus) * (scale - 1) + (baseBonus); } }
}
