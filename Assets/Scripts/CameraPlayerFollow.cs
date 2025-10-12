using UnityEngine;

public class CameraPlayerFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset = Vector3.zero; // Only X and Y offsets

    private Vector3 _velocity = Vector3.zero;
    private float _initialZ; // Store camera's initial Z

    private void Start()
    {
        // Capture the initial Z position of the camera
        _initialZ = transform.position.z;
    }

    private void FixedUpdate()
    {
        if (player == null)
            return;

        Vector3 desiredPosition = new Vector3(
            player.position.x + offset.x,
            player.position.y + offset.y,
            _initialZ // Keep initial Z
        );

        Vector3 smoothedPosition = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref _velocity,
            smoothSpeed
        );

        transform.position = smoothedPosition;
    }
}