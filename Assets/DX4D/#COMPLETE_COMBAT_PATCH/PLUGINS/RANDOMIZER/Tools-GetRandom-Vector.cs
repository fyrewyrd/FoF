using UnityEngine;

namespace DX4D.Tools
{
    public partial class GetRandom
    {
        public static Vector3 Vector(float minimum, float maximum)
        {
            Vector3 vec = new Vector3();
            vec.x = Random.Range(minimum, maximum);
            vec.y = Random.Range(minimum, maximum);
            vec.z = Random.Range(minimum, maximum);
            return vec;
        }
    }
}
