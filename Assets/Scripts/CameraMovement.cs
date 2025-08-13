using UnityEngine;

public class CameraMovement : MonoBehaviour {
    public float xRange = 14f;           // Horizontal movement limit on X-axis
    public float zRangeLow = 0f;         // Lower bound limit on Z-axis
    public float zRangeHigh = 20f;       // Upper bound limit on Z-axis
    public float yRangeLow = 8f;         // Minimum height of the camera on Y-axis
    public float yRangeHigh = 20f;       // Maximum height of the camera on Y-axis

    public float moveSpeed = 10f;        // Base speed of camera movement
    public float scrollSpeed = 100f;     // Speed of zoom (mouse scroll)
    public float smoothing = 5f;         // Smoothing factor for interpolation

    private Vector3 _targetPosition;     // Desired target position of the camera

    private void Start() {
        _targetPosition = new Vector3(0f, 15f, 10f);    // Initial camera position
        transform.position = _targetPosition;
    }

    public void Update() {
        float x = Input.GetAxis("Horizontal");          // A/D or Left/Right keys
        float z = Input.GetAxis("Vertical");            // W/S or Up/Down keys
        float scroll = Input.GetAxis("Mouse ScrollWheel");  // Mouse scroll input

        // Calculate movement direction based on input
        Vector3 move = new Vector3(-x, 0f, -z) * (moveSpeed * Time.deltaTime);

        // Apply scroll to vertical Y movement
        float zoom = -scroll * scrollSpeed * Time.deltaTime;

        _targetPosition += move + new Vector3(0f, zoom, 0f);

        // Clamp camera within allowed bounds
        _targetPosition.x = Mathf.Clamp(_targetPosition.x, -xRange, xRange);
        _targetPosition.y = Mathf.Clamp(_targetPosition.y, yRangeLow, yRangeHigh);
        _targetPosition.z = Mathf.Clamp(_targetPosition.z, zRangeLow, zRangeHigh);

        // Smoothly interpolate current position toward target position
        transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * smoothing);
    }
}