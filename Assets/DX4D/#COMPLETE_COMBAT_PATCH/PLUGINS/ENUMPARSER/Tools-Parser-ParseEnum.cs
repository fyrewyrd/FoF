using System;

namespace DX4D.Tools
{
    public partial class Parser
    {
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}