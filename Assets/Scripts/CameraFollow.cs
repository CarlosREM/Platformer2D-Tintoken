using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private Vector3 offset;
    [SerializeField] private float yDeadzone = -10f;
    [SerializeField] private bool xCorrection = true;

    [SerializeField] [Range(0.1f, 1f)]
    private float smoothSpeed = 0.125f;
    private Vector3 velocity = Vector3.zero;

    private void LateUpdate() {
        Vector3 desiredPosition = target.position + offset;
        if (desiredPosition.y <= yDeadzone)
            desiredPosition.y = yDeadzone;

        if (offset.x != 0 && xCorrection)
            CorrectOffsetX();
        

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
    }

    private void CorrectOffsetX() {
        bool flipX = target.GetComponent<SpriteRenderer>().flipX;
        if ( (offset.x > 0 && flipX) ||
             (offset.x < 0 && !flipX))
            offset.x *= -1;

    }
}
