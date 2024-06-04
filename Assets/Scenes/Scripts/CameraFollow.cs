using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Referință către transformul jucătorului
    public float smoothSpeed = 0.125f; // Viteza la care camera se va mișca către poziția țintă
    public Vector3 offset; // Distanța dintre camera și jucător

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(target.position); // Camera se va orienta întotdeauna către jucător
        }
    }
}
