namespace DX4D.Tools
{
    public partial class GetHex
    {
        public static string FromElement(Element element)
        {
            return GetHex.FromColor(GetColor.FromElement(element));
        }
    }
}
