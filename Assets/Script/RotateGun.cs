using UnityEngine;

public class RotateGun : MonoBehaviour
{
    public float rotationSpeed = 5f;    // Rotation speed for the object
    public bool rotateOnY = true;       // Flag to rotate left-right (around Y axis)
    public bool rotateOnX = false;      // Flag to rotate up-down (around X axis)
    public float zoomSpeed = 10f;       // Speed of zoom in/out
    public float minZoom = 40f;         // Minimum field of view or distance (zoom in limit)
    public float maxZoom = 80f;         // Maximum field of view or distance (zoom out limit)

    private bool isDragging = false;    // To track if the object is being dragged
    private Vector3 lastMousePosition;  // To store the last mouse position
    private Camera mainCamera;          // Reference to the main camera

    void Start()
    {
        // Get the main camera in the scene
        mainCamera = Camera.main;
    }

    void OnMouseDown()
    {
        // When the user clicks on the object, start dragging
        isDragging = true;

        // Store the initial mouse position when clicked
        lastMousePosition = Input.mousePosition;
    }

    void OnMouseUp()
    {
        // When the user releases the mouse, stop dragging
        isDragging = false;
    }

    void Update()
    {
        HandleRotation();
        HandleZoom();
    }

    // Function to handle object rotation
    void HandleRotation()
    {
        if (isDragging)
        {
            // Calculate the difference in mouse position between frames
            Vector3 mouseDelta = Input.mousePosition - lastMousePosition;

            // Rotation logic for Y-axis (left-right rotation)
            if (rotateOnY)
            {
                // Rotate the object based on horizontal mouse movement (x-axis)
                float rotationAmount = mouseDelta.x * rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.up, -rotationAmount);
            }

            // Rotation logic for X-axis (up-down rotation)
            if (rotateOnX)
            {
                // Rotate the object based on vertical mouse movement (y-axis)
                float rotationAmount = mouseDelta.x * rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.right, rotationAmount);
            }

            // Update the last mouse position
            lastMousePosition = Input.mousePosition;
        }
    }

    // Function to handle camera zoom
    void HandleZoom()
    {
        if (mainCamera != null)
        {
            // Get the scroll wheel input (positive = zoom in, negative = zoom out)
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");

            // Zoom based on perspective camera (adjusting field of view)
            if (mainCamera.orthographic == false)
            {
                // Adjust the field of view based on scroll input
                mainCamera.fieldOfView -= scrollInput * zoomSpeed;
                // Clamp the field of view to prevent over-zooming in/out
                mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView, minZoom, maxZoom);
            }
            // If it's an orthographic camera, you can adjust the size
            else
            {
                // Adjust the orthographic size (works as zoom in orthographic mode)
                mainCamera.orthographicSize -= scrollInput * zoomSpeed;
                mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, minZoom, maxZoom);
            }
        }
    }
}
