using UnityEngine;

#if !RPG2D
[RequireComponent(typeof(Rigidbody))]
public class Velocity3D : MonoBehaviour
{
    public Rigidbody rigidBody;
    public Vector3 velocity;

    void Start()
    {
        rigidBody.velocity = velocity;
    }
}
#endif
