/* //DEPRECIATED
using UnityEngine;
using System;

// T E X T  M O T I O N
[Serializable] public class DynamicTextMotion
{
    [SerializeField] const float forcelimit = 10f;

    [Header("TEXT MOTION")]
    //public bool randomizeStatusTextLocation = false;
    //public bool randomizeDamageDirection = true;

    [SerializeField] [Range(-forcelimit, forcelimit)] float gravityMultiplier = 1.0f;
    [SerializeField] [Range(-forcelimit, forcelimit)] float forceMultiplier = 1.0f;
    [SerializeField] [Range(-forcelimit, forcelimit)] float randomization = 0.0f;
    [SerializeField] Vector3 _velocity = Vector3.down;
    public Vector3 velocity
    {
        get
        {
            Vector3 v = _velocity;

            // R A N D O M  T E X T  V E L O C I T Y
            if (randomization > 0)
            {
                v = v + DX4D.Tools.GetRandomVector(-randomization, randomization);
            }

            v.y = v.y * gravityMultiplier;

            v = v * forceMultiplier;

            return (v);
        }
    }
}
*/
