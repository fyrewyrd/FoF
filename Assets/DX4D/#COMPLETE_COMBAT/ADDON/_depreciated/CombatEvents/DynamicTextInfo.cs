/* //DEPRECIATED
using UnityEngine;
using System;

// D Y N A M I C  T E X T
[Serializable] public class DynamicTextInfo
{
    public string text
    {
        get { return (textPrefix + textBody + textSuffix); }
        set { textPrefix = string.Empty; textBody = value; textSuffix = string.Empty; }
    }

    [Header("TEXT STRING")]
    [SerializeField] public string textPrefix;
    [SerializeField] public string textBody;
    [SerializeField] public string textSuffix;
}
*/
