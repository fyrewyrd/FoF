using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    Transform cam;

    void LateUpdate()
    {
        // Re-acquire the camera if it was destroyed (scene change)
        if (cam == null)
        {
            if (Camera.main == null) return;
            cam = Camera.main.transform;
        }

        transform.forward = cam.forward;
    }

    void OnBecameVisible()   { enabled = true; }
    void OnBecameInvisible() { enabled = false; }
}