using UnityEngine;

public class AlwaysPointUp : MonoBehaviour
{
    void LateUpdate()
    {
        transform.up = Vector3.up;
    }
}
