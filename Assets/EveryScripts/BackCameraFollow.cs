using UnityEngine;

public class BackCameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the capsule
    public float distance = 5.0f; // Distance behind the target
    public float height = 2.0f; // Height of the camera

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position - target.forward * distance + Vector3.up * height;
            transform.position = desiredPosition;
            transform.LookAt(target);
        }
    }
}
