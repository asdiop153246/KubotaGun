using UnityEngine;

public class RotateGun : MonoBehaviour
{
    public float rotationSpeed = 5f;  // Rotation speed for the object
    public bool rotateOnY = true;     // Flag to rotate left-right (around Y axis)
    public bool rotateOnX = false;    // Flag to rotate up-down (around X axis)

    private bool isDragging = false;  // To track if the object is being dragged
    private Vector3 lastMousePosition;  // To store the last mouse position

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
        if (isDragging)
        {
            // Calculate the difference in mouse position between frames
            Vector3 mouseDelta = Input.mousePosition - lastMousePosition;

            // Rotation logic for Y-axis (left-right rotation)
            if (rotateOnY)
            {
                // Rotate the object based on horizontal mouse movement (x-axis)
                float rotationAmount = mouseDelta.x * rotationSpeed * Time.deltaTime;

                // Rotate around the Y axis
                transform.Rotate(Vector3.up, -rotationAmount);
            }

            // Rotation logic for X-axis (up-down rotation)
            if (rotateOnX)
            {
                // Rotate the object based on vertical mouse movement (y-axis)
                float rotationAmount = mouseDelta.y * rotationSpeed * Time.deltaTime;

                // Rotate around the X axis
                transform.Rotate(Vector3.right, rotationAmount);
            }

            // Update the last mouse position
            lastMousePosition = Input.mousePosition;
        }
    }
}