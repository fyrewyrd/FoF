/* //DEPRECIATED
using UnityEngine;
using System;

// T E X T  M O T I O N
[Serializable] public class TextMotionInfo
{

    public bool gravityEffectsPopupText = true;

    [Header("TEXT ANIMATION")]
    public bool randomizeTextDirection = false;
    public float velocityMultiplier = 1.0f;
    //public bool randomizeStatusTextLocation = false;
    //public bool randomizeDamageDirection = true;

    [SerializeField] Vector3 _velocity = Vector3.zero;
    public Vector3 velocity {
        get
        {
            // R A N D O M  T E X T  V E L O C I T Y
            if (randomizeTextDirection)
            {
                _velocity = DX4D.Tools.GetRandomVector(-velocityMultiplier, velocityMultiplier);
            }

            return (_velocity * velocityMultiplier);
        }
    }
}
*/
