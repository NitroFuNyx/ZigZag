using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public Vector3 startGameOffset = new Vector3(0, 0, -6.5f);
    private Vector3 _velocity = Vector3.zero;

    private void Update()
    {
        if (target.position.y >= -0.9f) Follow();
    }

    private void Follow()
    {
        var desiredPosition = target.position + offset + startGameOffset;
        var smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, smoothSpeed);
        transform.position = smoothedPosition;
    }
}