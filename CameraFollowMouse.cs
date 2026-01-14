using UnityEngine;

public class CameraFollowMouse : MonoBehaviour
{
    public Transform player; // The object the camera follows
    public float sensitivity = 2.0f; // Mouse sensitivity
    public float verticalClamp = 45.0f; // Limit for vertical rotation

    private float rotationX = 0.0f;
    void Start()
    {
        // Hide and lock the cursor for gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Press Escape to release and show the cursor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Rotate the player horizontally
        player.Rotate(Vector3.up * mouseX);

        // Clamp and rotate the camera vertically
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -verticalClamp, verticalClamp);
        transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }
}
