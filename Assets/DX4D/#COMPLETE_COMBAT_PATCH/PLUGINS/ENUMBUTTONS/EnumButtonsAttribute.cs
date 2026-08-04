using UnityEngine;

public class EnumButtonsAttribute : PropertyAttribute
{
    public string enumName;

    public EnumButtonsAttribute() { }

    public EnumButtonsAttribute(string name)
    {
        enumName = name;
    }
}
