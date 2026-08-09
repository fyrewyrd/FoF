using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Velocity2D : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public Vector2 velocity;

    void Start()
    {
        rigidBody.velocity = velocity;
    }
}
