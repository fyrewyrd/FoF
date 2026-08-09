using UnityEngine;

namespace DX4D.Tools
{
    public partial class GetHex
    {
        public static string FromColor(Color color)
        {
            return "#" + ColorUtility.ToHtmlStringRGBA(color);
        }
    }
}
