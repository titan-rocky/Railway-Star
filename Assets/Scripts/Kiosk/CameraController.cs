using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10f;  // Speed at which the camera moves
    public float panBorderThickness = 10f;  // Thickness for edge panning (for mouse control)
    public Vector2 panLimit;  // Limit the camera movement within the 3D model bounds

    void Update()
    {
        Vector3 pos = transform.position;

        // Keyboard controls for camera movement (WASD or arrow keys)
        if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
        {
            pos.z += moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
        {
            pos.z -= moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            pos.x -= moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            pos.x += moveSpeed * Time.deltaTime;
        }

        // // Mouse edge panning (optional)
        // if (Input.mousePosition.y >= Screen.height - panBorderThickness)
        // {
        //     pos.z += moveSpeed * Time.deltaTime;
        // }
        // if (Input.mousePosition.y <= panBorderThickness)
        // {
        //     pos.z -= moveSpeed * Time.deltaTime;
        // }
        // if (Input.mousePosition.x <= panBorderThickness)
        // {
        //     pos.x -= moveSpeed * Time.deltaTime;
        // }
        // if (Input.mousePosition.x >= Screen.width - panBorderThickness)
        // {
        //     pos.x += moveSpeed * Time.deltaTime;
        //}

        // Clamp camera position to ensure it stays within the bounds of the 3D model
        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);

        // Apply new position to the camera
        transform.position = pos;
    }
}
